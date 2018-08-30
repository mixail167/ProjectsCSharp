using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation
{
    class Program
    {
        private static Random random = new Random();
        // Коэффициент при x
        private static double k = random.NextDouble() * (5 - (-5)) + (-5);
        // Свободный член уравнения прямой
        private static double c = random.NextDouble() * (5 - (-5)) + (-5);
        // Набор точек X:Y
        private static Dictionary<double, double> data = new Dictionary<double, double>
        {
            { 1, 2 },
            { 2, 4.2 },
            { 2.5, 5 },
            { 3.8, 7.9 },
            { 4, 9 },
            { 6, 10.2 },
            { 6.6, 13 },
            { 7.2, 15.3 },
            { 8, 17.1 },
            { 8.5, 19.5 }
        };

        // Скорость обучения
        private static double rate = 0.0001;

        private static void Main(string[] args)
        {
            // Вывод данных начальной прямой
            Console.WriteLine("Начальная прямая: {0} * X + {1}", k, c);

            // Тренировка сети
            for (int i = 0; i < 9999999; i++)
            {
                // Получить случайную X координату точки
                double x = data.ElementAt(random.Next(data.Count)).Key;

                // Получить соответствующую Y координату точки
                double true_result = data[x];

                // Получить ответ сети
                double output = Function(x);

                // Считаем ошибку сети
                double delta = true_result - output;

                // Меняем вес при x в соответствии с дельта-правилом
                k += delta * rate * x;

                // Меняем вес при постоянном входе в соответствии с дельта-правилом
                c += delta * rate;
            }

            // Вывод данных готовой прямой
            Console.WriteLine("Готовая прямая: {0} * X + {1}", k, c);

            Console.WriteLine("\r\nНажмите любую кнопку для выхода");
            Console.ReadKey();
        }

        private static double Function(double x)
        {
            return x * k + c;
        }
    }
}
