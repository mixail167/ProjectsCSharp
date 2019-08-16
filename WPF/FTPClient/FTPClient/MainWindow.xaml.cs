using System;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using EnterpriseDT.Net.Ftp;
using FTPClient.Properties;

namespace FTPClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FTPConnection ftpConnection;
        bool connected;

        public MainWindow()
        {
            InitializeComponent();
            connected = false;
            textBoxHost.Text = Settings.Default.ServerName;
            textBoxPort.Text = Settings.Default.ServerPort.ToString();
            textBoxUser.Text = Settings.Default.UserName;
            textBoxPassword.Password = Settings.Default.UserPassword;
            checkBoxPassivMode.IsChecked = Settings.Default.PassivMode;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectInfo connectInfo = new ConnectInfo();
            try
            {
                connectInfo.ServerName = textBoxHost.Text;
                connectInfo.ServerPort = textBoxPort.Text;
                connectInfo.UserName = textBoxUser.Text;
                connectInfo.UserPassword = textBoxPassword.Password;
                connectInfo.PassivMode = (checkBoxPassivMode.IsChecked == true) ? true : false;
                try
                {
                    ftpConnection = connectInfo.Connect();
                    connected = ftpConnection.IsConnected;
                    connectInfo.Save();
                }
                catch (FTPException exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка " + exception.ReplyCode, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (IOException exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SocketException exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsConnected
        {
            get
            {
                return connected;
            }
        }
    }
}
