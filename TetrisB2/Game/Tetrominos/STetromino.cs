using TetrisB2.Game.Blocks;

namespace TetrisB2.Game.Tetrominos
{
    public class STetromino : Tetromino
    {
        public STetromino()
            : base()
        { }

        protected override void CreateBlocks(int width, int height)
        {
            m_blocks.Add(new DrawableBlock(height, width, 0, width, "green_block"));
            m_blocks.Add(new DrawableBlock(height, width, 0, 2 * width, "green_block"));
            m_blocks.Add(new DrawableBlock(height, width, height, 0, "green_block"));
            m_blocks.Add(new DrawableBlock(height, width, height, width, "green_block"));
        }

        public override void MakeRotation(int rotationType)
        {
            switch (rotationType)
            {
                case 0:
                    m_blocks[0].MoveRightDown();
                    m_blocks[1].MoveDown();
                    m_blocks[1].MoveDown();
                    m_blocks[2].MoveRightUp();
                    break;
                case 1:
                    m_blocks[0].MoveLeftDown();
                    m_blocks[1].MoveLeft();
                    m_blocks[1].MoveLeft();
                    m_blocks[2].MoveRightDown();
                    break;
                case 2:
                    m_blocks[0].MoveLeftUp();
                    m_blocks[1].MoveUp();
                    m_blocks[1].MoveUp();
                    m_blocks[2].MoveLeftDown();
                    break;
                case 3:
                    m_blocks[0].MoveRightUp();
                    m_blocks[1].MoveRight();
                    m_blocks[1].MoveRight();
                    m_blocks[2].MoveLeftUp();
                    break;
            }
        }
    }
}
