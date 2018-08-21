using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalGame
{
    public class AnimalNode
    {
        public string Question;
        public AnimalNode YesChild, NoChild;

        public AnimalNode(string question)
        {
            Question = question;
        }
    }
}
