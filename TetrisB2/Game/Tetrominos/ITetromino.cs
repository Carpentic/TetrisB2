using TetrisB2.Game.Blocks;

namespace TetrisB2.Game.Tetrominos
{
    public class ITetromino : Tetromino
    {
        public ITetromino()
            : base()
        { }

        protected override void CreateBlocks(int width, int height)
        {
            m_blocks.Add(new DrawableBlock(height, width, 0, 0, "cyan_block"));
            m_blocks.Add(new DrawableBlock(height, width, 0, width, "cyan_block"));
            m_blocks.Add(new DrawableBlock(height, width, 0, 2 * width, "cyan_block"));
            m_blocks.Add(new DrawableBlock(height, width, 0, 3 * width, "cyan_block"));
        }

        public override void MakeRotation(int rotationType)
        {
            switch (rotationType)
            {
                case 0:
                    m_blocks[0].MoveRight();
                    m_blocks[0].MoveRightUp();
                    m_blocks[1].MoveRight();
                    m_blocks[2].MoveDown();
                    m_blocks[3].MoveDown();
                    m_blocks[3].MoveLeftDown();
                    break;
                case 1:
                    m_blocks[0].MoveDown();
                    m_blocks[0].MoveRightDown();
                    m_blocks[1].MoveDown();
                    m_blocks[2].MoveLeft();
                    m_blocks[3].MoveLeft();
                    m_blocks[3].MoveLeftUp();
                    break;
                case 2:
                    m_blocks[0].MoveLeft();
                    m_blocks[0].MoveLeftDown();
                    m_blocks[1].MoveLeft();
                    m_blocks[2].MoveUp();
                    m_blocks[3].MoveUp();
                    m_blocks[3].MoveRightUp();
                    break;
                case 3:
                    m_blocks[0].MoveUp();
                    m_blocks[0].MoveLeftUp();
                    m_blocks[1].MoveUp();
                    m_blocks[2].MoveRight();
                    m_blocks[3].MoveRight();
                    m_blocks[3].MoveRightDown();
                    break;
            }
        }
    }
}
