using Windows.UI.Xaml.Controls;

namespace TetrisB2.Game.Blocks
{
    public abstract class Block
    {
        public Block(int top, int left, int width, int heigth)
        {
            m_top = top;
            m_left = left;
            m_widht = width;
            m_height = heigth;
        }

        #region Basic Movements
        public virtual void MoveLeft() { m_left -= m_widht; }
        public virtual void MoveRight() { m_left += m_widht; }
        public virtual void MoveUp() { m_top -= m_height; }
        public virtual void MoveDown() { m_top += m_height; }
        #endregion

        #region Complex Movements
        public void MoveLeftUp() { MoveLeft(); MoveUp(); }
        public void MoveRightUp() { MoveRight(); MoveUp(); }
        public void MoveLeftDown() { MoveLeft(); MoveDown(); }
        public void MoveRightDown() { MoveRight(); MoveDown(); }
        #endregion

        #region Draw
        public abstract void DrawBlock(Canvas canvas);
        public abstract void Erase(Canvas canvas);
        #endregion

        #region Getters
        public int GetTop() { return m_top; }
        public int GetLeft() { return m_left; }
        public int GetWidth() { return m_widht; }
        public int GetHeight() { return m_height; }
        #endregion

        private int m_top, m_left, m_widht, m_height;
    }
}
