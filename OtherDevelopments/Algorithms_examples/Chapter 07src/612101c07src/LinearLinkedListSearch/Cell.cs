using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearLinkedListSearch
{
    class Cell
    {
        public int Value;
        public Cell Next;
        public Cell(int value, Cell next)
        {
            Value = value;
            Next = next;
        }
    }
}
