using Jace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
    class Program
    {
        static CalculationEngine engine;
        static Dictionary<string, Func<double, double>> functions;

        private static string InputEquation()
        {
            string equation = string.Empty;
            do
            {
                try
                {
                    Console.Write("Уравнение y = ");
                    equation = Console.ReadLine();
                }
                catch (Exception)
                {

                }
            } while (equation == string.Empty);
            return equation;
        }

        private static int InputCountParameters()
        {
            int count = 0;
            do
            {
                try
                {
                    Console.Write("Введите количество неизвестных (1-9):");
                    count = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                    Console.WriteLine();
                }
                catch (Exception)
                {

                }
            } while (count == 0);
            return count;
        }

        private static Dictionary<string, double> InputParametersValues(int count)
        {
            Dictionary<string, double> parameters = new Dictionary<string, double>();
            int i = 1;
            while (i <= count)
            {
                try
                {
                    Console.Write(string.Format("Введите имя параметра {0} (кроме y и e): ", i));
                    ConsoleKeyInfo key = Console.ReadKey();
                    string parameter = key.KeyChar.ToString();
                    Console.WriteLine();
                    if (Char.IsLetter(key.KeyChar) && parameter != "y" && parameter != "e" && !parameters.Keys.Contains(parameter))
                    {
                        Console.Write(string.Format("Введите значение параметра {0}: ", i));
                        double value = Convert.ToDouble(Console.ReadLine());
                        parameters.Add(parameter, value);
                        i++;
                    }
                }
                catch (Exception)
                {

                }
            }
            return parameters;
        }

        static void Main(string[] args)
        {
            functions = new Dictionary<string, Func<double, double>>();
            functions.Add("abs", x => Math.Abs(x));
            functions.Add("ln", x => Math.Log(x));
            functions.Add("cos", x => Math.Cos(x));
            functions.Add("sin", x => Math.Sin(x));
            functions.Add("tan", x => Math.Tan(x));
            functions.Add("ctan", x => 1.0 / Math.Tan(x));
            functions.Add("sinh", x => Math.Sinh(x));
            functions.Add("cosh", x => Math.Cosh(x));
            functions.Add("tanh", x => Math.Tanh(x));
            functions.Add("ctanh", x => 1.0 / Math.Tanh(x));
            functions.Add("acos", x => Math.Acos(x));
            functions.Add("asin", x => Math.Asin(x));
            functions.Add("atan", x => Math.Atan(x));
            functions.Add("actan", x => 1.0 / Math.Atan(x));
            functions.Add("sech", x => 1.0 / Math.Cosh(x));
            functions.Add("csch", x => 1.0 / Math.Sinh(x));
            functions.Add("arsh", x => Math.Log(x + Math.Sqrt(x * x + 1)));
            functions.Add("arch", x => (x >= 1) ? Math.Log(x + Math.Sqrt(x * x - 1)) : 0);
            functions.Add("artanh", x => 0.5 * Math.Log((1 + x) / (1 - x)));
            functions.Add("arctanh", x => 0.5 * Math.Log((1 + x) / (x - 1)));
            engine = new CalculationEngine();
            foreach (var item in functions)
            {
                try
                {
                    engine.AddFunction(item.Key, item.Value);
                }
                catch (Exception)
                {

                }
            }
            ConsoleKeyInfo key;
            do
            {
                Console.Write("Ввести значения неизвестных? (0 - нет) ");
                key = Console.ReadKey();
                Console.WriteLine();
                Dictionary<string, double> parameters;
                if (key.Key != ConsoleKey.D0 && key.Key != ConsoleKey.NumPad0)
                {
                    int count = InputCountParameters();
                    parameters = InputParametersValues(count);
                }
                else
                {
                    parameters = new Dictionary<string, double>();
                }
                do
                {
                    try
                    {
                        string equation = InputEquation();
                        Console.WriteLine(string.Format("Ответ: {0} = {1:f3}", equation, engine.Calculate(equation, parameters)));
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Некорректное уравнение!");
                    }
                } while (true);
                Console.Write("Повторить ввод? (0 - нет) ");
                key = Console.ReadKey();
                Console.WriteLine();
            } while (key.KeyChar.ToString() != "0");
        }
    }
}