using System;
using TetrisB2.Game;
using Windows.UI.Xaml.Controls;

namespace TetrisB2
{
    public sealed partial class GameTotalView : UserControl
    {
        public GameTotalView()
        {
            InitializeComponent();

            BackgroundMusic.Source = new Uri("ms-appx:///Assets/Sounds/" + Plugins.SettingsReader.GetSoundTrackName());
            BackgroundMusic.IsLooping = true;

            VolumeControl.Value = Plugins.SettingsReader.GetSoundVolume();

            NextPieceCanvas = NextPiece;
            ScoreText = Score;
            StatusText = GameStatus;
            TimerText = Timer;
            s_gameView = FindName("GridUI") as GameView;
        }

        public static void Stop()
        {
            if (s_gameView != null)
                s_gameView.Stop();
        }

        private void OnVolumeChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            BackgroundMusic.Volume = VolumeControl.Value / 100;
        }

        private static GameView s_gameView;
        public static Canvas NextPieceCanvas;
        public static TextBlock ScoreText, StatusText, TimerText;
    }
}
