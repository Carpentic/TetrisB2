using System.Collections.Generic;
using TetrisB2.Game.Blocks;
using Windows.UI.Xaml.Controls;

namespace TetrisB2.Game.Tetrominos
{
    public abstract class Tetromino
    {
        public Tetromino()
        {
            m_blocks = new List<Block>(4);
            CreateBlocks(50, 50);
        }
        protected abstract void CreateBlocks(int width, int height);

        #region Movements
        public abstract void MakeRotation(int rotationType);
        public virtual void Rotate(bool direct)
        {
            MakeRotation(direct ? m_state : (m_state + 1) % 4);
            m_state = direct ? ((m_state + 1) % 4) : ((m_state == 0) ? 3 : (m_state - 1) % 4);
        }
        public void Fall()
        {
            foreach (Block block in m_blocks)
                block.MoveDown();
        }
        public void MoveLeft()
        {
            foreach (Block block in m_blocks)
                block.MoveLeft();
        }
        public void MoveRight()
        {
            foreach (Block block in m_blocks)
                block.MoveRight();
        }
        #endregion

        #region Draw
        public void Draw(Canvas canvas)
        {
            foreach (Block block in m_blocks)
                block.DrawBlock(canvas);
        }
        public void Erase(Canvas canvas)
        {
            foreach (Block block in m_blocks)
                block.Erase(canvas);
        }
        #endregion

        #region Getters
        public List<Block> GetBlocks() { return m_blocks; }
        public int GetBlockWidth() { return m_blocks[0].GetWidth(); }
        public int GetBlockHeight() { return m_blocks[0].GetHeight(); }
        public int[] GetBlockCoordinates()
        {
            int[] coords = new int[8];
            int i = 0;
            foreach (Block block in m_blocks)
            {
                coords[i++] = block.GetTop();
                coords[i++] = block.GetLeft();
            }
            return coords;
        }
        #endregion

        protected List<Block> m_blocks;
        private int m_state;
    }
}
