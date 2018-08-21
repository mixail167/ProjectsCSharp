using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionsortPriorityQueue
{
    class Cell
    {
        public int Priority;
        public string Value;
        public Cell Next;

        public Cell(int priority, string value)
        {
            Priority = priority;
            Value = value;
        }
    }
}
