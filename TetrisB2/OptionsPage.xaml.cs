using System;
using System.Collections.Generic;
using System.Linq;
using TetrisB2.Plugins;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TetrisB2
{
    public sealed partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            InitializeComponent();
            DrawKeys();

            InitSoundTracks();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            VolumeControl.Value = SettingsReader.GetSoundVolume();
            GridResetScore.Text = SettingsReader.GetGridResetScore().ToString();
        }

        private async void InitSoundTracks()
        {
            Soundtracks = new List<string>(1);
            List<string> fileTypeFilter = new List<string>()
            {
                ".mp3", ".wma", ".wav", ".ogg"
            };

            QueryOptions queryOptions = new QueryOptions(CommonFileQuery.OrderByName, fileTypeFilter);
            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Sounds");

            StorageFileQueryResult results = folder.CreateFileQueryWithOptions(queryOptions);
            IReadOnlyList<StorageFile> sortedFiles = await results.GetFilesAsync();
            foreach (StorageFile item in sortedFiles)
                Soundtracks.Add(item.Name);
            SoundtrackList.ItemsSource = Soundtracks;
        }

        private void DrawKeys()
        {
            leftKeyText.Text = keys.Left.ToString();
            rightKeyText.Text = keys.Right.ToString();
            downKeyText.Text = keys.Down.ToString();
            rotateKeyText.Text = keys.Rotate.ToString();
            pauseKeyText.Text = keys.Pause.ToString();
        }

        private void WriteKeys()
        {
            SettingsWriter.SetKey("left", keys.Left);
            SettingsWriter.SetKey("right", keys.Right);
            SettingsWriter.SetKey("rotate", keys.Rotate);
            SettingsWriter.SetKey("down", keys.Down);
            SettingsWriter.SetKey("pause", keys.Pause);
        }

        private async void AddSongClicked(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".wma");
            picker.FileTypeFilter.Add(".wav");
            picker.FileTypeFilter.Add(".ogg");

            StorageFolder sounds = await ApplicationData.Current.LocalFolder.GetFolderAsync("Sounds");
            StorageFile file = await picker.PickSingleFileAsync();
            await file.CopyAsync(sounds);

            Soundtracks.Clear();
            InitSoundTracks();
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (IsKeyInProgress)
            {
                switch (KeyInProgress)
                {
                    case "left":
                        keys.Left = args.VirtualKey;
                        leftKeySet.IsEnabled = true;
                        break;
                    case "right":
                        keys.Right = args.VirtualKey;
                        rightKeySet.IsEnabled = true;
                        break;
                    case "down":
                        keys.Down = args.VirtualKey;
                        downKeySet.IsEnabled = true;
                        break;
                    case "rotate":
                        keys.Rotate = args.VirtualKey;
                        rotateKeySet.IsEnabled = true;
                        break;
                    case "pause":
                        keys.Pause = args.VirtualKey;
                        pauseKeySet.IsEnabled = true;
                        break;
                }
                IsKeyInProgress = false;
                DrawKeys();
                WriteKeys();
            }
        }

        private void LeftKeyChangeRequested(object sender, RoutedEventArgs e)
        {
            if (IsKeyInProgress)
                return;

            KeyInProgress = "left";
            IsKeyInProgress = true;
            leftKeySet.IsEnabled = false;
        }

        private void RightKeyChangeRequested(object sender, RoutedEventArgs e)
        {
            if (IsKeyInProgress)
                return;

            KeyInProgress = "right";
            IsKeyInProgress = true;
            rightKeySet.IsEnabled = false;
        }

        private void DownKeyChangeRequested(object sender, RoutedEventArgs e)
        {
            if (IsKeyInProgress)
                return;

            KeyInProgress = "down";
            IsKeyInProgress = true;
            downKeySet.IsEnabled = false;
        }

        private void RotateKeyChangeRequested(object sender, RoutedEventArgs e)
        {
            if (IsKeyInProgress)
                return;

            KeyInProgress = "rotate";
            IsKeyInProgress = true;
            rotateKeySet.IsEnabled = false;
        }

        private void PauseKeyChangeRequested(object sender, RoutedEventArgs e)
        {
            if (IsKeyInProgress)
                return;

            KeyInProgress = "pause";
            IsKeyInProgress = true;
            pauseKeySet.IsEnabled = false;
        }

        private void OnVoulmeChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            SettingsWriter.SetSoundVolume(e.NewValue);
        }

        private void OnGridResetScoreChanged(object sender, TextChangedEventArgs e)
        {
            SettingsWriter.SetGridResetScore(uint.Parse(GridResetScore.Text));
        }

        private void BeforeGridResetScoreChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void SelectedSoundtrackChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SoundtrackList.SelectedItem != null)
                SettingsWriter.SetSoundTrackName(SoundtrackList.SelectedItem.ToString());
        }

        private void MainMenuClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private bool IsKeyInProgress = false;
        private string KeyInProgress = string.Empty;
        private TetrisKeys keys = SettingsReader.GetKeys();
        private List<string> Soundtracks;
    }
}