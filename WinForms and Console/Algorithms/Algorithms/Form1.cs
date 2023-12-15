using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Drawing = System.Drawing;

namespace Algorithms
{
    public partial class Form1 : Form
    {
        #region Vars

        private int oldValue;
        private int oldValueCount;
        private readonly Graph graph;
        private DataGridCell cell;
        private bool semaphore;
        private int[] path;
        private delegate T[] Sort<T>(T[] massiv, bool revers) where T : IComparable<T>;
        private readonly List<Drawing.Point> points;
        private bool drawPolygon;
        private readonly Drawing.Bitmap bitmap;
        private readonly Drawing.Graphics graphics;
        private readonly Romb[,] rombMesh;
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
            points = new List<Drawing.Point>();
            drawPolygon = false;
            bitmap = new Drawing.Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Drawing.Graphics.FromImage(bitmap);
            rombMesh = Romb.CreateRombMesh(0, 200, 20, 10, 10, 2.5);
            objects = new List<Sprait>();
            comboBox1.SelectedIndex = 0;
            numericUpDown8.Minimum = 2;
        }

        /// <summary>
        /// Алгоритм нахождения максимального потока
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <param name="left">Начальный узел</param>
        /// <param name="right">Конечный узел</param>
        /// <param name="found">Индикатор нахождения потока</param>
        /// <returns></returns>
        private int AlgorithmMaxStream(int[,] graph, int left, int right)
        {
            int max = 0;
            bool path_found;
            do
            {
                path = AlgorithmDFS(graph, left, right, out path_found);
                if (path_found)
                {
                    int min = graph[path[path.Length - 1] - 1, path[path.Length - 2] - 1];
                    for (int i = path.Length - 2; i > 0; i--)
                    {
                        if (graph[path[i] - 1, path[i - 1] - 1] < min)
                        {
                            min = graph[path[i] - 1, path[i - 1] - 1];
                        }
                    }
                    max += min;
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        graph[path[i] - 1, path[i + 1] - 1] += min;
                        graph[path[path.Length - i - 1] - 1, path[path.Length - i - 2] - 1] -= min;
                    }
                }
            } while (path_found);
            return max;
        }

        /// <summary>
        /// Алгоритм нахождения пути в графе поиском в глубину
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <param name="left">Начальный узел</param>
        /// <param name="right">Конечный узел</param>
        /// <param name="found">Индикатор нахождения пути</param>
        /// <returns></returns>
        private int[] AlgorithmDFS(int[,] graph, int left, int right, out bool found)
        {
            int n = graph.GetLength(0);
            int[] nodes = new int[n];
            Stack<int> stack = new Stack<int>();
            Stack<Tuple<int, int>> edges = new Stack<Tuple<int, int>>();
            stack.Push(left);
            while (stack.Count > 0)
            {
                int node = stack.Pop();
                if (nodes[node] != 2)
                {
                    nodes[node] = 2;
                    for (int i = n - 1; i >= 0; i--)
                    {
                        if (graph[node, i] != 0 && nodes[i] != 2)
                        {
                            stack.Push(i);
                            nodes[i] = 1;
                            Tuple<int, int> edge = new Tuple<int, int>(node, i);
                            edges.Push(edge);
                            if (node == right)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            List<int> list = new List<int>
                {
                    right + 1
                };
            found = false;
            while (edges.Count > 0)
            {
                Tuple<int, int> edge = edges.Pop();
                if (edge.Item2 == right)
                {
                    right = edge.Item1;
                    list.Add(right + 1);
                    found = right == left;
                }
            }
            return list.ToArray();
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
                        if (i != j && graph[i, k] != 0 && graph[k, j] != 0 && (temp < graph[i, j] || graph[i, j] == 0))
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

        private Dictionary<int, Tuple<int, int>[]> InitAdjacencyListFromAdjacencyMatrix(int[,] adjacencyMatrix)
        {
            Dictionary<int, Tuple<int, int>[]> adjacencyList = new Dictionary<int, Tuple<int, int>[]>();
            int length = adjacencyMatrix.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                List<Tuple<int, int>> list = new List<Tuple<int, int>>();
                for (int j = 0; j < length; j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        list.Add(new Tuple<int, int>(j, adjacencyMatrix[i, j]));
                    }
                }
                adjacencyList.Add(i, list.ToArray());

            }
            return adjacencyList;
        }


        /// <summary>
        /// Алгоритм Дейкстры
        /// </summary>
        /// <param name="start">Начальный узел</param>
        /// <param name="finish">Конечный узел</param>
        /// <param name="adjacencyList">Список ребер с весами</param>
        /// <param name="distance">Минимальное расстояние между вершинами</param>
        /// <returns>Путь между вершинами</returns>
        private int[] AlgorithmDijkstra(int start, int finish, int count, Dictionary<int, Tuple<int, int>[]> adjacencyList, out int distance)
        {
            bool[] visited = new bool[count];
            int[] distanceArray = new int[count];
            int[] pathArray = new int[count];
            for (int i = 0; i < count; i++)
            {
                distanceArray[i] = int.MaxValue;
                pathArray[i] = int.MaxValue;
                visited[i] = false;
            }
            distanceArray[start] = 0;
            int minindex, min;
            do
            {
                minindex = count;
                min = int.MaxValue;
                for (int i = 0; i < count; i++)
                {
                    if (visited[i] == false && distanceArray[i] < min)
                    {
                        min = distanceArray[i];
                        minindex = i;
                    }
                }
                if (minindex != count)
                {
                    for (int i = 0; i < adjacencyList[minindex].Length; i++)
                    {
                        int to = adjacencyList[minindex][i].Item1;
                        int len = adjacencyList[minindex][i].Item2;
                        int temp = distanceArray[minindex] + len;
                        if (temp < distanceArray[to])
                        {
                            distanceArray[to] = temp;
                            pathArray[to] = minindex;
                        }
                    }
                    visited[minindex] = true;
                }
            } while (minindex < count);
            distance = distanceArray[finish];
            List<int> path = new List<int>();
            for (int i = finish; i != start && pathArray[i] != int.MaxValue; i = pathArray[i])
            {
                path.Add(i + 1);
            }
            path.Add(start + 1);
            return path.ToArray();
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

        private T[] BinaryInsertionSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            for (int i = 1; i < massiv.Length; i++)
            {
                if ((revers && massiv[i - 1].CompareTo(massiv[i]) < 0) || (!revers && massiv[i - 1].CompareTo(massiv[i]) > 0))
                {
                    T x = massiv[i];
                    int left = 0;
                    int right = i - 1;
                    do
                    {
                        int mid = (left + right) / 2;
                        if ((revers && massiv[mid].CompareTo(x) > 0) || (!revers && massiv[mid].CompareTo(x) < 0))
                        {
                            left = mid + 1;
                        }
                        else
                        {
                            right = mid - 1;
                        }
                    } while (left <= right);
                    for (int j = i - 1; j >= left; j--)
                    {
                        massiv[j + 1] = massiv[j];
                    }
                    massiv[left] = x;
                }
            }
            return massiv;
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

        T[] Sift<T>(T[] massiv, int n, int i, bool revers) where T : IComparable<T>
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;
            if (l < n && ((revers && massiv[l].CompareTo(massiv[largest]) < 0) ||
                        (!revers && massiv[l].CompareTo(massiv[largest]) > 0)))
            {
                largest = l;
            }
            if (r < n && ((revers && massiv[r].CompareTo(massiv[largest]) < 0) ||
                        (!revers && massiv[r].CompareTo(massiv[largest]) > 0)))
            {
                largest = r;
            }
            if (largest != i)
            {
                massiv = Swap(massiv, i, largest);
                massiv = Sift(massiv, n, largest, revers);
            }
            return massiv;
        }

        /// <summary>
        /// Пирамидальная сортировка
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        public T[] HeapSort<T>(T[] massiv, bool revers) where T : IComparable<T>
        {
            for (int i = massiv.Length / 2 - 1; i >= 0; i--)
            {
                massiv = Sift(massiv, massiv.Length, i, revers);
            }
            for (int i = massiv.Length - 1; i >= 0; i--)
            {
                massiv = Swap(massiv, 0, i);
                massiv = Sift(massiv, i, 0, revers);
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
                DataGridViewButtonCell buttonCell = new DataGridViewButtonCell
                {
                    Value = i
                };
                dataGridView2[0, i] = buttonCell;
                buttonCell = new DataGridViewButtonCell
                {
                    Value = i
                };
                dataGridView2[i, 0] = buttonCell;
                dataGridView2[i, i].Value = 0;
                dataGridView2[i, i].Style.BackColor = Drawing.Color.Yellow;
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

        private void CreateEdges()
        {
            for (int i = 1; i < dataGridView2.RowCount - 1; i++)
            {
                for (int j = i + 1; j < dataGridView2.ColumnCount; j++)
                {
                    try
                    {
                        graph.RemoveEdge(graph.Edges.First(item => (item.Source.Equals(i.ToString()) && item.Target.Equals(j.ToString()))));
                    }
                    catch
                    {

                    }
                    try
                    {
                        graph.RemoveEdge(graph.Edges.First(item => (item.Source.Equals(j.ToString()) && item.Target.Equals(i.ToString()))));
                    }
                    catch
                    {

                    }
                    int value1 = Convert.ToInt32(dataGridView2[j, i].Value);
                    int value2 = Convert.ToInt32(dataGridView2[i, j].Value);
                    if (value1 != 0 && value2 != 0)
                    {
                        Edge edge = graph.AddEdge(i.ToString(), value1.ToString(), j.ToString());
                        if (value1 == value2)
                        {
                            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
                        }
                        else
                        {
                            edge.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
                            edge = graph.AddEdge(j.ToString(), value2.ToString(), i.ToString());
                            edge.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
                        }
                    }
                    else if (value1 != 0 || value2 != 0)
                    {
                        Edge edge;
                        if (value1 != 0)
                        {
                            edge = graph.AddEdge(i.ToString(), value1.ToString(), j.ToString());
                        }
                        else
                        {
                            edge = graph.AddEdge(j.ToString(), value2.ToString(), i.ToString());
                        }
                        edge.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
                    }
                }
            }
        }


        private void ShowGraph(Graph graph)
        {
            using (GraphForm graphForm = new GraphForm(graph))
            {
                graphForm.ShowDialog();
            }
        }

        private void UpdateLabel(int left, int right, double distance)
        {
            label8.Text = string.Format("Минимальное расстояние из узла {0} в узел {1} равно {2}.", left, right, distance);
        }

        private void UpdateLabel(int left, int right, bool found)
        {
            label8.Text = string.Format("Путь из узла {0} в узел {1} {2}найден.", left, right, (found) ? string.Empty : "не ");
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

        private void Button1_Click(object sender, EventArgs e)
        {
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
            else if ((radioButton1.Checked && AlgKMP(richTextBox1.Text, textBox2.Text, out int k)) ||
                     (radioButton2.Checked && AlgBM(richTextBox1.Text, textBox2.Text, out k)))
            {
                label3.Text = "Вывод: Образ найден. Индекс первого вхождения: " + k.ToString() + ".";
            }
            else
            {
                label3.Text = "Вывод: Образ не найден.";
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown1.Value);
        }

        private void NumericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 50 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void DataGridView1_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
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

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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

        private void Button2_Click(object sender, EventArgs e)
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
            else if (radioButton13.Checked)
            {
                sort = BinaryInsertionSort;
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

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
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

        private void NumericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 51 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != e.ColumnIndex)
                {
                    int temp = Convert.ToInt32(dataGridView2[e.ColumnIndex, e.RowIndex].Value);
                    if (temp < 0)
                        throw new Exception();
                    if (dataGridView2[e.ColumnIndex, e.RowIndex].Value == null)
                    {
                        dataGridView2[e.ColumnIndex, e.RowIndex].Value = temp;
                    }
                    ButtonVisible(false);
                    UpdateLabel(string.Empty);
                }
            }
            catch
            {
                dataGridView2[e.ColumnIndex, e.RowIndex].Value = oldValue;
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!semaphore && cell.ColumnNumber != 0 && cell.RowNumber != 0)
            {
                dataGridView2[cell.ColumnNumber, 0].Style.BackColor = Drawing.Color.White;
                dataGridView2[0, cell.RowNumber].Style.BackColor = Drawing.Color.White;
                if (cell.ColumnNumber == cell.RowNumber)
                {
                    dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = Drawing.Color.Yellow;
                }
                else
                {
                    dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = Drawing.Color.White;
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
                            dataGridView2[cell.ColumnNumber, 0].Style.BackColor = Drawing.Color.White;
                            dataGridView2[e.ColumnIndex, 0].Style.BackColor = Drawing.Color.Red;
                            cell.ColumnNumber = e.ColumnIndex;
                        }
                        else
                        {
                            cell.ColumnNumber = e.ColumnIndex;
                            dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = Drawing.Color.Red;
                            semaphore = !semaphore;
                        }
                    }
                    else
                    {
                        dataGridView2[e.ColumnIndex, 0].Style.BackColor = Drawing.Color.Red;
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
                            dataGridView2[0, cell.RowNumber].Style.BackColor = Drawing.Color.White;
                            dataGridView2[0, e.RowIndex].Style.BackColor = Drawing.Color.Red;
                            cell.RowNumber = e.RowIndex;
                        }
                        else
                        {
                            cell.RowNumber = e.RowIndex;
                            dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = Drawing.Color.Red;
                            semaphore = !semaphore;
                        }
                    }
                    else
                    {
                        dataGridView2[0, e.RowIndex].Style.BackColor = Drawing.Color.Red;
                        cell.RowNumber = e.RowIndex;
                        semaphore = !semaphore;
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            CreateEdges();
            ShowGraph(graph);
        }

        private void NumericUpDown3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 49 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int left = Convert.ToInt32(numericUpDown3.Value);
            int right = Convert.ToInt32(numericUpDown4.Value);
            if (right != left)
            {
                int[,] massiv = GetMassiv(dataGridView2.RowCount - 1, dataGridView2.ColumnCount - 1);
                if (radioButton7.Checked)
                {
                    Dictionary<int, Tuple<int, int>[]> adjacencyList = InitAdjacencyListFromAdjacencyMatrix(massiv);
                    path = AlgorithmDijkstra(left - 1, right - 1, massiv.GetLength(0), adjacencyList, out int distance);
                    if (distance != int.MaxValue)
                    {
                        UpdateLabel(left, right, distance);
                        ButtonVisible(true);
                    }
                    else
                    {
                        UpdateLabel(left, right, double.PositiveInfinity);
                        ButtonVisible(false);
                    }
                }
                else if (radioButton14.Checked)
                {
                    Tuple<int, int, int>[] edges = GetEdges(massiv);
                    int[] pathFordBellman = AlgorithmFordBellman(edges, left - 1, right - 1, massiv.GetLength(0), out int distance);
                    if (distance != int.MaxValue)
                    {
                        UpdateLabel(left, right, distance);
                        path = GetPathFordBellman(pathFordBellman, right - 1);
                        ButtonVisible(true);
                    }
                    else
                    {
                        UpdateLabel(left, right, double.PositiveInfinity);
                        ButtonVisible(false);
                    }
                }
                else if (radioButton15.Checked)
                {
                    path = AlgorithmDFS(massiv, left - 1, right - 1, out bool found);
                    UpdateLabel(left, right, found);
                    ButtonVisible(found);
                }
                else if (radioButton16.Checked)
                {
                    int max = AlgorithmMaxStream(massiv, left - 1, right - 1);
                    label8.Text = string.Format("Максимальный поток равен {0}.", max);
                }
                else
                {
                    int[,] pathFloyd = AlgorithmFloyd(massiv, left - 1, right - 1, out int distance);
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

        private int[] GetPathFordBellman(int[] pathFordBellman, int right)
        {
            List<int> path = new List<int>();
            for (int i = right; i != -1; i = pathFordBellman[i])
            {
                path.Add(i + 1);
            }
            return path.ToArray();
        }

        /// <summary>
        /// Алгоритм Форда-Беллмана 
        /// </summary>
        /// <param name="edges">Массив расстояний между узлами</param>
        /// <param name="left">Начальный узел</param>
        /// <param name="right">Конечный узел</param>
        /// <param name="distance">Минимальное расстояние от начального до конечного узла</param>
        /// <returns></returns>
        private int[] AlgorithmFordBellman(Tuple<int, int, int>[] edges, int left, int right, int n, out int distance)
        {
            int[] d = new int[n];
            int[] p = new int[n];
            for (int i = 0; i < n; i++)
            {
                d[i] = int.MaxValue;
                p[i] = -1;
            }
            d[left] = 0;
            while (true)
            {
                bool any = false;
                for (int j = 0; j < edges.Length; ++j)
                {
                    if (d[edges[j].Item1] < int.MaxValue)
                    {
                        if (d[edges[j].Item2] > d[edges[j].Item1] + edges[j].Item3)
                        {
                            d[edges[j].Item2] = d[edges[j].Item1] + edges[j].Item3;
                            p[edges[j].Item2] = edges[j].Item1;
                            any = true;
                        }
                    }
                }
                if (!any)
                {
                    break;
                }
            }
            distance = d[right];
            return p;
        }

        private Tuple<int, int, int>[] GetEdges(int[,] massiv)
        {
            List<Tuple<int, int, int>> edges = new List<Tuple<int, int, int>>();
            int width = massiv.GetLength(0);
            int height = massiv.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (massiv[i, j] > 0)
                    {
                        edges.Add(new Tuple<int, int, int>(i, j, massiv[i, j]));
                    }
                }
            }
            return edges.ToArray();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            CreateEdges();
            SetEdgeColor(path, Color.Red);
            ShowGraph(graph);
            SetEdgeColor(path, Color.Black);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            int[,] graph = AlgorithmFloyd(GetMassiv(dataGridView2.RowCount - 1, dataGridView2.ColumnCount - 1));
            int[] maxDistances = new int[graph.GetLength(1)];
            for (int i = 0; i < maxDistances.Length; i++)
            {
                maxDistances[i] = MaxInColumn(i, graph);
            }
            UpdateLabel(IndexMinInMassiv(maxDistances));
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
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

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
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

        private void Button7_Click(object sender, EventArgs e)
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

        private void Panel3_Paint(object sender, PaintEventArgs e)
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

        private void Panel3_MouseMove(object sender, MouseEventArgs e)
        {
            label16.Text = e.Location.ToString();
            Drawing.Point point = Romb.Check(rombMesh, e.Location);
            if (point.X != -1)
            {
                label17.Text = point.ToString();
            }
            else label17.Text = string.Empty;
        }

        private void Panel3_MouseLeave(object sender, EventArgs e)
        {
            label17.Text = string.Empty;
            label16.Text = string.Empty;
        }

        private void Panel3_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void Button10_Click(object sender, EventArgs e)
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

        private void Button8_Click(object sender, EventArgs e)
        {
            double[] x = new double[dataGridView3.RowCount];
            double[] y = new double[dataGridView3.RowCount];
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                try
                {
                    x[i] = Convert.ToDouble(dataGridView3[0, i].Value);
                }
                catch { }
                try
                {
                    y[i] = Convert.ToDouble(dataGridView3[1, i].Value);
                }
                catch { }
            }
            int n = Convert.ToInt32(numericUpDown7.Value);
            double[] a = MethodMinSquares(x, y, n);
            listBox1.Items.Clear();
            foreach (double item in a)
            {
                listBox1.Items.Add(item);
            }
        }

        /// <summary>
        /// Метод минимальных квадратов
        /// </summary>
        /// <param name="x">Координаты X</param>
        /// <param name="y">Координаты Y</param>
        /// <param name="n">Степень полинома</param>
        /// <returns>Коэффициенты полинома</returns>
        private double[] MethodMinSquares(double[] x, double[] y, int n)
        {
            int m = n + 1;
            double[,] matrix = new double[m, m];
            double[] b = new double[m];
            double[] a = new double[m];
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = i; j >= 1; j--)
                {
                    if (x[j] < x[j - 1])
                    {
                        double t = x[j - 1];
                        x[j - 1] = x[j];
                        x[j] = t;
                        t = y[j - 1];
                        y[j - 1] = y[j];
                        y[j] = t;
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    for (int k = 0; k < x.Length; k++)
                    {
                        matrix[i, j] += Math.Pow(x[k], i + j);
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int k = 0; k < x.Length; k++)
                {
                    b[i] += Math.Pow(x[k], i) * y[k];
                }
            }
            for (int k = 0; k < m; k++)
            {
                for (int i = k + 1; i < m; i++)
                {
                    double tmp = matrix[i, k] / matrix[k, k];
                    for (int j = k; j < m; j++)
                    {
                        matrix[i, j] -= tmp * matrix[k, j];
                    }
                    b[i] -= tmp * b[k];
                }
            }
            for (int i = n; i >= 0; i--)
            {
                double tmp = 0;
                for (int j = i; j < m; j++)
                {
                    tmp += matrix[i, j] * a[j];
                }
                a[i] = (b[i] - tmp) / matrix[i, i];
            }
            return a;
        }

        private void NumericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            while (dataGridView3.Rows.Count < numericUpDown8.Value)
            {
                dataGridView3.Rows.Add();
            }
            while (dataGridView3.Rows.Count > numericUpDown8.Value)
            {
                dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 1);
            }
            numericUpDown7.Maximum = numericUpDown8.Value - 1;
        }

        private void NumericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            while (dataGridView4.Rows.Count < numericUpDown10.Value)
            {
                dataGridView4.Rows.Add();
            }
            while (dataGridView4.Rows.Count > numericUpDown10.Value)
            {
                dataGridView4.Rows.RemoveAt(dataGridView4.Rows.Count - 1);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            double[] x = new double[dataGridView4.RowCount];
            double[] y = new double[dataGridView4.RowCount];
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                try
                {
                    x[i] = Convert.ToDouble(dataGridView4[0, i].Value);
                }
                catch { }
                try
                {
                    y[i] = Convert.ToDouble(dataGridView4[1, i].Value);
                }
                catch { }
            }
            double value = Convert.ToDouble(numericUpDown11.Value);
            double l = PolynomialLagrange(x, y, value);
            label24.Text = l.ToString();
        }

        /// <summary>
        /// Интерполяционный полином Лагранжа
        /// </summary>
        /// <param name="x">Координаты X</param>
        /// <param name="y">Координаты Y</param>
        /// <param name="value">Искомая точка</param>
        /// <returns>Искомое значение</returns>
        private double PolynomialLagrange(double[] x, double[] y, double value)
        {
            double s = 0;
            int n = x.Length;
            for (int i = 0; i < n; i++)
            {
                double p = 1.0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        p *= (value - x[j]) / (x[i] - x[j]);
                    }
                }
                s += y[i] * p;
            }
            return s;
        }
    }
}