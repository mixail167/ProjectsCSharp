using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace FileEditDistance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            file1TextBox.Text = path + "\\Sample1.txt";
            file2TextBox.Text = path + "\\Sample2.txt";
        }

        // The direction we are traveling through the edit graph.
        private enum Direction
        {
            FromAbove,
            FromLeft,
            FromDiagonal
        }

        // A node in the edit graph.
        private struct Node
        {
            public int distance;
            public Direction direction;
        }

        // Create an edit graph for two strings.
        private Node[,] MakeEditGraph(string[] lines1, string[] lines2)
        {
            // Make the edit graph array.
            int numCols = lines1.Length + 1;
            int numRows = lines2.Length + 1;
            Node[,] nodes = new Node[numRows, numCols];

            // Initialize the leftmost column.
            for (int r = 0; r < numRows; r++)
            {
                nodes[r, 0].distance = r;
                nodes[r, 0].direction = Direction.FromAbove;
            }

            // Initialize the top row.
            for (int c = 0; c < numCols; c++)
            {
                nodes[0, c].distance = c;
                nodes[0, c].direction = Direction.FromLeft;
            }

            // Fill in the rest of the array.
            for (int c = 1; c < numCols; c++)
            {
                // Fill in column c.
                for (int r = 1; r < numRows; r++)
                {
                    // Fill in entry [r, c].
                    // Check the three possible paths to here and pick the best.
                    // From above.
                    nodes[r, c].distance = nodes[r - 1, c].distance + 1;
                    nodes[r, c].direction = Direction.FromAbove;

                    // From the left.
                    if (nodes[r, c].distance > nodes[r, c - 1].distance + 1)
                    {
                        nodes[r, c].distance = nodes[r, c - 1].distance + 1;
                        nodes[r, c].direction = Direction.FromLeft;
                    }

                    // Diagonal.
                    if ((lines1[c - 1] == lines2[r - 1]) &&
                        (nodes[r, c].distance > nodes[r - 1, c - 1].distance))
                    {
                        nodes[r, c].distance = nodes[r - 1, c - 1].distance;
                        nodes[r, c].direction = Direction.FromDiagonal;
                    }
                }
            }

            return nodes;
        }

        // Display the changes in a RichTextBox.
        private void DisplayResults(string[] lines1, string[] lines2, Node[,] nodes, RichTextBox rch)
        {
            // Build a list of the moves from finish to start.
            int numRows = nodes.GetUpperBound(0) + 1;
            int numCols = nodes.GetUpperBound(1) + 1;
            int r = numRows - 1;
            int c = numCols - 1;

            // Make some fonts.
            Font normalFont = rch.Font;
            using (Font insertFont = new Font(rch.Font, FontStyle.Underline))
            {
                using (Font deleteFont = new Font(rch.Font, FontStyle.Strikeout))
                {
                    rch.Clear();
                    // Continue until we reach the upper left corner.
                    while ((r > 0) || (c > 0))
                    {
                        switch (nodes[r, c].direction)
                        {
                            case Direction.FromAbove:
                                rch.Select(0, 0);
                                rch.SelectedText = lines2[r - 1];
                                rch.Select(0, lines2[r - 1].Length);
                                rch.SelectionFont = insertFont;
                                rch.SelectionColor = Color.Blue;
                                r--;
                                break;
                            case Direction.FromLeft:
                                rch.Select(0, 0);
                                rch.SelectedText = lines1[c - 1];
                                rch.Select(0, lines1[c - 1].Length);
                                rch.SelectionFont = deleteFont;
                                rch.SelectionColor = Color.Red;
                                c--;
                                break;
                            case Direction.FromDiagonal:
                                rch.Select(0, 0);
                                rch.SelectedText = lines2[r - 1];
                                rch.Select(0, lines2[r - 1].Length);
                                rch.SelectionFont = normalFont;
                                rch.SelectionColor = Color.Black;
                                r--;
                                c--;
                                break;
                        }
                        rch.Select(0, 0);
                        rch.SelectedText = Environment.NewLine;
                    }
                }
            }
        }

        // Compare the strings.
        private void compareButton_Click(object sender, EventArgs e)
        {
            distanceTextBox.Clear();
            editsRichTextBox.Clear();

            try
            {
                // Get the files.
                string[] lines1 = File.ReadAllLines(file1TextBox.Text);
                string[] lines2 = File.ReadAllLines(file2TextBox.Text);

                // Build the edit graph.
                Node[,] nodes = MakeEditGraph(lines1, lines2);

                // Display the edits.
                DisplayResults(lines1, lines2, nodes, editsRichTextBox);

                // Display the edit distance.
                distanceTextBox.Text = nodes[
                    nodes.GetUpperBound(0),
                    nodes.GetUpperBound(1)].distance.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
