using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Quadtree
{
    public class QuadtreeNode
    {
        // The maximum number of points allowed in a quadtree node.
        public const int MaxPoints = 10;

        // The points in this quadtree node.
        public List<PointF> Points = new List<PointF>();

        // The area that this quadtree node represents.
        public RectangleF Area;

        // The middle X and Y values.
        public float Xmid, Ymid;

        // The child quadtree nodes.
        QuadtreeNode NWchild, NEchild, SEchild, SWchild;

        // Initializing constructor.
        public QuadtreeNode(RectangleF area)
        {
            Area = area;
            Xmid = (Area.Left + Area.Right) / 2f;
            Ymid = (Area.Top + Area.Bottom) / 2f;
        }

        // Add a point to this subtree.
        public void AddPoint(PointF newPoint)
        {
            // See if this quadtree node us full.
            if ((Points != null) && (Points.Count + 1 > MaxPoints))
            {
                // Divide this quadtree node.
                float wid = (Area.Right - Area.Left) / 2f;
                float hgt = (Area.Bottom - Area.Top) / 2f;
                NWchild = new QuadtreeNode(new RectangleF(Area.Left, Area.Top, wid, hgt));
                NEchild = new QuadtreeNode(new RectangleF(Area.Left + wid, Area.Top, wid, hgt));
                SEchild = new QuadtreeNode(new RectangleF(Area.Left + wid, Area.Top + hgt, wid, hgt));
                SWchild = new QuadtreeNode(new RectangleF(Area.Left, Area.Top + hgt, wid, hgt));

                // Move the points into the appropriate subtrees.
                foreach (PointF pt in Points)
                {
                    if (pt.Y < Ymid)
                    {
                        if (pt.X < Xmid) NWchild.AddPoint(pt);
                        else NEchild.AddPoint(pt);
                    }
                    else
                    {
                        if (pt.X < Xmid) SWchild.AddPoint(pt);
                        else SEchild.AddPoint(pt);
                    }
                }

                // Remove this node's Points list.
                Points = null;
            }

            // Add the point to the appropriate subtree.
            if (Points != null) Points.Add(newPoint);
            else if (newPoint.Y < Ymid)
            {
                if (newPoint.X < Xmid) NWchild.AddPoint(newPoint);
                else NEchild.AddPoint(newPoint);
            }
            else
            {
                if (newPoint.X < Xmid) SWchild.AddPoint(newPoint);
                else SEchild.AddPoint(newPoint);
            }
        }

        // Draw the points in this quadtree node.
        public void DrawPoints(Graphics gr, Brush brush, Pen pen, float radius)
        {
            // See if this node has children.
            if (Points == null)
            {
                // Make the children draw themselves.
                NWchild.DrawPoints(gr, brush, pen, radius);
                NEchild.DrawPoints(gr, brush, pen, radius);
                SEchild.DrawPoints(gr, brush, pen, radius);
                SWchild.DrawPoints(gr, brush, pen, radius);
            }
            else
            {
                // Draw the points in this node.
                foreach (PointF point in Points)
                {
                    gr.FillEllipse(brush, point.X - radius, point.Y - radius,
                        2 * radius, 2 * radius);
                    gr.DrawEllipse(pen, point.X - radius, point.Y - radius,
                        2 * radius, 2 * radius);
                }
            }
        }

        // Draw the quadtree areas.
        public void DrawAreas(Graphics gr, Pen pen)
        {
            // Draw this quadtree node's area.
            gr.DrawRectangle(pen, Area.Left, Area.Top, Area.Width, Area.Height);

            // Draw the child nodes.
            if (NWchild != null)
            {
                NWchild.DrawAreas(gr, pen);
                NEchild.DrawAreas(gr, pen);
                SEchild.DrawAreas(gr, pen);
                SWchild.DrawAreas(gr, pen);
            }
        }

        // Find the specified point. Return true if we find it.
        public bool FindPoint(PointF target, float radius, out PointF point)
        {
            // If we have children, call FindPointInChildren.
            if (Points == null) return FindPointInChildren(target, radius, out point);

            // Search for the point in this quadtree node.
            return FindPointHere(target, radius, out point);
        }

        // Search the children to find the point closest to the target.
        private bool FindPointInChildren(PointF target, float radius, out PointF resultPoint)
        {
            // The best point we have found so far.
            resultPoint = new PointF(float.MinValue, float.MinValue);
            float bestDist = float.MaxValue;

            // See if the northern subtrees intersect the area of interest.
            if (target.Y - radius < Ymid)
            {
                // See if the northwest subtree intersects the area of interest.
                if (target.X - radius < Xmid)
                {
                    // The northwest subtree does intersect the area of interest.
                    PointF testPoint;
                    if (NWchild.FindPoint(target, radius, out testPoint))
                    {
                        float dx = testPoint.X - target.X;
                        float dy = testPoint.Y - target.Y;
                        float testDist = (float)Math.Sqrt(dx * dx + dy * dy);
                        if ((testDist < radius) && (testDist < bestDist))
                        {
                            bestDist = testDist;
                            resultPoint = testPoint;
                        }
                    }
                }
                // See if the northeast subtree intersects the area of interest.
                if (target.X + radius > Xmid)
                {
                    // The northeast subtree does intersect the area of interest.
                    PointF testPoint;
                    if (NEchild.FindPoint(target, radius, out testPoint))
                    {
                        float dx = testPoint.X - target.X;
                        float dy = testPoint.Y - target.Y;
                        float testDist = (float)Math.Sqrt(dx * dx + dy * dy);
                        if ((testDist < radius) && (testDist < bestDist))
                        {
                            bestDist = testDist;
                            resultPoint = testPoint;
                        }
                    }
                }
            } // End if the northern subtrees intersect the area of interest.

            // See if the southern subtrees intersect the area of interest.
            if (target.Y + radius > Ymid)
            {
                // See if the southwest subtree intersects the area of interest.
                if (target.X - radius < Xmid)
                {
                    // The southwest subtree does intersect the area of interest.
                    PointF testPoint;
                    if (SWchild.FindPoint(target, radius, out testPoint))
                    {
                        float dx = testPoint.X - target.X;
                        float dy = testPoint.Y - target.Y;
                        float testDist = (float)Math.Sqrt(dx * dx + dy * dy);
                        if ((testDist < radius) && (testDist < bestDist))
                        {
                            bestDist = testDist;
                            resultPoint = testPoint;
                        }
                    }
                }
                // See if the southeast subtree intersects the area of interest.
                if (target.X + radius > Xmid)
                {
                    // The southeast subtree does intersect the area of interest.
                    PointF testPoint;
                    if (SEchild.FindPoint(target, radius, out testPoint))
                    {
                        float dx = testPoint.X - target.X;
                        float dy = testPoint.Y - target.Y;
                        float testDist = (float)Math.Sqrt(dx * dx + dy * dy);
                        if ((testDist < radius) && (testDist < bestDist))
                        {
                            bestDist = testDist;
                            resultPoint = testPoint;
                        }
                    }
                }
            } // End if the southern subtrees intersect the area of interest.

            // Return true if we found a point.
            return (bestDist < float.MaxValue);
        }

        // Search this node's points for the target.
        private bool FindPointHere(PointF target, float radius, out PointF resultPoint)
        {
            // The best point we have found so far.
            resultPoint = new PointF(float.MinValue, float.MinValue);
            float bestDist = float.MaxValue;

            // Search the points.
            foreach (PointF testPoint in Points)
            {
                float dx = testPoint.X - target.X;
                float dy = testPoint.Y - target.Y;
                float testDist = (float)Math.Sqrt(dx * dx + dy * dy);
                if ((testDist < radius) && (testDist < bestDist))
                {
                    bestDist = testDist;
                    resultPoint = testPoint;
                }
            }

            // Return true if we found a point.
            return (bestDist < float.MaxValue);
        }
    }
}
