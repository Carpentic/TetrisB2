using TetrisB2.Game.Blocks;

namespace TetrisB2.Game.Tetrominos
{
    public class LTetromino : Tetromino
    {
        public LTetromino()
            : base()
        { }

        protected override void CreateBlocks(int width, int height)
        {
            m_blocks.Add(new DrawableBlock(height, width, 0, 2 * width, "orange_block"));
            m_blocks.Add(new DrawableBlock(height, width, height, 0, "orange_block"));
            m_blocks.Add(new DrawableBlock(height, width, height, width, "orange_block"));
            m_blocks.Add(new DrawableBlock(height, width, height, 2 * width, "orange_block"));
        }

        public override void MakeRotation(int rotationType)
        {
            switch (rotationType)
            {
                case 0:
                    m_blocks[0].MoveLeft();
                    m_blocks[0].MoveLeft();
                    m_blocks[1].MoveRightDown();
                    m_blocks[3].MoveLeftUp();
                    break;
                case 1:
                    m_blocks[0].MoveDown();
                    m_blocks[0].MoveDown();
                    m_blocks[1].MoveRightUp();
                    m_blocks[3].MoveLeftDown();
                    break;
                case 2:
                    m_blocks[0].MoveRight();
                    m_blocks[0].MoveRight();
                    m_blocks[1].MoveLeftUp();
                    m_blocks[3].MoveRightDown();
                    break;
                case 3:
                    m_blocks[0].MoveUp();
                    m_blocks[0].MoveUp();
                    m_blocks[1].MoveLeftDown();
                    m_blocks[3].MoveRightUp();
                    break;
            }
        }
    }
}
