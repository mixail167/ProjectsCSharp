using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace ParenthesisMatching
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Evaluate the expression.
        private void evaluateButton_Click(object sender, EventArgs e)
        {
            // Make sure the expression is well-formed.
            if (IsWellFormed(expressionTextBox.Text))
                resultTextBox.Text = "Parentheses match";
            else resultTextBox.Text = "Parentheses don't match";
        }

        // Verify that the expression's parenthesis are properly nested.
        private bool IsWellFormed(string expression)
        {
            int count = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(') count++;
                else if (expression[i] == ')') count--;

                if (count < 0) return false;
            }
            if (count == 0) return true;
            return false;
        }
    }
}
