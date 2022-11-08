using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SLAU
{
    abstract class SimpleIterations
    {
        public abstract void calculateMatrix();
    }

    /// <summary>
    /// Class Jacobi 
    /// Класс отвечает за работу метода Якоби
    /// </summary>
    class Jacobi : SimpleIterations
    {
        // Матрица ответов
        private double[] resultMatrix;
        public double[] ResultMatrix
        {
            get
            {
                if (resultMatrix != null)
                    return resultMatrix;
                else
                {
                    return new double[3] { 0, 0, 0 };
                }
            }
        }

        // Основная матрица и свободные члены.
        private double[,] matrix;
        private double[] addtional;

        // точность (кол-во итераций)
        private double accuracy;
        // избегаем ошибок с итерациями.
        public double Accuracy
        {
            get
            {
                return accuracy;
            }
            set
            {
                if (value <= 0.0)
                    accuracy = 0.1;
                else
                    accuracy = value;
            }
        }

        // Конструктор. Получает значения при создании.
        public Jacobi(double[,] Matrix, double[] FreeElements, double Accuracy)
        {
            this.matrix = Matrix;
            this.addtional = FreeElements;
            this.Accuracy = Accuracy;

        }

        // Сам метод рассчета.
        public override void calculateMatrix()
        {
            // общий вид:
            // [x1]   [ b1/a11 ]   / 0 x x \ 
            // [x2] = [ b2/a22 ] - | x 0 x |
            // [x3]   [ b3/a33 ]   \ x x 0 /
            // где x - делится на диагональый элемент первоначальной матрицы.
            // где b - эелементы из свободных членов
            // где а - элементы из матрицы

            // матрица коеффициентов + столбец свободных членов.
            double[,] a = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1) - 1; j++)
                    a[i, j] = matrix[i, j];

            for (int i = 0; i < a.GetLength(0); i++)
                a[i, a.GetLength(1) - 1] = addtional[i];

            //---------------
            // Метод Якоби.
            //---------------

            // Введем вектор значений неизвестных на предыдущей итерации,
            // размер которого равен числу строк в матрице, т.е. size,
            // причем согласно методу изначально заполняем его нулями

            double[] previousValues = new double[a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                previousValues[i] = 0.0;
            }

            // Будем выполнять итерационный процесс до тех пор,
            // пока не будет достигнута необходимая точность
            while (true)
            {
                // Введем вектор значений неизвестных на текущем шаге
                double[] currentValues = new double[a.GetLength(0)];

                // Посчитаем значения неизвестных на текущей итерации
                // в соответствии с теоретическими формулами
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    // Инициализируем i-ую неизвестную значением
                    // свободного члена i-ой строки матрицы
                    currentValues[i] = a[i, a.GetLength(0)];

                    // Вычитаем сумму по всем отличным от i-ой неизвестным
                    for (int j = 0; j < a.GetLength(0); j++)
                    {
                        if (i != j)
                        {
                            currentValues[i] -= a[i, j] * previousValues[j];
                        }
                    }

                    // Делим на коэффициент при i-ой неизвестной
                    currentValues[i] /= a[i, i];
                }

                // Посчитаем текущую погрешность относительно предыдущей итерации
                double differency = 0.0;

                for (int i = 0; i < a.GetLength(0); i++)
                    differency += Math.Abs(currentValues[i] - previousValues[i]);

                // Если необходимая точность достигнута, то завершаем процесс
                if (differency < accuracy)
                    break;

                // Переходим к следующей итерации, так
                // что текущие значения неизвестных
                // становятся значениями на предыдущей итерации
                previousValues = currentValues;
            }

            resultMatrix = previousValues;
        }

    }

    /// <summary>
    /// Класс Отвечает за работу метода Зейделя.
    /// Class Seidel
    /// </summary>
    class Seidel : SimpleIterations
    {
        // Матрица ответов
        private double[] resultMatrix;
        public double[] ResultMatrix
        {
            get
            {
                if (resultMatrix != null)
                    return resultMatrix;
                else
                {
                    return new double[3] { 0, 0, 0 };
                }
            }
        }
        // private int t;

        // Основная матрица и свободные члены.
        private double[,] matrix;
        private double[] addtional;

        // точность (кол-во итераций)
        private double accuracy;
        // избегаем ошибок с итерациями.
        public double Accuracy
        {
            get
            {
                return accuracy;
            }
            set
            {
                if (value <= 0.0)
                    accuracy = 0.1;
                else
                    accuracy = value;
            }
        }

        // Конструктор. Получает значения при создании.
        public Seidel(double[,] Matrix, double[] FreeElements, double Accuracy)
        {
            this.matrix = Matrix;
            this.addtional = FreeElements;
            this.Accuracy = Accuracy;

        }

        // Сам метод рассчета.
        public override void calculateMatrix()
        {

            // общий вид:
            // [x1]   [ b1/a11 ]   / 0 x x \ 
            // [x2] = [ b2/a22 ] - | x 0 x |
            // [x3]   [ b3/a33 ]   \ x x 0 /
            // где x - делится на диагональый элемент первоначальной матрицы.
            // где b - эелементы из свободных членов
            // где а - элементы из матрицы

            // матрица коеффициентов + столбец свободных членов.
            double[,] a = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1) - 1; j++)
                    a[i, j] = matrix[i, j];

            for (int i = 0; i < a.GetLength(0); i++)
                a[i, a.GetLength(1) - 1] = addtional[i];

            //---------------
            // Метод Зейделя.
            //---------------

            // Введем вектор значений неизвестных на предыдущей итерации,
            // размер которого равен числу строк в матрице, т.е. size,
            // причем согласно методу изначально заполняем его нулями
            double[] previousValues = new double[matrix.GetLength(0)];
            for (int i = 0; i < previousValues.GetLength(0); i++)
            {
                previousValues[i] = 0.0;
            }

            // Будем выполнять итерационный процесс до тех пор,
            // пока не будет достигнута необходимая точность
            while (true)
            {
                // Введем вектор значений неизвестных на текущем шаге
                double[] currentValues = new double[a.GetLength(0)];

                // Посчитаем значения неизвестных на текущей итерации
                // в соответствии с теоретическими формулами
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    // Инициализируем i-ую неизвестную значением
                    // свободного члена i-ой строки матрицы
                    currentValues[i] = a[i, a.GetLength(0)];

                    // Вычитаем сумму по всем отличным от i-ой неизвестным
                    for (int j = 0; j < a.GetLength(0); j++)
                    {
                        // При j < i можем использовать уже посчитанные
                        // на этой итерации значения неизвестных
                        if (j < i)
                        {
                            currentValues[i] -= a[i, j] * currentValues[j];
                        }

                        // При j > i используем значения с прошлой итерации
                        if (j > i)
                        {
                            currentValues[i] -= a[i, j] * previousValues[j];
                        }
                    }

                    // Делим на коэффициент при i-ой неизвестной
                    currentValues[i] /= a[i, i];
                }

                // Посчитаем текущую погрешность относительно предыдущей итерации
                double differency = 0.0;

                for (int i = 0; i < a.GetLength(0); i++)
                    differency += Math.Abs(currentValues[i] - previousValues[i]);

                // Если необходимая точность достигнута, то завершаем процесс
                if (differency < accuracy)
                    break;

                // Переходим к следующей итерации, так
                // что текущие значения неизвестных
                // становятся значениями на предыдущей итерации

                previousValues = currentValues;
            }

            // результат присваиваем матрице результатов.
            resultMatrix = previousValues;
        }
    }




        class Program
    {
        //------------------------------------------
        // setVal - method of array values enter overloads 
        // setVal - Перегрузки методов ввода значений для
        // матрицы коеффициентов и свободных членов.
        //------------------------------------------
        static double[,] setVal(double[,] x)
        {
            Console.WriteLine("\n Matrix cofficients:");
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write("Enter value of {0}{1}: ", i, j);
                    x[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }

            return x;
        }
        static double[] setVal(double[] x)
        {
            Console.WriteLine("\n Addtional matrix values:");

            for (int i = 0; i < x.Length; i++)
            {
                Console.Write("Enter value of {0}: ", i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }

            return x;
        }
        //------------------------------------------
        // showMatrix - method overloads showing results 
        // showMatrix - перегрузки разнотипных выводов
        //------------------------------------------
        static void showMatrix(double[,] x)
        {
            Console.WriteLine("\n Result:");

            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write(" {0} ", x[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void showMatrix(double[] x)
        {
            Console.WriteLine("\n Result:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine(" {0} ", x[i]);
            }
        }


        static void Main(string[] args)
        {
            // Matrix coefficients
            // Матрица коеффциентов СЛАУ
            double[,] matrix = new double[3, 3] {
            { 4, 0.24, -0.08},
            { 0.09, 3, -0.15},
            { 0.04, -0.08, 4}
            };


            // matrix of free coefficients
            // матрица свободных членов
            double[] additional = new double[3] {
                8,
                9,
                20
            };

            // Enter values by user
            // Ввод значений вручную.
            // matrix = setVal(matrix);
            // additional = setVal(additional);

            // Create and init.
            // Объявляем и инициализимруем классы.
            Seidel i = new Seidel(matrix, additional, 0.0001);
            Jacobi j = new Jacobi(matrix, additional, 0.0001);

            // set method args of ThreadStart delegate 
            // Передаем методы потоку через делегат ThreadStart
            Thread Z = new Thread(new ThreadStart(i.calculateMatrix));
            Thread Y = new Thread(new ThreadStart(j.calculateMatrix));

            // Start threads
            // Запускаем потоки.
            Z.Start();
            Y.Start();

            // wait for endings
            // Ожидаем завершения.
            Z.Join();
            Y.Join();

            // Show results of calculations 
            // Выводим на экран.
            Console.WriteLine("\n Seidel method:");
            showMatrix(i.ResultMatrix);
            Console.WriteLine("\n Jakobi method:");
            showMatrix(j.ResultMatrix);

            Console.ReadKey();
        }
    }
}
