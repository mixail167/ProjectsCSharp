using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindLoops
{
    class ValueCell
    {
        public int Value;
        public ValueCell Next;

        // Return true if the list contains a loop.
        public bool ContainsLoop()
        {
            // Lists with 0 or 1 items don't contain loops.
            if ((this.Next == null) ||
                (this.Next.Next == null)) return false;

            ValueCell slowCell = this.Next;
            ValueCell fastCell = slowCell.Next;

            // Start the cells running.
            for (; ; )
            {
                if (slowCell == fastCell) return true;
                slowCell = slowCell.Next;

                fastCell = fastCell.Next;
                if (fastCell.Next == null) return false;
                fastCell = fastCell.Next;
                if (fastCell.Next == null) return false;
            }
        }

        // If this list after this cell contains a loop,
        // break it so we have a normal list.
        public void BreakLoop()
        {
            if (!ContainsLoop()) return;

            ValueCell slowCell = this.Next;
            ValueCell fastCell = slowCell.Next;

            // Start the cells running and see where they meet.
            for (; ; )
            {
                if (slowCell == fastCell) break;
                slowCell = slowCell.Next;

                fastCell = fastCell.Next;
                fastCell = fastCell.Next;
            }

            // Start slowCell again at the beginning and
            // run the two at the same speed until their Next
            // cells are the same.
            slowCell = this;
            while (slowCell.Next != fastCell.Next)
            {
                slowCell = slowCell.Next;
                fastCell = fastCell.Next;
            }

            // At this point, slowCell.Next is the first cell in the
            // loop and lastCell.Next is the last cell in the loop.
            fastCell.Next = null;
        }
    }
}
