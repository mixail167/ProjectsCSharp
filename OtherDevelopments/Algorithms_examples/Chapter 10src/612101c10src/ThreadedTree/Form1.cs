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

namespace ThreadedTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The sentinel root node.
        private ThreadedNode Root = null;

        // Add the value to the tree.
        private void addButton_Click(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(valueTextBox.Text, out value)) return;

            if (Root != null) Root.AddNode(value);
            else Root = new ThreadedNode(value);

            // Display the traversals.
            DisplayTraversals();

            // Display the tree.
            treePictureBox.Refresh();

            valueTextBox.Clear();
            valueTextBox.Focus();
        }

        private void DisplayTraversals()
        {
            // Forward.
            List<ThreadedNode> forward = GetTraversal();
            string text = "";
            foreach (ThreadedNode node in forward)
                text += " " + node.Value.ToString();
            forwardsTextBox.Text = text.Substring(1);

            // Backward.
            List<ThreadedNode> backward = GetBackwardTraversal();
            text = "";
            foreach (ThreadedNode node in backward)
                text += " " + node.Value.ToString();
            backwardsTextBox.Text = text.Substring(1);
        }

        // Get the forward traversal.
        private List<ThreadedNode> GetTraversal()
        {
            List<ThreadedNode> traversal = new List<ThreadedNode>();

            // Start at the root.
            ThreadedNode node = Root;

            // Remember whether we got to a node via a branch or thread.
            // Pretend we go to the root via a branch so we go left next.
            bool via_branch = true;

            // Repeat until the traversal is done.
            while (node != null)
            {
                // If we got here via a branch, then go
                // down and to the left as far as possible.
                if (via_branch)
                {
                    while (node.LeftChild != null) node = node.LeftChild;
                }

                // Process this node.
                traversal.Add(node);

                // Find the next node to process.
                if (node.RightChild == null)
                {
                    // Use the thread.
                    node = node.RightThread;
                    via_branch = false;
                }
                else
                {
                    // Use the right branch.
                    node = node.RightChild;
                    via_branch = true;
                }
            }
            return traversal;
        }

        // Get the backward traversal.
        private List<ThreadedNode> GetBackwardTraversal()
        {
            List<ThreadedNode> traversal = new List<ThreadedNode>();

            // Start at the root.
            ThreadedNode node = Root;

            // Remember whether we got to a node via a branch or thread.
            // Pretend we go to the root via a branch so we go right next.
            bool via_branch = true;

            // Repeat until the traversal is done.
            while (node != null)
            {
                // If we got here via a branch, then go
                // down and to the right as far as possible.
                if (via_branch)
                {
                    while (node.RightChild != null) node = node.RightChild;
                }

                // Process this node.
                traversal.Add(node);

                // Find the next node to process.
                if (node.LeftChild == null)
                {
                    // Use the thread.
                    node = node.LeftThread;
                    via_branch = false;
                }
                else
                {
                    // Use the right branch.
                    node = node.LeftChild;
                    via_branch = true;
                }
            }
            return traversal;
        }

        // Draw the tree.
        private void treePictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Position the tree.
            if (Root != null)
            {
                Root.PositionSubtree(10, 10);

#if COLOR
                // Draw the links.
                Root.DrawSubtreeLinks(e.Graphics, Pens.Green);

                // Draw the nodes.
                Root.DrawSubtreeNodes(e.Graphics, this.Font,
                    Brushes.Green, Brushes.LightGreen, Pens.Green);
#else
                // Draw the links.
                Root.DrawSubtreeLinks(e.Graphics, Pens.Black);

                // Draw the nodes.
                Root.DrawSubtreeNodes(e.Graphics, this.Font,
                    Brushes.Black, Brushes.LightGray, Pens.Black);
#endif
            }
        }
    }
}
