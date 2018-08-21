using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDArray
{
    public class NDArray<T>
    {
        // A one-dimensional array holding the data.
        private T[] Values;

        // The bounds.
        private int NumDimensions;
        private int[] LowerBound, SliceSize;

        // Constructor.
        public NDArray(params int[] bounds)
        {
            // Make sure there is an even number of bounds.
            if (bounds.Length % 2 == 1)
                throw new ArgumentException("Number of bounds must be even.", "bounds");

            // Make sure there are at least two bounds.
            if (bounds.Length < 2)
                throw new ArgumentException("There must be at least two bounds, one upper and one lower.", "bounds");

            // Get the bounds.
            NumDimensions = (int)(bounds.Length / 2);
            LowerBound = new int[NumDimensions];
            SliceSize = new int[NumDimensions];

            // Initialize LowerBound and SliceSize.
            int slice_size = 1;
            for (int i = NumDimensions - 1; i >= 0; i--)
            {
                SliceSize[i] = slice_size;

                LowerBound[i] = bounds[2 * i];
                int upper_bound = bounds[2 * i + 1];
                int bound_size = upper_bound - LowerBound[i] + 1;
                slice_size *= bound_size;
            }

            // Allocate room for all of the items.
            Values = new T[slice_size];
        }

        // The indexer.
        public T this[params int[] indexes]
        {
            get
            {
                if (indexes.Length != NumDimensions)
                    throw new IndexOutOfRangeException(
                        "Number of indexes does not match number of dimensions");
                return Values[Index(indexes)];
            }
            set
            {
                if (indexes.Length != NumDimensions)
                    throw new IndexOutOfRangeException(
                        "Number of indexes does not match number of dimensions");
                Values[Index(indexes)] = value;
            }
        }

        // Calculate the index for a series of indices.
        private int Index(params int[] indices)
        {
            int index = 0;
            for (int i = 0; i < indices.Length; i++)
            {
                index += (indices[i] - LowerBound[i]) * SliceSize[i];
            }
            return index;
        }
    }
}
