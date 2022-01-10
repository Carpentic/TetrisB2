using System;

namespace TetrisB2.Game.Tetrominos
{
    public enum TetrominoShape
    {
        S,
        Z,
        I,
        J,
        L,
        O,
        T
    }

    public class TetrominoGenerator
    {
        public static Tetromino CreateTetramino(TetrominoShape shape)
        {
            switch (shape)
            {
                case TetrominoShape.S:
                    return new STetromino();
                case TetrominoShape.Z:
                    return new ZTetromino();
                case TetrominoShape.I:
                    return new ITetromino();
                case TetrominoShape.J:
                    return new JTetromino();
                case TetrominoShape.L:
                    return new LTetromino();
                case TetrominoShape.O:
                    return new OTetromino();
                case TetrominoShape.T:
                    return new TTetromino();
                default:
                    throw new InvalidTetromino("Does not exist a tetromino with this shape !");
            }
        }

        public static Tetromino CreateRandomTetramino()
        {
            return CreateTetramino((TetrominoShape)randomizer.Next(7));
        }

        private static Random randomizer = new Random(DateTime.Now.Millisecond);
    }
}
