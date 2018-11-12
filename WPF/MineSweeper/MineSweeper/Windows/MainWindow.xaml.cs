using MineSweeper.Classes;
using MineSweeper.Windows;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MineSweeper
{
    public enum Level { Easy = 0, Middle = 1, Hard = 2 };

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Level currentLevel;
        bool firstStep;
        Cell[,] map;
        int allBombCount;
        int bombFlagCount;
        int cellOpenedCount;
        Stopwatch time;
        DispatcherTimer timer;
        int width;
        int height;

        public MainWindow()
        {
            InitializeComponent();
            switch (Properties.Settings.Default.Level)
            {
                case 1:
                    currentLevel = Level.Middle;
                    break;
                case 2:
                    currentLevel = Level.Hard;
                    break;
                default:
                    currentLevel = Level.Easy;
                    break;
            }
            WAVPlayer.Sound = Properties.Settings.Default.Sound;
            if (Properties.Settings.Default.DefaultLanguage.Equals(CultureInfo.GetCultureInfo("en-US")))
            {
                App.Set(CultureInfo.GetCultureInfo("en-US"));
            }
            else
            {
                App.Set(CultureInfo.GetCultureInfo("ru-RU"));
            }
            time = new Stopwatch();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            NewGame();
        }

        private void timerTick(object sender, EventArgs e)
        {
            timeText.Text = time.Elapsed.ToString(@"hh\:mm\:ss");
        }

        void Reset(bool firstStep)
        {
            time.Reset();
            timeText.Text = time.Elapsed.ToString();
            this.firstStep = firstStep;
            bombFlagCount = 0;
            cellOpenedCount = 0;
            bombText.Text = allBombCount.ToString();
        }

        void NewGame()
        {

            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
            Image[,] images = null;
            switch (currentLevel)
            {
                case Level.Easy:
                    GameGrid.Height = 356;
                    GameGrid.Width = GameGrid.Height;
                    width = 9;
                    height = 9;
                    allBombCount = 10;
                    break;
                case Level.Middle:
                    GameGrid.Height = 356;
                    GameGrid.Width = GameGrid.Height;
                    width = 16;
                    height = 16;
                    allBombCount = 40;
                    break;
                case Level.Hard:
                    GameGrid.Width = 618;
                    GameGrid.Height = GameGrid.Width * 16 / 30;
                    width = 16;
                    height = 30;
                    allBombCount = 99;
                    break;
                default:
                    break;
            }
            images = new Image[width, height];
            for (int i = 0; i < width; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < height; j++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            map = new Cell[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = new Cell(i, j);
                    images[i, j] = new Image()
                    {
                        Source = map[i, j].ExtImage,
                        Stretch = Stretch.Fill,
                        Tag = map[i, j]
                    };
                    images[i, j].MouseUp += image_MouseUp;
                    GameGrid.Children.Add(images[i, j]);
                    Grid.SetRow(images[i, j], i);
                    Grid.SetColumn(images[i, j], j);
                }
            }
            Reset(true);
        }

        private void Cell_Opened()
        {
            cellOpenedCount++;
            if (width * height - cellOpenedCount == allBombCount)
            {
                WAVPlayer.PlaySound(Properties.Resource.win);
                EnableTimer(false);
                OpenBombs();
                GameGrid.UpdateLayout();
                EndGame(true);
            }
        }

        void EndGame(bool win)
        {
            EndGameWindow endGameWindow = new EndGameWindow(currentLevel, win, time.Elapsed);
            switch (endGameWindow.ShowDialog())
            {
                case true:
                    NewGame();
                    break;
                case false:
                    if (endGameWindow.Exit)
                    {
                        Close();
                    }
                    else
                    {
                        Restart();
                    }
                    break;
                default:
                    Close();
                    break;
            }
        }

        void Restart()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j].ExtState = CellExtState.Empty;
                    map[i, j].IsOpen = false;
                    (GameGrid.Children[i * height + j] as Image).Source = map[i, j].ExtImage;
                    (GameGrid.Children[i * height + j] as Image).Tag = map[i, j];
                }
            }
            Reset(false);
        }

        bool CheckCellIntState(int x, int y, CellIntState intState)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return false;
            }
            if (map[x, y].IntState == intState)
            {
                return true;
            }
            return false;

        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            Cell cell = image.Tag as Cell;
            if (e.ChangedButton == MouseButton.Left && cell.ExtState != CellExtState.Flag)
            {
                if (firstStep)
                {
                    Random r = new Random();
                    int bombCount = 0;
                    do
                    {
                        int x = r.Next(width);
                        int y = r.Next(height);
                        if (map[x, y].IntState != CellIntState.Bomb && x != cell.X && y != cell.Y)
                        {
                            map[x, y].IntState = CellIntState.Bomb;
                            bombCount++;
                        }
                    } while (bombCount != allBombCount);
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (map[i, j].IntState != CellIntState.Bomb)
                            {
                                bombCount = 0;
                                map[i, j].IntState = CellIntState.Number;
                                bombCount += (CheckCellIntState(i - 1, j - 1, CellIntState.Bomb)) ? 1 : 0;
                                bombCount += (CheckCellIntState(i - 1, j, CellIntState.Bomb)) ? 1 : 0;
                                bombCount += (CheckCellIntState(i - 1, j + 1, CellIntState.Bomb)) ? 1 : 0;
                                bombCount += (CheckCellIntState(i, j - 1, CellIntState.Bomb)) ? 1 : 0;
                                bombCount += (CheckCellIntState(i, j + 1, CellIntState.Bomb)) ? 1 : 0;
                                bombCount += (CheckCellIntState(i + 1, j - 1, CellIntState.Bomb)) ? 1 : 0;
                                bombCount += (CheckCellIntState(i + 1, j, CellIntState.Bomb)) ? 1 : 0;
                                bombCount += (CheckCellIntState(i + 1, j + 1, CellIntState.Bomb)) ? 1 : 0;
                                switch (bombCount)
                                {
                                    case 1:
                                        map[i, j].IntImage = Images.NumberOne;
                                        break;
                                    case 2:
                                        map[i, j].IntImage = Images.NumberTwo;
                                        break;
                                    case 3:
                                        map[i, j].IntImage = Images.NumberThree;
                                        break;
                                    case 4:
                                        map[i, j].IntImage = Images.NumberFour;
                                        break;
                                    case 5:
                                        map[i, j].IntImage = Images.NumberFive;
                                        break;
                                    case 6:
                                        map[i, j].IntImage = Images.NumberSix;
                                        break;
                                    case 7:
                                        map[i, j].IntImage = Images.NumberSeven;
                                        break;
                                    case 8:
                                        map[i, j].IntImage = Images.NumberEight;
                                        break;
                                    default:
                                        map[i, j].IntState = CellIntState.Empty;
                                        break;
                                }
                            }
                            (GameGrid.Children[i * height + j] as Image).Tag = map[i, j];
                        }
                    }
                    firstStep = false;
                    image_MouseUp(sender, e);
                }
                else if (!cell.IsOpen)
                {
                    if (cell.IntState == CellIntState.Bomb)
                    {
                        WAVPlayer.PlaySound(Properties.Resource.small_blast);
                        EnableTimer(false);
                        OpenBombs();
                        EndGame(false);
                    }
                    else
                    {
                        WAVPlayer.PlaySound(Properties.Resource.opencell);
                        OpenCell(cell.X, cell.Y);
                        EnableTimer(true);
                    }
                }
            }
            else if (e.ChangedButton == MouseButton.Right && !cell.IsOpen)
            {
                switch (cell.ExtState)
                {
                    case CellExtState.Empty:
                        map[cell.X, cell.Y].ExtState = CellExtState.Flag;
                        image.Tag = map[cell.X, cell.Y];
                        image.Source = map[cell.X, cell.Y].ExtImage;
                        bombFlagCount++;
                        break;
                    case CellExtState.Flag:
                        map[cell.X, cell.Y].ExtState = CellExtState.Question;
                        image.Tag = map[cell.X, cell.Y];
                        image.Source = map[cell.X, cell.Y].ExtImage;
                        bombFlagCount--;
                        break;
                    case CellExtState.Question:
                        map[cell.X, cell.Y].ExtState = CellExtState.Empty;
                        image.Tag = map[cell.X, cell.Y];
                        image.Source = map[cell.X, cell.Y].ExtImage;
                        break;
                }
                bombText.Text = (allBombCount - bombFlagCount).ToString();
            }
        }

        void OpenBombs()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j].IntState == CellIntState.Bomb)
                    {
                        (GameGrid.Children[i * height + j] as Image).Source = map[i, j].IntImage;
                    }
                }
            }
        }

        void EnableTimer(bool start)
        {
            if (start)
            {
                timer.Start();
                time.Start();
            }
            else
            {
                time.Stop();
                timer.Stop();
            }
        }

        void OpenCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height && !map[x, y].IsOpen)
            {
                (GameGrid.Children[x * height + y] as Image).Source = map[x, y].IntImage;
                ((GameGrid.Children[x * height + y] as Image).Tag as Cell).IsOpen = true;
                if (map[x, y].IntState == CellIntState.Empty)
                {
                    OpenCell(x - 1, y - 1);
                    OpenCell(x - 1, y);
                    OpenCell(x - 1, y + 1);
                    OpenCell(x, y - 1);
                    OpenCell(x, y + 1);
                    OpenCell(x + 1, y - 1);
                    OpenCell(x + 1, y);
                    OpenCell(x + 1, y + 1);
                }
                Cell_Opened();
            }
        }

        private void RecordsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RecordsWindow recordsWindow = new RecordsWindow(currentLevel);
            recordsWindow.ShowDialog();
        }

        private void ParametersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ParametersWindow parametersWindow = new ParametersWindow(currentLevel);
            parametersWindow.ShowDialog();
            Level newLevel = parametersWindow.GetLevel();
            if (currentLevel != newLevel)
            {
                currentLevel = newLevel;
                NewGame();
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NewGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void RestartGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }
    }
}
