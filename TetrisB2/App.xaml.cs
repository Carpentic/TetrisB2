using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TetrisB2
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            CheckOrCreateConfig();

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        private async void CheckOrCreateConfig()
        {
            StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
            try
            {
                await LocalFolder.GetFileAsync("settings.xml");
            }
            catch (FileNotFoundException)
            {
                StorageFile file = await LocalFolder.CreateFileAsync("settings.xml");
                Windows.Storage.Streams.IBuffer buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(XMLTemplate, Windows.Security.Cryptography.BinaryStringEncoding.Utf8);

                await FileIO.WriteBufferAsync(file, buffer);
            }
        }
        private string XMLTemplate = "<TetrisB2><PublicProperties><SelectedSoundtrack name=\"tetris_soundtrack.mp3\"/><Volume volume=\"0\"/><GridResetScore score=\"50\"/><Keys left=\"37\" right=\"39\" down=\"40\" rotate=\"38\" pause=\"80\"/></PublicProperties><PrivateProperties><Blocks width=\"50\" height=\"50\"/><GridSize width=\"10\" height=\"20\"/><RapidFall speed=\"10\"/></PrivateProperties></TetrisB2>";
    }
}
