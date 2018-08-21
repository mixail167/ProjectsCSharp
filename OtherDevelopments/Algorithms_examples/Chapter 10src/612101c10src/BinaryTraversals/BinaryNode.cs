using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTraversals
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
        public string TraversePreorder()
        {
            string result = Name;
            if (LeftChild != null) result += " " + LeftChild.TraversePreorder();
            if (RightChild != null) result += " " + RightChild.TraversePreorder();
            return result;
        }

        // Return a string containing the subtree's inorder traversal.
        public string TraverseInorder()
        {
            string result = "";
            if (LeftChild != null) result += LeftChild.TraverseInorder() + " ";
            result += Name;
            if (RightChild != null) result += " " + RightChild.TraverseInorder();
            return result;
        }

        // Return a string containing the subtree's postorder traversal.
        public string TraversePostorder()
        {
            string result = "";
            if (LeftChild != null) result += LeftChild.TraversePostorder() + " ";
            if (RightChild != null) result += RightChild.TraversePostorder() + " ";
            result += Name;
            return result;
        }

        // Return a string containing the subtree's depth-first traversal.
        public string TraverseDepthFirst()
        {
            string result = "";

            Queue<BinaryNode> children = new Queue<BinaryNode>();

            // Place this node on the stack.
            children.Enqueue(this);

            // Process the stack until it is empty.
            while (children.Count > 0)
            {
                // Get the next node in the stack.
                BinaryNode node = children.Dequeue();

                // Process the node.
                result += " " + node.Name;

                // Add the node's children to the stack.
                if (node.LeftChild != null) children.Enqueue(node.LeftChild);
                if (node.RightChild != null) children.Enqueue(node.RightChild);
            }

            // Remove the initial space.
            return result.Substring(1);
        }
    }
}
