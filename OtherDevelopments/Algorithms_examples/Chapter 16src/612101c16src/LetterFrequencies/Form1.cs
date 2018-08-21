using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetterFrequencies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Calculate the number of occurrances of each letter.
        private void evaluateButton_Click(object sender, EventArgs e)
        {
            string message = messageTextBox.Text.ToUpper().Replace(" ", "");
            int[] occurrences = new int[26];
            foreach (char ch in message)
            {
                int chNum = ch - 'A';
                occurrences[chNum]++;
            }

            // Display the results.
            percentListBox.Items.Clear();
            for (int i = 0; i < 26; i++)
            {
                float percent = occurrences[i] / (float)message.Length;
                // The offset at the end is the offset if the letter is E.
                int offset = (i - 4 + 26) % 26;
                string txt = string.Format("{0,6:P1} {1} Offset: {2}",
                    percent, (char)(i + 'A'), offset);
                percentListBox.Items.Add(txt);
            }
            percentListBox.SelectedIndex = 25;
        }
    }
}
