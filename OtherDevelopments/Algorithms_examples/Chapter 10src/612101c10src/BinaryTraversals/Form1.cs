using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryTraversals
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Build a tree and display its traversals.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Build the tree.
            BinaryNode root = new BinaryNode("E");
            BinaryNode nodeA = new BinaryNode("A");
            BinaryNode nodeB = new BinaryNode("B");
            BinaryNode nodeC = new BinaryNode("C");
            BinaryNode nodeD = new BinaryNode("D");
            BinaryNode nodeF = new BinaryNode("F");
            BinaryNode nodeG = new BinaryNode("G");
            BinaryNode nodeH = new BinaryNode("H");
            BinaryNode nodeI = new BinaryNode("I");
            BinaryNode nodeJ = new BinaryNode("J");
            root.LeftChild = nodeB;
            root.RightChild = nodeF;
            nodeB.LeftChild = nodeA;
            nodeB.RightChild = nodeD;
            nodeD.LeftChild = nodeC;
            nodeF.RightChild = nodeI;
            nodeI.LeftChild = nodeG;
            nodeI.RightChild = nodeJ;
            nodeG.RightChild = nodeH;

            // Display the traversals.
            preorderTextBox.Text = root.TraversePreorder();
            preorderTextBox.Select(0, 0);
            inorderTextBox.Text = root.TraverseInorder();
            inorderTextBox.Select(0, 0);
            postorderTextBox.Text = root.TraversePostorder();
            postorderTextBox.Select(0, 0);
            depthFirstTextBox.Text = root.TraverseDepthFirst();
            depthFirstTextBox.Select(0, 0);
        }
    }
}
