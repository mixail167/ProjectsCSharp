using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveAlgorithmConsole
{
    class Program
    {
        enum CellValue
        {
            StartPosition = 0,
            EmptySpace = -1,
            Destination = -2,
            Path = -3,
            Barrier = -4
        }

        private static int[,] InitMap(int n, int m, int persentage)
        {
            int[,] map = new int[n, m];
            Random r = new Random();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (r.Next(100) > persentage)
                    {
                        map[i, j] = (int)CellValue.Barrier;
                    }
                    else
                    {
                        map[i, j] = (int)CellValue.EmptySpace;
                    }
                }
            }
            return map;
        }

        private static string PrintMap(int[,] map)
        {
            string text = string.Empty;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    switch (map[i, j])
                    {
                        case (int)CellValue.Path:
                            text += string.Format("{0,2}", "+");
                            break;
                        case (int)CellValue.StartPosition:
                            text += string.Format("{0,2}", "S");
                            break;
                        case (int)CellValue.Destination:
                            text += string.Format("{0,2}", "D");
                            break;
                        case (int)CellValue.EmptySpace:
                            text += string.Format("{0,2}", " ");
                            break;
                        case (int)CellValue.Barrier:
                            text += string.Format("{0,2}", "*");
                            break;
                        default:
                            text += string.Format("{0,2}", " ");
                            break;
                    }
                }
                text += "\n";
            }
            return text;
        }

        private static int[,] ModifyCell(int[,] map, int i, int j, CellValue value)
        {
            map[i, j] = (int)value;
            return map;
        }

        private static int[,] WavePropagation(int[,] map, int startX, int startY, int finishX, int finishY)
        {
            if (startX == finishX && startY == finishY)
            {
                return null;
            }
            map[startX, startY] = (int)CellValue.StartPosition;
            map[finishX, finishY] = (int)CellValue.Destination;
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
                        if (map[i, j] == step)
                        {
                            if (i != width - 1)
                                if (map[i + 1, j] == (int)CellValue.EmptySpace) map[i + 1, j] = step + 1;
                            if (j != heigth - 1)
                                if (map[i, j + 1] == (int)CellValue.EmptySpace) map[i, j + 1] = step + 1;
                            if (i != 0)
                                if (map[i - 1, j] == (int)CellValue.EmptySpace) map[i - 1, j] = step + 1;
                            if (j != 0)
                                if (map[i, j - 1] == (int)CellValue.EmptySpace) map[i, j - 1] = step + 1;
                            if (i < width - 1)
                                if (map[i + 1, j] == (int)CellValue.Destination)
                                {
                                    finishX = i + 1;
                                    finishY = j;
                                    finished = true;
                                }
                            if (j < heigth - 1)
                                if (map[i, j + 1] == (int)CellValue.Destination)
                                {
                                    finishX = i;
                                    finishY = j + 1;
                                    finished = true;
                                }
                            if (i > 0)
                                if (map[i - 1, j] == (int)CellValue.Destination)
                                {
                                    finishX = i - 1;
                                    finishY = j;
                                    finished = true;
                                }
                            if (j > 0)
                                if (map[i, j - 1] == (int)CellValue.Destination)
                                {
                                    finishX = i;
                                    finishY = j - 1;
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
                path.Add(new Tuple<int, int>(finishX, finishY));
                do
                {
                    if (finishX < width - 1)
                        if (map[finishX + 1, finishY] == step - 1)
                        {
                            path.Add(new Tuple<int, int>(++finishX, finishY));
                        }
                    if (finishY < heigth - 1)
                        if (map[finishX, finishY + 1] == step - 1)
                        {
                            path.Add(new Tuple<int, int>(finishX, ++finishY));
                        }
                    if (finishX > 0)
                        if (map[finishX - 1, finishY] == step - 1)
                        {
                            path.Add(new Tuple<int, int>(--finishX, finishY));
                        }
                    if (finishY > 0)
                        if (map[finishX, finishY - 1] == step - 1)
                        {
                            path.Add(new Tuple<int, int>(finishX, --finishY));
                        }
                    step--;
                } while (step != 0);
                foreach (Tuple<int, int> item in path)
                {
                    if (item == path.Last())
                        map[item.Item1, item.Item2] = (int)CellValue.StartPosition;
                    else if (item == path.First())
                        map[item.Item1, item.Item2] = (int)CellValue.Destination;
                    else
                        map[item.Item1, item.Item2] = (int)CellValue.Path;
                }
                return map;
            }
            return null;
        }

        static void Main(string[] args)
        {
            int n = 10;
            int m = 10;
            int persantage = 70;
            Random r = new Random();
            int[,] map = InitMap(n, m, persantage);
            int startX = r.Next(map.GetLength(0));
            int startY = r.Next(map.GetLength(1));
            int finishX = r.Next(map.GetLength(0));
            int finishY = r.Next(map.GetLength(1));
            map = WavePropagation(map, startX, startY, finishX, finishY);
            if (map != null)
            {
                Console.WriteLine(PrintMap(map));
            }
            else
            {
                Console.WriteLine("Путь не найден");
            }
            Console.ReadKey();
        }
    }
}
