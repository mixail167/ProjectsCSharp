using System;
using System.Windows.Forms;

using System.Diagnostics;

namespace PartitionProblem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool AllowShortCircuit = false;
        private int NumWeights;
        private int[] Weights;

        // The best solution.
        private int[] BestAssignedTo;     // 0 or 1
        private int[] BestTotalWeight = new int[2];
        private int BestDifference;

        // The test solution.
        private int[] TestAssignedTo;     // 0 or 1
        private int[] TestTotalWeight = new int[2];

        private long NumNodesVisited;

        // Set AllowShortCircuit.
        private void allowShortCircuitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AllowShortCircuit = allowShortCircuitCheckBox.Checked;
        }

        // Generate weights.
        private void buildButton_Click(object sender, EventArgs e)
        {
            // Get the parameters.
            int min = int.Parse(minWeightTextBox.Text);
            int max = int.Parse(maxWeightTextBox.Text);
            int numWeights = int.Parse(numWeightsTextBox.Text);

            // Make the weights.
            Random rnd = new Random();
            string txt = "";
            for (int i = 0; i < numWeights; i++)
            {
                txt += Environment.NewLine +
                    rnd.Next(min, max + 1).ToString();
            }
            txt = txt.Substring(Environment.NewLine.Length);
            weightsTextBox.Text = txt;
        }

        #region "Helper Routines"

        // Load the weights before running an algorithm.
        private void ReadWeights()
        {
            // Read the weights.
            string txt = weightsTextBox.Text;
            char[] separators = { '\r', '\n' };
            string[] weights = txt.Split(separators,
                StringSplitOptions.RemoveEmptyEntries);

            // Reset the solution.
            NumWeights = weights.Length;
            Weights = new int[NumWeights];
            for (int i = 0; i < NumWeights; i++)
            {
                Weights[i] = int.Parse(weights[i]);
            }

            BestAssignedTo = new int[NumWeights];
            BestTotalWeight = new int[2];

            TestAssignedTo = new int[NumWeights];
            TestTotalWeight = new int[2];

            for (int i = 0; i < NumWeights; i++)
            {
                BestAssignedTo[i] = -1;
                TestAssignedTo[i] = -1;
            }

            treeSizeLabel.Text = "Tree size: " +
                Math.Pow(2, NumWeights + 1).ToString("n0") +
                " nodes";
        }

        // Clear previous results.
        private void weightsTextBox_TextChanged(object sender, EventArgs e)
        {
            foreach (Control ctl in algorithmsGroupBox.Controls)
            {
                if (ctl is TextBox)
                {
                    TextBox txt = (TextBox)ctl;
                    if (txt.ReadOnly) txt.Clear();
                }
                else if (ctl is ComboBox)
                {
                    ComboBox cbo = (ComboBox)ctl;
                    cbo.Items.Clear();
                }
            }
            treeSizeLabel.Text = "";
        }

        // Blank the results for an algorithm.
        private void ClearResults(TextBox nodesVisitedTextBox, TextBox timeTextBox, TextBox differenceTextBox)
        {
            // Reset the test and best solutions.
            ResetSolutions();

            // Clear previous results.
            nodesVisitedTextBox.Clear();
            timeTextBox.Clear();
            differenceTextBox.Clear();

            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
        }

        // Report the results for an algorithm.
        private void ReportResults(TextBox nodesVisitedTextBox, TextBox timeTextBox,
            TextBox differenceTextBox, TimeSpan elapsedTime)
        {
            nodesVisitedTextBox.Text = NumNodesVisited.ToString("n0");
            timeTextBox.Text = elapsedTime.TotalSeconds.ToString("0.0000");
            int difference = Math.Abs(BestTotalWeight[0] - BestTotalWeight[1]);
            differenceTextBox.Text = difference.ToString("0");

            // Debugging: Display assignments.
            Console.WriteLine("Assignments:");
            for (int i = 0; i < NumWeights; i++)
            {
                Console.Write(BestAssignedTo[i].ToString() + " ");
            }
            Console.WriteLine("");
            string[] txt = new string[] { "", "" };
            for (int i = 0; i < NumWeights; i++)
            {
                txt[BestAssignedTo[i]] += " + " + Weights[i].ToString();
            }
            if (txt[0].Length > 0) txt[0] = txt[0].Substring(3);
            if (txt[1].Length > 0) txt[1] = txt[1].Substring(3);
            Console.WriteLine("Weights(0): " + txt[0] + " = " + BestTotalWeight[0].ToString());
            Console.WriteLine("Weights(1): " + txt[1] + " = " + BestTotalWeight[1].ToString());
            Console.WriteLine("");

            this.Cursor = Cursors.Default;
        }

        // Reset the best and test solutions.
        private void ResetSolutions()
        {
            // Reset AssignedTo weights.
            for (int i = 0; i < NumWeights; i++)
            {
                BestAssignedTo[i] = -1;
                TestAssignedTo[i] = -1;
            }

            // Reset totals.
            BestTotalWeight[0] = 0;
            BestTotalWeight[1] = 0;
            TestTotalWeight[0] = 0;
            TestTotalWeight[1] = 0;

            // Reset difference and nodes visited.
            BestDifference = int.MaxValue;
            NumNodesVisited = 0;
        }

        // If the test solution is an improvement,
        // update the best solution.
        private void CheckForImprovement()
        {
            int testDifference = Math.Abs(TestTotalWeight[0] - TestTotalWeight[1]);
            if (BestDifference > testDifference)
            {
                // This is an improvement, Save it.
                BestDifference = testDifference;
                for (int i = 0; i < NumWeights; i++)
                {
                    BestAssignedTo[i] = TestAssignedTo[i];
                }
                BestTotalWeight[0] = TestTotalWeight[0];
                BestTotalWeight[1] = TestTotalWeight[1];
            }
        }

        // Return True if we have an optimal solution.
        private bool ShortCircuit()
        {
            // Return true if:
            //   - We are allowed to short-circuit.
            //   - We have assigned weights.
            //   - The totals are equal.
            return AllowShortCircuit &&
                (BestTotalWeight[0] > 0) &&
                (BestTotalWeight[0] == BestTotalWeight[1]);
        }

        #endregion // Helper Routines

        #region "Exhaustive"

        private void exhaustiveButton_Click(object sender, EventArgs e)
        {
            // Get ready to run the algorithm.
            ReadWeights();
            if (NumWeights > 25)
            {
                if (MessageBox.Show("Warning: This tree contains " +
                    (Math.Pow(2, NumWeights + 1) - 2).ToString("n0") + " nodes" +
                     "\n\nDo you want to continue?",
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.No) return;
            }
            ClearResults(exhaustiveNodesVisitedTextBox, exhaustiveTimeTextBox, exhaustiveDifferenceTextBox);
            Stopwatch stopwatch = new Stopwatch();

            // Run the algorithm.
            stopwatch.Start();

            // Assign the first weight.
            ExhaustiveAssignWeight(0);

            stopwatch.Stop();
            ReportResults(exhaustiveNodesVisitedTextBox, exhaustiveTimeTextBox, exhaustiveDifferenceTextBox, stopwatch.Elapsed);
        }

        // Assign weight number nextIndex and
        // then recursively assign the other weights.
        private void ExhaustiveAssignWeight(int nextIndex)
        {
            // Short-circuit.
            if (ShortCircuit()) return;

            NumNodesVisited++;

            // See if we have assigned all weights.
            if (nextIndex >= NumWeights)
            {
                // We have assigned all weights.
                // See if the test solution is an improvement.
                CheckForImprovement();
            }
            else
            {
                // We have not assigned all weights.

                // Assign the weight to group 0.
                TestAssignedTo[nextIndex] = 0;
                TestTotalWeight[0] += Weights[nextIndex];
                ExhaustiveAssignWeight(nextIndex + 1);  // Recurse.
                TestTotalWeight[0] -= Weights[nextIndex];

                // Assign the weight to group 1.
                TestAssignedTo[nextIndex] = 1;
                TestTotalWeight[1] += Weights[nextIndex];
                ExhaustiveAssignWeight(nextIndex + 1);  // Recurse.
                TestTotalWeight[1] -= Weights[nextIndex];
            }
        }

        #endregion // Exhaustive

        #region "Random"

        // Perform random trials.
        private void randomButton_Click(object sender, EventArgs e)
        {
            // Calculate the number of trials.
            int numTrials = (int)(3 * Math.Pow(NumWeights, 3));

            // Get ready to run the algorithm.
            ReadWeights();
            ClearResults(randomNodesVisitedTextBox, randomTimeTextBox, randomDifferenceTextBox);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Perform the trials.
            for (int trial = 0; trial < numTrials; trial++)
            {
                NumNodesVisited += NumWeights;

                // Reset the test totals.
                TestTotalWeight[0] = 0;
                TestTotalWeight[1] = 0;

                // Make random assignments.
                Random rnd = new Random();
                for (int i = 0; i < NumWeights; i++)
                {
                    // Pick the group.
                    int group = rnd.Next(0, 2);

                    // Assign the weight.
                    TestAssignedTo[i] = group;
                    TestTotalWeight[group] += Weights[i];
                }

                // See if the test solution is an improvement.
                CheckForImprovement();

                // Short-circuit.
                if (ShortCircuit()) break;
            }

            stopwatch.Stop();
            ReportResults(randomNodesVisitedTextBox, randomTimeTextBox, randomDifferenceTextBox, stopwatch.Elapsed);
        }

        #endregion // Random

        #region "Improvements"

        // Make random trials with improvements.
        private void improvementsButton_Click(object sender, EventArgs e)
        {
            // Get the number of trials.
            int numTrials = (int)(Math.Pow(NumWeights, 3));

            // Get ready to run the algorithm.
            ReadWeights();
            ClearResults(improvementsNodesVisitedTextBox, improvementsTimeTextBox, improvementsDifferenceTextBox);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Perform the trials.
            for (int trial = 1; trial <= numTrials; trial++)
            {
                NumNodesVisited += NumWeights;

                // Reset the test totals.
                TestTotalWeight[0] = 0;
                TestTotalWeight[1] = 0;

                // Make random assignments.
                Random rnd = new Random();
                for (int i = 0; i < NumWeights; i++)
                {
                    // Pick the group.
                    int group = rnd.Next(0, 2);

                    // Assign the weight.
                    TestAssignedTo[i] = group;
                    TestTotalWeight[group] += Weights[i];
                }

                // Get the current difference.
                int testDifference = Math.Abs(TestTotalWeight[0] - TestTotalWeight[1]);

                // Try to make improvements.
                int numWithoutImprovement = 0;
                while (numWithoutImprovement < NumWeights)
                {
                    // Pick a random weight to move.
                    int i = rnd.Next(NumWeights);
                    int group = TestAssignedTo[i];
                    int newDifference = Math.Abs(
                        (TestTotalWeight[group] - Weights[i]) -
                        (TestTotalWeight[1 - group] + Weights[i]));
                    if (testDifference > newDifference)
                    {
                        testDifference = newDifference;
                        TestTotalWeight[group] -= Weights[i];
                        TestTotalWeight[1 - group] += Weights[i];
                        TestAssignedTo[i] = 1 - group;
                        numWithoutImprovement = 0;
                    }
                    else
                    {
                        numWithoutImprovement += 1;
                    }

                    NumNodesVisited += 1;
                }

                // See if the improved test solution is an improvement.
                CheckForImprovement();

                // Short-circuit.
                if (ShortCircuit()) break;
            }

            stopwatch.Stop();
            ReportResults(improvementsNodesVisitedTextBox, improvementsTimeTextBox, improvementsDifferenceTextBox, stopwatch.Elapsed);
        }
        #endregion // Improvements

        #region "Hill Climbing"

        // Add weights to the group with the smaller current total.
        private void hillClimbingButton_Click(object sender, EventArgs e)
        {
            // Get ready to run the algorithm.
            ReadWeights();
            ClearResults(hillClimbingNodesVisitedTextBox, hillClimbingTimeTextBox, hillClimbingDifferenceTextBox);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Make the assignments.
            for (int i = 0; i < NumWeights; i++)
            {
                // See which group has the smaller total.
                int group;
                if (BestTotalWeight[0] < BestTotalWeight[1])
                {
                    group = 0;
                }
                else
                {
                    group = 1;
                }

                BestAssignedTo[i] = group;
                BestTotalWeight[group] += Weights[i];
            }
            BestDifference = Math.Abs(BestTotalWeight[0] - BestTotalWeight[1]);
            NumNodesVisited = NumWeights;

            stopwatch.Stop();
            ReportResults(hillClimbingNodesVisitedTextBox, hillClimbingTimeTextBox, hillClimbingDifferenceTextBox, stopwatch.Elapsed);
        }
        #endregion // Hill Climbing

        #region "Sorted Hill Climbing"

        // Sort the weights and then add them in order of decreasing
        // weight to the group with the smaller current total.
        private void sortedHillClimbingButton_Click(object sender, EventArgs e)
        {
            // Get ready to run the algorithm.
            ReadWeights();
            ClearResults(sortedHillClimbingNodesVisitedTextBox, sortedHillClimbingTimeTextBox, sortedHillClimbingDifferenceTextBox);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Sort the weights.
            Array.Sort(Weights);

            // Make the assignments.
            for (int i = NumWeights - 1; i >= 0; i--)
            {
                // See which group has the smaller total.
                int group;
                if (BestTotalWeight[0] < BestTotalWeight[1])
                {
                    group = 0;
                }
                else
                {
                    group = 1;
                }

                BestAssignedTo[i] = group;
                BestTotalWeight[group] += Weights[i];
            }
            BestDifference = Math.Abs(BestTotalWeight[0] - BestTotalWeight[1]);
            NumNodesVisited = NumWeights;

            stopwatch.Stop();
            ReportResults(sortedHillClimbingNodesVisitedTextBox, sortedHillClimbingTimeTextBox, sortedHillClimbingDifferenceTextBox, stopwatch.Elapsed);
        }
        #endregion // Sorted Hill Climbing

        #region "Branch and Bound"

        // Branch and bound.
        private void branchAndBoundButton_Click(object sender, EventArgs e)
        {
            // Get ready to run the algorithm.
            ReadWeights();
            if (NumWeights > 28)
            {
                if (MessageBox.Show("Warning: This tree contains " +
                    (Math.Pow(2, NumWeights + 1) - 2).ToString("n0") + " nodes" +
                     "\n\nDo you want to continue?",
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.No) return;
            }
            ClearResults(branchAndBoundNodesVisitedTextBox, branchAndBoundTimeTextBox, branchAndBoundDifferenceTextBox);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Calculate the total weight available.
            int totalWeight = 0;
            for (int i = 0; i < NumWeights; i++)
            {
                totalWeight += Weights[i];
            }

            // Assign the first weight.
            BranchAndBoundAssignWeight(0, totalWeight);

            stopwatch.Stop();
            ReportResults(branchAndBoundNodesVisitedTextBox, branchAndBoundTimeTextBox, branchAndBoundDifferenceTextBox, stopwatch.Elapsed);
        }

        // Assign weight number nextIndex and
        // then recursively assign the other weights.
        // Use branch and bound.
        private void BranchAndBoundAssignWeight(int nextIndex, int totalUnassigned)
        {
            // Short-circuit.
            if (ShortCircuit()) return;

            NumNodesVisited++;

            // See if we have assigned all weights.
            if (nextIndex >= NumWeights)
            {
                // We have assigned all weights.
                // If we get here, this *is* an improvement.
                CheckForImprovement();
            }
            else
            {
                // We have not assigned all weights.

                // See if we can improve on the best solution.
                // If the current difference is so big that
                // all of the remaining weights cannot reduce
                // it below the best difference, then there//s no hope.
                int testDifference = Math.Abs(TestTotalWeight[0] - TestTotalWeight[1]);
                if (testDifference - totalUnassigned > BestDifference) return;

                totalUnassigned -= Weights[nextIndex];

                // Assign the weight to group 0.
                TestAssignedTo[nextIndex] = 0;
                TestTotalWeight[0] += Weights[nextIndex];
                BranchAndBoundAssignWeight(nextIndex + 1, totalUnassigned);  // Recurse.
                TestTotalWeight[0] -= Weights[nextIndex];

                // Assign the weight to group 1.
                TestAssignedTo[nextIndex] = 1;
                TestTotalWeight[1] += Weights[nextIndex];
                BranchAndBoundAssignWeight(nextIndex + 1, totalUnassigned);  // Recurse.
                TestTotalWeight[1] -= Weights[nextIndex];
            }
        }

        #endregion // Branch and Bound

    }
}