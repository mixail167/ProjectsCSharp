using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwapRowsAndColumns
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
            string colKey = key1TextBox.Text;
            string rowKey = key2TextBox.Text;
            string ciphertext = Encrypt(plaintext, colKey, rowKey);
            ciphertextTextBox.Text = ToChunks(ciphertext);
        }

        // Decrypt.
        private void decryptButton_Click(object sender, EventArgs e)
        {
            string ciphertext = ciphertextTextBox.Text.ToUpper().Replace(" ", "");
            string colKey = key1TextBox.Text;
            string rowKey = key2TextBox.Text;
            string plaintext = Decrypt(ciphertext, colKey, rowKey);
            plaintextTextBox.Text = ToChunks(plaintext);
        }

        // Use a row/column transformation to encrypt the message.
        private string Encrypt(string plaintext, string colKey, string rowKey)
        {
            return EncryptDecrypt(plaintext, colKey, rowKey, false);
        }

        // Use a row/column transformation to decrypt the message.
        private string Decrypt(string ciphertext, string colKey, string rowKey)
        {
            return EncryptDecrypt(ciphertext, colKey, rowKey, true);
        }

        // Use a row/column transformation to encrypt or decrypt the message.
        private string EncryptDecrypt(string plaintext, string colKey, string rowKey, bool decrypt)
        {
            // Calculate the number of rows.
            int numColumns = colKey.Length;

            // (The division rounds down so this calculation gives the smallest
            // integer greater than or equal to plaintext.Length / numColumns.)
            int numRows = 1 + (plaintext.Length - 1) / numColumns;

            // Pad the string if necessary to make it fit the rectangle evenly.
            if (numRows * numColumns != plaintext.Length)
                plaintext += new string('X', numRows * numColumns - plaintext.Length);

            // Make the key mappings.
            int[] forwardColMapping, inverseColMapping, forwardRowMapping, inverseRowMapping;
            MakeKeyMapping(colKey, out forwardColMapping, out inverseColMapping);
            MakeKeyMapping(rowKey, out forwardRowMapping, out inverseRowMapping);
            int[] colMapping, rowMapping;
            if (decrypt)
            {
                colMapping = forwardColMapping;
                rowMapping = forwardRowMapping;
            }
            else
            {
                colMapping = inverseColMapping;
                rowMapping = inverseRowMapping;
            }

            // Construct the encrypted/decrypted string.
            string result = "";
            for (int row = 0; row < numRows; row++)
            {
                // Read this row in permuted order.
                for (int col = 0; col < numColumns; col++)
                {
                    int index = rowMapping[row] * numColumns + colMapping[col];
                    result += plaintext[index];
                }
            }

            return result;
        }

        // Make a mapping for this key.
        private void MakeKeyMapping(string key, out int[] mapping, out int[] inverseMapping)
        {
            // Sort the characters.
            char[] chars = key.ToCharArray();
            Array.Sort(chars);
            string sortedKey = new string(chars);

            // Make the mapping.
            mapping = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
                mapping[i] = sortedKey.IndexOf(key[i]);

            // Make the inverse mapping.
            inverseMapping = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
                inverseMapping[mapping[i]] = i;
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
