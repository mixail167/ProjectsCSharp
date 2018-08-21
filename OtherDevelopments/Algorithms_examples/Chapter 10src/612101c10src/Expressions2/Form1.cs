using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expressions2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Make and evaluate some expressions.
        private void Form1_Load(object sender, EventArgs e)
        {
            string results = "";

            ExpressionNode root;

            // Sqrt((36 * 2) / (9 * 32))
            root = new ExpressionNode(Operators.SquareRoot);
            root.LeftOperand = new ExpressionNode(Operators.Divide);
            root.LeftOperand.LeftOperand = new ExpressionNode(Operators.Times);
            root.LeftOperand.LeftOperand.LeftOperand = new ExpressionNode("36");
            root.LeftOperand.LeftOperand.RightOperand = new ExpressionNode("2");
            root.LeftOperand.RightOperand = new ExpressionNode(Operators.Times);
            root.LeftOperand.RightOperand.LeftOperand = new ExpressionNode("9");
            root.LeftOperand.RightOperand.RightOperand = new ExpressionNode("32");
            results += "Sqrt((36 * 2) / (9 * 32)) = " + root.Evaluate() + Environment.NewLine;
            results += "Check: " + (Math.Sqrt((36f * 2) / (9 * 32))).ToString() +
                Environment.NewLine + Environment.NewLine;

            // 5! / ((5 - 3)! * 3!)
            root = new ExpressionNode(Operators.Divide);
            // 5!
            root.LeftOperand = new ExpressionNode(Operators.Factorial);
            root.LeftOperand.LeftOperand = new ExpressionNode("5");
            // (5 - 3)! * 3!
            root.RightOperand = new ExpressionNode(Operators.Times);
            root.RightOperand.LeftOperand = new ExpressionNode(Operators.Factorial);
            root.RightOperand.LeftOperand.LeftOperand = new ExpressionNode(Operators.Minus);
            root.RightOperand.LeftOperand.LeftOperand.LeftOperand = new ExpressionNode("5");
            root.RightOperand.LeftOperand.LeftOperand.RightOperand = new ExpressionNode("3");
            // 3!
            root.RightOperand.RightOperand = new ExpressionNode(Operators.Factorial);
            root.RightOperand.RightOperand.LeftOperand = new ExpressionNode("3");
            results += "5! / (5 - 3)! / 3! = " + root.Evaluate() + Environment.NewLine;
            float result = ExpressionNode.Factorial(5) /
                (ExpressionNode.Factorial(5 - 3) * ExpressionNode.Factorial(3));
            results += "Check: " + (result).ToString() +
                Environment.NewLine + Environment.NewLine;

            // Sine(45)^2
            root = new ExpressionNode(Operators.Squared);
            root.LeftOperand = new ExpressionNode(Operators.Sine);
            root.LeftOperand.LeftOperand = new ExpressionNode("45");
            results += "Sine(45)^2 = " + root.Evaluate() + Environment.NewLine;
            results += "Check: " + (Math.Pow(Math.Sin(45 * Math.PI / 180), 2)).ToString() +
                Environment.NewLine + Environment.NewLine;

            resultsTextBox.Text = results;
            resultsTextBox.Select(0, 0);
        }
    }
}
