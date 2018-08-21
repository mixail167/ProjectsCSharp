using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DFAMatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // See if the expression matches.
        private void evaluateButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Clear();

            // Load the state transitions.
            try
            {
                // The state transitions.
                List<int> fromState = new List<int>();
                List<char> onInput = new List<char>();
                List<int> newState = new List<int>();
                Dictionary<int, bool> isAccepting = new Dictionary<int, bool>();

                // Get the state transitions.
                string[] lines = transitionsTextBox.Lines;
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        string[] fields = line.Split('\t');
                        int state = int.Parse(fields[0]);
                        fromState.Add(state);
                        onInput.Add(fields[1][0]);
                        newState.Add(int.Parse(fields[2]));

                        // If we don't know yet whether this state
                        // is accepting, save that value now.
                        if (!isAccepting.ContainsKey(state))
                            isAccepting.Add(state, fields[3].ToUpper() == "YES");
                    }
                }
                int numTransitions = fromState.Count;

                // Process the input.
                if (IsMatch(fromState, onInput, newState, isAccepting, inputTextBox.Text))
                    resultTextBox.Text = "Accepting";
                else resultTextBox.Text = "Not accepting";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Return true if the state transitions match the input.
        private bool IsMatch(List<int> fromState, List<char> onInput, List<int> newState, Dictionary<int, bool> isAccepting, string input)
        {
            int numTransitions = fromState.Count;

            // Begin in the start state 0.
            int state = 0;

            // Process the input.
            foreach (char ch in inputTextBox.Text)
            {
                // Find the appropriate transition.
                bool foundTransition = false;
                for (int i = 0; i < numTransitions; i++)
                {
                    if ((fromState[i] == state) && (onInput[i] == ch))
                    {
                        // This is the correct transition. Apply it.
                        state = newState[i];

                        // Process the next input character.
                        foundTransition = true;
                        break;
                    }
                }

                // If we didn't find the transition, do not accept.
                if (!foundTransition) return false;
            }

            // See if we finished in an accepting state.
            return isAccepting[state];
        }
    }
}
