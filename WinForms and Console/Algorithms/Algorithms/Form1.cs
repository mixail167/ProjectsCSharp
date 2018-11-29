using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Drawing = System.Drawing;
using System.Windows;
using System.Diagnostics;
using System.Text;

namespace Algorithms
{
    public partial class Form1 : Form
    {
        #region Vars

        private int oldValue;
        private int oldValueCount;
        private Graph graph;
        private DataGridCell cell;
        private bool semaphore;
        private bool semaphore2;
        private int[] path;
        private delegate T[] Sort<T>(T[] massiv, bool revers) where T : IComparable<T>;
        private List<Drawing.Point> points;
        private bool drawPolygon;
        private Drawing.Bitmap bitmap;
        private Drawing.Graphics graphics;
        private Drawing.Graphics graphics2;
        private Drawing.Graphics graphics3;
        private Cell[,] map;
        private Cell[,] map1;
        private Cell startPosition;
        private Cell finishPosition;
        private Romb[,] rombMesh;
        private List<Sprait> objects;
        #endregion

        public Form1()
        {
            InitializeComponent();
            oldValueCount = 3;
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowCount = 2;
            dataGridView1.Rows[1].ReadOnly = true;
            dataGridView2.ColumnCount = oldValueCount + 1;
            dataGridView2.RowCount = oldValueCount + 1;
            dataGridView2.Rows[0].ReadOnly = true;
            dataGridView2.Columns[0].ReadOnly = true;
            graph = new Graph("graph");
            AddCellsAndNodes(0, dataGridView2.ColumnCount);
            cell = new DataGridCell(0, 0);
            semaphore = false;
            semaphore2 = false;
            points = new List<Drawing.Point>();
            drawPolygon = false;
            bitmap = new Drawing.Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Drawing.Graphics.FromImage(bitmap);
            rombMesh = Romb.CreateRombMesh(0, 200, 20, 10, 10, 2.5);
            objects = new List<Sprait>();
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Генератор лабиринта
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        /// <returns>Карта лабиринта</returns>
        private Cell[,] GenerateLabirint(int width, int height)
        {
            if (width % 2 == 0)
            {
                width++;
            }
            if (height % 2 == 0)
            {
                height++;
            }
            Cell[,] map = new Cell[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                        map[i, j] = new Cell(i, j, -1);
                    else
                        map[i, j] = new Cell(i, j, -4);
                }
            }

            Random random = new Random();
            Stack<Cell> path = new Stack<Cell>();
            map[1, 1].Visited = true;
            path.Push(map[1, 1]);
            while (path.Count > 0)
            {
                Cell cell = path.Peek();
                List<Cell> nextStep = new List<Cell>();
                if (cell.X > 1 && !map[cell.X - 2, cell.Y].Visited)
                    nextStep.Add(map[cell.X - 2, cell.Y]);
                if (cell.X < width - 2 && !map[cell.X + 2, cell.Y].Visited)
                    nextStep.Add(map[cell.X + 2, cell.Y]);
                if (cell.Y > 1 && !map[cell.X, cell.Y - 2].Visited)
                    nextStep.Add(map[cell.X, cell.Y - 2]);
                if (cell.Y < height - 2 && !map[cell.X, cell.Y + 2].Visited)
                    nextStep.Add(map[cell.X, cell.Y + 2]);
                if (nextStep.Count() > 0)
                {
                    Cell next = nextStep[random.Next(nextStep.Count())];
                    if (next.X != cell.X)
                    {
                        if (cell.X > next.X)
                        {
                            map[next.X + 1, next.Y].Value = (int)CellValue.EmptySpace;
                        }
                        else
                        {
                            map[next.X - 1, next.Y].Value = (int)CellValue.EmptySpace;
                        }
                    }
                    if (next.Y != cell.Y)
                    {
                        if (cell.Y > next.Y)
                        {
                            map[next.X, next.Y + 1].Value = (int)CellValue.EmptySpace;
                        }
                        else
                        {
                            map[next.X, next.Y - 1].Value = (int)CellValue.EmptySpace;
                        }
                    }
                    next.Visited = true;
                    path.Push(next);
                }
                else
                {
                    path.Pop();
                }
            }
            return map;
        }

