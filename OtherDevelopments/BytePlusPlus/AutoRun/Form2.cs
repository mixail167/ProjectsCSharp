using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoRun
{
    public partial class Form2 : Form
    {
        private Tuple<string, string> keyValue;

        public Form2(RegistryKey key)
        {
            InitializeComponent();
            string[] names = key.GetValueNames();
            foreach (string item in names)
            {
                listView1.Items.Add(new ListViewItem(new string[] { item, key.GetValue(item).ToString() }));
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView1.SelectedItems != null && listView1.SelectedItems.Count == 1)
            {
                keyValue = new Tuple<string, string>(listView1.SelectedItems[0].SubItems[0].Text, listView1.SelectedItems[0].SubItems[1].Text);
                Close();
            }
        }

        public Tuple<string, string> KeyValue
        {
            get { return keyValue; }
        }
    }
}
