using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TetrisB2.Game.Blocks;
using TetrisB2.Game.Tetrominos;
using Windows.UI.Core;

namespace TetrisB2.Game
{
    public class Engine
    {
        public Engine(GameView view, CoreDispatcher dispatcher)
        {
            m_view = view;
            m_dispatcher = dispatcher;
            m_rows = 20;
            m_columns = 10;

            m_moveMutex = new Mutex(false);
            m_speedMutex = new Mutex(false);

            m_speed = 1000;
            m_gameOver = false;
            m_landedBlocks = new Block[m_rows, m_columns];
            m_blocksPerRow = new byte[m_rows];

            m_nextTetromino = TetrominoGenerator.CreateRandomTetramino();

            InitAsyncTask();
        }

        public void StartGame() { m_task.Start(); }
        public void TogglePause() { m_isPaused = !m_isPaused; }

        private void InitAsyncTask()
        {
            m_task = new Task(async () =>
            {
                while (!m_gameOver)
                {
                    await CreateNewBlock();
                    await FallDown();
                    ClearCollisions();
                }
            });
        }

        private async Task CreateNewBlock()
        {
            await m_dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                m_actualTetromino = m_nextTetromino;
                m_nextTetromino = TetrominoGenerator.CreateRandomTetramino();
                CheckForBottomCollision();
                if (m_bottomCollison)
                    m_gameOver = true;
                else
                {
                    m_view.ActualTetromino = m_actualTetromino;
                    m_view.NextTetromino = m_nextTetromino;
                    m_view.DrawTetrominos();
                }
            });
        }

        #region Speed
        public void SpeedUp(uint dv)
        {
            lock (m_speedMutex)
                m_speed /= dv;
        }

        public void SpeedDown(uint dv)
        {
            lock (m_speedMutex)
                m_speed *= dv;
        }
        #endregion

        #region Rotation
        public void RotateTetramino()
        {
            lock (m_moveMutex)
            {
                m_actualTetromino.Rotate(true);
                CheckRotation();
            }
        }

        private void CheckRotation()
        {
            int[] coord = m_actualTetromino.GetBlockCoordinates();

            for (int i = 0; i < coord.Length; i += 2)
            {
                if (coord[i] < 0 || coord[i] >= m_view.CanvasHeight || coord[i + 1] < 0 || coord[i + 1] >= m_view.CanvasWidth ||
                    m_landedBlocks[(coord[i] / m_blockSize.Item2), (coord[i + 1] / m_blockSize.Item1)] != null)
                {
                    m_actualTetromino.Rotate(false);
                    break;
                }
            }
        }

        private void CheckRotationWithWallKick()
        {
            int[] coord = m_actualTetromino.GetBlockCoordinates();
            bool pieceCollision = false;
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            int minCollisionPoint = Int32.MaxValue;

            for (int c = 0; c < coord.Length; c += 2)
            {
                if (coord[c + 1] < min)
                    min = coord[c + 1];
                if (coord[c + 1] > max)
                    max = coord[c + 1];

                bool collision = m_landedBlocks[(coord[c] / m_blockSize.Item2), (coord[c + 1] / m_blockSize.Item1)] != null;
                pieceCollision |= collision;
                if (collision && coord[c + 1] < minCollisionPoint)
                    minCollisionPoint = coord[c + 1];
            }

            if (pieceCollision)
            {
                if (minCollisionPoint - min < max - minCollisionPoint) // Check for left piece collision and eventually do a wall kick
                    m_actualTetromino.Rotate(false);
                else // Check for right piece collision and eventually do a wall kick
                    m_actualTetromino.Rotate(false);
            }
            else // Check for left wall collision and eventually do a wall kick
            {
                if (min < 0)
                {
                    int moves = -min / m_blockSize.Item1;
                    bool canMove = true;

                    for (int i = 0; i < coord.Length; i += 2)
                        canMove &= m_landedBlocks[(coord[i] / m_blockSize.Item2), (coord[i + 1] / m_blockSize.Item1 + moves)] == null;

                    if (canMove)
                        for (int m = 0; m < moves; m++)
                            m_actualTetromino.MoveRight();
                    else
                        m_actualTetromino.Rotate(false);
                }
                else if (max >= m_view.CanvasWidth) // Check for right wall collision and eventually do a wall kick
                {
                    int moves = (max - (int)m_view.CanvasWidth) / m_blockSize.Item1 + 1;
                    bool canMove = true;

                    for (int i = 0; i < coord.Length; i += 2)
                        canMove &= m_landedBlocks[(coord[i] / m_blockSize.Item2), (coord[i + 1] / m_blockSize.Item1 - moves)] == null;

                    if (canMove)
                        for (int m = 0; m < moves; m++)
                            m_actualTetromino.MoveLeft();
                    else
                        m_actualTetromino.Rotate(false);
                }
            }
        }

        #endregion

        #region Fall
        private async Task FallDown()
        {
            uint fallingSpeed;
            while (!m_bottomCollison)
            {
                lock (m_speedMutex)
                    fallingSpeed = m_speed;

                await Task.Delay((int)fallingSpeed);
                await m_dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    lock (m_moveMutex)
                    {
                        CheckForBottomCollision();
                        if (!m_bottomCollison)
                            m_actualTetromino.Fall();
                        else
                            ClearRows();
                    }
                });
            }
        }

        private void CheckForBottomCollision()
        {
            lock (m_moveMutex)
            {
                // First check for collision with other tetrominoes
                List<Block> blocks = m_actualTetromino.GetBlocks();
                int width = m_actualTetromino.GetBlockWidth();
                int height = m_actualTetromino.GetBlockHeight();

                int left, top;
                int grid_x, grid_y;

                foreach (Block b in blocks)
                {
                    top = b.GetTop();
                    left = b.GetLeft();
                    grid_y = top / height;
                    grid_x = left / width;

                    if ((top + height) == m_view.CanvasHeight)
                    {
                        m_bottomCollison = true;
                        break;
                    }
                    else
                    {
                        if (m_landedBlocks[grid_y + 1, grid_x] != null)
                        {
                            m_bottomCollison = true;
                            break;
                        }
                    }
                }

                if (m_bottomCollison && !m_gameOver)
                {
                    foreach (Block b in blocks)
                    {
                        top = b.GetTop();
                        left = b.GetLeft();
                        grid_y = top / height;
                        grid_x = left / width;

                        m_landedBlocks[grid_y, grid_x] = b;
                        m_blocksPerRow[grid_y]++;
                    }
                }
            }
        }
        #endregion

        #region Clear
        private void ClearCollisions()
        {
            m_bottomCollison = m_leftCollision = m_rightCollision = false;
        }

        private void ClearRows()
        {
            for (int row = 0; row < m_gridSize.Item2; row++)
                if (m_blocksPerRow[row] == m_gridSize.Item1)
                {
                    m_blocksPerRow[row] = 0;
                    for (int col = 0; col < m_gridSize.Item1; col++)
                    {
                        Block b = m_landedBlocks[row, col];
                        m_view.DeleteElementFromCanvas(b);

                        m_landedBlocks[row, col] = null;

                        if (row != 0)
                            for (int p_row = row - 1; p_row >= 0; p_row--)
                            {
                                b = m_landedBlocks[p_row, col];
                                if (b != null)
                                {
                                    b.MoveDown();
                                    m_landedBlocks[p_row + 1, col] = b;
                                    m_landedBlocks[p_row, col] = null;
                                    m_blocksPerRow[p_row] -= 1;
                                    m_blocksPerRow[p_row + 1] += 1;
                                }
                            }
                    }
                }
        }
        #endregion

        #region Left move
        public void MoveLeft()
        {
            lock (m_moveMutex)
            {
                CheckForLeftCollision();
                if (!m_leftCollision)
                {
                    m_actualTetromino.MoveLeft();
                    m_rightCollision = false;
                }
                m_leftCollision = false;
            }
        }

        private void CheckForLeftCollision()
        {
            lock (m_moveMutex)
            {
                //First check for collision with other tetrominoes
                List<Block> blocks = m_actualTetromino.GetBlocks();
                int width = m_actualTetromino.GetBlockWidth();
                int height = m_actualTetromino.GetBlockHeight();

                int block_x, block_y;
                int grid_x, grid_y;

                foreach (Block b in blocks)
                {
                    block_y = b.GetTop();
                    block_x = b.GetLeft();
                    grid_y = block_y / height;
                    grid_x = block_x / width;

                    if ((block_x - width) < 0)
                    {
                        m_leftCollision = true;
                        break;
                    }
                    else
                    {
                        if ((grid_x - 1) > 0 && m_landedBlocks[grid_y, grid_x - 1] != null)
                        {
                            m_leftCollision = true;
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region Right move
        public void MoveRight()
        {
            lock (m_moveMutex)
            {
                CheckForRightCollision();
                if (!m_rightCollision)
                {
                    m_actualTetromino.MoveRight();
                    m_leftCollision = false;
                }
                m_rightCollision = false;
            }
        }

        private void CheckForRightCollision()
        {
            lock (m_moveMutex)
            {
                // First check for collision with other tetrominoes
                List<Block> blocks = m_actualTetromino.GetBlocks();
                int width = m_actualTetromino.GetBlockWidth();
                int height = m_actualTetromino.GetBlockHeight();

                int block_x, block_y;
                int grid_x, grid_y;

                foreach (Block b in blocks)
                {
                    block_y = b.GetTop();
                    block_x = b.GetLeft();
                    grid_y = block_y / height;
                    grid_x = block_x / width;

                    if ((block_x + width) == m_view.CanvasWidth)
                    {
                        m_rightCollision = true;
                        break;
                    }
                    else
                    {
                        if ((grid_x + 1) < m_gridSize.Item1 && m_landedBlocks[grid_y, grid_x + 1] != null)
                        {
                            m_rightCollision = true;
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        private bool m_gameOver, m_leftCollision, m_rightCollision, m_bottomCollison, m_isPaused;
        private uint m_speed, m_rows, m_columns;
        private Tuple<int, int> m_blockSize = Plugins.SettingsReader.GetBlocksSize();
        private Tuple<int, int> m_gridSize = Plugins.SettingsReader.GetGridSize();

        private Block[,] m_landedBlocks;
        private byte[] m_blocksPerRow;

        private readonly Mutex m_moveMutex, m_speedMutex;

        private Tetromino m_actualTetromino, m_nextTetromino;
        private Task m_task;
        private GameView m_view;
        private CoreDispatcher m_dispatcher;
    }
}
