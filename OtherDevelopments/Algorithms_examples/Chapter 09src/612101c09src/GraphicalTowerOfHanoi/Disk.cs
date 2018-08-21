using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace GraphicalTowerOfHanoi
{
    class Disk
    {
        static Brush Brush = Brushes.LightBlue;
        static Pen Pen = Pens.Blue;

        public Rectangle Location;
        public Disk(Rectangle rect)
        {
            Location = rect;
        }
        public void Draw(Graphics gr)
        {
            gr.FillRectangle(Brush, Location);
            gr.DrawRectangle(Pen, Location);
        }

        // The points where the disk should move.
        public List<Point> Points = new List<Point>();

        // Move towards the next point in the Points list.
        // Return true if we have more moving to do.
        public bool Move()
        {
            // Do nothing if there are no points.
            if (Points.Count == 0) return false;

            // Get the direction of movement.
            int dx = Points[0].X - Location.X;
            int dy = Points[0].Y - Location.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < Form1.PixelsPerFrame)
            {
                // Move to the point and remove it from the Points list.
                Location.X = Points[0].X;
                Location.Y = Points[0].Y;
                Points.RemoveAt(0);
                return (Points.Count > 0);
            }
            else
            {
                // Move towards the point.
                Location.X += (int)(dx / distance * Form1.PixelsPerFrame);
                Location.Y += (int)(dy / distance * Form1.PixelsPerFrame);
                return true;
            }
        }
    }
}
