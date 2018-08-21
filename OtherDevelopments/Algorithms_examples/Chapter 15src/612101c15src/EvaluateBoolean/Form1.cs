using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvaluateBoolean
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
            try
            {
                bool result = EvaluateExpression(expressionTextBox.Text);
                resultTextBox.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Recursively evaluate the expression.
        private bool EvaluateExpression(string expression)
        {
            // Find the operator.
            int count = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(') count++;
                else if (expression[i] == ')')
                {
                    count--;
                    if (count < 0) throw new InvalidExpressionException(
                        "Unexpected ) at position " + i + " in " + expression);
                }
                else if (count == 0)
                {
                    // Look for an operator.
                    char ch = expression[i];
                    if ((ch == '&') || (ch == '|'))
                    {
                        // Get the operands.
                        string operand1 = expression.Substring(0, i);
                        string operand2 = expression.Substring(i + 1);

                        // Evaluate the operands.
                        bool value1 = EvaluateExpression(operand1);
                        bool value2 = EvaluateExpression(operand2);

                        // Combine the operands and return the result.
                        bool result;
                        switch (ch)
                        {
                            case '&': result = value1 && value2; break;
                            case '|': result = value1 || value2; break;
                            default:
                                throw new InvalidExpressionException("Unknown operator " + ch);
                        }
                        Console.WriteLine(expression + " = " + result);
                        return result;
                    }
                }
            }

            // If we get here, we did not find an operator.
            // See if this is (expression).
            if ((expression[0] == '(') &&
                (MatchingParenIndex(expression, 0) == expression.Length - 1))
            {
                // Remove the parentheses.
                return EvaluateExpression(expression.Substring(1, expression.Length - 2));
            }

            // See if this is -expression.
            if (expression[0] == '-')
            {
                // Remove the parentheses.
                return !EvaluateExpression(expression.Substring(1));
            }

            // This must be a literal value.
            try
            {
                if (expression.ToUpper() == "T") return true;
                if (expression.ToUpper() == "F") return false;
                throw new InvalidExpressionException(
                    "Unable to parse literal " + expression);
            }
            catch
            {
                throw new InvalidExpressionException("Error parsing literal value " + expression);
            }
        }

        // Return the index of the parenthesis matching the one in the indicated position.
        private int MatchingParenIndex(string expression, int openParenIndex)
        {
            int count = 1;
            for (int i = openParenIndex + 1; i < expression.Length; i++)
            {
                if (expression[i] == '(') count++;
                else if (expression[i] == ')') count--;
                if (count == 0) return i;
                if (count < 0) return -1;
            }
            return -1;
        }
    }
}
