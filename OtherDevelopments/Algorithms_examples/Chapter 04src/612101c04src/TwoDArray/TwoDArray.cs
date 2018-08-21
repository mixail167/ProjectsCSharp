using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDArray
{
    public class TwoDArray<T>
    {
        private int LowerBound0, LowerBound1;
        private T[,] Values;

        // Constructor.
        public TwoDArray(int lower_bound0, int upper_bound0, int lower_bound1, int upper_bound1)
        {
            // Save the lower bounds.
            LowerBound0 = lower_bound0;
            LowerBound1 = lower_bound1;

            // Allocate the array.
            Values = new T[upper_bound0 - lower_bound0 + 1, upper_bound1 - lower_bound1 + 1];
        }

        // Get and set values.
        public T this[int row, int col]
        {
            get
            {
                return Values[row - LowerBound0, col - LowerBound1];
            }
            set
            {
                Values[row - LowerBound0, col - LowerBound1] = value;
            }
        }
    }
}
