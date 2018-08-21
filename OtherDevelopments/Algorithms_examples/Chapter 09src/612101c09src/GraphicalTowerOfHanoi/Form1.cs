using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace GraphicalTowerOfHanoi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Drawing constants.
        public static int PixelsPerFrame = 2;
        public static int YMargin = 10;
        public static int XMargin = 10;
        public static int PegWidth = 10;
        public static int DiskHeight = 10;
        public static int MinDiskWidth = 30;
        public int MaxDiskWidth;
        public int DiskWidthDifference = 20;
        private static int PlatformHeight = 20;

        // The moves.
        private List<Move> Moves = new List<Move>();

        // The peg stacks.
        private Stack<Disk>[] Pegs = new Stack<Disk>[3];

        // The pegs locations.
        public Rectangle[] PegLocations = new Rectangle[3];

        private Rectangle PlatformLocation;

        // All disks.
        private List<Disk> Disks = new List<Disk>();

        private void Form1_Load(object sender, EventArgs e)
        {
            PrepareScene();
        }

        // Solve it and start the animation.
        private void solveButton_Click(object sender, EventArgs e)
        {
            // Prepare the objects for the scene.
            PrepareScene();

            // Create the moves.
            Moves = new List<Move>();
            TowerOfHanoi(Moves, 0, 1, 2, Disks.Count);

            // Start the movement timer.
            moveTimer.Enabled = true;
        }

        // Move the top numDisks disks from peg fromPeg to peg toPeg
        // using otherPeg to hold disks temporarily as needed.
        private void TowerOfHanoi(List<Move> moves, int fromPeg, int toPeg, int otherPeg, int numDisks)
        {
            // Recursively move the top n - 1 disks from from_peg to other_peg.
            if (numDisks > 1)
                TowerOfHanoi(moves, fromPeg, otherPeg, toPeg, numDisks - 1);

            // Move the last disk from from_peg to to_peg.
            moves.Add(new Move(fromPeg, toPeg));

            // Recursively move the top n - 1 disks back from other_peg to to_peg.
            if (numDisks > 1)
                TowerOfHanoi(moves, otherPeg, toPeg, fromPeg, numDisks - 1);
        }

        // Move disks and redraw.
        private void moveTimer_Tick(object sender, EventArgs e)
        {
            // Get the speed.
            switch (speedNumericUpDown.Text)
            {
                case "1":
                    PixelsPerFrame = 3;
                    break;
                case "2":
                    PixelsPerFrame = 10;
                    break;
                case "3":
                    PixelsPerFrame = 20;
                    break;
                case "4":
                    PixelsPerFrame = 50;
                    break;
                case "5":
                    PixelsPerFrame = 100;
                    break;
            }

            // Move the disks.
            bool stillMoving = false;
            foreach (Disk disk in Disks)
                stillMoving |= disk.Move();

            // If no disk is moving, start the next move.
            if (!stillMoving)
            {
                // If there are no more moves, stop.
                if (Moves.Count == 0) moveTimer.Enabled = false;
                else
                {
                    // Make the moves for the next disk to move.
                    Moves[0].MakeMovePoints(Pegs, PegLocations);

                    // Remove the move. (The disk is ready to go.)
                    Moves.RemoveAt(0);
                }
            }

            // Draw the new scene.
            DrawScene();
        }

        // Draw the scene.
        private void DrawScene()
        {
            Bitmap bm = new Bitmap(
                PlatformLocation.Right + XMargin,
                PlatformLocation.Bottom + YMargin);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(pegPictureBox.BackColor);
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw the platform and pegs.
                gr.FillRectangle(Brushes.LightGreen, PlatformLocation);
                gr.DrawRectangle(Pens.Green, PlatformLocation);
                for (int i = 0; i < 3; i++)
                {
                    gr.FillRectangle(Brushes.Pink, PegLocations[i]);
                    gr.DrawRectangle(Pens.Red, PegLocations[i]);
                }

                // Draw the disks.
                foreach (Disk disk in Disks) disk.Draw(gr);
            }

            // Display the result.
            pegPictureBox.Image = bm;
        }

        // Draw the scene.
        private void numDisksNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            PrepareScene();
        }

        // Prepare the objects to represent the scene.
        private void PrepareScene()
        {
            // Get the number of disks.
            int numDisks = (int)numDisksNumericUpDown.Value;
            MaxDiskWidth = MinDiskWidth + numDisks * DiskWidthDifference;

            // Position the pegs.
            int pegHeight = DiskHeight * (numDisks + 2);
            PegLocations[0] = new Rectangle(
                2 * XMargin + MaxDiskWidth / 2 - PegWidth / 2,
                2 * YMargin + DiskHeight, PegWidth, pegHeight);
            PegLocations[1] = new Rectangle(
                PegLocations[0].X + XMargin + MaxDiskWidth,
                PegLocations[0].Y, PegWidth, pegHeight);
            PegLocations[2] = new Rectangle(
                PegLocations[1].X + XMargin + MaxDiskWidth,
                PegLocations[0].Y, PegWidth, pegHeight);

            // Make the platform.
            PlatformLocation = new Rectangle(
                XMargin, PegLocations[0].Y + pegHeight,
                3 * MaxDiskWidth + 4 * XMargin,
                PlatformHeight);

            // Empty the pegs.
            for (int i = 0; i < 3; i++) Pegs[i] = new Stack<Disk>();

            // Create the disks.
            Disks = new List<Disk>();
            int y = PlatformLocation.Y - DiskHeight;
            int diskWidth = MaxDiskWidth;
            for (int i = numDisks; i > 0; i--)
            {
                // Make disk i.
                Disk disk = new Disk(new Rectangle(
                    PegLocations[0].X + PegWidth / 2 - diskWidth / 2,
                    y, diskWidth, DiskHeight));
                Pegs[0].Push(disk);
                Disks.Add(disk);

                // Prepare for the next disk.
                y -= DiskHeight;
                diskWidth -= DiskWidthDifference;
            }

            // Display the result.
            DrawScene();
        }
    }
}
