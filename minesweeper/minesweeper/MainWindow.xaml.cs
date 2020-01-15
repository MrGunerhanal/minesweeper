using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;

namespace minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool gameStarted;
        public Scene scene { get; private set; }
        public readonly DispatcherTimer timer;
        private int gameTimer;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += timeIncreaser;
            gameStarted = false;
            scene = new Scene(this.sceneGrid, this);
        }

        private void onCounterChanged(object sender, EventArgs e)
        {
            MineCounter.Content = (scene.numOfMines - scene.flaggedMines).ToString();
        }

        private void timeIncreaser(object sender, EventArgs e)
        {
            gameTimer += 1;
            TimerLabel.Content = gameTimer.ToString();
        }

        private void sceneSetup() {

            gameTimer = 0;
            timer.Start();
            Play.Content = FindResource("Smile");

            scene =  new Scene(this.sceneGrid, this);
            foreach (Button btn in sceneGrid.Children)
            {
                btn.Content = "";
                btn.IsEnabled = true;
            }

            scene.mineCounterChanged += onCounterChanged;
            MineCounter.Content = scene.numOfMines.ToString();
            
            scene.ClickMine += scene.onClickCover;
            scene.Run();
            gameStarted = true;

        }

        public void Button_Click(object sender, RoutedEventArgs e)  {

            sceneSetup();
        }
    }
}