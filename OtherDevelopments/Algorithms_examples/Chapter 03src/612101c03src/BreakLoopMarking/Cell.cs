using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakLoopMarking
{
    class Cell
    {
        public string Value;
        public Cell Next;
        public bool Visited;

        public Cell(string value, Cell next)
        {
            Value = value;
            Next = next;
        }
    }
}
