using System;

namespace WindowsFormsApp1
{
    class Cell : IEquatable<Cell>
    {
        public Cell(int x, int y, int value = 0)
        {
            IsVisited = false;
            X = x;
            Y = y;
            Value = value;
        }

        public Cell(Cell cell, bool isLeftDirection = false)
        {
            if (cell != null)
            {
                X = cell.X;
                Y = cell.Y;
                IsVisited = cell.IsVisited;
                Value = cell.Value;
                IsLeftDirection = isLeftDirection;
            }
        }

        public int Value { get; set; } // значение ячейки

        public int X { get; set; } // координата X

        public int Y { get; set; } // координата Y

        public bool IsVisited { get; set; } // статус посещения ячейки

        public bool IsLeftDirection { get; set; } // направление разрушения стен влево

        public bool Equals(Cell obj)
        {
            return X == obj.X && Y == obj.Y;
        }

        /// <summary>
        /// Копирование карты лабиринта
        /// </summary>
        /// <param name="map">Исходная карта лабиринта</param>
        /// <returns>Копия исходной карты лабиринта</returns>
        public static Cell[,] CopyMap(Cell[,] map)
        {
            Cell[,] mapOut = new Cell[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < mapOut.GetLength(0); i++)
            {
                for (int j = 0; j < mapOut.GetLength(1); j++)
                {
                    mapOut[i, j] = new Cell(map[i, j]);
                }
            }
            return mapOut;
        }
    }
}
