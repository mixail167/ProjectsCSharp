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

namespace DrawTree2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The tree's root.
        BinaryNode Root;

        // Build a tree.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Build the tree.
            Root = new BinaryNode("E");
            BinaryNode nodeA = new BinaryNode("A");
            BinaryNode nodeB = new BinaryNode("B");
            BinaryNode nodeC = new BinaryNode("C");
            BinaryNode nodeD = new BinaryNode("D");
            BinaryNode nodeF = new BinaryNode("F");
            BinaryNode nodeG = new BinaryNode("G");
            BinaryNode nodeH = new BinaryNode("H");
            BinaryNode nodeI = new BinaryNode("I");
            BinaryNode nodeJ = new BinaryNode("J");
            Root.LeftChild = nodeB;
            Root.RightChild = nodeF;
            nodeB.LeftChild = nodeA;
            nodeB.RightChild = nodeD;
            nodeD.LeftChild = nodeC;
            nodeF.RightChild = nodeI;
            nodeI.LeftChild = nodeG;
            nodeI.RightChild = nodeJ;
            nodeG.RightChild = nodeH;
        }

        // Draw the tree.
        private void treePictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Position the tree.
            Root.PositionSubtree(10, 10);

#if COLOR
            // Draw the links.
            Root.DrawSubtreeLinks(e.Graphics, Pens.Blue);

            // Draw the nodes.
            Root.DrawSubtreeNodes(e.Graphics, this.Font,
                Brushes.Blue, Brushes.LightBlue, Pens.Blue);
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
