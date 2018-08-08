
namespace OpenGL3DApp
{
    class Point3DUV : Point3D
    {
        double u;
        double v;

        public Point3DUV(double x, double y, double z, double u, double v)
            : base(x, y, z)
        {

            this.u = u;
            this.v = v;
        }

        public double U
        {
            get { return u; }
            set { u = value; }
        }

        public double V
        {
            get { return v; }
            set { v = value; }
        }
    }
}
