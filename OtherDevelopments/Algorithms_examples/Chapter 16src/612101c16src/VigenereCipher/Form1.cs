using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VigenereCipher
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
            string key = keyTextBox.Text.ToUpper();
            string ciphertext = Encrypt(plaintext, key);
            ciphertextTextBox.Text = ToChunks(ciphertext);
        }

        // Decrypt.
        private void decryptButton_Click(object sender, EventArgs e)
        {
            string ciphertext = ciphertextTextBox.Text.ToUpper().Replace(" ", "");
            string key = keyTextBox.Text.ToUpper();
            string plaintext = Decrypt(ciphertext, key);
            plaintextTextBox.Text = ToChunks(plaintext);
        }

        // Use a Vigenere cihper to encrypt the message.
        private string Encrypt(string plaintext, string key)
        {
            return EncryptDecrypt(plaintext, key, false);
        }

        // Use a Vigenere cihper to decrypt the message.
        private string Decrypt(string ciphertext, string key)
        {
            return EncryptDecrypt(ciphertext, key, true);
        }

        // Use a Vigenere cihper to encrypt or decrypt the message.
        private string EncryptDecrypt(string plaintext, string key, bool decrypt)
        {
            // Convert the key into an array of offsets.
            int[] offset = new int[key.Length];
            if (decrypt)
            {
                for (int i = 0; i < key.Length; i++) offset[i] = 26 - (key[i] - 'A');
            }
            else
            {
                for (int i = 0; i < key.Length; i++) offset[i] = key[i] - 'A';
            }

            // Process the message.
            string result = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                int j = (i % key.Length);
                int chNum = plaintext[i] - 'A';
                chNum = (chNum + offset[j]) % 26;
                result += (char)(chNum + 'A');
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
