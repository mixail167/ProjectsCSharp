#define COLOR

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

namespace SortedTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The sentinel root node.
        private BinaryNode Root = new BinaryNode(int.MinValue);

        // Add the value to the tree.
        private void addButton_Click(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(valueTextBox.Text, out value)) return;
            Root.AddNode(value);
            valueTextBox.Clear();
            valueTextBox.Focus();
            treePictureBox.Refresh();
        }

        // Draw the tree.
        private void treePictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Position the tree.
            if (Root.RightChild != null)
            {
                Root.RightChild.PositionSubtree(10, 10);

#if COLOR
                // Draw the links.
                Root.RightChild.DrawSubtreeLinks(e.Graphics, Pens.Blue);

                // Draw the nodes.
                Root.RightChild.DrawSubtreeNodes(e.Graphics, this.Font,
                    Brushes.Blue, Brushes.LightBlue, Pens.Blue);
#else
                // Draw the links.
                Root.RightChild.DrawSubtreeLinks(e.Graphics, Pens.Black);

                // Draw the nodes.
                Root.RightChild.DrawSubtreeNodes(e.Graphics, this.Font,
                    Brushes.Black, Brushes.LightGray, Pens.Black);
#endif
            }
        }
    }
}
