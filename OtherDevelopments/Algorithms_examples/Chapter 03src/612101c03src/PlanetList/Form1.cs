using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanetList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The top sentinel.
        private Planet Sentinel;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Make the planets and the the NextDistance links.
            Sentinel = new Planet();
            Planet[] planets =
            {
                new Planet("Mercury", 0.39, 0.06, 0.382),
                new Planet("Venus", 0.72, 0.82, 0.949),
                new Planet("Earth", 1, 1, 1),
                new Planet("Mars", 1.52, 0.11, 0.532),
                new Planet("Jupiter", 5.20, 317.8, 11.209),
                new Planet("Saturn", 9.54, 95.2, 9.449),
                new Planet("Uranus", 19.22, 14.6, 4.007),
                new Planet("Neptune", 30.06, 17.2, 3.883),
            };

            // Create the threads.
            foreach (Planet planet in planets) AddPlanetToList(Sentinel, planet);

            // Start ordered by distance to the sun.
            distanceRadioButton.Checked = true;
        }

        // Add a Planet to the linked list.
        private void AddPlanetToList(Planet sentinel, Planet planet)
        {
            // Add the planet to the NextDistance thread.
            Planet afterMe = sentinel;
            while ((afterMe.NextDistance != null) &&
                   (afterMe.NextDistance.DistanceToSun < planet.DistanceToSun))
            {
                afterMe = afterMe.NextDistance;
            }
            planet.NextDistance = afterMe.NextDistance;
            afterMe.NextDistance = planet;

            // Add the planet to the NextMass thread.
            afterMe = sentinel;
            while ((afterMe.NextMass != null) &&
                   (afterMe.NextMass.Mass < planet.Mass))
            {
                afterMe = afterMe.NextMass;
            }
            planet.NextMass = afterMe.NextMass;
            afterMe.NextMass = planet;

            // Add the planet to the NextDiameter thread.
            afterMe = sentinel;
            while ((afterMe.NextDiameter != null) &&
                   (afterMe.NextDiameter.Diameter < planet.Diameter))
            {
                afterMe = afterMe.NextDiameter;
            }
            planet.NextDiameter = afterMe.NextDiameter;
            afterMe.NextDiameter = planet;
        }

        // Display the list sorted appropriately.
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            planetListBox.Items.Clear();

            if (distanceRadioButton.Checked)
                for (Planet planet = Sentinel.NextDistance;
                    planet != null;
                    planet = planet.NextDistance)
                    planetListBox.Items.Add(planet.Name);
            else if (massRadioButton.Checked)
                for (Planet planet = Sentinel.NextMass;
                    planet != null;
                    planet = planet.NextMass)
                    planetListBox.Items.Add(planet.Name);
            else
                for (Planet planet = Sentinel.NextDiameter;
                    planet != null;
                    planet = planet.NextDiameter)
                    planetListBox.Items.Add(planet.Name);
        }
    }
}
