using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bookletter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ushort count = Convert.ToUInt16(numericUpDown1.Value);
            if (count % 4 == 0)
            {
                label5.Text = count.ToString();
            }
            else
            {
                label5.Text = string.Format("{0} + {1} = {2}", count, 4 - count % 4, count + 4 - count % 4);
                count += Convert.ToUInt16(4 - count % 4);
            }
            ushort n = Convert.ToUInt16((count - 4) / 2);
            List<ushort> list1 = new List<ushort>();
            List<ushort> list2 = new List<ushort>();
            for (ushort i = 0; i <= n; i += 2)
            {
                list1.Add(Convert.ToUInt16(count - i));
                list1.Add(Convert.ToUInt16(i + 1));
                list2.Add(Convert.ToUInt16(i + 2));
                list2.Add(Convert.ToUInt16(count - i - 1));
            }
            richTextBox1.Text = string.Join(", ", list1.ToArray());
            richTextBox2.Text = string.Join(", ", list2.ToArray());
        }
    }
}
