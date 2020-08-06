using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form1 : Form
    {
        GraphClass graphClass;

        public Form1()
        {
            InitializeComponent();
        }

        private void AdjacencyMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input;
            if ((input = ReadFile()) != null)
            {
                string[] lines = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                char[,] matrix = new char[lines.Length, lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length && j < lines.Length; j++)
                    {
                        matrix[i, j] = lines[i][j];
                    }
                }
                graphClass = GraphClass.NewInstanceByAdjacencyMatrix(matrix);
            }
        }

        string ReadFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return File.ReadAllText(openFileDialog1.FileName);
                }
                catch { }
            }
            return null;
        }

        private void incidenceMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input;
            if ((input = ReadFile()) != null)
            {
                string[] lines = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                int length = lines[0].Length;
                char[,] matrix = new char[lines.Length, length];
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length && j < length; j++)
                    {
                        matrix[i, j] = lines[i][j];
                    }
                }
                graphClass = GraphClass.NewInstanceByIncidenceyMatrix(matrix);
            }
        }

        private void adjacencyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input;
            if ((input = ReadFile()) != null)
            {
                string[] lines = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Regex regex = new Regex("^[1-9]{1}[0-9]*:([1-9]{1}[0-9]*){1}(,[1-9]{1}[0-9]*)*$");
                string[] adjacencyList = lines.Where(x => regex.IsMatch(x)).ToArray();
                if (adjacencyList.Length > 0)
                {
                    graphClass = GraphClass.NewInstanceByAdjacencyList(adjacencyList);
                }
            }
        }

        private void edgesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input;
            if ((input = ReadFile()) != null)
            {
                string[] lines = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Regex regex = new Regex("^[1-9]{1}[0-9]*-[1-9]{1}[0-9]*$");
                string[] listEdges = lines.Where(x => regex.IsMatch(x)).ToArray();
                if (listEdges.Length > 0)
                {
                    graphClass = GraphClass.NewInstanceByListEdges(listEdges);
                }
            }
        }
    }
}
