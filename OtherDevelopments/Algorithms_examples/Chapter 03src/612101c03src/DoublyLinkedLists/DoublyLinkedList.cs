using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace DoublyLinkedLists
{
    class DoublyLinkedList<TKey, TValue>
    {
        // Holds a single item in the list.
        private class ListCell
        {
            public TKey Key;
            public TValue Value;
            public ListCell Next, Prev;
        }

        // The top sentinel.
        private ListCell Sentinel = new ListCell() { Next = null, Prev = null };

        // Get and set values by key.
        public TValue this[TKey key]
        {
            get
            {
                ListCell cell = FindCell(key);
                if (cell == null)
                    throw new ArgumentOutOfRangeException(
                        "key", key.ToString(), "Key not found in the list.");
                return cell.Value;
            }
            set
            {
                // Find the cell holding this key.
                ListCell cell = FindCell(key);

                // If the cell doesn't exist, create it.
                // Otherwise give it the new value.
                if (cell == null) Add(key, value);
                else cell.Value = value;
            }
        }

        // Add an item to the top of the list.
        private void Add(TKey key, TValue value)
        {
            // Create a new cell.
            ListCell cell = new ListCell();
            cell.Key = key;
            cell.Value = value;

            // Insert the cell in the Next list.
            cell.Next = Sentinel.Next;
            Sentinel.Next = cell;

            // Insert the cell in the Prev list.
            if (cell.Next != null) cell.Next.Prev = cell;
            cell.Prev = Sentinel;
        }

        // Delete an item from the list.
        public void Delete(TKey key)
        {
            // Find the cell holding the item.
            ListCell cell = FindCell(key);

            // If there's no such value, throw an exception.
            if (cell == null)
                throw new ArgumentOutOfRangeException("key", key,
                    "The indicated key is not in the list.");

            // Delete the cell containing the item.
            if (cell.Next != null) cell.Next.Prev = cell.Prev;
            cell.Prev.Next = cell.Next;
            //cell.Next = null;
            //cell.Prev = null;
            //free(cell);
        }

        // Return the cell holding the indicated value.
        private ListCell FindCell(TKey key)
        {
            ListCell cell = Sentinel.Next;
            while ((cell != null) && (!cell.Key.Equals(key)))
                cell = cell.Next;
            return cell;
        }

        // Iterator.
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            // Find the cell before the one holding the item.
            ListCell cell = Sentinel.Next;
            while (cell != null)
            {
                yield return new KeyValuePair<TKey, TValue>(cell.Key, cell.Value);
                cell = cell.Next;
            }
        }
    }
}
