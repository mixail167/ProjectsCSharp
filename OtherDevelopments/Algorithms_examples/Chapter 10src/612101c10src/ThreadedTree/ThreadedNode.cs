using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace ThreadedTree
{
    public class ThreadedNode
    {
        public int Value;
        public ThreadedNode LeftChild, RightChild;
        public ThreadedNode LeftThread, RightThread;

        public ThreadedNode(int value)
        {
            Value = value;
        }

        // Add a node to this node's sorted subtree.
        public void AddNode(int value)
        {
            // See if the new value is smaller than ours.
            if (value < Value)
            {
                // The new value is smaller. Add it to the left subtree.
                if (LeftChild != null) LeftChild.AddNode(value);
                else
                {
                    // Add the new child here.
                    ThreadedNode child = new ThreadedNode(value);
                    child.LeftThread = LeftThread;
                    child.RightThread = this;
                    this.LeftChild = child;
                    this.LeftThread = null;
                }
            }
            else
            {
                // The new value is not smaller. Add it to the right subtree.
                if (RightChild != null) RightChild.AddNode(value);
                else
                {
                    // Add the new child here.
                    ThreadedNode child = new ThreadedNode(value);
                    child.LeftThread = this;
                    child.RightThread = this.RightThread;
                    this.RightChild = child;
                    this.RightThread = null;
                }
            }
        }

        #region Drawing Methods

        // Drawing parameters.
        private const int NodeRadius = 20;
        private const int XSpacing = 20;
        private const int YSpacing = 20;
        private Point NodePoint;
        private Rectangle NodeRect;
        private Rectangle SubtreeRect;

        // Position the node assuming its size has already been set.
        public void PositionSubtree(int xmin, int ymin)
        {
            int xmax = xmin;

            // See if the node has any children.
            if ((LeftChild == null) && (RightChild == null))
            {
                // There are no children. Put the node here.
                SubtreeRect = new Rectangle(
                    xmin, ymin, 2 * NodeRadius, 2 * NodeRadius);
            }
            else
            {
                // Position the left subtree.
                int height = 0;

                if (LeftChild != null)
                {
                    LeftChild.PositionSubtree(xmax, ymin + 2 * NodeRadius + YSpacing);

                    // Update xmax to allow room for the left subtree.
                    xmax = LeftChild.SubtreeRect.Right;

                    // If there is also a right child, allow room between them.
                    if (RightChild != null) xmax += XSpacing;

                    // Update the height.
                    height = LeftChild.SubtreeRect.Bottom;
                }
                else xmax += 2 * NodeRadius;

                // Position the right subtree.
                if (RightChild != null)
                {
                    RightChild.PositionSubtree(xmax, ymin + 2 * NodeRadius + YSpacing);

                    // Update xmax.
                    xmax = RightChild.SubtreeRect.Right;

                    // Update the height.
                    if (RightChild.SubtreeRect.Bottom > height)
                        height = RightChild.SubtreeRect.Bottom;
                }
                else xmax += 2 * NodeRadius;

                // Position this node centered over the subtrees.
                SubtreeRect = new Rectangle(
                    xmin, ymin, xmax - xmin, 2 * NodeRadius + YSpacing + height);
            }

            // Position the node.
            NodePoint = new Point(
                (SubtreeRect.Left + SubtreeRect.Right) / 2,
                SubtreeRect.Top + NodeRadius);
            NodeRect = new Rectangle(
                NodePoint.X - NodeRadius, NodePoint.Y - NodeRadius,
                2 * NodeRadius, 2 * NodeRadius);
        }

        // Draw the subtree's links.
        public void DrawSubtreeLinks(Graphics gr, Pen pen)
        {
            if (LeftChild != null)
            {
                LeftChild.DrawSubtreeLinks(gr, pen);
                gr.DrawLine(pen, NodePoint, LeftChild.NodePoint);
            }
            if (RightChild != null)
            {
                RightChild.DrawSubtreeLinks(gr, pen);
                gr.DrawLine(pen, NodePoint, RightChild.NodePoint);
            }
        }

        // Draw the subtree's nodes.
        public void DrawSubtreeNodes(Graphics gr, Font font, Brush fgBrush, Brush bgBrush, Pen pen)
        {
            // Draw the node.
            gr.FillEllipse(bgBrush, NodeRect);
            gr.DrawEllipse(pen, NodeRect);
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                string text = Value.ToString() + Environment.NewLine;
                if (LeftThread == null) text += "--";
                else text += LeftThread.Value.ToString();
                text += "  ";
                if (RightThread == null) text += "--";
                else text += RightThread.Value.ToString();
                gr.DrawString(text, font, fgBrush, NodeRect, format);
            }

            // Draw the descendants' nodes.
            if (LeftChild != null) LeftChild.DrawSubtreeNodes(gr, font, fgBrush, bgBrush, pen);
            if (RightChild != null) RightChild.DrawSubtreeNodes(gr, font, fgBrush, bgBrush, pen);
        }

        #endregion Drawing Methods

    }
}
