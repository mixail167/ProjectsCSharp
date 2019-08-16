using EnterpriseDT.Net.Ftp;
using FTPClient.Properties;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace FTPClient
{
    public class ConnectInfo
    {
        const string regIP = @"^(25[0-5]|2[0-4][\d]|[0-1][\d]{2}|[\d]{2}|[\d])(\.(25[0-5]|2[0-4][\d]|[0-1][\d]{2}|[\d]{2}|[\d])){3}$";
        const string regURL = @"^([\da-z\.-]+)\.([a-z\.]{2,6})$";
        const string regPort = @"^(([\d]{1,4})|([1-5][\d]{4})|(6[0-4][\d]{3})|(65[0-4][\d]{2})|(655[0-2][\d])|(6553[0-5]))$";
        const string regUserOrPassword = @"^[\da-zA-Z_]*$";

        string serverName;
        int serverPort;
        string userName;
        string userPassword;
        bool passivMode;

        public ConnectInfo()
        {

        }

        public string ServerName
        {
            set
            {
                if (!Regex.IsMatch(value, regIP + "|" + regURL))
                    throw new FormatException("Неверное имя сервера");
                serverName = value;
            }
        }

        public string ServerPort
        {
            set
            {
                if (!Regex.IsMatch(value, regPort))
                    throw new FormatException("Неверный номер порта");
                serverPort = Convert.ToInt32(value);
            }
        }

        public string UserName
        {
            set
            {
                if (!Regex.IsMatch(value, regUserOrPassword))
                    throw new FormatException("Неверное имя пользователя");
                userName = value;
            }
        }

        public void Save()
        {
            Settings.Default.ServerName = serverName;
            Settings.Default.ServerPort = serverPort;
            Settings.Default.UserName = userName;
            Settings.Default.UserPassword = userPassword;
            Settings.Default.PassivMode = passivMode;
            Settings.Default.Save();
        }

        public string UserPassword
        {
            set
            {
                if (!Regex.IsMatch(value, regUserOrPassword))
                    throw new FormatException("Неверный пароль");
                userPassword = value;
            }
        }

        public bool PassivMode
        {
            set
            {
                passivMode = value;
            }
        }

        public FTPConnection Connect()
        {
            FTPConnection ftpConnection = new FTPConnection();
            try
            {
                ftpConnection.ServerAddress = serverName;
                ftpConnection.ServerPort = serverPort;
                ftpConnection.UserName = (userName == string.Empty) ? "anonymous" : userName;
                ftpConnection.Password = (userName == string.Empty) ? "1" : userPassword;
                ftpConnection.ConnectMode = (passivMode) ? FTPConnectMode.PASV : FTPConnectMode.ACTIVE;
                ftpConnection.Connect();
                return ftpConnection;
            }
            catch (FTPException)
            {
                CloseConnection(ftpConnection);
                throw;
            }
            catch (IOException)
            {
                CloseConnection(ftpConnection);
                throw;
            }
            catch (SocketException)
            {
                CloseConnection(ftpConnection);
                throw;
            }
        }

        private void CloseConnection(FTPConnection ftpConnection)
        {
            if (ftpConnection.IsConnected)
            {
                ftpConnection.Close();
            }
        }
    }
}
