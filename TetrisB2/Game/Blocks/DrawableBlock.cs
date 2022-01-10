using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TetrisB2.Game.Blocks
{
    public class DrawableBlock : Block
    {
        public DrawableBlock(int heigth, int width, int top, int left, string name)
            : base(top, left, width, heigth)
        {
            m_image = new Image();
            m_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Blocks/" + name + ".png"));
            m_image.Width = width;
            m_image.Height = heigth;
            m_image.SetValue(Canvas.TopProperty, top);
            m_image.SetValue(Canvas.LeftProperty, left);
        }

        #region Basic Movements Override
        public override void MoveLeft()
        {
            base.MoveLeft();
            m_image.SetValue(Canvas.LeftProperty, GetLeft());
        }
        public override void MoveRight()
        {
            base.MoveRight();
            m_image.SetValue(Canvas.LeftProperty, GetLeft());
        }
        public override void MoveUp()
        {
            base.MoveUp();
            m_image.SetValue(Canvas.TopProperty, GetTop());
        }
        public override void MoveDown()
        {
            base.MoveDown();
            m_image.SetValue(Canvas.TopProperty, GetTop());
        }
        #endregion

        #region Render Override
        public override void DrawBlock(Canvas canvas)
        {
            canvas.Children.Add(m_image);
        }
        public override void Erase(Canvas canvas)
        {
            canvas.Children.Remove(m_image);
        }
        #endregion

        private Image m_image;
    }
}
