using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expressions
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

            ExpressionNode root, leftOper, rightOper;

            // (15 / 3) + (24 / 6)
            root = new ExpressionNode(Operators.Plus);
            leftOper = new ExpressionNode(Operators.Divide);
            leftOper.LeftOperand = new ExpressionNode("15");
            leftOper.RightOperand = new ExpressionNode("3");
            rightOper = new ExpressionNode(Operators.Divide);
            rightOper.LeftOperand = new ExpressionNode("24");
            rightOper.RightOperand = new ExpressionNode("6");
            root.LeftOperand = leftOper;
            root.RightOperand = rightOper;
            results += "(15 / 3) + (24 / 6) = " + root.Evaluate() + Environment.NewLine;
            results += "Check: " + ((15f / 3) + (24f / 6)).ToString() +
                Environment.NewLine + Environment.NewLine;

            // 8 * 12 - 14 * 32
            root = new ExpressionNode(Operators.Minus);
            leftOper = new ExpressionNode(Operators.Times);
            leftOper.LeftOperand = new ExpressionNode("8");
            leftOper.RightOperand = new ExpressionNode("12");
            rightOper = new ExpressionNode(Operators.Times);
            rightOper.LeftOperand = new ExpressionNode("14");
            rightOper.RightOperand = new ExpressionNode("32");
            root.LeftOperand = leftOper;
            root.RightOperand = rightOper;
            results += "8 * 12 - 14 * 32 = " + root.Evaluate() + Environment.NewLine;
            results += "Check: " + (8 * 12 - 14 * 32).ToString() +
                Environment.NewLine + Environment.NewLine;

            // 1 / 2 + 1 / 4 + 1 / 20
            root = new ExpressionNode(Operators.Plus);
            // 1 / 2
            leftOper = new ExpressionNode(Operators.Divide);
            leftOper.LeftOperand = new ExpressionNode("1");
            leftOper.RightOperand = new ExpressionNode("2");
            root.LeftOperand = leftOper;

            // 1 / 4
            root.RightOperand = new ExpressionNode(Operators.Plus);
            leftOper = new ExpressionNode(Operators.Divide);
            leftOper.LeftOperand = new ExpressionNode("1");
            leftOper.RightOperand = new ExpressionNode("4");
            root.RightOperand.LeftOperand = leftOper;

            // 1 / 20
            rightOper = new ExpressionNode(Operators.Divide);
            rightOper.LeftOperand = new ExpressionNode("1");
            rightOper.RightOperand = new ExpressionNode("20");
            root.RightOperand.RightOperand = rightOper;
            results += "1 / 2 + 1 / 4 + 1 / 20 = " + root.Evaluate() + Environment.NewLine;
            results += "Check: " + (1f / 2 + 1f / 4 + 1f / 20).ToString() +
                Environment.NewLine + Environment.NewLine;

            resultsTextBox.Text = results;
            resultsTextBox.Select(0, 0);
        }
    }
}
