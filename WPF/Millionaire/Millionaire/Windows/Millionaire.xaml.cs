using Crypt;
using Millionaire.Classes;
using Millionaire.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Millionaire
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Record> records;
        List<Data> data1;
        List<Data> data2;
        List<Data> data3;
        List<Data> data;
        Record player;
        Data currentQuestion;
        int indexOfQuestion;
        DispatcherTimer timer;
        DispatcherTimer timer2;
        Label answerOfPlayer;
        bool endOfGame;

        public MainWindow()
        {
            InitializeComponent();
            records = new List<Record>();
            data1 = new List<Data>();
            data2 = new List<Data>();
            data3 = new List<Data>();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 3);
            timer2 = new DispatcherTimer();
            timer2.Tick += new EventHandler(timerTick2);
            timer2.Interval = new TimeSpan(0, 0, 3);
        }

        public static void ShowError(string message)
        {
            MessageBoxCustom messageBoxСustom = new MessageBoxCustom("Ошибка", message);
            messageBoxСustom.ShowDialog();
        }

        private bool CheckFile()
        {
            if (!File.Exists("data.bin"))
            {
                NoFileMessage noFileMessage = new NoFileMessage();
                noFileMessage.ShowDialog();
                return noFileMessage.next;
            }
            else return true;

        }

        private void Records_Click(object sender, RoutedEventArgs e)
        {
            if (records.Count == 0 && File.Exists("records.bin"))
            {
                ReadDataRecords();
            }
            RecordsWindow recordsWindow = new RecordsWindow(records);
            recordsWindow.ShowDialog();
        }

        private string ReadData(string fileName)
        {
            try
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    if (fileStream.Read(buffer, 0, buffer.Length) > 0)
                    {
                        return AesCrypt.DecryptStringFromBytes(buffer, Encoding.ASCII.GetBytes("zxcvqwerasdfqazx"), Encoding.ASCII.GetBytes("qazxcvbnmlpoiuyt"));
                    }
                    else throw new Exception("Ошибка чтения данных.");
                }
            }
            catch (Exception exception)
            {
                ShowError(exception.Message);
                return string.Empty;
            }
        }

        private void ReadDataRecords()
        {
            string temp = ReadData("records.bin");
            if (temp != string.Empty)
            {
                try
                {
                    string[] parts = temp.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        foreach (string item in parts)
                        {
                            Record record = Record.Parse(item.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));
                            if (record != null)
                            {
                                records.Add(record);
                            }
                        }
                        records = records.OrderByDescending(item => item.Score).ToList();
                        if (records.Count > 10)
                        {
                            records.RemoveRange(10, records.Count - 10);
                        }
                    }
                    else throw new Exception("Данные повреждены.");
                }
                catch (Exception exception)
                {
                    ShowError(exception.Message);
                }
            }
        }

        private List<Data> GetData(List<Data> data)
        {
            List<Data> temp = new List<Data>(data);
            Random r = new Random();
            do
            {
                temp.RemoveAt(r.Next(0, temp.Count));
            }
            while (temp.Count > 5);
            foreach (Data item in temp)
            {
                item.Random();
            }
            return temp;
        }

        private void NewQuestion()
        {
            currentQuestion = data[indexOfQuestion];
            LabelA.Content = string.Format("A: {0}", currentQuestion.Answers[0]);
            LabelB.Content = string.Format("B: {0}", currentQuestion.Answers[1]);
            LabelC.Content = string.Format("C: {0}", currentQuestion.Answers[2]);
            LabelD.Content = string.Format("D: {0}", currentQuestion.Answers[3]);
            LabelQuestion.Text = string.Format("{0}. {1}", (indexOfQuestion + 1), currentQuestion.Question);
            LabelA.Visibility = Visibility.Visible;
            LabelB.Visibility = Visibility.Visible;
            LabelC.Visibility = Visibility.Visible;
            LabelD.Visibility = Visibility.Visible;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void EnabledControls(bool flag)
        {
            LabelA.IsEnabled = flag;
            LabelB.IsEnabled = flag;
            LabelC.IsEnabled = flag;
            LabelD.IsEnabled = flag;
            StackPanel.IsEnabled = flag;
        }

        private void NewGame()
        {
            WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_начало_игры);
            data = new List<Data>();
            data.AddRange(GetData(data1));
            data.AddRange(GetData(data2));
            data.AddRange(GetData(data3));
            player = new Record();
            player.Score = 0;
            indexOfQuestion = 0;
            EnabledControls(true);
            Reset();
            NewQuestion();
            GameGrid.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DownloadFile downloadFile = new DownloadFile(true);
            downloadFile.ShowDialog();
            if (downloadFile.next)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox();
                customMessageBox.ShowDialog();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\ReadMe.txt";
                if (File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                }
                else throw new FileNotFoundException("Файл ReadMe.txt не найден.");
            }
            catch (Exception exception)
            {
                ShowError(exception.Message);
            }

        }

        private void image50x50_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = 0;
            Random r = new Random();
            do
            {
                switch (r.Next(0, 4))
                {
                    case 0:
                        index += Check(LabelA);
                        break;
                    case 1:
                        index += Check(LabelB);
                        break;
                    case 2:
                        index += Check(LabelC);
                        break;
                    case 3:
                        index += Check(LabelD);
                        break;
                }
            } while (index < 2);
            image50x50.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/image50x50Cancel.png"));
            image50x50.IsEnabled = false;
        }

        private int Check(Label label)
        {
            if (label.Content.ToString().EndsWith(currentQuestion.TrueAnswer))
            {
                return 0;
            }
            if (label.Visibility != Visibility.Hidden)
            {
                label.Visibility = Visibility.Hidden;
                label.Content = string.Empty;
                return 1;
            }
            return 0;
        }

        private void imagePeaple_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HelpPeaple helpPeaple = new HelpPeaple(
                new Data(new string[]
                {
                    LabelA.Content.ToString(),
                    LabelB.Content.ToString(),
                    LabelC.Content.ToString(),
                    LabelD.Content.ToString()
                },
                    currentQuestion.TrueAnswer)
                    );
            helpPeaple.ShowDialog();
            imagePeaple.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imagePeapleCancel.png"));
            imagePeaple.IsEnabled = false;
        }

        private void imageFriend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HelpFriends helpFriends = new HelpFriends(
                new Data(new string[]
                {
                    LabelA.Content.ToString(), 
                    LabelB.Content.ToString(), 
                    LabelC.Content.ToString(), 
                    LabelD.Content.ToString()
                },
                    currentQuestion.TrueAnswer)
                    );
            helpFriends.ShowDialog();
            if (helpFriends.next)
            {
                imageFriend.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imageFriendCancel.png"));
                imageFriend.IsEnabled = false;
            }
        }

        private void image50x50_MouseEnter(object sender, MouseEventArgs e)
        {
            if (image50x50.IsEnabled)
            {
                image50x50.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/image50x50Focus.png"));
            }
        }

        private void image50x50_MouseLeave(object sender, MouseEventArgs e)
        {
            if (image50x50.IsEnabled)
            {
                image50x50.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/image50x50.png"));
            }
        }

        private void imagePeaple_MouseEnter(object sender, MouseEventArgs e)
        {
            if (imagePeaple.IsEnabled)
            {
                imagePeaple.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imagePeapleFocus.png"));
            }
        }

        private void imagePeaple_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imagePeaple.IsEnabled)
            {
                imagePeaple.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imagePeaple.png"));
            }
        }

        private void imageFriend_MouseEnter(object sender, MouseEventArgs e)
        {
            if (imageFriend.IsEnabled)
            {
                imageFriend.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imageFriendFocus.png"));
            }
        }

        private void imageFriend_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imageFriend.IsEnabled)
            {
                imageFriend.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imageFriend.png"));
            }
        }

        private void Millionaire_ContentRendered(object sender, EventArgs e)
        {
            if (!CheckFile())
            {
                Close();
            }
            else if (!ReadMainData())
            {
                Close();
            }
        }

        private bool ReadMainData()
        {
            string temp = ReadData("data.bin");
            if (temp != string.Empty)
            {
                try
                {
                    string[] parts = temp.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 3)
                    {
                        for (int i = 0; i < parts.Length; i++)
                        {
                            string[] parts2 = parts[i].Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts2.Length >= 5)
                            {
                                ParseData(i, parts2);
                            }
                            else throw new Exception("Данные повреждены.");
                        }
                        if (data1.Count >= 5 && data2.Count >= 5 && data3.Count >= 5)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("Данные повреждены.");
                        }
                    }
                    else throw new Exception("Данные повреждены.");
                }
                catch (Exception exception)
                {
                    ShowError(exception.Message);
                    return false;
                }
            }
            else return false;
        }

        private void ParseData(int i, string[] parts)
        {

            foreach (string item in parts)
            {
                Data data = Data.Parse(item.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));
                if (data != null)
                {
                    switch (i)
                    {
                        case 0:
                            data1.Add(data);
                            break;
                        case 1:
                            data2.Add(data);
                            break;
                        case 2:
                            data3.Add(data);
                            break;
                    }
                }
            }
        }

        private void imageMoney_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ListBox.SelectedIndex != -1)
            {
                string[] parts = ((ListBoxItem)ListBox.Items[ListBox.SelectedIndex]).ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string temp = parts[2];
                player.Score = Convert.ToInt32(temp.Remove(temp.Length - 2));
            }
            EndOfGame();
        }

        private void imageMoney_MouseEnter(object sender, MouseEventArgs e)
        {
            imageMoney.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imageMoneyFocus.png"));
        }

        private void imageMoney_MouseLeave(object sender, MouseEventArgs e)
        {
            imageMoney.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imageMoney.png"));
        }

        private void imageSound_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WAVPlayer.sound)
            {
                WAVPlayer.StopSound();
                imageSound.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/soundOff.png"));
            }
            else
            {
                imageSound.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/soundOn.png"));
            }
            WAVPlayer.sound = !WAVPlayer.sound;
        }

        private void LabelA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_ответ_принят);
            EnabledControls(false);
            SetColor(LabelA, Brushes.White);
            answerOfPlayer = LabelA;
            timer.IsEnabled = true;
        }

        private void LabelC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_ответ_принят);
            EnabledControls(false);
            SetColor(LabelC, Brushes.White);
            answerOfPlayer = LabelC;
            timer.IsEnabled = true;
        }

        private void LabelB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_ответ_принят);
            EnabledControls(false);
            SetColor(LabelB, Brushes.White);
            answerOfPlayer = LabelB;
            timer.IsEnabled = true;
        }

        private void LabelD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_ответ_принят);
            EnabledControls(false);
            SetColor(LabelD, Brushes.White);
            answerOfPlayer = LabelD;
            timer.IsEnabled = true;
        }

        private void SetColor(Label label1, SolidColorBrush solidColorBrush)
        {
            label1.Background = solidColorBrush;
        }

        private void EndOfGame()
        {
            Reset();
            EndOfGameWindow endOfGameWindow = new EndOfGameWindow(records, player);
            endOfGameWindow.ShowDialog();
            records = endOfGameWindow.records;
        }

        private void Reset()
        {
            image50x50.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/image50x50.png"));
            imageFriend.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imageFriend.png"));
            imagePeaple.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/imagePeaple.png"));
            image50x50.IsEnabled = true;
            imageFriend.IsEnabled = true;
            imagePeaple.IsEnabled = true;
            imageMoney.IsEnabled = true;
            SetColor(LabelA, Brushes.Black);
            SetColor(LabelB, Brushes.Black);
            SetColor(LabelC, Brushes.Black);
            SetColor(LabelD, Brushes.Black);
            endOfGame = false;
            ListBox.SelectedIndex = -1;
            GameGrid.Visibility = Visibility.Hidden;
        }

        private void timerTick2(object sender, EventArgs e)
        {
            timer2.IsEnabled = false;
            if (endOfGame)
            {
                EndOfGame();
            }
            else
            {
                timer2.IsEnabled = false;
                SetColor(LabelA, Brushes.Black);
                SetColor(LabelB, Brushes.Black);
                SetColor(LabelC, Brushes.Black);
                SetColor(LabelD, Brushes.Black);
                EnabledControls(true);
                NewQuestion();
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            if (answerOfPlayer.Content.ToString().EndsWith(currentQuestion.TrueAnswer))
            {
                WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_правильный_ответ);
                SetColor(answerOfPlayer, Brushes.Green);
                indexOfQuestion++;
                ListBox.SelectedIndex = ListBox.Items.Count - indexOfQuestion;
                switch (indexOfQuestion)
                {
                    case 5:
                        player.Score = 500;
                        break;
                    case 10:
                        player.Score = 5000;
                        break;
                    case 15:
                        player.Score = 50000;
                        endOfGame = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_неправильный_ответ);
                SetColor(answerOfPlayer, Brushes.Red);
                if (LabelA.Content.ToString().EndsWith(currentQuestion.TrueAnswer))
                {
                    SetColor(LabelA, Brushes.Green);
                }
                else if (LabelB.Content.ToString().EndsWith(currentQuestion.TrueAnswer))
                {
                    SetColor(LabelB, Brushes.Green);
                }
                else if (LabelC.Content.ToString().EndsWith(currentQuestion.TrueAnswer))
                {
                    SetColor(LabelC, Brushes.Green);
                }
                else
                {
                    SetColor(LabelD, Brushes.Green);
                }
                endOfGame = true;
            }
            timer2.IsEnabled = true;
        }

        private void Millionaire_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WAVPlayer.StopSound();
        }
    }
}
