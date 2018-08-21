using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bucketsort
{
    class Cell
    {
        public int Value;
        public Cell Next = null;

        // Constructors.
        public Cell(int value)
        {
            Value = value;
        }
        public Cell()
        {
        }
    }
}
