using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace minesweeper{

    public class Scene : InterfaceGame
    {
        public event EventHandler mineCounterChanged;
        public event EventHandler<MineEventArgs> ClickMine;
        
        public int Mines { get; private set; }
        private Cover[,] covers;
        private MainWindow mainWindow;
        private int correctFlags, wrongFlags;
        public int flaggedMines { get { return (this.correctFlags + this.wrongFlags); } }
        public Grid sceneGridO;

        /* 
         * change numOfRows for the number of ROWS on the grid
         * change numOfColums for the number of COLUMNS on the grid
        */
        public int numOfRows = 10;
        public int numOfColumns = 10;

        // change this value for the number of mines on the grid
        public int numOfMines = 15;  

        public Scene(Grid sceneGrid, MainWindow mainWindow)
        {
            sceneGridO = sceneGrid;
            this.mainWindow = mainWindow;
        }

        public bool checkGrid(int rowPosition, int colPosition)
        {
            return ((rowPosition >= 0) && (rowPosition < numOfRows) && (colPosition >= 0) && (colPosition < numOfColumns));
        }

        public bool checkBomb(int rowPosition, int colPosition) {
        
            if (checkGrid(rowPosition, colPosition)) {
                return covers[rowPosition, colPosition].IsMined;
            }

            return false;
        }
        public bool checkFlagged(int rowPosition, int colPosition)
        {
            if (checkGrid(rowPosition, colPosition))
            {
                return covers[rowPosition, colPosition].IsFlagged;
            }
            return false;
        }

        public int revealCover(int rowPosition, int colPosition) {
            if (checkGrid(rowPosition, colPosition)){
                int result = covers[rowPosition, colPosition].Verify();
                isGameOver();
                return result;
            }
            throw new minesweeperException("Invalid MinesGrid reference call [row, colum] on reveal");
        }

        public void activateMines(int rowPosition, int colPosition){
            if (!checkGrid(rowPosition, colPosition)) {
                throw new minesweeperException("Invalid MinesGrid reference call on flag [row, column] on flag");
            }

            Cover coverO = covers[rowPosition, colPosition];
            if (!coverO.IsFlagged)
            {
                if (coverO.IsMined)
                {
                    this.correctFlags++;
                }
                else
                {
                    this.wrongFlags++;
                }
            } else {
                if (coverO.IsMined)
                {
                    this.correctFlags--;
                } else {
                    this.wrongFlags--;
                }
            }

            coverO.IsFlagged = !coverO.IsFlagged;
            isGameOver();
            MineCounterChange(new EventArgs());
        }

        public void openCover(int rowPosition, int colPosition)
        {
            if (checkGrid(rowPosition, colPosition) && !covers[rowPosition, colPosition].IsRevealed)
            {
                OnClickCover(new MineEventArgs(rowPosition, colPosition));
            }
        }

        private void isGameOver(){

            bool isGameFinished = false;

            if (wrongFlags == 0 && flaggedMines == numOfMines) {
                isGameFinished = true;

                foreach (Cover item in covers){

                    if (!item.IsRevealed && !item.IsMined){
                        
                        isGameFinished = false;
                        break;
                    }
                }
            }

            if (isGameFinished) {

                mainWindow.timer.Stop();
                Image imageOfCool = new Image();
                StackPanel spCool = new StackPanel();
                spCool.Orientation = Orientation.Horizontal;
                BitmapImage cool = new BitmapImage();
                cool.BeginInit();
                cool.UriSource = new Uri("./cool.jpg", UriKind.Relative);
                cool.EndInit();
                imageOfCool.Source = cool;
                spCool.Children.Add(imageOfCool);
                mainWindow.Play.Content = spCool;
            }
        }

        public void Run()
        {
            correctFlags = 0;
            wrongFlags = 0;

            covers = new Cover[numOfRows, numOfColumns];

            for (int i = 0; i < numOfRows; i++)
            {
                var myRowDefinition = new RowDefinition();
                myRowDefinition.Height = new GridLength(sceneGridO.Height / numOfRows);
                sceneGridO.RowDefinitions.Add(myRowDefinition);

                var myColumnDefinition = new ColumnDefinition();
                myColumnDefinition.Width = new GridLength(sceneGridO.Width / numOfColumns);
                sceneGridO.ColumnDefinitions.Add(myColumnDefinition);

                for (int j = 0; j < numOfColumns; j++)
                {
                    Cover cell = new Cover(this, i, j);
                    covers[i,j] = cell;

                    Button buttons = new Button();
                    buttons.Name = "Button" + i + "_" + j;
                    Grid.SetColumn(buttons, j);
                    Grid.SetRow(buttons, i);
                    sceneGridO.Children.Add(buttons);
                    
                    buttons.Click += clickEventHandler;
                    buttons.MouseRightButtonDown += rightClickEventHandler;
                    
                }
            }
            
            int bombCounter = 0;
            Random randomisedPosition = new Random();
            while (bombCounter < numOfMines) {
                int row = randomisedPosition.Next(numOfRows);
                int col = randomisedPosition.Next(numOfColumns);

                Cover cell = this.covers[row, col];

                  if (!cell.IsMined) {
                    cell.IsMined = true;
                    Console.WriteLine(row + " " + col + " is now a mine" + " and the bombCounter is: " + bombCounter);
                    bombCounter++;
                }
            }
        }

        public void clickEventHandler(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = parseButtonRow(button);
            int column = parseButtonColumn(button);
            if (!checkGrid(row, column)) throw new minesweeperException("Invalid Button to MinesGrid reference on reveal");

            if (checkFlagged(row, column)) {
                return;
            }

            button.IsEnabled = false;

            if (covers[row,column].IsMined){
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                Image imageOfBomb = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri("./bomb.png", UriKind.Relative);
                bi.EndInit();
                imageOfBomb.Source = bi;
                sp.Children.Add(imageOfBomb);
                button.Content = sp;
                mainWindow.timer.Stop();

                Image imageOfCry = new Image();
                StackPanel spCry = new StackPanel();
                spCry.Orientation = Orientation.Horizontal;
                BitmapImage cry = new BitmapImage();
                cry.BeginInit();
                cry.UriSource = new Uri("./cry.png", UriKind.Relative);
                cry.EndInit();
                imageOfCry.Source = cry;
                spCry.Children.Add(imageOfCry);
                mainWindow.Play.Content = spCry;

                foreach (Button btn in sceneGridO.Children)
                {
                    if (btn.IsEnabled) this.clickEventHandler(btn, e);
                }
                
            } else {
                int count = revealCover(row, column);
                if (count > 0)
                {
                    button.Foreground = new SolidColorBrush(Colors.Red);
                    button.FontWeight = FontWeights.Bold;
                    button.Content = count.ToString();
                }
            }
        }

        private void rightClickEventHandler(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = parseButtonRow(button);
            int column = parseButtonColumn(button);
            if (!this.checkGrid(row, column)) throw new minesweeperException("Invalid Button to MinesGrid reference on flag");
            
            if (checkFlagged(row, column)) {

                button.Content = "";

            } else {

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                Image imageOfFlag = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri("./flag.png", UriKind.Relative);
                bi.EndInit();
                imageOfFlag.Source = bi;
                sp.Children.Add(imageOfFlag);
                button.Content = sp;
            }
            activateMines(row, column);
        }

        public void onClickCover(object sender, MineEventArgs e)
        {
            string buttonName = "Button";
            Button senderButton = new Button();

            senderButton.Name = buttonName + e.MineRow + "_" + e.MineColumn;
            Console.WriteLine("should be revealed are: " + senderButton.Name);
            if (senderButton == null) throw new minesweeperException("Invalid Button to MinesGrid reference on multiple reveal"); 
            
            clickEventHandler(senderButton, new RoutedEventArgs());

        }

        public int parseButtonRow(Button button) {
            Console.WriteLine("Button name is: " + button.Name);
            if (button.Name.IndexOf("Button") != 0) {
                throw new minesweeperException("Wrong button name");
            }
            string buttonPos = button.Name.Split(new string[] { "Button" }, StringSplitOptions.None)[1];
            int buttonRow = Convert.ToInt32(buttonPos.Split('_')[0]);
            return buttonRow;
        }

        public int parseButtonColumn(Button button){
            if (button.Name.IndexOf("Button") != 0)
            {
                throw new minesweeperException("Wrong button name");
            }
            string buttonPos = button.Name.Split(new string[] { "Button" }, StringSplitOptions.None)[1];
            int buttonCol = Convert.ToInt32(buttonPos.Split('_')[1]);
            return buttonCol;
        }

        protected virtual void OnClickCover(MineEventArgs e)
        {
            EventHandler<MineEventArgs> handler = ClickMine;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void MineCounterChange(EventArgs e)
        {
            EventHandler handler = mineCounterChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}