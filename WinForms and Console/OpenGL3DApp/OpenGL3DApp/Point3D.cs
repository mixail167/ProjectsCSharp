
namespace OpenGL3DApp
{
    class Point3D
    {
        float x;
        float y;
        float z;

        public Point3D(float x, float y, float z)
        {
            this.z = z;
            this.x = x;
            this.y = y;
        }
       
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }
    }
}
