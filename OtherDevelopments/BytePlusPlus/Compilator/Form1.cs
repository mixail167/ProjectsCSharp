using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Compilator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", textBox2.Text } });
            CompilerParameters parameters = new CompilerParameters(new string[] { "mscorlib.dll", "System.Core.dll" }, textBox1.Text, true) { GenerateExecutable = true };
            try
            {
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, richTextBox1.Text);
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError item in results.Errors.Cast<CompilerError>())
                    {
                        richTextBox2.Text += string.Format("Строка {0}: {1}\n", item.Line, item.ErrorText);
                    }
                }
                else
                {
                    richTextBox2.Text += "Сборка завершена.\n";
                    Process.Start(Application.StartupPath + "//" + textBox1.Text);
                }
            }
            catch (Exception exception)
            {
                richTextBox2.Text += exception.Message + "\n";
            }
        }
    }
}
