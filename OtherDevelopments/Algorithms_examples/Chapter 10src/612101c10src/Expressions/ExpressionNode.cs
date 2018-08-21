using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressions
{
    public enum Operators
    {
        Literal,
        Plus,
        Minus,
        Times,
        Divide,
        Negate,
    }

    public class ExpressionNode
    {
        public Operators Operator;
        public ExpressionNode LeftOperand, RightOperand;
        public string LiteralText;

        public ExpressionNode(Operators op)
        {
            Operator = op;
        }
        public ExpressionNode(string text)
        {
            Operator = Operators.Literal;
            LiteralText = text;
        }

        // Evaluate the expression.
        public float Evaluate()
        {
            switch (Operator)
            {
                case Operators.Literal:
                    return float.Parse(LiteralText);
                case Operators.Plus:
                    return LeftOperand.Evaluate() + RightOperand.Evaluate();
                case Operators.Minus:
                    return LeftOperand.Evaluate() - RightOperand.Evaluate();
                case Operators.Times:
                    return LeftOperand.Evaluate() * RightOperand.Evaluate();
                case Operators.Divide:
                    return LeftOperand.Evaluate() / RightOperand.Evaluate();
                case Operators.Negate:
                    return -LeftOperand.Evaluate();
            }

            throw new ArithmeticException("Unknown operator " + Operator.ToString());
        }
    }
}
