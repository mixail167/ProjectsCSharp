using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private Cell[,] map; // карта лабиринта
        private List<Cell> path; // путь
        private Cell startPosition; // начальная точка
        private Cell finishPosition; // конечная точка

        public Form2(int width, int height, Algorithms algorithm)
        {
            InitializeComponent();
            if (width % 2 == 0) // если ширина чётная
            {
                width++; // делаем нечётной
            }
            if (height % 2 == 0) // если высота чётная
            {
                height++; // делаем нечётной
            }
            switch (algorithm)
            {
                case Algorithms.DFS:
                    map = GenerateMazeDFS(width, height);
                    break;
                case Algorithms.Prime:
                    map = GenerateMazePrime(width, height);
                    break;
                case Algorithms.Kruskal:
                    map = GenerateMazeKruskal(width, height);
                    break;
                default:
                    break;
            }
            panel1.Invalidate(); // перерисовка панели
        }

        /// <summary>
        /// Поиск и создание пути для Алгоритма Краскала
        /// </summary>
        /// <param name="map">Карта лабиринта</param>
        /// <param name="cell1">Первая ячейка</param>
        /// <param name="cell2">Вторая ячейка</param>
        /// <returns>Обновлённая карта лабиринта</returns>
        private Cell[,] SearchPath(Cell[,] map, Cell cell1, Cell cell2)
        {
            if (!WavePropagationCheck(map, cell1, cell2, out _, out _))
            {
                map = BreakBarrier(map, cell1, cell2);
            }
            map = RefreshMap(map);
            return map;
        }

        /// <summary>
        /// Генератор лабиринта по алгоритму Прайма
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        /// <returns>Карта лабиринта</returns>
        private Cell[,] GenerateMazeKruskal(int width, int height)
        {
            Cell[,] map = InitMap(width, height, CellValues.Empty, CellValues.Barrier, out List<Cell> cells);
            Random random = new Random();
            while (cells.Count != 0)
            {
                int r1 = random.Next(cells.Count);
                Cell curCell = cells[r1];
                Cell nextCell;
                if (curCell.IsLeftDirection)
                {
                    nextCell = map[curCell.X - 2, curCell.Y];
                }
                else
                {
                    nextCell = map[curCell.X, curCell.Y - 2];
                }
                map = SearchPath(map, curCell, nextCell);
                cells.RemoveAt(r1);
            }
            return map;
        }

        /// <summary>
        /// Волновой алгоритм (проверка наличия пути распространением волны)
        /// </summary>
        /// <param name="map">Карта</param>
        /// <param name="startPosition">Начальная позиция</param>
        /// <param name="finishPosition">Целевая позиция</param>
        /// <param name="finishPositionOut">Целевая позиция для восстановления пути</param>
        /// <param name="stepOut">Шаг распространения волны</param>
        /// <returns>Статус наличия пути</returns>
        private bool WavePropagationCheck(Cell[,] map, Cell startPosition, Cell finishPosition, out Cell finishPositionOut, out int stepOut)
        {
            int width = map.GetLength(0);
            int heigth = map.GetLength(1);
            Cell tmpFinishPosition = new Cell(finishPosition);
            map[startPosition.X, startPosition.Y].Value = CellValues.StartPosition.ToInt(); // указываем Старт и
            map[tmpFinishPosition.X, tmpFinishPosition.Y].Value = CellValues.Destination.ToInt(); // Финиш на карте
            int step = 0; // шаг распространения волны
            bool isFinished = false;
            do
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < heigth; j++)
                    {
                        if (map[i, j].Value == step) // ячейка с конечным шагом распространения волны
                        {
                            if (i != width - 1)
                                if (map[i + 1, j].Value == CellValues.Empty.ToInt()) map[i + 1, j].Value = step + 1; // распространяем волну
                            if (j != heigth - 1)
                                if (map[i, j + 1].Value == CellValues.Empty.ToInt()) map[i, j + 1].Value = step + 1;
                            if (i != 0)
                                if (map[i - 1, j].Value == CellValues.Empty.ToInt()) map[i - 1, j].Value = step + 1;
                            if (j != 0)
                                if (map[i, j - 1].Value == CellValues.Empty.ToInt()) map[i, j - 1].Value = step + 1;
                            if (i < width - 1)
                                if (map[i + 1, j].Value == CellValues.Destination.ToInt())
                                {
                                    tmpFinishPosition.X = i + 1;
                                    tmpFinishPosition.Y = j;
                                    isFinished = true; // достигнут финиш
                                }
                            if (j < heigth - 1)
                                if (map[i, j + 1].Value == CellValues.Destination.ToInt())
                                {
                                    tmpFinishPosition.X = i;
                                    tmpFinishPosition.Y = j + 1;
                                    isFinished = true;
                                }
                            if (i > 0)
                                if (map[i - 1, j].Value == CellValues.Destination.ToInt())
                                {
                                    tmpFinishPosition.X = i - 1;
                                    tmpFinishPosition.Y = j;
                                    isFinished = true;
                                }
                            if (j > 0)
                                if (map[i, j - 1].Value == CellValues.Destination.ToInt())
                                {
                                    tmpFinishPosition.X = i;
                                    tmpFinishPosition.Y = j - 1;
                                    isFinished = true;
                                }
                        }
                    }
                }
                step++;
            } while (!isFinished && step < width * heigth); // пока финиш не достигнут и шаг меньше максимально возможного
            finishPositionOut = tmpFinishPosition;
            stepOut = step;
            return isFinished;
        }

        /// <summary>
        /// Начальная инициализация карты лабиринта для Алгоритма Краскала
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="cellValues1">Первое значение</param>
        /// <param name="cellValues2">Второе значение</param>
        /// <param name="cells">Cписок ячеек</param>
        /// <returns>Карта лабиринта</returns>
        private Cell[,] InitMap(int width, int height, CellValues cellValues1, CellValues cellValues2, out List<Cell> cells)
        {
            Cell[,] map = new Cell[width, height]; // инициализация карты
            List<Cell> list = new List<Cell>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0) // создание решётки
                    {
                        map[i, j] = new Cell(i, j, (int)cellValues1);
                        if (j > 1)
                        {
                            list.Add(new Cell(map[i, j])); // cписок ячеек для Алгоритма Краскала
                        }
                        if (i > 1)
                        {
                            list.Add(new Cell(map[i, j], true)); // в двух экземплярах с указанием направления разрушения стен
                        }
                    }
                    else
                        map[i, j] = new Cell(i, j, (int)cellValues2);
                }
            }
            cells = list;
            return map;
        }

        /// <summary>
        /// Начальная инициализация карты лабиринта
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="cellValues1">Первое значение</param>
        /// <param name="cellValues2">Второе значение</param>
        /// <returns>Карта лабиринта</returns>
        private Cell[,] InitMap(int width, int height, CellValues cellValues1, CellValues cellValues2)
        {
            Cell[,] map = new Cell[width, height]; // инициализация карты
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0) // создание решётки
                    {
                        map[i, j] = new Cell(i, j, (int)cellValues1);
                    }
                    else
                        map[i, j] = new Cell(i, j, (int)cellValues2);
                }
            }
            return map;
        }

        /// <summary>
        /// Разрушение стен между соседями
        /// </summary>
        /// <param name="map">Карта лабиринта</param>
        /// <param name="cell1">Первая ячейка</param>
        /// <param name="cell2">Вторая ячейка</param>
        /// <returns>Обновлённая карта лабиринта</returns>
        private Cell[,] BreakBarrier(Cell[,] map, Cell cell1, Cell cell2)
        {
            if (cell2.X != cell1.X) // проверка относительного расположения ячеек
            {
                if (cell1.X > cell2.X)
                {
                    map[cell2.X + 1, cell2.Y].Value = CellValues.Empty.ToInt();
                }
                else
                {
                    map[cell2.X - 1, cell2.Y].Value = CellValues.Empty.ToInt();
                }
            }
            if (cell2.Y != cell1.Y)
            {
                if (cell1.Y > cell2.Y)
                {
                    map[cell2.X, cell2.Y + 1].Value = CellValues.Empty.ToInt();
                }
                else
                {
                    map[cell2.X, cell2.Y - 1].Value = CellValues.Empty.ToInt();
                }
            }
            return map;
        }

        /// <summary>
        /// Установка стен
        /// </summary>
        /// <param name="map">Карта лабиринта</param>
        /// <param name="neighbours">Список ячеек</param>
        /// <returns>Обновлённая карта лабиринта</returns>
        private Cell[,] SetBarrier(Cell[,] map, ref List<Cell> neighbours)
        {
            foreach (Cell item in neighbours)
            {
                item.Value = CellValues.Barrier.ToInt();
                map[item.X, item.Y].Value = item.Value;
            }
            return map;
        }

        /// <summary>
        /// Генератор лабиринта по алгоритму Прайма
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        /// <returns>Карта лабиринта</returns>
        private Cell[,] GenerateMazePrime(int width, int height)
        {
            Cell[,] map = InitMap(width, height, CellValues.Outside, CellValues.Barrier);
            Random random = new Random();
            int r_x = random.Next(1, width - 1); // получаем случайную 
            int r_y = random.Next(1, height - 1); // Outside ячейку
            if (r_x % 2 == 0)
            {
                r_x--;
            }
            if (r_y % 2 == 0)
            {
                r_y--;
            }
            map[r_x, r_y].Value = CellValues.Empty.ToInt(); // делаем её Inside
            List<Cell> neighbours1 = GetNeighbours(map, map[r_x, r_y], CellValues.Outside.ToInt()); // получаем список Outside ячеек
            while (neighbours1.Count != 0) // список не пуст
            {
                map = SetBarrier(map, ref neighbours1); // устанавливаем стены вместо соседних Outside ячеек
                int r1 = random.Next(neighbours1.Count); // получаем случайную из соседних Outside ячеек
                map[neighbours1[r1].X, neighbours1[r1].Y].Value = CellValues.Empty.ToInt(); // делаем её Inside
                List<Cell> neighbours2 = GetNeighbours(map, neighbours1[r1], CellValues.Outside.ToInt()); // получаем список соседних Outside ячеек к новой Inside ячейке
                if (neighbours2.Count != 0) // список не пуст
                {
                    map = SetBarrier(map, ref neighbours2); // устанавливаем стены вместо соседних Outside ячеек
                    neighbours1.AddRange(neighbours2); // добавляем новые соседние Outside ячейки в список
                }
                neighbours2 = GetNeighbours(map, neighbours1[r1], CellValues.Empty.ToInt()); // получаем список соседних Inside ячеек к новой Inside ячейке
                if (neighbours2.Count != 0) // список не пуст
                {
                    int r2 = random.Next(neighbours2.Count); // получаем случайную Inside ячейку
                    map = BreakBarrier(map, neighbours1[r1], neighbours2[r2]);
                }
                neighbours1.RemoveAt(r1); // удаляем выбранную Inside ячейку из списка Outside ячеек
            }
            return map;
        }

        /// <summary>
        /// Получение соседей заданной ячейки
        /// </summary>
        /// <param name="map">Карта лабиринта</param>
        /// <param name="localCell">Заданная ячейка</param>
        /// <param name="value">Значение ячейки</param>
        /// <returns>Список соседей</returns>
        private List<Cell> GetNeighbours(Cell[,] map, Cell localCell, int value)
        {
            int width = map.GetLength(0); // ширина лабиринта
            int height = map.GetLength(1); // высота лабиринта
            Cell[] possibleNeighbours = new[] // список всех возможных соседeй
            {
                new Cell(localCell.X, localCell.Y - 2), // сверху
                new Cell(localCell.X + 2, localCell.Y), // справа
                new Cell(localCell.X, localCell.Y + 2), // снизу
                new Cell(localCell.X - 2, localCell.Y) // слева
            };
            List<Cell> neighbours = new List<Cell>(); // список соседей
            for (int i = 0; i < 4; i++) // проверяем все 4 направления
            {
                if (possibleNeighbours[i].X > 0 && possibleNeighbours[i].X < width - 1 && possibleNeighbours[i].Y > 0 && possibleNeighbours[i].Y < height - 1 && // если сосед не выходит за стены лабиринта
                    map[possibleNeighbours[i].X, possibleNeighbours[i].Y].Value == value)
                    neighbours.Add(possibleNeighbours[i]); // добавляем соседа
            }
            return neighbours;
        }


        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (map != null) // если лабиринт построен
            {
                PaintPanel(e.Graphics);
                PaintPoint(e.Graphics);
            }
        }

        /// <summary>
        /// Генерация лабиринта по алгоритму DFS
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        /// <returns></returns>
        private Cell[,] GenerateMazeDFS(int width, int height)
        {
            Cell[,] map = InitMap(width, height, CellValues.Empty, CellValues.Barrier);
            Random random = new Random();
            Stack<Cell> path = new Stack<Cell>();
            map[1, 1].IsVisited = true; // выбираем начальную ячеёку и делаем её посещённой
            path.Push(map[1, 1]); // добавляем её в стек
            while (path.Count > 0) // пока остались непосещённые ячейки
            {
                Cell cell = path.Peek(); // получаем ячейку из стека
                List<Cell> nextStep = new List<Cell>(); // инициализируем список соседних непосещённых пустых ячеек
                if (cell.X > 1 && !map[cell.X - 2, cell.Y].IsVisited)
                    nextStep.Add(map[cell.X - 2, cell.Y]); // добавляем ячейку в список (игнорируя стены)
                if (cell.X < width - 2 && !map[cell.X + 2, cell.Y].IsVisited)
                    nextStep.Add(map[cell.X + 2, cell.Y]);
                if (cell.Y > 1 && !map[cell.X, cell.Y - 2].IsVisited)
                    nextStep.Add(map[cell.X, cell.Y - 2]);
                if (cell.Y < height - 2 && !map[cell.X, cell.Y + 2].IsVisited)
                    nextStep.Add(map[cell.X, cell.Y + 2]);
                if (nextStep.Count > 0) // если есть соседние непосещённые пустые ячейки 
                {
                    Cell next = nextStep[random.Next(nextStep.Count)]; // берём случайную ячейку из списка соседних
                    map = BreakBarrier(map, cell, next);
                    next.IsVisited = true; // случайная ячейка посещена
                    path.Push(next); // добавляем случайную ячейку в стек
                }
                else
                {
                    path.Pop(); // получаем ячейку из стека
                }
            }
            return map;
        }

        private void Panel1_SizeChanged(object sender, EventArgs e) // событие изменения размеров панели
        {
            panel1.Invalidate();
        }

        /// <summary>
        /// Рисование лабиринта
        /// </summary>
        /// <param name="graphics">Поверхность рисования</param>
        private void PaintPanel(Graphics graphics)
        {
            int width = map.GetLength(0); // ширина лабиринта
            int height = map.GetLength(1); // высота лабиринта
            float widthBlock = panel1.Width * 1.0f / width; // размер блока по ширине
            float heightBlock = panel1.Height * 1.0f / height; // размер блока по высоте
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j].Value == CellValues.Barrier.ToInt()) // если стена
                    {
                        graphics.FillRectangle(Brushes.Black, new RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock)); // рисуем стену
                    }
                    else
                    {
                        graphics.FillRectangle(Brushes.White, new RectangleF(i * widthBlock, j * heightBlock, widthBlock, heightBlock)); // рисуем проход
                    }
                }
            }
        }

        /// <summary>
        /// Рисование точек пути
        /// </summary>
        /// <param name="graphics">Поверхность рисования</param>
        private void PaintPoint(Graphics graphics)
        {
            int width = map.GetLength(0); // ширина лабиринта
            int height = map.GetLength(1); // высота лабиринта
            float widthBlock = panel1.Width * 1.0f / width; // размер блока по ширине
            float heightBlock = panel1.Height * 1.0f / height; // размер блока по высоте
            if (startPosition != null)
            {
                graphics.FillRectangle(Brushes.Yellow, new RectangleF(startPosition.X * widthBlock, startPosition.Y * heightBlock, widthBlock, heightBlock)); // рисуем Старт
            }
            if (finishPosition != null)
            {
                graphics.FillRectangle(Brushes.Red, new RectangleF(finishPosition.X * widthBlock, finishPosition.Y * heightBlock, widthBlock, heightBlock)); // рисуем Финиш
            }
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            if (path != null) // если путь построен
            {
                PaintPanel(e.Graphics);
                int width = map.GetLength(0); // ширина лабиринта
                int height = map.GetLength(1); // высота лабиринта
                float widthBlock = panel1.Width * 1.0f / width; // размер блока по ширине
                float heightBlock = panel1.Height * 1.0f / height; // размер блока по высоте
                foreach (Cell item in path)
                {
                    e.Graphics.FillRectangle(Brushes.Blue, new RectangleF(item.X * widthBlock, item.Y * heightBlock, widthBlock, heightBlock));
                }
                PaintPoint(e.Graphics);
            }
        }

        private void Pane2_SizeChanged(object sender, EventArgs e)
        {
            panel2.Invalidate();
        }

        /// <summary>
        /// Получение непосещённых соседей заданной ячейки
        /// </summary>
        /// <param name="map">Карта лабиринта</param>
        /// <param name="localCell">Заданная ячейка</param>
        /// <returns></returns>
        private List<Cell> GetNeighbours(Cell[,] map, Cell localCell)
        {
            int width = map.GetLength(0); // ширина лабиринта
            int height = map.GetLength(1); // высота лабиринта
            Cell[] possibleNeighbours = new[] // список всех возможных соседeй
            {
                new Cell(localCell.X, localCell.Y - 1), // сверху
                new Cell(localCell.X + 1, localCell.Y), // справа
                new Cell(localCell.X, localCell.Y + 1), // снизу
                new Cell(localCell.X - 1, localCell.Y) // слева
            };
            List<Cell> neighbours = new List<Cell>(); // список соседей
            for (int i = 0; i < 4; i++) // проверяем все 4 направления
            {
                if (possibleNeighbours[i].X > 0 && possibleNeighbours[i].X < width && possibleNeighbours[i].Y > 0 && possibleNeighbours[i].Y < height && // если сосед не выходит за стену лабиринта
                    map[possibleNeighbours[i].X, possibleNeighbours[i].Y].Value != CellValues.Barrier.ToInt() && !map[possibleNeighbours[i].X, possibleNeighbours[i].Y].IsVisited)  // и не является стеной и непосещён
                    neighbours.Add(possibleNeighbours[i]); // добавляем соседа
            }
            return neighbours;
        }

        /// <summary>
        /// Алгоритм DFS
        /// </summary>
        /// <param name="map">Карта лабиринта</param>
        /// <param name="start">Стартовая ячейка</param>
        /// <param name="finish">Финишная ячейка</param>
        /// <returns></returns>
        private List<Cell> DFS(Cell[,] map, Cell start, Cell finish)
        {
            bool isFinish = false; //флаг достижения финиша
            Random random = new Random();
            Stack<Cell> stack = new Stack<Cell>(); // список посещённых ячеек
            List<Cell> path = new List<Cell>(); // путь
            int width = map.GetLength(0); // ширина лабиринта
            int height = map.GetLength(1); // высота лабиринта
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j].Equals(start))
                    {
                        map[i, j].IsVisited = true; // стартовая ячейка становится посещённой
                        stack.Push(map[i, j]); // и добавляется в список
                    }
                    else if (map[i, j].Value != CellValues.Barrier.ToInt()) // если ячейка не является стеной 
                    {
                        map[i, j].IsVisited = false; // значит непосещена
                    }
                }
            }
            while (stack.Count != 0) //пока в списке есть ячейки
            {
                if (stack.Peek().Equals(finish))
                {
                    isFinish = true; // финиш достигнут
                    stack.Pop(); // финишная ячейка в пути не нужна
                }
                if (!isFinish)
                {
                    List<Cell> neighbours = GetNeighbours(map, stack.Peek());
                    if (neighbours.Count != 0)
                    {
                        Cell nextCell = neighbours[random.Next(neighbours.Count)]; // выбираем случайную непосещённую ячейку
                        nextCell.IsVisited = true; // делаем выбранную ячейку посещенной
                        map[nextCell.X, nextCell.Y].IsVisited = nextCell.IsVisited;
                        stack.Push(nextCell); // затем добавляем её в список
                    }
                    else
                    {
                        stack.Pop(); // соседей не осталось
                    }
                }
                else
                {
                    if (!stack.Peek().Equals(start)) // стартовая ячейка в пути не нужна
                    {
                        path.Add(stack.Peek());
                    }
                    stack.Pop();
                }
            }
            return path;
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                startPosition = null; // очистка
                finishPosition = null; // точек пути
                panel1.Invalidate();
            }
            else
            {
                int width = map.GetLength(0);
                int height = map.GetLength(1);
                double widthBlock = panel1.Width * 1.0f / width;
                double heightBlock = panel1.Height * 1.0f / height;
                int x = (int)Math.Truncate(e.X / widthBlock); // получение
                int y = (int)Math.Truncate(e.Y / heightBlock); // координат
                if (map[x, y].Value != CellValues.Barrier.ToInt())
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        startPosition = new Cell(x, y, CellValues.StartPosition.ToInt()); // инициализация начальной точки
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        finishPosition = new Cell(x, y, CellValues.Destination.ToInt()); // инициализация конечной точки
                    }
                    panel1.Invalidate();
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (startPosition != null && finishPosition != null) // если указаны точки пути
            {
                if (radioButton1.Checked)
                {
                    path = WavePropagation(map, startPosition, finishPosition);
                    map = RefreshMap(map);
                }
                else
                {
                    path = DFS(map, startPosition, finishPosition);
                }
            }
            else
            {
                path = null; // путь не построен
            }
            panel2.Invalidate();
        }

        /// <summary>
        /// Очистка карты от волны
        /// </summary>
        /// <param name="map">Карта лабиринта</param>
        /// <returns>Обновлённая карта лабиринта</returns>
        private Cell[,] RefreshMap(Cell[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j].Value != CellValues.Barrier.ToInt())
                        map[i, j].Value = CellValues.Empty.ToInt();
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
        private List<Cell> WavePropagation(Cell[,] map, Cell startPosition, Cell finishPosition)
        {
            int width = map.GetLength(0);
            int heigth = map.GetLength(1);
            if (WavePropagationCheck(map, startPosition, finishPosition, out Cell tmpFinishPosition, out int step)) // финиш достигнут
            {
                List<Cell> path = new List<Cell>(); //путь
                do // восстановление пути
                {
                    if (tmpFinishPosition.X < width - 1)
                        if (map[tmpFinishPosition.X + 1, tmpFinishPosition.Y].Value == step - 1)  // поиск ячейки предыдущего шага
                        {
                            path.Add(new Cell(++tmpFinishPosition.X, tmpFinishPosition.Y)); // добавляем ячейку в путь
                        }
                    if (tmpFinishPosition.Y < heigth - 1)
                        if (map[tmpFinishPosition.X, tmpFinishPosition.Y + 1].Value == step - 1)
                        {
                            path.Add(new Cell(tmpFinishPosition.X, ++tmpFinishPosition.Y));
                        }
                    if (tmpFinishPosition.X > 0)
                        if (map[tmpFinishPosition.X - 1, tmpFinishPosition.Y].Value == step - 1)
                        {
                            path.Add(new Cell(--tmpFinishPosition.X, tmpFinishPosition.Y));
                        }
                    if (tmpFinishPosition.Y > 0)
                        if (map[tmpFinishPosition.X, tmpFinishPosition.Y - 1].Value == step - 1)
                        {
                            path.Add(new Cell(tmpFinishPosition.X, --tmpFinishPosition.Y));
                        }
                    step--;
                } while (step > 0); // пока не дошли до Старта
                return path;
            }
            return null;
        }
    }
}
