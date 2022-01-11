using Windows.UI.Xaml.Controls;

namespace TetrisB2
{
    public sealed partial class GameTotalView : UserControl
    {
        public GameTotalView()
        {
            InitializeComponent();

            NextPieceCanvas = NextPiece;
            ScoreText = Score;
            StatusText = GameStatus;
            TimerText = Timer;
        }

        public static Canvas NextPieceCanvas;
        public static TextBlock ScoreText, StatusText, TimerText;
    }
}
