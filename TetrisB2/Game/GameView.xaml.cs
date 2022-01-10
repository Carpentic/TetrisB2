using System;
using Windows.UI.Xaml.Controls;

namespace TetrisB2.Game
{
    public sealed partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            Tuple<int, int> BlockSize = Plugins.SettingsReader.GetBlocksSize();
            GameCanvas.Width = 10 * BlockSize.Item1;
            GameCanvas.Height = 20 * BlockSize.Item2;

            Tetrominos.TetrominoGenerator.CreateRandomTetramino().Draw(GameCanvas);
        }

        private void GameLoop(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
