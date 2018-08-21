using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDisplay
{
    public class BinaryNode
    {
        public string Name;
        public BinaryNode LeftChild, RightChild;

        public BinaryNode(string name)
        {
            Name = name;
        }

        // Return a string containing the subtree's preorder traversal.
        public string TextDisplay(int indent)
        {
            string result = new string(' ', indent) + Name + Environment.NewLine;
            if (LeftChild != null) result += LeftChild.TextDisplay(indent + 4);
            if (RightChild != null) result += RightChild.TextDisplay(indent + 4);
            return result;
        }
    }
}
