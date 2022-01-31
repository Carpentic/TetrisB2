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

            s_NextPieceCanvas = NextPiece;
            s_ScoreText = Score;
            s_StatusText = GameStatus;
            s_TimerText = Timer;
            s_GameView = FindName("GridUI") as GameView;
        }

        public static void Stop()
        {
            if (s_GameView != null)
                s_GameView.Stop();
        }

        private void OnVolumeChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            BackgroundMusic.Volume = VolumeControl.Value / 100;
        }

        public static GameView s_GameView;
        public static Canvas s_NextPieceCanvas;
        public static TextBlock s_ScoreText, s_StatusText, s_TimerText;
    }
}
