using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;

namespace ChatClient
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OneInstanceApp.Run(new Form1());
        }
    }
}
