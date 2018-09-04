using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Algorithms
{
    class Sprait: IComparer<Sprait>
    {
        private Bitmap bitmap;
        private PointF pointF;

        public Sprait(Bitmap bitmap, PointF pointF)
        {
            this.bitmap = bitmap;
            this.pointF = pointF;
        }

        public PointF Point
        {
            get { return pointF; }
            set { this.pointF = value; }
        }

        public Bitmap Image
        {
            get { return bitmap; }
            set { this.bitmap = value; }
        }

        public int Compare(Sprait x, Sprait y)
        {
            if (x.Point.X>y.Point.X)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
