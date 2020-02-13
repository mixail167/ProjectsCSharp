using Menees.Diffs;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            IList<string> a = DiffUtility.GetStringTextLines(richTextBox1.Text);
            IList<string> b = DiffUtility.GetStringTextLines(richTextBox2.Text);
            TextDiff diff = new TextDiff(HashType.Unique, true, true);
            EditScript script = diff.Execute(a, b);
            diffControl1.SetData(a, b, script, "Text 1", "Text 2");
        }
    }
}
