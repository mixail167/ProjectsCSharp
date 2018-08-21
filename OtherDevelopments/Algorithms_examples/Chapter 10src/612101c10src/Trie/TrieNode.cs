using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class TrieNode
    {
        public string RemainingKey = "";
        public string Value = null;
        public TrieNode[] Children = null;

        // Add a value to this node's subtrie.
        public void AddValue(string newKey, string newValue)
        {
            int index;

            // If the remaining new key is not blank and matches
            // the remaining node key, place the value here.
            if ((newKey.Length > 0) && (newKey == RemainingKey))
            {
                Value = newValue;
                return;
            }

            // If the remaining new key is blank and
            // so is the remaining node key, place the value here.
            if ((newKey.Length == 0) && (RemainingKey.Length == 0))
            {
                Value = newValue;
                return;
            }

            // If the new key is blank but the node's remaining key isn't blank,
            // move the node's remaining key into a child and place the value here.
            if ((newKey.Length == 0) && (RemainingKey.Length > 0))
            {
                // This must be a leaf node so give it children.
                Children = new TrieNode[26];
                index = RemainingKey[0] - 'A';
                Children[index] = new TrieNode();
                Children[index].RemainingKey = RemainingKey.Substring(1);
                Children[index].Value = Value;
                RemainingKey = "";
                Value = newValue;
                return;
            }

            // We need a child node.
            if (Children == null)
            {
                // Make the children.
                Children = new TrieNode[26];

                // See if we have a remaining key.
                if (RemainingKey.Length > 0)
                {
                    // Move this into the appropriate child.
                    index = RemainingKey[0] - 'A';
                    Children[index] = new TrieNode();
                    Children[index].RemainingKey = RemainingKey.Substring(1);
                    Children[index].Value = Value;
                    RemainingKey = "";
                    Value = null;
                }
            }

            // Search the appropriate subtrie.
            index = newKey[0] - 'A';
            if (Children[index] == null)
            {
                // This child doesn't exist. Make it and
                // let it represent the rest of the new key.
                Children[index] = new TrieNode();
                Children[index].RemainingKey = newKey.Substring(1);
                Children[index].Value = newValue;
                return;
            }

            // Search the appropriate subtrie.
            Children[index].AddValue(newKey.Substring(1), newValue);
        }

        // Find a value in this node's subtrie.
        public string FindValue(string targetKey)
        {
            // If the remaining key matches the
            // remaining node key, return this node's value.
            if (targetKey == RemainingKey) return Value;

            // Search the appropriate child.
            if (Children == null) return null;
            int index = targetKey[0] - 'A';
            if (Children[index] == null) return null;
            return Children[index].FindValue(targetKey.Substring(1));
        }

        // Return a textual representation of this subtrie.
        public override string ToString()
        {
            return MakeString('-', 0);
        }

        // Return a textual representation for this subtrie.
        // Variable letter is this node's letter. 
        private string MakeString(char letter, int indent)
        {
            // Start with our value.
            string result = new string(' ', indent);
            result += letter;
            if (RemainingKey.Length > 0) result += " -> " + RemainingKey;
            if (Value != null) result += " (" + Value + ")";
            result += Environment.NewLine;

            // Add the child values if we have any.
            if (Children != null)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (Children[i] != null)
                    {
                        char ch = (char)('A' + i);
                        result += Children[i].MakeString(ch, indent + 2);
                    }
                }
            }
            return result;
        }
    }
}
