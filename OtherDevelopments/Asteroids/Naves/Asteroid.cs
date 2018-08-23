using System;
using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using Tao.OpenGl; 

namespace Naves
{
    public class Asteroid
    {
        static int list;
        static Random r = new Random();
        Position p;
        float incX;
        float incY;
        float asteroidSpeed;
        float radio;
        float rot;
        int texture;
        bool crashed;

        public bool Crashed
        {
            get { return crashed; }
            set { crashed = value; }
        }

        public Position Position
        {
            get { return p; }
            set { p = value; }
        }

        public Asteroid()
        {
            p = default(Position);
            rot = r.Next(90);
            Reset();
            if (r.Next(2) == 1)
            {
                this.texture = ContentManager.GetTextureByName("asteroide1.jpg");
            }
            else
            {
                this.texture = ContentManager.GetTextureByName("asteroide2.jpg");  
            }

        }

        public void Reset()
        {
            p.x = (r.Next(11)) * (float)Math.Pow(-1, r.Next());
            p.z = (35+ (r.Next(35))) * -1;
            p.y = (r.Next(8)) * (float)Math.Pow(-1, r.Next());
            radio = (float)(r.NextDouble() * 2);
            if (p.x > 0)
                incX = (float)r.NextDouble() * -1;
            else
                incX = (float)r.NextDouble();

            if (p.y > 0)
                incY = (float)r.NextDouble() * -1;
            else
                incY = (float)r.NextDouble();

            incX *= 0.03f;
            incY *= 0.03f;
            asteroidSpeed = (float)(r.NextDouble() * 0.1); 
        }

        static public void Crear()
        {
            Glu.GLUquadric quadratic = Glu.gluNewQuadric(); //crear el objeto cuadric
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);

            list = Gl.glGenLists(1); // crear la lista
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glPushMatrix();
            Gl.glRotated(270, 1, 0, 0);
            Glu.gluSphere(quadratic, 1, 5, 5); //creo la esfera 
            Gl.glPopMatrix();
            Gl.glEndList();
        }

        public void Draw()
        {
            p.z += asteroidSpeed;
            p.y += incY;
            p.x += incX;
            rot += 1f;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
 
            Gl.glPushMatrix();
            Gl.glTranslatef(p.x, p.y, p.z);    
            Gl.glRotatef(rot, 1, 1, 1); 
            Gl.glCallList(list);  
            Gl.glPopMatrix();

            if (p.z > 4)
            {
                Reset();  
            }
            Gl.glDisable(Gl.GL_TEXTURE_2D);  
        }
    }
}
