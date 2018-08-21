using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace GraphExpression
{
    public class ExpressionNode
    {
        public enum OperatorTypes
        {
            Literal,
            Variable,
            Plus,
            Minus,
            Times,
            Divide,
            Sine,
        }
        public OperatorTypes OperatorType;
        public ExpressionNode LeftChild, RightChild;
        public float LiteralValue;

        // The constructor parses the expression and initializes the object.
        public ExpressionNode(string expression)
        {
            Parse(expression);
        }
        private void Parse(string expression)
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
                    if ((ch == '+') || (ch == '-') || (ch == '*') || (ch == '/'))
                    {
                        // Get the operands.
                        string operand1 = expression.Substring(0, i);
                        string operand2 = expression.Substring(i + 1);

                        // Parse the operands.
                        LeftChild = new ExpressionNode(operand1);
                        RightChild = new ExpressionNode(operand2);

                        // Remember the operator.
                        switch (ch)
                        {
                            case '+': OperatorType = OperatorTypes.Plus; break;
                            case '-': OperatorType = OperatorTypes.Minus; break;
                            case '*': OperatorType = OperatorTypes.Times; break;
                            case '/': OperatorType = OperatorTypes.Divide; break;
                            default:
                                throw new InvalidExpressionException("Unknown operator " + ch);
                        }

                        // We're done initializing this object.
                        return;
                    }
                }
            }

            // If we get here, we did not find an operator.
            // This must be:
            //      (expression)
            //      Sine(expression)
            //      X
            //      A literal.
            if ((expression[0] == '(') &&
                (MatchingParenIndex(expression, 0) == expression.Length - 1))
            {
                // This is (expression).
                // Remove the parentheses.
                Parse(expression.Substring(1, expression.Length - 2));
            }
            else if (expression.ToUpper() == "X")
            {
                // This is X.
                OperatorType = OperatorTypes.Variable;
            }
            else if ((expression.ToUpper().StartsWith("SINE(")) &&
                (MatchingParenIndex(expression, 4) == expression.Length - 1))
            {
                // This is Sine(expression).
                OperatorType = OperatorTypes.Sine;

                expression = expression.Substring(5, expression.Length - 6);
                LeftChild = new ExpressionNode(expression);
            }
            else
            {
                // This is a literal.
                try
                {
                    OperatorType = OperatorTypes.Literal;
                    LiteralValue = float.Parse(expression);
                }
                catch
                {
                    throw new InvalidExpressionException("Error parsing literal value " + expression);
                }
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

        // Return the value of the expression for the indicated value of x.
        public float Evaluate(float x)
        {
            switch (OperatorType)
            {
                case OperatorTypes.Literal:
                    return LiteralValue;
                case OperatorTypes.Variable:
                    return x;
                case OperatorTypes.Plus:
                    return LeftChild.Evaluate(x) + RightChild.Evaluate(x);
                case OperatorTypes.Minus:
                    return LeftChild.Evaluate(x) - RightChild.Evaluate(x);
                case OperatorTypes.Times:
                    return LeftChild.Evaluate(x) * RightChild.Evaluate(x);
                case OperatorTypes.Divide:
                    return LeftChild.Evaluate(x) / RightChild.Evaluate(x);
                case OperatorTypes.Sine:
                    return (float)Math.Sin(LeftChild.Evaluate(x));
                default:
                    throw new InvalidOperationException(
                        "Unknown operator in Evaluate method");
            }
        }
    }
}
