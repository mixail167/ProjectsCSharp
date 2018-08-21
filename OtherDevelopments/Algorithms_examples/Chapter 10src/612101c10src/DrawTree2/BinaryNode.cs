using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace DrawTree2
{
    public class BinaryNode
    {
        public string Name;
        public BinaryNode LeftChild, RightChild;

        public BinaryNode(string name)
        {
            Name = name;
        }

        #region Drawing Methods

        // Drawing parameters.
        private const int NodeRadius = 10;
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
                gr.DrawString(Name, font, fgBrush, NodeRect, format);
            }

            // Draw the descendants' nodes.
            if (LeftChild != null) LeftChild.DrawSubtreeNodes(gr, font, fgBrush, bgBrush, pen);
            if (RightChild != null) RightChild.DrawSubtreeNodes(gr, font, fgBrush, bgBrush, pen);
        }

        #endregion Drawing Methods

    }
}
