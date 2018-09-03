
namespace Algorithms
{
    enum CellValue
    {
        StartPosition = 0,
        EmptySpace = -1,
        Destination = -2,
        Path = -3,
        Barrier = -4
    }
		
    class Cell
    {
        private int x;
        private int y;
        private bool visited;
        private int value;

        public Cell(int x, int y, int value = 0)
        {
            this.visited = false;
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public Cell (Cell cell)
        {
            try
            {
                this.x = cell.x;
                this.y = cell.y;
                this.visited = cell.visited;
                this.value = cell.value;
            }
            catch 
            {

            }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public bool Visited
        {
            get { return visited; }
            set { visited = value; }
        }

        public static Cell[,] CopyMatrix(Cell[,] map)
        {
            Cell[,] map1 = new Cell[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map1.GetLength(0); i++)
            {
                for (int j = 0; j < map1.GetLength(1); j++)
                {
                    map1[i, j] = new Cell(map[i, j]);
                }
            }
            return map1;
        }
    }
}