        /// <summary>
        /// Волновой алгоритм
        /// </summary>
        /// <param name="map">Карта</param>
        /// <param name="startPosition">Начальная позиция</param>
        /// <param name="finishPosition">Целевая позиция</param>
        /// <returns>Карта решения</returns>
        private static Cell[,] WavePropagation(Cell[,] map, Cell startPosition, Cell finishPosition)
        {
            if (map == null || startPosition == null || finishPosition == null || (startPosition.X == finishPosition.X && startPosition.Y == finishPosition.Y))
                return null;
            map[startPosition.X, startPosition.Y].Value = (int)CellValue.StartPosition;
            map[finishPosition.X, finishPosition.Y].Value = (int)CellValue.Destination;
            int width = map.GetLength(0);
            int heigth = map.GetLength(1);
            int step = 0;
            bool finished = false;
            do
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < heigth; j++)
                    {
                        if (map[i, j].Value == step)
                        {
                            if (i != width - 1)
                                if (map[i + 1, j].Value == (int)CellValue.EmptySpace) map[i + 1, j].Value = step + 1;
                            if (j != heigth - 1)
                                if (map[i, j + 1].Value == (int)CellValue.EmptySpace) map[i, j + 1].Value = step + 1;
                            if (i != 0)
                                if (map[i - 1, j].Value == (int)CellValue.EmptySpace) map[i - 1, j].Value = step + 1;
                            if (j != 0)
                                if (map[i, j - 1].Value == (int)CellValue.EmptySpace) map[i, j - 1].Value = step + 1;
                            if (i < width - 1)
                                if (map[i + 1, j].Value == (int)CellValue.Destination)
                                {
                                    finishPosition.X = i + 1;
                                    finishPosition.Y = j;
                                    finished = true;
                                }
                            if (j < heigth - 1)
                                if (map[i, j + 1].Value == (int)CellValue.Destination)
                                {
                                    finishPosition.X = i;
                                    finishPosition.Y = j + 1;
                                    finished = true;
                                }
                            if (i > 0)
                                if (map[i - 1, j].Value == (int)CellValue.Destination)
                                {
                                    finishPosition.X = i - 1;
                                    finishPosition.Y = j;
                                    finished = true;
                                }
                            if (j > 0)
                                if (map[i, j - 1].Value == (int)CellValue.Destination)
                                {
                                    finishPosition.X = i;
                                    finishPosition.Y = j - 1;
                                    finished = true;
                                }
                        }
                    }
                }
                step++;
            } while (!finished && step < width * heigth);
            if (finished)
            {
                List<Tuple<int, int>> path = new List<Tuple<int, int>>();
                path.Add(new Tuple<int, int>(finishPosition.X, finishPosition.Y));
                do
                {
                    if (finishPosition.X < width - 1)
                        if (map[finishPosition.X + 1, finishPosition.Y].Value == step - 1)
                        {
                            path.Add(new Tuple<int, int>(++finishPosition.X, finishPosition.Y));
                        }
                    if (finishPosition.Y < heigth - 1)
                        if (map[finishPosition.X, finishPosition.Y + 1].Value == step - 1)
                        {
                            path.Add(new Tuple<int, int>(finishPosition.X, ++finishPosition.Y));
                        }
                    if (finishPosition.X > 0)
                        if (map[finishPosition.X - 1, finishPosition.Y].Value == step - 1)
                        {
                            path.Add(new Tuple<int, int>(--finishPosition.X, finishPosition.Y));
                        }
                    if (finishPosition.Y > 0)
                        if (map[finishPosition.X, finishPosition.Y - 1].Value == step - 1)
                        {
                            path.Add(new Tuple<int, int>(finishPosition.X, --finishPosition.Y));
                        }
                    step--;
                } while (step != 0);
                foreach (Tuple<int, int> item in path)
                {
                    if (item == path.Last())
                        map[item.Item1, item.Item2].Value = (int)CellValue.StartPosition;
                    else if (item == path.First())
                        map[item.Item1, item.Item2].Value = (int)CellValue.Destination;
                    else
                        map[item.Item1, item.Item2].Value = (int)CellValue.Path;
                }
                return map;
            }
            return null;
        }

        /// <summary>
        /// Алгоритм Флойда
        /// </summary>
        /// <param name="graph">Массив расстояний между узлами</param>
        /// <param name="left">Начальный узел</param>
        /// <param name="right">Конечный узел</param>
        /// <param name="distance">Минимальное расстояние от начального до конечного узла</param>
        /// <returns></returns>
        private int[,] AlgorithmFloyd(int[,] graph, int left, int right, out int distance)
        {
            int n = graph.GetLength(0);
            int[,] path = new int[n, n];
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int temp = graph[i, k] + graph[k, j];
                        if (graph[i, k] != 0 && graph[k, j] != 0 && temp < graph[i, j])
                        {
                            graph[i, j] = temp;
                            path[i, j] = k + 1;

                        }
                    }
                }
            }
            distance = graph[left, right];
            return path;
        }

        private int[,] AlgorithmFloyd(int[,] graph)
        {
            int n = graph.GetLength(0);
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int temp = graph[i, k] + graph[k, j];
                        if (graph[i, k] != 0 && graph[k, j] != 0 && temp < graph[i, j])
                        {
                            graph[i, j] = temp;

                        }
                    }
                }
            }
            return graph;
        }

        private int[] GetPathFloyd(int[,] pathFloyd, int left, int right)
        {
            List<int> list = new List<int>() { left };
            list = GetPathFloyd(pathFloyd, left - 1, right - 1, list);
            list.Add(right);
            list.Reverse();
            return list.ToArray();
        }

        private List<int> GetPathFloyd(int[,] pathFloyd, int left, int right, List<int> list)
        {
            int k = pathFloyd[left, right];
            if (k == 0)
                return list;
            list = GetPathFloyd(pathFloyd, left, k - 1, list);
            list.Add(k);
            return GetPathFloyd(pathFloyd, k - 1, right, list);
        }

        /// <summary>
        /// Алгоритм Дейкстры
        /// </summary>
        /// <param name="start">Начальный узел</param>
        /// <param name="graph">Массив расстояний между узлами</param>
        /// <returns>Массив минимальных расстояний</returns>
        int[] AlgorithmDijkstra(int start, int[,] graph)
        {
            bool[] visited = new bool[graph.GetLength(1)];
            int[] distance = new int[graph.GetLength(1)];
            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = int.MaxValue;
                visited[i] = false;
            }
            distance[start] = 0;
            int minindex, min, temp;
            // Шаг алгоритма
            do
            {
                minindex = graph.GetLength(0);
                min = int.MaxValue;
                for (int i = 0; i < distance.Length; i++)
                { // Если вершину ещё не обошли и вес меньше min
                    if (visited[i] == false && distance[i] < min)
                    { // Переприсваиваем значения
                        min = distance[i];
                        minindex = i;
                    }
                }
                // Добавляем найденный минимальный вес
                // к текущему весу вершины
                // и сравниваем с текущим минимальным весом вершины
                if (minindex != graph.GetLength(0))
                {
                    for (int i = 0; i < distance.Length; i++)
                    {
                        if (graph[minindex, i] > 0)
                        {
                            temp = min + graph[minindex, i];
                            if (temp < distance[i])
                            {
                                distance[i] = temp;
                            }
                        }
                    }
                    visited[minindex] = true;
                }
            } while (minindex < graph.GetLength(0));
            return distance;
        }

        private int[] GetPathDijkstra(int[] distance, int[,] graph, int right)
        {
            int[] path = new int[distance.Length];
            path[0] = right + 1;
            int k = 1; // индекс предыдущей вершины
            int weight = distance[right]; // вес конечной вершины
            while (right > 0) // пока не дошли до начальной вершины
            {
                for (int i = 0; i < path.Length; i++)
                {
                    // просматриваем все вершины
                    if (graph[right, i] != 0)   // если связь есть
                    {
                        int temp = weight - graph[right, i]; // определяем вес пути из предыдущей вершины
                        if (temp == distance[i]) // если вес совпал с рассчитанным
                        {                 // значит из этой вершины и был переход
                            weight = temp; // сохраняем новый вес
                            right = i;       // сохраняем предыдущую вершину
                            path[k] = i + 1; // и записываем ее в массив
                            k++;
                        }
                    }
                }
            }
            return path;
        }

        /// <summary>
        /// Алгоритм Бойера-Мура
        /// </summary>
        /// <param name="text"> Исходный текст</param>
        /// <param name="substring">Образ</param>
        /// <param name="index">Индекс первого вхождения образа в исходном тексте</param>
        /// <returns>Значение, показывающее, содержится ли образ в тексте</returns>
        private bool AlgBM(string text, string substring, out int index)
        {
            int n = text.Length;
            int m = substring.Length;
            int ind;
            int[] table = new int[9999];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = m;
            }
            for (int i = m - 1; i >= 0; i--)
            {
                if (table[(short)(substring[i])] == m)
                {
                    table[(short)(substring[i])] = m - i - 1;
                }
            }
            ind = m - 1;
            while (ind < n)
            {
                if (substring[m - 1] != text[ind])
                {
                    ind += table[(short)(text[ind])];
                }
                else
                {
                    for (int i = m - 2; i >= 0; i--)
                    {
                        if (substring[i] != text[ind - m + i + 1])
                        {
                            ind += table[(short)(text[ind - m + i + 1])] - 1;
                            break;
                        }
                        else if (i == 0)
                        {
                            index = ind - m + 1;
                            return true;
                        }
                    }
                }
            }
            index = 0;
            return false;
        }

        /// <summary>
        /// Алгоритм Кнута-Морриса-Пратта
        /// </summary>
        /// <param name="text"> Исходный текст</param>
        /// <param name="substring">Образ</param>
        /// <param name="index">Индекс первого вхождения образа в исходном тексте</param>
        /// <returns>Значение, показывающее, содержится ли образ в тексте</returns>
        private bool AlgKMP(string text, string substring, out int index)
        {
            int[] pi = new int[substring.Length];
            int j = 0, i = 1;
            pi[0] = 0;
            while (i != substring.Length)
            {
                if (!substring[i].Equals(substring[j]))
                {
                    if (j == 0)
                    {
                        pi[i] = 0;
                        i++;
                    }
                    else
                    {
                        j = pi[j - 1];
                    }
                }
                else
                {
                    pi[i] = j + 1;
                    i++;
                    j++;
                }
            }
            i = 0;
            j = 0;
            while (i != text.Length)
            {
                if (text[i].Equals(substring[j]))
                {
                    i++;
                    j++;
                    if (j == substring.Length)
                    {
                        index = i - substring.Length;
                        return true;
                    }
                }
                else if (j != 0)
                {
                    j = pi[j - 1];
                }
                else
                {
                    i++;
                    if (i == text.Length)
                    {
                        index = 0;
                        return false;
                    }
                }
            }
            index = 0;
            return false;
        }

        /// <summary>
        /// Сортировка вставками
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private T[] InsertionSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            for (int i = 1; i < massiv.Length; i++)
            {
                T x = massiv[i];
                int j = i;
                while (j > 0 && ((revers && massiv[j - 1].CompareTo(x) < 0) || (!revers && massiv[j - 1].CompareTo(x) > 0)))
                {
                    massiv[j] = massiv[j - 1];
                    j--;
                }
                massiv[j] = x;
            }
            return massiv;
        }

        /// <summary>
        /// Сортировка пузырьком
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private T[] BubbleSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            for (int i = 0; i < massiv.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < massiv.Length - i - 1; j++)
                {
                    if ((revers && massiv[j].CompareTo(massiv[j + 1]) < 0) ||
                        (!revers && massiv[j].CompareTo(massiv[j + 1]) > 0))
                    {
                        massiv = Swap(massiv, j, j + 1);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    break;
                }
            }
            return massiv;
        }

        /// <summary>
        /// Сортировка выбором
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private T[] SelectionSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            for (int i = 0; i < massiv.Length - 1; i++)
            {
                int index = i;
                for (int j = i + 1; j < massiv.Length; j++)
                {
                    if ((revers && massiv[j].CompareTo(massiv[index]) > 0) ||
                        (!revers && massiv[j].CompareTo(massiv[index]) < 0))
                    {
                        index = j;
                    }
                }
                if (index != i)
                {
                    massiv = Swap(massiv, i, index);
                }
            }
            return massiv;
        }

        int Sift<T>(T[] massiv, int i, int N, bool revers) where T : IComparable<T>
        {
            int imax;
            if (2 * i + 2 < N)
            {
                if ((!revers && massiv[2 * i + 1].CompareTo(massiv[2 * i + 2]) < 0) ||
                    revers && massiv[2 * i + 1].CompareTo(massiv[2 * i + 2]) > 0)
                    imax = 2 * i + 2;
                else
                    imax = 2 * i + 1;
            }
            else
                imax = 2 * i + 1;
            if (imax >= N)
                return i;
            if ((!revers && massiv[i].CompareTo(massiv[imax]) < 0) || (revers && massiv[i].CompareTo(massiv[imax]) > 0))
            {
                massiv = Swap(massiv, i, imax);
                if (imax < N / 2)
                    i = imax;
            }
            return i;
        }

        /// <summary>
        /// Пирамидальная сортировка
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        public T[] HeapSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            for (int i = massiv.Length / 2 - 1; i >= 0; --i)
            {
                int prev_i = i;
                i = Sift(massiv, i, massiv.Length, revers);
                if (prev_i != i) ++i;
            }
            for (int k = massiv.Length - 1; k > 0; --k)
            {
                massiv = Swap(massiv, 0, k);
                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = Sift(massiv, i, k, revers);
                }
            }
            return massiv;
        }

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private T[] MergeSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            if (massiv.Length == 1)
                return massiv;
            return Merge(MergeSort(massiv.Take(massiv.Length / 2).ToArray(), revers), MergeSort(massiv.Skip(massiv.Length / 2).ToArray(), revers), revers);
        }

        private T[] Merge<T>(T[] massiv1, T[] massiv2, bool revers) where T : IComparable<T>
        {
            int a = 0, b = 0;
            T[] massiv = new T[massiv1.Length + massiv2.Length];
            for (int i = 0; i < massiv1.Length + massiv2.Length; i++)
            {
                if (b < massiv2.Length && a < massiv1.Length)
                {
                    if ((!revers && massiv1[a].CompareTo(massiv2[b]) > 0) ||
                        (revers && massiv1[a].CompareTo(massiv2[b]) < 0))
                    {
                        massiv[i] = massiv2[b++];
                    }
                    else massiv[i] = massiv1[a++];
                }
                else
                {
                    if (b < massiv2.Length)
                    {
                        massiv[i] = massiv2[b++];
                    }
                    else
                    {
                        massiv[i] = massiv1[a++];
                    }
                }
            }
            return massiv;
        }

        private int Partition<T>(T[] massiv, int start, int end, bool revers) where T : IComparable<T>
        {
            int marker = start;
            for (int i = start; i <= end; i++)
            {
                if ((!revers && massiv[i].CompareTo(massiv[end]) <= 0) ||
                    (revers && massiv[i].CompareTo(massiv[end]) >= 0))
                {
                    massiv = Swap(massiv, marker, i);
                    marker += 1;
                }
            }
            return marker - 1;
        }

        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private T[] QuickSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            int pivot = Partition(massiv, 0, massiv.Length - 1, revers);
            massiv = QuickSort(massiv, 0, pivot - 1, revers);
            massiv = QuickSort(massiv, pivot + 1, massiv.Length - 1, revers);
            return massiv;
        }

        private T[] QuickSort<T>(T[] massiv, int start, int end, bool revers) where T : IComparable<T>
        {
            if (start >= end)
            {
                return massiv;
            }
            int pivot = Partition(massiv, start, end, revers);
            massiv = QuickSort(massiv, start, pivot - 1, revers);
            massiv = QuickSort(massiv, pivot + 1, end, revers);
            return massiv;
        }

        /// <summary>
        /// Сортировка подсчетом
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private T[] CountingSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            T[] massiv_out = new T[massiv.Length];
            int[] counts = new int[massiv.Length];
            for (int i = 1; i < massiv.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if ((!revers && massiv[i].CompareTo(massiv[j]) < 0) ||
                        (revers && massiv[i].CompareTo(massiv[j]) > 0))
                    {
                        counts[j]++;
                    }
                    else counts[i]++;
                }
            }
            for (int i = 0; i < massiv.Length; i++)
            {
                massiv_out[counts[i]] = massiv[i];
            }
            return massiv_out;
        }

        /// <summary>
        /// "Шейкерная" сортировка
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private T[] ShakerSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            int left = 0,
                right = massiv.Length - 1,
                count = 0;

            while (left <= right)
            {
                for (int i = left; i < right; i++)
                {
                    count++;
                    if ((!revers && massiv[i].CompareTo(massiv[i + 1]) > 0) ||
                        (revers && massiv[i].CompareTo(massiv[i + 1]) < 0))
                        massiv = Swap(massiv, i, i + 1);
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    count++;
                    if ((!revers && massiv[i - 1].CompareTo(massiv[i]) > 0) ||
                        (revers && massiv[i - 1].CompareTo(massiv[i]) < 0))
                        massiv = Swap(massiv, i - 1, i);
                }
                left++;
            }
            return massiv;
        }

        private T[] Swap<T>(T[] massiv, int i, int j)
        {
            T temp = massiv[i];
            massiv[i] = massiv[j];
            massiv[j] = temp;
            return massiv;
        }

        private void AddCellsAndNodes(int left, int right)
        {
            for (int i = left + 1; i < right; i++)
            {
                DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();
                buttonCell.Value = i;
                dataGridView2[0, i] = buttonCell;
                buttonCell = new DataGridViewButtonCell();
                buttonCell.Value = i;
                dataGridView2[i, 0] = buttonCell;
                dataGridView2[i, i].Value = 0;
                dataGridView2[i, i].Style.BackColor = System.Drawing.Color.Yellow;
                dataGridView2[i, i].ReadOnly = true;
                graph.AddNode(i.ToString());
            }
        }

        private void RemoveNodes(int left, int right)
        {
            for (int i = left; i < right + 1; i++)
            {
                try
                {
                    graph.RemoveNode(graph.FindNode(i.ToString()));
                }
                catch
                {

                }
            }
        }

        private void ShowGraph(Graph graph)
        {
            GraphForm graphForm = new GraphForm(graph);
            graphForm.ShowDialog();
        }

        private void UpdateLabel(int left, int right, double distance)
        {
            label8.Text = string.Format("Минимальное расстояние из узла {0} в узел {1} равно {2}.", left, right, distance);
        }

        private void UpdateLabel(double index)
        {
            if (index != 0)
            {
                label8.Text = string.Format("Центр графа - вершина под номером {0}.", index);
            }
            else label8.Text = "Невозможно определить центр графа.";
        }
        private void UpdateLabel(string text)
        {
            label8.Text = text;
        }

        private void ButtonVisible(bool value)
        {
            button5.Visible = value;
        }

        private void SetEdgeColor(int[] path, Color color)
        {
            for (int i = path.Length - 1; i > 0; i--)
            {
                if (path[i] != 0)
                {
                    try
                    {
                        graph.FindNode(path[i].ToString()).Attr.Color = color;
                        graph.FindNode(path[i - 1].ToString()).Attr.Color = color;
                        graph.Edges.First(item => (item.Source.Equals(path[i].ToString()) && item.Target.Equals(path[i - 1].ToString()))).Attr.Color = color;
                    }
                    catch
                    {
                        graph.Edges.First(item => (item.Source.Equals(path[i - 1].ToString()) && item.Target.Equals(path[i].ToString()))).Attr.Color = color;
                    }
                }
            }
        }

        private int[,] GetMassiv(int rowCount, int columnCount)
        {
            int[,] massiv = new int[rowCount, columnCount];
            for (int i = 0; i < massiv.GetLength(0); i++)
            {
                for (int j = 0; j < massiv.GetLength(1); j++)
                {
                    try
                    {
                        massiv[i, j] = Convert.ToInt32(dataGridView2[j + 1, i + 1].Value);

                    }
                    catch
                    {
                        massiv[i, j] = 0;
                    }
                }
            }
            return massiv;
        }

        private int MaxInColumn(int index, int[,] massiv)
        {
            int max = massiv[0, index];
            for (int i = 1; i < massiv.GetLength(0); i++)
            {
                if (max < massiv[i, index] && massiv[i, index] != 0)
                {
                    max = massiv[i, index];
                }
            }
            return max;
        }

        private int IndexMinInMassiv(int[] massiv)
        {
            int min = massiv[0];
            int index = 0;
            for (int i = 1; i < massiv.Length; i++)
            {
                if (min > massiv[i] && massiv[i] != 0)
                {
                    min = massiv[i];
                    index = i + 1;
                }
            }
            return index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = -1;
            if (richTextBox1.Text == string.Empty)
            {
                label3.Text = "Ошибка: Исходный текст не задан.";
            }
            else if (textBox2.Text == string.Empty)
            {
                label3.Text = "Ошибка: Образ не задан.";
            }
            else if (richTextBox1.Text.Length < textBox2.Text.Length)
            {
                label3.Text = "Ошибка: Длина образа больше длины исходного текста.";
            }
            else if ((radioButton1.Checked && AlgKMP(richTextBox1.Text, textBox2.Text, out k)) ||
                     (radioButton2.Checked && AlgBM(richTextBox1.Text, textBox2.Text, out k)))
            {
                label3.Text = "Вывод: Образ найден. Индекс первого вхождения: " + k.ToString() + ".";
            }
            else
            {
                label3.Text = "Вывод: Образ не найден.";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 50 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView dataGridView = sender as DataGridView;
                oldValue = Convert.ToInt32(dataGridView[e.ColumnIndex, e.RowIndex].Value);
            }
            catch
            {
                oldValue = 0;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int temp = Convert.ToInt32(dataGridView1[e.ColumnIndex, e.RowIndex].Value);
            }
            catch
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = oldValue;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] massiv = new int[dataGridView1.ColumnCount];
            Sort<int> sort;
            for (int i = 0; i < massiv.Length; i++)
            {
                massiv[i] = Convert.ToInt32(dataGridView1[i, 0].Value);
            }
            if (radioButton3.Checked)
            {
                sort = SelectionSort;
            }
            else if (radioButton4.Checked)
            {
                sort = InsertionSort;
            }
            else if (radioButton5.Checked)
            {
                sort = BubbleSort;
            }
            else if (radioButton9.Checked)
            {
                sort = HeapSort;
            }
            else if (radioButton10.Checked)
            {
                sort = QuickSort;
            }
            else if (radioButton11.Checked)
            {
                sort = CountingSort;
            }
            else if (radioButton12.Checked)
            {
                sort = ShakerSort;
            }
            else
            {
                sort = MergeSort;
            }
            massiv = sort.Invoke(massiv, checkBox1.Checked);
            for (int i = 0; i < massiv.Length; i++)
            {
                dataGridView1[i, 1].Value = massiv[i];
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Maximum = numericUpDown2.Value;
            numericUpDown4.Maximum = numericUpDown2.Value;
            dataGridView2.ColumnCount = Convert.ToInt32(numericUpDown2.Value) + 1;
            dataGridView2.RowCount = dataGridView2.ColumnCount;
            if (dataGridView2.ColumnCount - 1 > oldValueCount)
            {
                AddCellsAndNodes(oldValueCount, dataGridView2.ColumnCount);
            }
            else if (dataGridView2.ColumnCount - 1 < oldValueCount)
            {
                RemoveNodes(dataGridView2.ColumnCount, oldValueCount);
            }
            oldValueCount = dataGridView2.ColumnCount - 1;
        }

        private void numericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 51 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (semaphore2)
            {
                semaphore2 = !semaphore2;
                return;
            }
            try
            {
                if (e.RowIndex != e.ColumnIndex && !semaphore2)
                {
                    int temp = Convert.ToInt32(dataGridView2[e.ColumnIndex, e.RowIndex].Value);
                    if (temp < 0)
                        throw new Exception();
                    semaphore2 = !semaphore2;
                    bool isFound = false;
                    dataGridView2[e.RowIndex, e.ColumnIndex].Value = temp;
                    if (dataGridView2[e.ColumnIndex, e.RowIndex].Value == null)
                    {
                        semaphore2 = !semaphore2;
                        dataGridView2[e.ColumnIndex, e.RowIndex].Value = temp;
                    }
                    if (temp != 0)
                    {
                        try
                        {
                            graph.Edges.First(item => (item.Source.Equals(e.ColumnIndex.ToString()) && item.Target.Equals(e.RowIndex.ToString()))
                                                     || (item.Source.Equals(e.RowIndex.ToString()) && item.Target.Equals(e.ColumnIndex.ToString()))).LabelText = temp.ToString();
                            isFound = true;
                        }
                        catch
                        {

                        }
                    }
                    if (!isFound)
                    {
                        if (temp != 0)
                        {
                            graph.AddEdge(e.RowIndex.ToString(), temp.ToString(), e.ColumnIndex.ToString()).Attr.ArrowheadAtTarget = ArrowStyle.None;
                        }
                        else
                        {
                            try
                            {
                                graph.RemoveEdge(graph.Edges.First(item => (item.Source.Equals(e.ColumnIndex.ToString()) && item.Target.Equals(e.RowIndex.ToString()))
                                                         || (item.Source.Equals(e.RowIndex.ToString()) && item.Target.Equals(e.ColumnIndex.ToString()))));
                            }
                            catch
                            {

                            }
                        }
                    }
                    ButtonVisible(false);
                    UpdateLabel(string.Empty);
                }
            }
            catch
            {
                semaphore2 = !semaphore2;
                dataGridView2[e.ColumnIndex, e.RowIndex].Value = oldValue;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!semaphore && cell.ColumnNumber != 0 && cell.RowNumber != 0)
            {
                dataGridView2[cell.ColumnNumber, 0].Style.BackColor = System.Drawing.Color.White;
                dataGridView2[0, cell.RowNumber].Style.BackColor = System.Drawing.Color.White;
                if (cell.ColumnNumber == cell.RowNumber)
                {
                    dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.White;
                }
                cell.ColumnNumber = 0;
                cell.RowNumber = 0;
            }
            if (e.RowIndex != e.ColumnIndex)
            {
                if (e.RowIndex == 0)
                {
                    if (semaphore)
                    {
                        if (cell.ColumnNumber != 0)
                        {
                            dataGridView2[cell.ColumnNumber, 0].Style.BackColor = System.Drawing.Color.White;
                            dataGridView2[e.ColumnIndex, 0].Style.BackColor = System.Drawing.Color.Red;
                            cell.ColumnNumber = e.ColumnIndex;
                        }
                        else
                        {
                            cell.ColumnNumber = e.ColumnIndex;
                            dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.Red;
                            semaphore = !semaphore;
                        }
                    }
                    else
                    {
                        dataGridView2[e.ColumnIndex, 0].Style.BackColor = System.Drawing.Color.Red;
                        cell.ColumnNumber = e.ColumnIndex;
                        semaphore = !semaphore;
                    }
                }
                else if (e.ColumnIndex == 0)
                {
                    if (semaphore)
                    {
                        if (cell.RowNumber != 0)
                        {
                            dataGridView2[0, cell.RowNumber].Style.BackColor = System.Drawing.Color.White;
                            dataGridView2[0, e.RowIndex].Style.BackColor = System.Drawing.Color.Red;
                            cell.RowNumber = e.RowIndex;
                        }
                        else
                        {
                            cell.RowNumber = e.RowIndex;
                            dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.Red;
                            semaphore = !semaphore;
                        }
                    }
                    else
                    {
                        dataGridView2[0, e.RowIndex].Style.BackColor = System.Drawing.Color.Red;
                        cell.RowNumber = e.RowIndex;
                        semaphore = !semaphore;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowGraph(graph);
        }

        private void numericUpDown3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 49 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int left = Convert.ToInt32(numericUpDown3.Value);
            int right = Convert.ToInt32(numericUpDown4.Value);
            if (right != left)
            {
                int[,] massiv = GetMassiv(dataGridView2.RowCount - 1, dataGridView2.ColumnCount - 1);
                if (radioButton7.Checked)
                {
                    int[] distance = AlgorithmDijkstra(left - 1, massiv);
                    if (distance[right - 1] != int.MaxValue)
                    {
                        UpdateLabel(left, right, distance[right - 1]);
                        path = GetPathDijkstra(distance, massiv, right - 1);
                        ButtonVisible(true);
                    }
                    else
                    {
                        UpdateLabel(left, right, double.PositiveInfinity);
                        ButtonVisible(false);
                    }
                }
                else
                {
                    int distance;
                    int[,] pathFloyd = AlgorithmFloyd(massiv, left - 1, right - 1, out distance);
                    if (distance != 0)
                    {
                        UpdateLabel(left, right, distance);
                        path = GetPathFloyd(pathFloyd, left, right);
                        ButtonVisible(true);
                    }
                    else
                    {
                        UpdateLabel(left, right, double.PositiveInfinity);
                        ButtonVisible(false);
                    }
                }
            }
            else
            {
                UpdateLabel("Ошибка: Начальный и конечный узлы равны.");
                ButtonVisible(false);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetEdgeColor(path, Color.Red);
            ShowGraph(graph);
            SetEdgeColor(path, Color.Black);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[,] graph = AlgorithmFloyd(GetMassiv(dataGridView2.RowCount - 1, dataGridView2.ColumnCount - 1));
            int[] maxDistances = new int[graph.GetLength(1)];
            for (int i = 0; i < maxDistances.Length; i++)
            {
                maxDistances[i] = MaxInColumn(i, graph);
            }
            UpdateLabel(IndexMinInMassiv(maxDistances));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            graphics.Clear(Drawing.Color.White);
            if (drawPolygon)
            {
                graphics.FillPolygon(Drawing.Brushes.Red, points.ToArray());
            }
            for (int i = 0; i < points.Count && !drawPolygon; i++)
            {
                graphics.DrawEllipse(Drawing.Pens.Red, points[i].X, points[i].Y, 1, 1);
                graphics.DrawString(i.ToString(), new Drawing.Font("Courier New", 10, Drawing.FontStyle.Italic), Drawing.Brushes.Black, points[i]);
            }
            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (points.Count > 2)
                {
                    drawPolygon = true;
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (drawPolygon)
                {
                    points.Clear();
                    drawPolygon = false;
                    label13.Text = string.Empty;
                }
                points.Add(new Drawing.Point(e.X, e.Y));
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                points.Clear();
                drawPolygon = false;
                label13.Text = string.Empty;
            }
            pictureBox1.Invalidate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (drawPolygon)
            {
                int count = 0;
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Drawing.Color color = bitmap.GetPixel(i, j);
                        if (color.R == 255 && color.G == 0 && color.B == 0)
                        {
                            count++;
                        }
                    }
                }
                double square = (double)numericUpDown5.Value * (double)numericUpDown6.Value * count / (bitmap.Width * bitmap.Height);
                label13.Text = string.Format("{0:f2}", square);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            startPosition = null;
            finishPosition = null;
            graphics3 = null;
            map = GenerateLabirint((int)numericUpDown7.Value, (int)numericUpDown8.Value);
            graphics2 = panel1.CreateGraphics();
            panel1.Invalidate();
            panel2.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (graphics2 != null)
            {
                int width = map.GetLength(0);
                int height = map.GetLength(1);
                float widthBlock = (float)(panel1.Width * 1.0f / width);
                float heightBlock = (float)(panel1.Height * 1.0f / height);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        switch (map[i, j].Value)
                        {
                            case -1:
                                graphics2.FillRectangle(Drawing.Brushes.White, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                            default:
                                graphics2.FillRectangle(Drawing.Brushes.Black, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                        }
                    }
                }
                if (startPosition != null)
                {
                    graphics2.FillRectangle(Drawing.Brushes.Yellow, new Drawing.RectangleF(startPosition.X * widthBlock, startPosition.Y * heightBlock, widthBlock, heightBlock));
                }
                if (finishPosition != null)
                {
                    graphics2.FillRectangle(Drawing.Brushes.Red, new Drawing.RectangleF(finishPosition.X * widthBlock, finishPosition.Y * heightBlock, widthBlock, heightBlock));
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (graphics3 != null)
            {
                int width = map1.GetLength(0);
                int height = map1.GetLength(1);
                float widthBlock = (float)(panel1.Width * 1.0f / width);
                float heightBlock = (float)(panel1.Height * 1.0f / height);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        switch (map1[i, j].Value)
                        {
                            case 0:
                                graphics3.FillRectangle(Drawing.Brushes.Yellow, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                            case -1:
                                graphics3.FillRectangle(Drawing.Brushes.White, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                            case -2:
                                graphics3.FillRectangle(Drawing.Brushes.Red, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                            case -3:
                                graphics3.FillRectangle(Drawing.Brushes.Blue, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                            case -4:
                                graphics3.FillRectangle(Drawing.Brushes.Black, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                            default:
                                graphics3.FillRectangle(Drawing.Brushes.White, new Drawing.RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock));
                                break;
                        }
                    }
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (graphics2 != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    startPosition = null;
                    finishPosition = null;
                    panel1.Invalidate();
                }
                else
                {
                    int width = map.GetLength(0);
                    int height = map.GetLength(1);
                    double widthBlock = (double)(panel2.Width * 1.0f / width);
                    double heightBlock = (double)(panel2.Height * 1.0f / height);
                    int x = (int)Math.Truncate((double)e.X / widthBlock);
                    int y = (int)Math.Truncate((double)e.Y / heightBlock);
                    if (map[x, y].Value != -4)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            startPosition = new Cell(x, y);
                        }
                        else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                        {
                            finishPosition = new Cell(x, y);
                        }
                        panel1.Invalidate();
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            graphics3 = null;
            map1 = WavePropagation(Cell.CopyMatrix(map), new Cell(startPosition), new Cell(finishPosition));
            if (map1 != null)
            {
                graphics3 = panel2.CreateGraphics();
            }
            panel2.Invalidate();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < rombMesh.GetLength(0); i++)
            {
                for (int j = 0; j < rombMesh.GetLength(1); j++)
                {
                    float d = (float)rombMesh[i, j].Radius * 2;
                    e.Graphics.DrawEllipse(Drawing.Pens.Black, (float)(rombMesh[i, j].CenterPoint.X - rombMesh[i, j].Radius), (float)(rombMesh[i, j].CenterPoint.Y - rombMesh[i, j].Radius), d, d);
                    e.Graphics.DrawPolygon(Drawing.Pens.Black, rombMesh[i, j].Points);
                }
            }
            foreach (Sprait item in objects)
            {
                e.Graphics.DrawImage(item.Image, item.Point);
            }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            label16.Text = e.Location.ToString();
            Drawing.Point point = Romb.Check(rombMesh, e.Location);
            if (point.X != -1)
            {
                label17.Text = point.ToString();
            }
            else label17.Text = string.Empty;
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            label17.Text = string.Empty;
            label16.Text = string.Empty;
        }

        private void panel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Drawing.Point coordinate = Romb.Check(rombMesh, e.Location);
            if (coordinate.X != -1)
            {
                Drawing.PointF pilot = rombMesh[coordinate.X, coordinate.Y].CenterPoint;
                objects.Add(new Sprait(Properties.Resources.Tree, new Drawing.PointF(pilot.X - Properties.Resources.Tree.Width / 2, pilot.Y - Properties.Resources.Tree.Height)));
                objects = objects.ToArray().OrderBy(x => x.Point.Y).ThenBy(x => x.Point.X).ToList();
                panel3.Invalidate();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text.Length > 0)
            {
                int key = Convert.ToInt32(numericUpDown9.Value);
                byte[] text;
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        text = Encoding.ASCII.GetBytes(richTextBox2.Text);
                        break;
                    case 1:
                        text = Encoding.Unicode.GetBytes(richTextBox2.Text);
                        break;
                    case 2:
                        text = Encoding.BigEndianUnicode.GetBytes(richTextBox2.Text);
                        break;
                    case 3:
                        text = Encoding.UTF7.GetBytes(richTextBox2.Text);
                        break;
                    case 4:                        
                        text = Encoding.UTF8.GetBytes(richTextBox2.Text);
                        break;
                    case 5:
                        text = Encoding.UTF32.GetBytes(richTextBox2.Text);
                        break;
                    default:
                        text = Encoding.Default.GetBytes(richTextBox2.Text);
                        break;
                }
                byte[] text2 = new byte[text.Length];
                for (int i = 0; i < text2.Length; i++)
                {
                    text2[i] = (byte)(text[i] ^ key);
                }
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        richTextBox3.Text = Encoding.ASCII.GetString(text2);
                        break;
                    case 1:
                        richTextBox3.Text = Encoding.Unicode.GetString(text2);
                        break;
                    case 2:
                        richTextBox3.Text = Encoding.BigEndianUnicode.GetString(text2);
                        break;
                    case 3:
                        richTextBox3.Text = Encoding.UTF7.GetString(text2);
                        break;
                    case 4:
                        richTextBox3.Text = Encoding.UTF8.GetString(text2);
                        break;
                    case 5:
                        richTextBox3.Text = Encoding.UTF32.GetString(text2);
                        break;
                    default:
                        richTextBox3.Text = Encoding.Default.GetString(text2);
                        break;
                }
            }
        }
    }
}
