using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneTimePad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The number of offsets in the one-time pad.
        private int[] Pad;
        private const int PadLength = 200;

        // The pad as a string.
        private string PadString;

        // The number of offsets used to encrypt and decrypt.
        private int NumUsedToEncrypt, NumUsedToDecrypt;

        // Initialize the one-time pad.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize a random number generator with a
            // fixed seed so we get the same pad every time.
            Random rand = new Random(1);
            Pad = new int[PadLength];
            PadString = "";
            for (int i = 0; i < PadLength; i++)
            {
                Pad[i] = rand.Next(0, 26);
                PadString += (char)(Pad[i] + 'A');
            }
            NumUsedToEncrypt = 0;
            NumUsedToDecrypt = 0;

            // Display the pad.
            DisplayPad();
        }

        // Encrypt.
        private void encryptButton_Click(object sender, EventArgs e)
        {
            string message = messageTextBox.Text.ToUpper().Replace(" ", "");
            string ciphertext = Encrypt(message);
            ciphertextTextBox.Text = ToChunks(ciphertext);
        }

        // Decrypt.
        private void decryptButton_Click(object sender, EventArgs e)
        {
            string ciphertext = ciphertextTextBox.Text;
            string plaintext = Decrypt(ciphertext);
            plaintextTextBox.Text = ToChunks(plaintext);
        }

        // Encrypt.
        private string Encrypt(string plaintext)
        {
            return EncryptDecrypt(plaintext, false);
        }

        // Decrypt.
        private string Decrypt(string ciphertext)
        {
            return EncryptDecrypt(ciphertext, true);
        }

        // Use the one-time pad to encrypt or decrypt the text.
        // Update NumUsedToEncrypt or NumUsedToDecrypt.
        private string EncryptDecrypt(string text, bool decrypt)
        {
            text = text.ToUpper().Replace(" ", "");

            // Start at the right entry in the pad.
            int i;
            if (decrypt)
                i = NumUsedToDecrypt;
            else
                i = NumUsedToEncrypt;

            // Make sure we have enough pad left for this message.
            if (i + text.Length > PadLength)
            {
                MessageBox.Show("The pad doesn't contain enough unnused characters for this operation.");
                return "**********";
            }

            string result = "";
            foreach (char ch in text)
            {
                int newCh;
                if (decrypt)
                    newCh = 'A' + (((ch - 'A') - Pad[i] + 26) % 26);
                else
                    newCh = 'A' + (((ch - 'A') + Pad[i]) % 26);
                result += (char)newCh;
                i++;
            }

            // Update NumUsedToEncrypt.
            if (decrypt)
                NumUsedToDecrypt += text.Length;
            else
                NumUsedToEncrypt += text.Length;

            // Re-display the pad.
            DisplayPad();

            // Return the ciphertext.
            return result;
        }

        // Display the one-time pad.
        private void DisplayPad()
        {
            padRichTextBox.Text = PadString;
            padRichTextBox.Select(0, PadString.Length);
            padRichTextBox.SelectionColor = padRichTextBox.ForeColor;
            padRichTextBox.SelectionBackColor = padRichTextBox.BackColor;

            padRichTextBox.Select(0, NumUsedToEncrypt);
            padRichTextBox.SelectionColor = Color.Blue;

            padRichTextBox.Select(0, NumUsedToDecrypt);
            padRichTextBox.SelectionBackColor = Color.Yellow;
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
