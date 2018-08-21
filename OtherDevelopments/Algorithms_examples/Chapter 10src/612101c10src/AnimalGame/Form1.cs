using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The tree's root.
        private AnimalNode Root;

        // Create an initial tree.
        private void Form1_Load(object sender, EventArgs e)
        {
            Root = new AnimalNode("Is it a mammal?");
            Root.YesChild = new AnimalNode("dog");
            Root.NoChild = new AnimalNode("fish");
        }

        // Play the game.
        private void playButton_Click(object sender, EventArgs e)
        {
            AnimalNode node = Root;
            for (; ; )
            {
                // See if this is an internal or leaf node.
                if (node.YesChild == null)
                {
                    // It's a leaf node.
                    ProcessLeafNode(node);
                    return;
                }

                // It's an internal node.
                // Ask the node's question and move to the appropriate child node.
                if (MessageBox.Show(node.Question, "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes) node = node.YesChild;
                else node = node.NoChild;
            }
        }

        // Process a leaf node.
        private void ProcessLeafNode(AnimalNode leaf)
        {
            // This is a leaf node. Guess the animal.
            string text = "Is your animal " + Article(leaf.Question) +
                " " + leaf.Question + "?";
            if (MessageBox.Show(text, leaf.Question,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                // We got it right. Gloat and exit.
                MessageBox.Show("Victory is mine!", "I Win!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // We got it wrong.
            // Ask the user for the new animal.
            PromptForm dialog = new PromptForm();
            dialog.promptLabel.Text = "What is your animal?";
            dialog.responseTextBox.Clear();
            dialog.ShowDialog();
            string newAnimal = dialog.responseTextBox.Text.ToLower();

            // Ask the user for a new question.
            dialog.promptLabel.Text =
                "What question could I ask to differentiate between " +
                Article(leaf.Question) + " " + leaf.Question + " and " +
                Article(newAnimal) + " " + newAnimal + "?";
            dialog.responseTextBox.Clear();
            dialog.ShowDialog();
            string newQuestion = dialog.responseTextBox.Text;

            // See if the question's answer is true for the new animal.
            bool newAnimalIsTrue =
                MessageBox.Show("Is the answer to this question true for " +
                Article(newAnimal) + " " + newAnimal + "?",
                "New Animal", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            // Update the knowledge tree.
            string oldAnimal = leaf.Question;
            leaf.Question = newQuestion;
            if (newAnimalIsTrue)
            {
                leaf.YesChild = new AnimalNode(newAnimal);
                leaf.NoChild = new AnimalNode(oldAnimal);
            }
            else
            {
                leaf.YesChild = new AnimalNode(oldAnimal);
                leaf.NoChild = new AnimalNode(newAnimal);
            }
        }

        // Return "a" or "an" as appropriate for the noun.
        private string Article(string noun)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            if (noun.IndexOfAny(vowels) == 0) return "an";
            return "a";
        }
    }
}
