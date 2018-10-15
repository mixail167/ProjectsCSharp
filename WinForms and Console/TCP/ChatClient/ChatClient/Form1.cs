using System;
using System.Collections.Generic;
using System.Configuration;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        bool run;
        const string regExp = @"(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}";
        TcpClient client;
        NetworkStream networkStream;
        Thread receiveThread;
        string user;
        SoundPlayer soundPlayer;
        List<User> users;

        public Form1()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer(Properties.Resources.zvuk_soobshcheniya_v_kontakte);
            soundPlayer.Load();
            run = false;
            user = Environment.UserName;
            RefreshTextBox(string.Format("Ваше имя: {0}.", user));
            try
            {
                textBox1.Text = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["ip"].Value;
            }
            catch (ConfigurationErrorsException)
            {

            }
            users = new List<User>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (run)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text.Trim('\n').Trim();
            if (text != string.Empty)
            {
                SendMessage(text);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!run && e.KeyChar == 13)
            {
                Connect();
            }
        }

        private void RefreshTextBox(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new Action<string>(RefreshTextBox), new object[] { text });
            }
            else
            {
                try
                {
                    if (text.StartsWith("[users]") && text.EndsWith("[/users]"))
                    {
                        string[] users_string = text.Replace("[users]", "").Replace("[/users]", "").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string item in users_string)
                        {
                            try
                            {
                                User user = new User(item);
                                users.Add(user);
                                listBox1.Items.Add(user.Name);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    else if (text.StartsWith("[adduser]") && text.EndsWith("[/adduser]"))
                    {
                        text = text.Replace("[adduser]", "").Replace("[/adduser]", "");
                        try
                        {
                            User user = new User(text);
                            users.Add(user);
                            listBox1.Items.Add(user.Name);
                            Notify(string.Format("{0} вошел в чат", user.Name));
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else if (text.StartsWith("[deluser]") && text.EndsWith("[/deluser]"))
                    {
                        text = text.Replace("[deluser]", "").Replace("[/deluser]", "");
                        for (int i = 0; i < users.Count; i++)
                        {
                            try
                            {
                                if (users[i].ID == Guid.Parse(text))
                                {
                                    listBox1.Items.RemoveAt(i);
                                    Notify(string.Format("{0} покинул чат", users[i].Name));
                                    users.RemoveAt(i);
                                    break;
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    else
                    {
                        if (text.StartsWith("[message]") && text.EndsWith("[/message]"))
                        {
                            text = text.Replace("[message]", "").Replace("[/message]", "");
                            Notify(text);
                        }
                        richTextBox1.Text += "[" + DateTime.Now.ToLongTimeString() + "] " + text + "\r\n";
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.ScrollToCaret();
                    }
                }
                catch
                {

                }
            }
        }

        private void Notify(string text)
        {
            soundPlayer.Play();
            foreach (Form item in Application.OpenForms)
            {
                if (item is Form2)
                {
                    item.Close();
                }
            }
            Form2 form2 = new Form2(text);
            form2.Show();
        }

        private void EnableComponents(bool flag, string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<bool, string>(EnableComponents), new object[] { flag, text });
            }
            else
            {
                button2.Enabled = !flag;
                textBox1.ReadOnly = !flag;
                run = !flag;
                button1.Text = text;
                listBox1.Items.Clear();
            }
        }

        private void Disconnect()
        {
            receiveThread.Abort();
            CloseConnection();
            RefreshTextBox("Отключение выполнено.");
            EnableComponents(true, "Подключение");
            users.Clear();
        }

        private void Connect()
        {
            Regex regex = new Regex(regExp);
            if (regex.IsMatch(textBox1.Text))
            {

                client = new TcpClient();
                try
                {
                    if (client.ConnectAsync(textBox1.Text, 8888).Wait(5000))
                    {

                        networkStream = client.GetStream();
                        byte[] data = Encoding.Unicode.GetBytes(user);
                        networkStream.Write(data, 0, data.Length);
                        receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                        receiveThread.Start();
                        RefreshTextBox("Подключение выполнено.");
                        EnableComponents(false, "Отключение");
                    }
                    else throw new AggregateException();
                }
                catch (AggregateException)
                {
                    RefreshTextBox("Сервер не найден.");
                }
                catch (Exception exception)
                {
                    RefreshTextBox(exception.Message);
                }
            }
            else
            {
                RefreshTextBox("Неверный IP-адрес.");
            }
        }

        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder stringBuilder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = networkStream.Read(data, 0, data.Length);
                        stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (networkStream.DataAvailable);
                    if (bytes > 0)
                    {
                        RefreshTextBox(stringBuilder.ToString());
                    }
                    else
                    {
                        RefreshTextBox("Потеряно соединение с сервером.");
                        CloseConnection();
                        EnableComponents(true, "Подключение");
                        break;
                    }

                }
                catch (Exception exception)
                {
                    if (!exception.Message.StartsWith("Поток находился в процессе прерывания"))
                    {
                        RefreshTextBox(exception.Message);
                        CloseConnection();
                        EnableComponents(true, "Подключение");
                        break;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (run)
            {
                Disconnect();
            }
            foreach (Form item in Application.OpenForms)
            {
                if (item is Form2)
                {
                    item.Close();
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (run && e.KeyChar == 13)
            {
                button2_Click(this, new EventArgs());
                e.Handled = true;
            }
        }

        private void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(string.Format("[{0}]{1}[/{0}]", "message", message));
            networkStream.Write(data, 0, data.Length);
            RefreshTextBox("Ваше сообщение: " + message);
            textBox2.Text = string.Empty;
        }

        private void CloseConnection()
        {
            if (networkStream != null)
                networkStream.Close();
            if (client != null)
                client.Close();
        }
    }
}
