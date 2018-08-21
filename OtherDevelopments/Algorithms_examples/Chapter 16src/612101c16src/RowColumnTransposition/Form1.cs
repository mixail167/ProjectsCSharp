using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RowColumnTransposition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Encrypt.
        private void encryptButton_Click(object sender, EventArgs e)
        {
            string plaintext = messageTextBox.Text.ToUpper().Replace(" ", "");
            int numColumns = (int)numColumnsNumericUpDown.Value;
            string ciphertext = Encrypt(plaintext, numColumns);
            ciphertextTextBox.Text = ToChunks(ciphertext);
        }

        // Decrypt.
        private void decryptButton_Click(object sender, EventArgs e)
        {
            string ciphertext = ciphertextTextBox.Text.ToUpper().Replace(" ", "");
            int numColumns = (int)numColumnsNumericUpDown.Value;
            string plaintext = Decrypt(ciphertext, numColumns);
            plaintextTextBox.Text = ToChunks(plaintext);
        }

        // Use a row/column transposition to encrypt the message.
        private string Encrypt(string plaintext, int numColumns)
        {
            // Calculate the number of rows.
            // (The division rounds down so this calculation gives the smallest
            // integer greater than or equal to plaintext.Length / numColumns.)
            int numRows = 1 + (plaintext.Length - 1) / numColumns;

            // Pad the string if necessary to make it fit the rectangle evenly.
            if (numRows * numColumns != plaintext.Length)
                plaintext += new string('X', numRows * numColumns - plaintext.Length);

            // Construct the encrypted string.
            string result = "";
            for (int col = 0; col < numColumns; col++)
            {
                int index = col;
                for (int row = 0; row < numRows; row++)
                {
                    result += plaintext[index];
                    index += numColumns;
                }
            }

            return result;
        }

        // Use a row/column transposition to encrypt the message.
        private string Decrypt(string ciphertext, int numColumns)
        {
            int numRows = ciphertext.Length / numColumns;
            return Encrypt(ciphertext, numRows);
        }

        // Break the text into 5-character chunks.
        private string ToChunks(string message)
        {
            // Pad the message in case its length isn't a multiple of 5.
            message += "     ";

            // Create the 5-character chunks.
            string result = "";
            for (int i = 0; i < message.Length - 5; i += 5)
                result += message.Substring(i, 5) + " ";

            // Remove trailing spaces.
            return result.TrimEnd();
        }
    }
}
