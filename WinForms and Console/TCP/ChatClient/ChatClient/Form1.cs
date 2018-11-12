using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Media;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Forms;
using WPF = System.Windows.Input;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private bool run;
        private const string regExp = @"(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}";
        private TcpClient client;
        private NetworkStream networkStream;
        private Thread receiveThread;
        private string user;
        private SoundPlayer soundPlayer;
        private List<User> users;
        private TextBoxSpellCheck textBoxSpellCheck;
        private bool close;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public Form1()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer(Properties.Resources.zvuk_soobshcheniya_v_kontakte);
            soundPlayer.Load();
            run = false;
            close = false;
            user = Environment.UserName;
            RefreshTextBox(string.Format("Ваше имя: {0}.", user));
            try
            {
                textBox1.Text = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["ip"].Value;
            }
            catch (ConfigurationErrorsException)
            {

            }
            pictureBox1.Image = (Properties.Settings.Default.Sound) ? null : Properties.Resources.cancel2;
            users = new List<User>();
            textBoxSpellCheck = new TextBoxSpellCheck();
            textBoxSpellCheck.textBox2.KeyUp += textBox2_KeyUp;
            elementHost1.Child = textBoxSpellCheck;
        }

        private void textBox2_KeyUp(object sender, WPF.KeyEventArgs e)
        {
            if (run && e.Key == WPF.Key.Enter && e.KeyboardDevice.Modifiers == WPF.ModifierKeys.Control)
            {
                SendMessage(textBoxSpellCheck.textBox2.Text);
                textBoxSpellCheck.textBox2.Text = string.Empty;
                e.Handled = true;
            }

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
            SendMessage(textBoxSpellCheck.textBox2.Text);
            textBoxSpellCheck.textBox2.Text = string.Empty;
            textBoxSpellCheck.textBox2.Focus();
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
                richTextBox1.Invoke(new Action<string>(RefreshTextBox), new object[] { text });
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
                            text = string.Format("{0} присоединился к чату", user.Name);
                            Notification(text);
                            AppendText(text, Color.Black);
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
                                    text = string.Format("{0} покинул чат", users[i].Name);
                                    Notification(text);
                                    AppendText(text, Color.Black);
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
                        Color color;
                        if (text.StartsWith("[message]") && text.EndsWith("[/message]"))
                        {
                            text = text.Replace("[message]", "").Replace("[/message]", "");
                            Notification(text);
                            color = Color.Red;
                        }
                        else if (text.StartsWith("[usermessage]") && text.EndsWith("[/usermessage]"))
                        {
                            text = string.Format("Вы: {0}", text.Replace("[usermessage]", "").Replace("[/usermessage]", ""));
                            color = Color.Blue;
                        }
                        else
                        {
                            color = Color.Black;
                        }
                        AppendText(text, color);
                    }
                }
                catch
                {

                }
            }
        }

        private void AppendText(string text, Color color)
        {
            text = "[" + DateTime.Now.ToLongTimeString() + "] " + text + "\r\n";
            int length = richTextBox1.TextLength;
            richTextBox1.AppendText(text);
            richTextBox1.SelectionStart = length;
            richTextBox1.SelectionLength = text.Length;
            richTextBox1.SelectionColor = color;
            richTextBox1.SelectionCharOffset = 5;
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        private void Notification(string text)
        {
            if (Properties.Settings.Default.Sound)
            {
                soundPlayer.Play();
            }
            try
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item is Form2)
                    {
                        item.Close();
                    }
                }
            }
            catch
            {

            }
            IntPtr handle = GetForegroundWindow();
            Form2 form2 = new Form2(text, this);
            form2.Visible = true;
            if (this.Handle == handle && !this.Focused)
            {
                this.Focus();
                if (!string.IsNullOrEmpty(textBoxSpellCheck.textBox2.Text))
                    textBoxSpellCheck.textBox2.CaretIndex = textBoxSpellCheck.textBox2.Text.Length;
            }
        }

        private void EnableComponents(bool flag, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool, string>(EnableComponents), new object[] { flag, text });
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
                        receiveThread = new Thread(new ThreadStart(ReceiveMessage))
                        {
                            IsBackground = true
                        };
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
                    else throw new Exception("Потеряно соединение с сервером.");
                }
                catch (Exception exception)
                {
                    if (exception.HResult != -2146233040)
                    {
                        RefreshTextBox(exception.Message);
                        CloseConnection();
                        EnableComponents(true, "Подключение");
                    }
                    break;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
            {
                e.Cancel = true;
                ModifyStateForm(false);
            }
        }

        private void SendMessage(string message)
        {
            message = message.Trim('\n', ' ');
            if (message != string.Empty)
            {
                byte[] data = Encoding.Unicode.GetBytes(string.Format("[{0}]{1}[/{0}]", "message", message));
                networkStream.Write(data, 0, data.Length);
                RefreshTextBox(string.Format("[{0}]{1}[/{0}]", "usermessage", message));
            }
        }

        private void CloseConnection()
        {
            if (networkStream != null)
                networkStream.Close();
            if (client != null)
                client.Close();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ModifyStateForm(true);
        }

        public void ModifyStateForm(bool value)
        {
            WindowState = (value) ? FormWindowState.Normal : FormWindowState.Minimized;
            notifyIcon1.Visible = !value;
            this.ShowInTaskbar = value;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Equals(contextMenuStrip1.Items[0]))
            {
                ModifyStateForm(true);
            }
            else
            {
                if (run)
                {
                    Disconnect();
                }
                try
                {
                    foreach (Form item in Application.OpenForms)
                    {
                        if (item is Form2)
                        {
                            item.Close();
                        }
                    }
                }
                catch (Exception)
                {

                }
                close = true;
                this.Close();
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.Sound)
            {
                soundPlayer.Stop();
                pictureBox1.Image = Properties.Resources.cancel2;
            }
            else
            {
                pictureBox1.Image = null;
            }
            Properties.Settings.Default.Sound = !Properties.Settings.Default.Sound;
            Properties.Settings.Default.Save();
        }
    }
}
