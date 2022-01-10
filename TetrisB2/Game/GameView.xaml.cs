using System;
using TetrisB2.Game.Blocks;
using TetrisB2.Game.Tetrominos;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TetrisB2.Game
{
    public sealed partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            Tuple<int, int> BlockSize = Plugins.SettingsReader.GetBlocksSize();
            Tuple<int, int> GridSize = Plugins.SettingsReader.GetGridSize();
            GameCanvas.Width = CanvasWidth = GridSize.Item1 * BlockSize.Item1;
            GameCanvas.Height = CanvasHeight = GridSize.Item2 * BlockSize.Item2;

            BackgroundMusic.Source = new Uri("ms-appx:///Assets/Sounds/" + Plugins.SettingsReader.GetSoundTrackName());

            m_background = new Image();
            m_background.Source = new BitmapImage(new Uri("ms-appx:///Assets/grid.png"));
            m_background.Width = GameCanvas.Width;
            m_background.Height = GameCanvas.Height;
            m_background.SetValue(Canvas.LeftProperty, 0);
            m_background.SetValue(Canvas.TopProperty, 0);
            GameCanvas.Children.Add(m_background);

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            m_engine = new Engine(this, CoreWindow.GetForCurrentThread().Dispatcher);
        }

        private void StartGameLoop(object sender, RoutedEventArgs e)
        {
            m_engine.StartGame();
        }

        public void DrawTetrominos()
        {
            ActualTetromino.Draw(GameCanvas);
            // TODO : Draw next in place : TODO \\
        }

        public void DeleteElementFromCanvas(Block b)
        {
            b.Erase(GameCanvas);
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Down)
            {
                m_engine.SpeedDown(Plugins.SettingsReader.GetRapidFall());
                m_speedUp = false;
            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    m_engine.MoveLeft();
                    break;
                case VirtualKey.Right:
                    m_engine.MoveRight();
                    break;
                case VirtualKey.Up:
                    m_engine.RotateTetramino();
                    break;
                case VirtualKey.Down:
                    if (!m_speedUp)
                    {
                        m_engine.SpeedUp(FallSpeed);
                        m_speedUp = true;
                    }
                    break;
            }
        }

        public Tetromino ActualTetromino { get; set; }
        public Tetromino NextTetromino { get; set; }
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }

        private readonly uint FallSpeed = Plugins.SettingsReader.GetRapidFall();
        private bool m_speedUp;
        private Image m_background;
        private Engine m_engine;
    }
}
