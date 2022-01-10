using System.Threading.Tasks;
using TetrisB2.Game.Blocks;
using TetrisB2.Game.Tetrominos;
using Windows.UI.Core;

namespace TetrisB2.Game.Engine
{
    public class Engine
    {

        private bool m_gameOver, m_leftCollision, m_rightCollision, m_bottomCollison, m_isPaused;
        private uint m_speed;

        private Block[,] m_landedBlocks;
        private byte[] m_blocksPerRow;

        private Tetromino m_actualTetromino, m_nextTetromino;
        private Task m_task;
        private GameView m_view;
        private CoreDispatcher m_dispatcher;
    }
}
