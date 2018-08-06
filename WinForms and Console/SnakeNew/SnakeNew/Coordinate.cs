
namespace SnakeNew
{
    class Coordinate
    {
        int x;
        int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
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

        public Coordinate Copy()
        {
            return new Coordinate(this.x, this.y);
        }

        public bool Equals(Coordinate coordinate)
        {
            if (this.x == coordinate.x && this.y == coordinate.y)
            {
                return true;
            }
            return false;
        }
    }
}
