using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private List<Client> clients;
        private Thread listenThread;
        private bool run;
        private const string regExp = @"(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}";
        private bool close;

        public Form1()
        {
            InitializeComponent();
            run = false;
            close = false;
            GetAddresses();
        }

        private void GetAddresses()
        {
            bool found = false;
            button1.Enabled = found;
            comboBox1.Items.Clear();
            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            if (addresses.Length != 0)
            {
                Regex regex = new Regex(regExp);
                foreach (IPAddress item in addresses)
                {
                    if (regex.IsMatch(item.ToString()))
                    {
                        comboBox1.Items.Add(item.ToString());
                        found = true;
                    }
                }
                if (found)
                {
                    comboBox1.SelectedIndex = 0;
                    button1.Enabled = found;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetAddresses();
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

        private void EnableComponents(bool flag, string text)
        {
            button2.Enabled = flag;
            comboBox1.Enabled = flag;
            run = !flag;
            button1.Text = text;
        }

        private void Disconnect()
        {
            DisconnectServer();
            RefreshTextBox("Стоп.");
            EnableComponents(true, "Старт");
        }

        private void Connect()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(comboBox1.Items[comboBox1.SelectedIndex].ToString());
                listenThread = new Thread(Listen)
                {
                    IsBackground = true
                };
                listenThread.Start(ipAddress);
                RefreshTextBox("Старт.");
                EnableComponents(false, "Стоп");
            }
            catch (Exception exception)
            {
                RefreshTextBox(exception.Message);
                DisconnectServer();
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
                    richTextBox1.Text += "[" + DateTime.Now.ToLongTimeString() + "] " + text + "\r\n";
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.ScrollToCaret();
                }
                catch
                {

                }
            }
        }

        private void Listen(object obj)
        {
            try
            {
                tcpListener = new TcpListener((IPAddress)obj, 8888);
                tcpListener.Start();
                clients = new List<Client>();
                while (true)
                {
                    if (tcpListener.Pending())
                    {
                        Client client = new Client(tcpListener.AcceptTcpClient());
                        clients.Add(client);
                        Thread clientThread = new Thread(Process)
                        {
                            IsBackground = true
                        };
                        clientThread.Start(client);
                    }
                    else Thread.Sleep(2000);
                }
            }
            catch (Exception exception)
            {
                RefreshTextBox(exception.Message);
                DisconnectServer();
            }
        }

        private void DisconnectServer()
        {
            try
            {
                listenThread.Abort();
                foreach (Client item in clients)
                {
                    item.Close();
                }
                tcpListener.Stop();
            }
            catch (Exception exception)
            {
                RefreshTextBox(exception.Message);
            }
        }

        private void BroadcastMessage(string message, Guid id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            foreach (Client item in clients)
            {
                if (item.ID != id && item.NetworkStream.CanWrite)
                {
                    try
                    {
                        item.NetworkStream.Write(data, 0, data.Length);
                    }
                    catch (Exception exception)
                    {
                        RefreshTextBox(exception.Message);
                    }
                }
            }
        }

        private void Process(object obj)
        {
            Client client = (Client)obj;
            string message = GetMessage(client);
            if (!message.Equals(string.Empty))
            {
                client.User = message;
                message = string.Format("[{0}]{2} {1}[/{0}]", "adduser", client.User, client.ID.ToString());
                BroadcastMessage(message, client.ID);
                RefreshTextBox(string.Format("{0} присоединился к чату.", client.User));
                StringBuilder stringBuilder = new StringBuilder("[users]");
                if (clients.Count > 1)
                {
                    List<string> list = new List<string>();
                    foreach (Client item in clients)
                    {
                        if (client.ID != item.ID)
                        {
                            list.Add(item.ID + " " + item.User);
                        }
                    }
                    stringBuilder.Append(string.Join(";", list));
                }
                stringBuilder.Append("[/users]");
                if (client.NetworkStream.CanWrite)
                {
                    byte[] data = Encoding.Unicode.GetBytes(stringBuilder.ToString());
                    client.NetworkStream.Write(data, 0, data.Length);
                }
                while (true)
                {
                    try
                    {
                        message = GetMessage(client);
                        if (message.Equals(string.Empty))
                            throw new Exception();
                        message = String.Format("[message]{0}: {1}", client.User, message.Replace("[message]", ""));
                        BroadcastMessage(message, client.ID);
                    }
                    catch
                    {
                        if (run)
                        {
                            message = string.Format("[{0}]{1}[/{0}]", "deluser", client.ID);
                            RefreshTextBox(string.Format("{0} покинул чат.", client.User));
                            BroadcastMessage(message, client.ID);
                        }
                        break;
                    }
                }
                clients.Remove(client);
                client.Close();
            }
        }

        private string GetMessage(Client client)
        {
            byte[] data = new byte[64];
            StringBuilder stringBuilder = new StringBuilder();
            int bytes = 0;
            try
            {
                do
                {
                    if (client.NetworkStream.CanRead)
                    {
                        try
                        {
                            bytes = client.NetworkStream.Read(data, 0, data.Length);
                            stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        catch (ObjectDisposedException exception)
                        {
                            RefreshTextBox(exception.Message);
                        }
                    }
                }
                while (client.NetworkStream.DataAvailable);
            }
            catch (Exception exception)
            {
                RefreshTextBox(exception.Message);
            }
            return stringBuilder.ToString();
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
                close = true;
                Close();
            }
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
    }
}
