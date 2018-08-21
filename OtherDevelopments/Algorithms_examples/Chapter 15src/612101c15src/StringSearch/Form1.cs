using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Search for the target.
        private void searchButton_Click(object sender, EventArgs e)
        {
            string text = stringRichTextBox.Text;
            string target = targetRichTextBox.Text;
            int location = FindTarget(text, target);

            if (location < 0) stringRichTextBox.Select(0, 0);
            else stringRichTextBox.Select(location, target.Length);

            stringRichTextBox.Focus();
        }

        // Find the first instance of the target string.
        // Use a pre-calculated shift array.
        private int FindTarget(string text, string target)
        {
            string trace = text + Environment.NewLine;

            int targetLen = target.Length;
            int textLen = text.Length;

            // Pre-calculate the shifts.
            int[,] shift = new int[256, targetLen];
            for (char ch = (char)0; ch < 256; ch++)
            {
                for (int i = 0; i < targetLen; i++)
                {
                    // Fill in the shift if we see ch at position i.
                    // Assume ch doesn't appear to the left in the target.
                    shift[ch, i] = targetLen;

                    // See if ch actually appears to the left in the target.
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (target[j] == ch)
                        {
                            shift[ch, i] = i - j;
                            break;
                        }
                    }
                }
            }

            // Examine the string.
            for (int textPos = targetLen - 1; textPos < textLen; )
            {
                if (textPos > targetLen) trace += new string(' ', textPos - targetLen + 1);
                trace += target + Environment.NewLine;
                Console.WriteLine(trace);//@
                Console.WriteLine();//@

                bool matches = true;
                for (int j = 0; j < targetLen; j++)
                {
                    int targetEnd = targetLen - 1;

                    // See if the corresponding characters match.
                    if (text[textPos - j] != target[targetEnd - j])
                    {
                        // They don't match.
                        matches = false;

                        // Get the character that didn't match.
                        char badChar = text[textPos - j];

                        // Shift.
                        int targetPos = targetLen - j - 1;
                        textPos += shift[badChar, targetPos];
                        break;
                    }
                }

                // If we had a match, return the starting point of the match.
                if (matches)
                {
                    resultRichTextBox.Text = trace;
                    return textPos - targetLen + 1;
                }
            }

            // If we get here, there is no match.
            resultRichTextBox.Text = trace;
            return -1;
        }

        // Find the first instance of the target string.
        // Calculate the shift on the fly.
        private int FindTarget1(string text, string target)
        {
            string trace = text + Environment.NewLine;

            int targetLen = target.Length;
            int textLen = text.Length;

            // Examine the string.
            for (int textPos = targetLen - 1; textPos < textLen; )
            {
                if (textPos > targetLen) trace += new string(' ', textPos - targetLen + 1);
                trace += target + Environment.NewLine;
                Console.WriteLine(trace);//@
                Console.WriteLine();//@

                bool matches = true;
                for (int j = 0; j < targetLen; j++)
                {
                    int targetEnd = targetLen - 1;

                    // See if the corresponding characters match.
                    if (text[textPos - j] != target[targetEnd - j])
                    {
                        // They don't match.
                        matches = false;

                        // Get the character that didn't match.
                        char badChar = text[textPos - j];

                        // Find the next occurrance of badChar to the left in the target.
                        // See how many characters we need to shift.
                        int offset = targetLen;
                        for (int shift = 1; shift < targetLen - j; shift++)
                            if (target[targetEnd - j - shift] == badChar)
                            {
                                offset = shift;
                                break;
                            }

                        // Shift.
                        textPos += offset;
                        break;
                    }
                }

                // If we had a match, return the starting point of the match.
                if (matches)
                {
                    resultRichTextBox.Text = trace;
                    return textPos - targetLen + 1;
                }
            }

            // If we get here, there is no match.
            resultRichTextBox.Text = trace;
            return -1;
        }
    }
}
