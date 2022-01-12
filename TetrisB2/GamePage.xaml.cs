using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TetrisB2
{
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
        }

        private void ReturnToMainClicked(object sender, RoutedEventArgs e)
        {
            GameTotalView.Stop();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
