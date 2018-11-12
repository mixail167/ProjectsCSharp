using System.Windows.Media.Imaging;

namespace MineSweeper.Classes
{
    enum CellIntState
    {
        Empty, Bomb, Number
    }

    enum CellExtState
    {
        Empty, Flag, Question
    }

    class Cell
    {
        int x;
        int y;
        bool isOpen;
        CellExtState extState;
        CellIntState intState;
        BitmapImage extImage;
        BitmapImage intImage;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            extImage = Images.CellExtStateEmpty;
        }

        public CellExtState ExtState
        {
            get { return extState; }
            set
            {
                extState = value;
                switch (extState)
                {
                    case CellExtState.Empty:
                        extImage = Images.CellExtStateEmpty;
                        break;
                    case CellExtState.Flag:
                        extImage = Images.CellExtStateFlag;
                        break;
                    case CellExtState.Question:
                        extImage = Images.CellExtStateQuestion;
                        break;
                }
            }
        }

        public CellIntState IntState
        {
            get { return intState; }
            set
            {
                intState = value;
                if (intState == CellIntState.Bomb)
                {
                    intImage = Images.Bomb;
                }
            }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }

        public BitmapImage ExtImage
        {
            get { return extImage; }
        }

        public BitmapImage IntImage
        {
            get { return intImage; }
            set { intImage = value; }
        }
    }
}
