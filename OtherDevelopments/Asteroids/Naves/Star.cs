using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace Naves
{
    class Star
    {
        List<Position> stars = new List<Position>();
   
        public void CreateStars(int cantidad)
        {
            Random r = new Random();
            int count = 0;

            while (count != cantidad)
            {
                Position p = default(Position);
                p.x = (r.Next(110)) * (float)Math.Pow(-1, r.Next());
                p.z = (r.Next(110)) * (float)Math.Pow(-1, r.Next());
                p.y = (r.Next(110)) * (float)Math.Pow(-1, r.Next());
                if (Math.Pow(Math.Pow(p.x, 2) + Math.Pow(p.y, 2) + Math.Pow(p.z, 2), 1 / 3f) > 15)
                {
                    stars.Add(p);
                    count++;
                }
            }
        }

        public void Draw()
        {
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glColor3f(1, 1, 1);//color de lo que se pinta 
            Gl.glPointSize(3); 
            foreach (var item in stars)
            {
                Gl.glVertex3f(item.x, item.y, item.z);   
            }
            Gl.glEnd(); 
        }
    }
}