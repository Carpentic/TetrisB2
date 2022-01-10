using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TetrisB2
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(720, 1280);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void PlayButtonClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GameView));
        }

        private void OptionsButtonClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsPage));
        }

        private void QuitButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
