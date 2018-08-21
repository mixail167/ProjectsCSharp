using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetList
{
    class Planet
    {
        public string Name;
        public double DistanceToSun, Mass, Diameter;
        public Planet NextDistance, NextMass, NextDiameter;

        // Constructors.
        public Planet()
        {
        }
        public Planet(string name, double distanceToSun, double mass, double diameter)
        {
            Name = name;
            DistanceToSun = distanceToSun;
            Mass = mass;
            Diameter = diameter;
        }
    }
}
