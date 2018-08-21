using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressions2
{
    public enum Operators
    {
        Literal,
        Plus,
        Minus,
        Times,
        Divide,
        Negate,
        SquareRoot,
        Factorial,
        Sine,
        Squared,
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
                case Operators.SquareRoot:
                    return (float)Math.Sqrt(LeftOperand.Evaluate());
                case Operators.Factorial:
                    return Factorial(LeftOperand.Evaluate());
                case Operators.Sine:
                    return (float)Math.Sin(Math.PI / 180.0 * LeftOperand.Evaluate());
                case Operators.Squared:
                    float left = LeftOperand.Evaluate();
                    return left * left;
            }

            throw new ArithmeticException("Unknown operator " + Operator.ToString());
        }

        // Return n!
        public static float Factorial(float n)
        {
            float result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
