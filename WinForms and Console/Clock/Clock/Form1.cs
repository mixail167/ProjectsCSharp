using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Milliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime([In] ref SYSTEMTIME st);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public Form1()
        {
            InitializeComponent();
            int description;
            if (InternetGetConnectedState(out description, 0))
            {
                GetTime();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = new DateTime((int)numericUpDown3.Value,
                                           (int)numericUpDown2.Value,
                                           (int)numericUpDown1.Value,
                                           (int)numericUpDown4.Value,
                                           (int)numericUpDown5.Value,
                                           0).AddYears(2000).AddHours(-2);
                SYSTEMTIME st = new SYSTEMTIME();
                st.Day = (short)dt.Day;
                st.Month = (short)dt.Month;
                st.Year = (short)(dt.Year);
                st.Hour = (short)(dt.Hour);
                st.Minute = (short)dt.Minute;
                st.Second = (short)dt.Second;
                st.Milliseconds = (short)dt.Millisecond;
                if (!SetSystemTime(ref st))
                {
                    MessageBox.Show("Дата и время не установлены!");
                }
                else Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }

        private void numericUpDown2_Enter(object sender, EventArgs e)
        {
            numericUpDown2.Select(0, numericUpDown2.Text.Length);
        }

        private void numericUpDown3_Enter(object sender, EventArgs e)
        {
            numericUpDown3.Select(0, numericUpDown3.Text.Length);
        }

        private void numericUpDown4_Enter(object sender, EventArgs e)
        {
            numericUpDown4.Select(0, numericUpDown4.Text.Length);
        }

        private void numericUpDown5_Enter(object sender, EventArgs e)
        {
            numericUpDown5.Select(0, numericUpDown5.Text.Length);
        }

        public DateTime GetNetworkTime()
        {
            DateTime dt = new DateTime();
            try
            {
                const string ntpServer = "pool.ntp.org";
                byte[] ntpData = new byte[48];
                ntpData[0] = 0x1B;
                IPAddress[] addresses = Dns.GetHostEntry(ntpServer).AddressList;
                foreach (IPAddress item in addresses)
                {
                    try
                    {
                        IPEndPoint ipEndPoint = new IPEndPoint(item, 123);
                        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                        {
                            socket.Connect(ipEndPoint);
                            socket.ReceiveTimeout = 3000;
                            socket.Send(ntpData);
                            socket.Receive(ntpData);
                        }
                        const byte serverReplyTime = 40;
                        ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
                        ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
                        intPart = SwapEndianness(intPart);
                        fractPart = SwapEndianness(fractPart);
                        ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                        DateTime networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
                        dt = networkDateTime.ToLocalTime();
                        break;
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return dt;
        }

        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetTime();
        }

        private void GetTime()
        {
            DateTime dt = GetNetworkTime();
            if (dt != new DateTime())
            {
                numericUpDown1.Value = dt.Day;
                numericUpDown2.Value = dt.Month;
                numericUpDown3.Value = dt.Year % 100;
                numericUpDown4.Value = dt.Hour;
                numericUpDown5.Value = dt.Minute;
            }
        }
    }
}
