using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaesarSubstitution
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
            int shift = (int)shiftNumericUpDown.Value;
            string ciphertext = Encrypt(plaintext, shift);
            ciphertextTextBox.Text = ToChunks(ciphertext);
        }

        // Decrypt.
        private void decryptButton_Click(object sender, EventArgs e)
        {
            string ciphertext = ciphertextTextBox.Text.ToUpper().Replace(" ", "");
            int shift = (int)shiftNumericUpDown.Value;
            string plaintext = Decrypt(ciphertext, shift);
            plaintextTextBox.Text = ToChunks(plaintext);
        }

        // Use Caesar substitution to encrypt the message.
        private string Encrypt(string plaintext, int shift)
        {
            return EncryptDecrypt(plaintext, shift);
        }

        // Use Caesar substitution to decrypt the message.
        private string Decrypt(string ciphertext, int shift)
        {
            return EncryptDecrypt(ciphertext, 26 - shift);
        }

        // Use Caesar substitution to encrypt or decrypt the message.
        private string EncryptDecrypt(string plaintext, int shift)
        {
            // Process the message.
            string result = "";
            foreach (char ch in plaintext)
            {
                int chNum = ch - 'A';
                chNum = 'A' + ((chNum + shift) % 26);
                result += (char)chNum;
            }

            return result;
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
