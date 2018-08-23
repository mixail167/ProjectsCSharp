using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl; 

namespace Naves
{
    public class Camera
    {
        public void SelectCamara(int camara)
        {
            //voy a trabajar con la matriz de modelo
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //la reseteo
            Gl.glLoadIdentity();

            switch (camara)
            {
                case 0:
                    {
                        Glu.gluLookAt(0, 2, 4, 0, 0, -2, 0, 1, 0);
                        break;
                    }
                case 1:
                    {
                        Glu.gluLookAt(0, 3, 5, 0, 1, -4, 0, 1, 0);
                        break;
                    }
                case 2:
                    {
                        Glu.gluLookAt(0, 70, 0, 0, 0, -16, 0, 1, 0);
                        break;
                    }
                case 3:
                    {
                        Glu.gluLookAt(3, 3, -47, 1, 0, 1, 0, 1, 0);
                        break;
                    }
            }
        }

        public void SetPerspective()
        {
            //selecciono la matrix de proyección
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            //la reseteo
            Gl.glLoadIdentity();
            //45 = angulo de vision
            //1  = proporcion de alto por ancho
            //0.1f = distancia minima en la que se pinta
            //1000 = distancia maxima
            Glu.gluPerspective(65, 1, 0.1f, 1000);
            SelectCamara(0);
        }
    }
}
