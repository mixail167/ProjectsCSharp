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

        public Form1()
        {
            InitializeComponent();
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
                                           0);
                SYSTEMTIME st = new SYSTEMTIME();
                st.Day = (short)dt.Day;
                st.Month = (short)dt.Month;
                st.Year = (short)(dt.Year + 2000);
                st.Hour = (short)(dt.Hour - 2);
                st.Minute = (short)dt.Minute;
                st.Second = (short)dt.Second;
                st.Milliseconds = 0;
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

        public static DateTime GetNetworkTime()
        {
            try
            {
                const string ntpServer = "pool.ntp.org";
                // NTP message size - 16 bytes of the digest (RFC 2030)
                var ntpData = new byte[48];
                //Setting the Leap Indicator, Version Number and Mode values
                ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)
                var addresses = Dns.GetHostEntry(ntpServer).AddressList;
                //The UDP port number assigned to NTP is 123
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                //NTP uses UDP
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    socket.Connect(ipEndPoint);
                    //Stops code hang if NTP is blocked
                    socket.ReceiveTimeout = 3000;
                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                }
                //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                //departed the server for the client, in 64-bit timestamp format."
                const byte serverReplyTime = 40;
                //Get the seconds part
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
                //Get the seconds fraction
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
                //Convert From big-endian to little-endian
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);
                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                //**UTC** time
                var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
                return networkDateTime.ToLocalTime();
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt = GetNetworkTime();
            numericUpDown1.Value = dt.Day;
            numericUpDown2.Value = dt.Month;
            numericUpDown3.Value = dt.Year % 100;
            numericUpDown4.Value = dt.Hour;
            numericUpDown5.Value = dt.Minute;
        }
    }
}
