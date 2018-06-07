using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer
{
    class Record
    {
        string name;
        int score;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}#{1}", name, score);
        }
    }
}
