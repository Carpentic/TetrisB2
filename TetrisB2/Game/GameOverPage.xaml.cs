using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TetrisB2
{
    public sealed partial class GameOverPage : Page
    {
        public GameOverPage()
        {
            InitializeComponent();
            Score.Text = GameTotalView.s_ScoreText.Text;
            Timer.Text = GameTotalView.s_TimerText.Text;
        }

        private void RTM_Clicked(object sender, RoutedEventArgs e)
        {
            GameTotalView.Stop();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
