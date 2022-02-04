using System;
using TetrisB2.Game;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace TetrisB2
{
    public sealed partial class GameTotalView : UserControl
    {
        public GameTotalView()
        {
            InitializeComponent();

            SetBackgroundMusic();
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

        private async void SetBackgroundMusic()
        {
            StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
            StorageFolder sounds = await LocalFolder.GetFolderAsync("Sounds");
            StorageFile file = await sounds.GetFileAsync("tetris_soundtrack.mp3");
            var stream = await file.OpenAsync(FileAccessMode.Read);

            BackgroundMusic.SetSource(stream, file.ContentType);
            BackgroundMusic.IsLooping = true;
        }

        private static GameView s_GameView;
        public static Canvas s_NextPieceCanvas;
        public static TextBlock s_ScoreText, s_StatusText, s_TimerText;
    }
}
