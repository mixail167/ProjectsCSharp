using System;

namespace RPN
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Выражение: ");
            string input = Console.ReadLine();
            RPN rpn = new RPN(input);
            string output = string.Join(string.Empty, rpn.ConvertToPostfixNotation());
            decimal result = rpn.Result();
            Console.WriteLine("ОПЗ: " + output);
            Console.WriteLine("Результат: " + result);
            Console.ReadKey();
        }
    }
}
