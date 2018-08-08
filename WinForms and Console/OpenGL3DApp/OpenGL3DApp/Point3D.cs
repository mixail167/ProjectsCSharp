
namespace OpenGL3DApp
{
    class Point3D
    {
        double x;
        double y;
        double z;

        public Point3D(double x, double y, double z)
        {
            this.z = z;
            this.x = x;
            this.y = y;
        }
       
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }
    }
}
