using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The root of the trie.
        private TrieNode Root = new TrieNode();

        // Add a value to the trie.
        private void addButton_Click(object sender, EventArgs e)
        {
            string key = keyTextBox.Text.ToUpper();
            if (key.Length == 0)
            {
                MessageBox.Show("Key must not be blank");
                keyTextBox.Focus();
                return;
            }
            string value = valueTextBox.Text;
            if (value.Length == 0)
            {
                MessageBox.Show("Value must not be blank");
                valueTextBox.Focus();
                return;
            }

            Root.AddValue(key, value);

            trieTextBox.Text = Root.ToString();
            trieTextBox.Select(0, 0);

            keyTextBox.Clear();
            keyTextBox.Focus();
            valueTextBox.Clear();
        }

        // Find a value to the trie.
        private void findButton_Click(object sender, EventArgs e)
        {
            string key = keyTextBox.Text.ToUpper();
            string value = Root.FindValue(key);

            if (value == null) valueTextBox.Text = "null";
            else valueTextBox.Text = value;

            trieTextBox.Text = Root.ToString();
            trieTextBox.Select(0, 0);
            keyTextBox.Focus();
            keyTextBox.Select(0, keyTextBox.Text.Length);
        }

        // Uncomment to build a tree at startup for testing.
        private void Form1_Load(object sender, EventArgs e)
        {
            //Root.AddValue("WANE", "29");
            //Root.AddValue("WANT", "36");
            //Root.AddValue("WANTED", "10");
            //Root.AddValue("WAN", "18");
            //Root.AddValue("WISP", "72");
            trieTextBox.Text = Root.ToString();
            trieTextBox.Select(0, 0);
        }
    }
}
