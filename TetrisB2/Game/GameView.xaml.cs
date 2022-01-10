using Windows.UI.Xaml.Controls;

namespace TetrisB2.Game
{
    public sealed partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            GameCanvas.Width = 10 * 50;
            GameCanvas.Height = 20 * 50;

            Tetrominos.TetrominoGenerator.CreateRandomTetramino().Draw(GameCanvas);
        }

        private void GameLoop(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}