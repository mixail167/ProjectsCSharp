using System;
using System.Collections.Generic;
using System.Text;

namespace Naves
{
    public struct Position
    {
        public float x;
        public float y;
        public float z;

        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class Controller
    {
        Camera camara = new Camera();
        Star star = new Star();
        SpaceShip spaceShip = new SpaceShip(); 

        public SpaceShip Nave
        {
            get { return spaceShip; }
            set { spaceShip = value; }
        }

        public Camera Camara
        {
            get { return camara; }
        }

        public void BeginGame()
        {
            AsteroidGenerator.GenerateAsteroid(35, false);
        }

        public void ResetGame()
        {
            AsteroidGenerator.GenerateAsteroid(35, true);
            spaceShip.Reiniciar();  
        }

        public void CreateObjects()
        {
            star.CreateStars(450);
            spaceShip.Create();  
            Asteroid.Crear();  
        }

        public void DrawScene()
        {
            star.Draw();
            AsteroidGenerator.DrawAsteroids();
            spaceShip.Dibujar();  
        }
    }
}
