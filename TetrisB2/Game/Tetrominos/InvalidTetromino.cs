using System;

namespace TetrisB2.Game.Tetrominos
{
    public class InvalidTetromino : ArgumentException
    {
        public InvalidTetromino() : base() { }
        public InvalidTetromino(string s) : base(s) { }
    }
}
