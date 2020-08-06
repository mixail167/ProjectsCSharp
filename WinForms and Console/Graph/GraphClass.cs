using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    class GraphClass
    {
        int[,] adjacencyMatrix;
        int[,] incidenceMatrix;
        Dictionary<int, int[]> adjacencyList;
        Tuple<int, int>[] listEdges;

        public static GraphClass NewInstanceByAdjacencyMatrix(char[,] matrix)
        {
            GraphClass graphClass = new GraphClass();
            graphClass.adjacencyMatrix = graphClass.InitAdjacencyMatrix(matrix);
            graphClass.incidenceMatrix = graphClass.InitIncidenceMatrixFromAdjacencyMatrix();
            graphClass.adjacencyList = graphClass.InitAdjacencyListFromAdjacencyMatrix();
            graphClass.listEdges = graphClass.InitListEdgesFromIncidenceMatrix();
            return graphClass;
        }

        public static GraphClass NewInstanceByIncidenceyMatrix(char[,] matrix)
        {
            GraphClass graphClass = new GraphClass();
            graphClass.incidenceMatrix = graphClass.InitIncidenceMatrix(matrix);
            graphClass.adjacencyMatrix = graphClass.InitAdjacencyMatrixFromIncidenceMatrix();
            graphClass.adjacencyList = graphClass.InitAdjacencyListFromAdjacencyMatrix();
            graphClass.listEdges = graphClass.InitListEdgesFromIncidenceMatrix();
            return graphClass;
        }

        public static GraphClass NewInstanceByListEdges(string[] list)
        {
            GraphClass graphClass = new GraphClass();
            graphClass.listEdges = graphClass.InitListEdges(list);
            graphClass.adjacencyMatrix = graphClass.InitAdjacencyMatrixFromListEdges();
            graphClass.incidenceMatrix = graphClass.InitIncidenceMatrixFromAdjacencyMatrix();
            graphClass.adjacencyList = graphClass.InitAdjacencyListFromAdjacencyMatrix();
            return graphClass;
        }

        public static GraphClass NewInstanceByAdjacencyList(string[] list)
        {
            GraphClass graphClass = new GraphClass();
            graphClass.adjacencyList = graphClass.InitAdjacencyList(list);
            graphClass.adjacencyMatrix = graphClass.InitAdjacencyMatrixFromAdjacencyList();
            graphClass.incidenceMatrix = graphClass.InitIncidenceMatrixFromAdjacencyMatrix();
            graphClass.listEdges = graphClass.InitListEdgesFromIncidenceMatrix();
            return graphClass;
        }

        private int[,] InitAdjacencyMatrixFromListEdges()
        {
            int maxItem1 = listEdges.Max(x => x.Item1);
            int maxItem2 = listEdges.Max(x => x.Item2);
            int length;
            if (maxItem1 < maxItem2)
            {
                length = maxItem2;
            }
            else
            {
                length = maxItem1;
            }
            int[,] adjacencyMatrix = new int[length, length];
            foreach (Tuple<int, int> item in listEdges)
            {
                adjacencyMatrix[item.Item1 - 1, item.Item2 - 1] = 1;
            }
            return adjacencyMatrix;
        }

        private Tuple<int, int>[] InitListEdges(string[] list)
        {
            Tuple<int, int>[] listEdges = new Tuple<int, int>[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                string[] parts = list[i].Split(new char[] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
                int begin = Convert.ToInt32(parts[0]);
                int end = Convert.ToInt32(parts[1]);
                listEdges[i] = new Tuple<int, int>(begin, end);
            }
            return listEdges;
        }

        private int[,] InitAdjacencyMatrixFromAdjacencyList()
        {
            int length = adjacencyList.Keys.Max();
            foreach (int[] item in adjacencyList.Values)
            {
                int max = item.Max();
                if (max > length)
                {
                    length = max;
                }
            }
            int[,] adjacencyMatrix = new int[length, length];
            foreach (KeyValuePair<int, int[]> item in adjacencyList)
            {
                int[] array = item.Value;
                int key = item.Key - 1;
                foreach (int value in array)
                {
                    adjacencyMatrix[key, value - 1] = 1;
                }
            }
            return adjacencyMatrix;
        }

        private Dictionary<int, int[]> InitAdjacencyList(string[] list)
        {
            Dictionary<int, int[]> adjacencyList = new Dictionary<int, int[]>();
            foreach (string item in list)
            {
                string[] parts = item.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                int key = Convert.ToInt32(parts[0]);
                if (!adjacencyList.ContainsKey(key))
                {
                    parts = parts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int[] array = new int[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                    {
                        array[i] = Convert.ToInt32(parts[i]);
                    }
                    adjacencyList.Add(key, array);
                }
            }
            return adjacencyList;
        }

        private int[,] InitAdjacencyMatrixFromIncidenceMatrix()
        {
            int rowCount = incidenceMatrix.GetLength(0);
            int columnCount = incidenceMatrix.GetLength(1);
            int[,] adjacencyMatrix = new int[rowCount, rowCount];
            for (int j = 0; j < columnCount; j++)
            {
                int begin = -1;
                int end = -1;
                bool revers = false;
                bool isfound = false;
                for (int i = 0; i < rowCount && !isfound; i++)
                {
                    switch (incidenceMatrix[i, j])
                    {
                        case -1:
                            end = i;
                            isfound = begin != -1;
                            break;
                        case 1:
                            if (begin == -1)
                            {
                                begin = i;
                            }
                            else
                            {
                                end = i;
                                isfound = true;
                                revers = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                adjacencyMatrix[begin, end] = 1;
                if (revers)
                {
                    adjacencyMatrix[end, begin] = 1;
                }
            }
            return adjacencyMatrix;
        }

        private int[,] InitIncidenceMatrix(char[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);
            int[,] incidenceMatrix = new int[rowCount, columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                for (int i = 0, k = 0; i < rowCount && k < 2; i++)
                {
                    switch (matrix[i, j])
                    {
                        case '+':
                            incidenceMatrix[i, j] = 1;
                            k++;
                            break;
                        case '-':
                            incidenceMatrix[i, j] = -1;
                            k++;
                            break;
                        default:
                            break;
                    }
                }
            }
            return incidenceMatrix;
        }

        private Tuple<int, int>[] InitListEdgesFromIncidenceMatrix()
        {
            int rowCount = incidenceMatrix.GetLength(0);
            int columnCount = incidenceMatrix.GetLength(1);
            List<Tuple<int, int>> listEdges = new List<Tuple<int, int>>();
            for (int j = 0; j < columnCount; j++)
            {
                int begin = 0;
                int end = 0;
                bool revers = false;
                bool isfound = false;
                for (int i = 0; i < rowCount && !isfound; i++)
                {
                    switch (incidenceMatrix[i, j])
                    {
                        case -1:
                            end = i + 1;
                            isfound = begin != 0;
                            break;
                        case 1:
                            if (begin == 0)
                            {
                                begin = i + 1;
                            }
                            else
                            {
                                end = i + 1;
                                isfound = true;
                                revers = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                listEdges.Add(new Tuple<int, int>(begin, end));
                if (revers)
                {
                    listEdges.Add(new Tuple<int, int>(end, begin));
                }
            }
            return listEdges.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToArray();
        }

        private Dictionary<int, int[]> InitAdjacencyListFromAdjacencyMatrix()
        {
            Dictionary<int, int[]> adjacencyList = new Dictionary<int, int[]>();
            int length = adjacencyMatrix.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < length; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        list.Add(j + 1);
                    }
                }
                int[] array = list.ToArray();
                if (array.Length > 0)
                {
                    adjacencyList.Add(i + 1, array);
                }

            }
            return adjacencyList;
        }

        private int[,] InitIncidenceMatrixFromAdjacencyMatrix()
        {
            int length = adjacencyMatrix.GetLength(0);
            List<int[]> list = new List<int[]>();
            bool[,] visited = new bool[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (adjacencyMatrix[i, j] == 1 && !visited[i, j])
                    {
                        visited[i, j] = true;
                        int[] edge = new int[length];
                        if (adjacencyMatrix[j, i] == 1)
                        {
                            edge[i] = 1;
                            edge[j] = 1;
                            visited[j, i] = true;
                        }
                        else
                        {
                            edge[i] = 1;
                            edge[j] = -1;
                        }
                        list.Add(edge);
                    }
                }
            }
            int[,] incidenceMatrix = new int[length, list.Count];
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    incidenceMatrix[i, j] = list[j][i];
                }
            }
            return incidenceMatrix;
        }

        private int[,] InitAdjacencyMatrix(char[,] matrix)
        {
            int length = matrix.GetLength(0);
            int[,] adjacencyMatrix = new int[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (matrix[i, j] == '1')
                    {
                        adjacencyMatrix[i, j] = 1;
                    }
                }
            }
            return adjacencyMatrix;
        }
    }
}
