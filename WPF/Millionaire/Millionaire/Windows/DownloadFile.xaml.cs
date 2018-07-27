using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows;

namespace Millionaire
{
    /// <summary>
    /// Логика взаимодействия для DownloadFile.xaml
    /// </summary>
    public partial class DownloadFile : Window
    {
        bool check;
        public bool next;

        public DownloadFile(bool check)
        {
            InitializeComponent();
            this.check = check;
        }

        private delegate void NoArgDelegate();
        public void Refresh(DependencyObject obj)
        {
            obj.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Loaded,
                (NoArgDelegate)delegate { });
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        void ShowError(string text)
        {
            GridError.Visibility = Visibility.Visible;
            Error.Text = "Ошибка: " + text;
        }

        void ShowStatus(string text)
        {
            Status.Text = text;
            Refresh(Status);
        }

        void UpdateProgress()
        {
            Progress.Value++;
            Refresh(Progress);
        }

        byte[] CalculateMD5(string fileName)
        {
            byte[] checkSum;
            try
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    using (MD5 md5 = new MD5CryptoServiceProvider())
                    {
                        byte[] fileData = new byte[fileStream.Length];
                        if (fileStream.Read(fileData, 0, (int)fileStream.Length) > 0)
                            checkSum = md5.ComputeHash(fileData);
                        else throw new Exception("Ошибка чтения данных.");
                    }
                }
            }
            catch (Exception exception)
            {
                checkSum = new byte[0];
                ShowError(exception.Message);
            }
            return checkSum;
        }

        byte[] ReadMD5(string fileName)
        {
            byte[] checkSum;
            try
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    checkSum = new byte[fileStream.Length];
                    if (fileStream.Read(checkSum, 0, (int)fileStream.Length) <= 0)
                        throw new Exception("Ошибка чтения данных.");
                }
            }
            catch (Exception exception)
            {
                ShowError(exception.Message);
                checkSum = new byte[0];
            }
            return checkSum;
        }

        void Download()
        {
            Progress.Value = 0;
            GridError.Visibility = Visibility.Hidden;
            int Description;
            WebClient webClient = new WebClient();
            next = false;
            if (check)
            {
                Progress.Maximum = 5;
                try
                {
                    ShowStatus("Проверка соединения с интернетом...");
                    if (!InternetGetConnectedState(out Description, 0))
                        throw new Exception("Отсутствует соединение с Интернетом.");
                    UpdateProgress();
                    ShowStatus("Загрузка данных с хеш-суммой...");
                    webClient.DownloadFile(new Uri("https://raw.githubusercontent.com/mixail167/Millionaire/master/data.md5"), "data.md5");
                    UpdateProgress();
                    ShowStatus("Вычисление хеш-суммы...");
                    if (!File.Exists("data.bin"))
                        throw new Exception("Файл с основными данными не найден.");
                    byte[] checkSum = CalculateMD5("data.bin");
                    if (checkSum.Length > 0)
                    {
                        UpdateProgress();
                        if (!File.Exists("data.md5"))
                            throw new Exception("Файл с хеш-суммой отсутствует.");
                        byte[] checkSumNew = ReadMD5("data.md5");
                        if (checkSumNew.Length > 0)
                        {
                            if (!checkSumNew.SequenceEqual(checkSum))
                            {
                                UpdateProgress();
                                ShowStatus("Загрузка основных данных...");
                                webClient.DownloadFile(new Uri("https://raw.githubusercontent.com/mixail167/Millionaire/master/data.bin"), "data.bin");
                                UpdateProgress();
                                next = true;
                            }
                            else
                            {
                                this.Hide();
                                MessageBoxCustom messageBoxСustom = new MessageBoxCustom("Внимание", "Обновление данных не требуется.");
                                messageBoxСustom.ShowDialog();
                            }
                        }

                    }
                    Close();
                }
                catch (Exception exception)
                {
                    ShowError(exception.Message);
                }
                finally
                {
                    File.Delete("data.md5");
                }
            }
            else
            {
                Progress.Maximum = 2;
                try
                {
                    ShowStatus("Проверка соединения с интернетом...");
                    if (!InternetGetConnectedState(out Description, 0))
                        throw new Exception("Отсутствует соединение с Интернетом.");
                    UpdateProgress();
                    ShowStatus("Загрузка основных данных...");
                    webClient.DownloadFile(new Uri("https://raw.githubusercontent.com/mixail167/Millionaire/master/data.bin"), "data.bin");
                    UpdateProgress();
                    next = true;
                    Close();
                }
                catch (Exception exception)
                {
                    ShowError(exception.Message);
                }
            }
        }

        private void ButtonRepeat_Click(object sender, RoutedEventArgs e)
        {
            Download();
        }

        private void DownloadFile1_ContentRendered(object sender, EventArgs e)
        {
            Download();
        }
    }
}
