using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace GraphicalTowerOfHanoi
{
    // Represents moving the top disk from FromPeg to ToPeg.
    class Move
    {
        public int FromPeg, ToPeg;
        public Move(int fromPeg, int toPeg)
        {
            FromPeg = fromPeg;
            ToPeg = toPeg;
        }

        // Prepare the indicated disk to move.
        public void MakeMovePoints(Stack<Disk>[] pegs, Rectangle[] pegLocations)
        {
            // Remove the disk from FromPeg.
            Disk disk = pegs[FromPeg].Pop();

            // Create the disk's movement points.
            disk.Points.Add(new Point(disk.Location.X, Form1.XMargin));
            int x = pegLocations[ToPeg].X + Form1.PegWidth / 2 - disk.Location.Width / 2;
            disk.Points.Add(new Point(x, Form1.XMargin));
            int y = pegLocations[ToPeg].Bottom -
                Form1.DiskHeight * (pegs[ToPeg].Count + 1);
            disk.Points.Add(new Point(x, y));

            // Add the disk to ToPeg.
            pegs[ToPeg].Push(disk);
        }
    }
}
