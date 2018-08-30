using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentificationNumbers
{
    class Program
    {
        // Инициализация весов сети
        static int[] weights = new int[15]; 
        // Порог функции активации
        static int bias = 7; 
        static Random random = new Random();
 
        static void Main()
        {
            // Цифры (Обучающая выборка)
            char[] num0 = "111101101101111".ToCharArray();
            char[] num1 = "001001001001001".ToCharArray();
            char[] num2 = "111001111100111".ToCharArray();
            char[] num3 = "111001111001111".ToCharArray();
            char[] num4 = "101101111001001".ToCharArray();
            char[] num5 = "111100111001111".ToCharArray();
            char[] num6 = "111100111101111".ToCharArray();
            char[] num7 = "111001001001001".ToCharArray();
            char[] num8 = "111101111101111".ToCharArray();
            char[] num9 = "111101111001111".ToCharArray();
 
            // Список всех вышеуказанных цифр
            char[][] nums = { num0, num1, num2, num3, num4, num5, num6, num7, num8, num9 };
 
            // Виды цифры 5 (Тестовая выборка)
            char[] num51 = "111100111000111".ToCharArray();
            char[] num52 = "111100010001111".ToCharArray();
            char[] num53 = "111100011001111".ToCharArray();
            char[] num54 = "110100111001111".ToCharArray();
            char[] num55 = "110100111001011".ToCharArray();
            char[] num56 = "111100101001111".ToCharArray();
 
            // Тренировка сети
            for (int i = 0; i < 100000; i++)
            {
                // Генерируем случайное число от 0 до 9
                int option = random.Next(10);
 
                // Если получилось НЕ число 5
                if (option != 5)
                {
                    // Если сеть выдала True/Да/1, то наказываем ее
                    if (Proceed(nums[option])) Decrease(nums[option]);
                }
                // Если получилось число 5
                else
                {
                    // Если сеть выдала False/Нет/0, то показываем, что эта цифра - то, что нам нужно
                    if (!Proceed(num5)) Increase(num5);
                }
            }
 
            // Вывод значений весов
            Console.WriteLine(string.Join(", ", weights));
 
            // Прогон по обучающей выборке
            Console.WriteLine("0 это 5? " + Proceed(num0));
            Console.WriteLine("1 это 5? " + Proceed(num1));
            Console.WriteLine("2 это 5? " + Proceed(num2));
            Console.WriteLine("3 это 5? " + Proceed(num3));
            Console.WriteLine("4 это 5? " + Proceed(num4));
            Console.WriteLine("6 это 5? " + Proceed(num6));
            Console.WriteLine("7 это 5? " + Proceed(num7));
            Console.WriteLine("8 это 5? " + Proceed(num8));
            Console.WriteLine("9 это 5? " + Proceed(num9), "\r\n");
 
            // Прогон по тестовой выборке
            Console.WriteLine("Узнал 5? " + Proceed(num5));
            Console.WriteLine("Узнал 5 - 1? " + Proceed(num51));
            Console.WriteLine("Узнал 5 - 2? " + Proceed(num52));
            Console.WriteLine("Узнал 5 - 3? " + Proceed(num53));
            Console.WriteLine("Узнал 5 - 4? " + Proceed(num54));
            Console.WriteLine("Узнал 5 - 5? " + Proceed(num55));
            Console.WriteLine("Узнал 5 - 6? " + Proceed(num56));
 
            Console.WriteLine("\r\nНажмите любую кнопку для выхода");
            Console.ReadKey();
        }
 
        // Является ли данное число 5
        static bool Proceed(char[] number)
        {
            // Рассчитываем взвешенную сумму
            int net = 0;
            for (int i = 0; i < 15; i++)
            {
                net += int.Parse(number[i].ToString()) * weights[i];
            }
 
            // Превышен ли порог? (Да - сеть думает, что это 5. Нет - сеть думает, что это другая цифра)
            return net >= bias;
        }
 
        // Уменьшение значений весов, если сеть ошиблась и выдала 1
        static void Decrease(char[] number)
        {
            for (int i = 0; i < 15; i++)
            {
                // Возбужденный ли вход
                if (int.Parse(number[i].ToString()) == 1)
                {
                    // Уменьшаем связанный с ним вес на единицу
                    weights[i]--;
                }
            }
        }    
 
        // Увеличение значений весов, если сеть ошиблась и выдала 0
        static void Increase(char[] number)
        {
            for (int i = 0; i < 15; i++)
            {
                // Возбужденный ли вход
                if (int.Parse(number[i].ToString()) == 1)
                {
                    // Увеличиваем связанный с ним вес на единицу
                    weights[i]++;
                }
            }
        }
    }
}
