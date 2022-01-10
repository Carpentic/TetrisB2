using TetrisB2.Game.Blocks;

namespace TetrisB2.Game.Tetrominos
{
    public class OTetromino : Tetromino
    {
        public OTetromino()
            : base()
        { }

        protected override void CreateBlocks(int width, int height)
        {
            m_blocks.Add(new DrawableBlock(height, width, 0, 0, "yellow_block"));
            m_blocks.Add(new DrawableBlock(height, width, 0, width, "yellow_block"));
            m_blocks.Add(new DrawableBlock(height, width, height, 0, "yellow_block"));
            m_blocks.Add(new DrawableBlock(height, width, height, width, "yellow_block"));
        }

        public override void Rotate(bool direct) { }
        public override void MakeRotation(int rotationType) { }
    }
}
