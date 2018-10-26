using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;

namespace ChatClient
{
    public class OneInstanceApp : WindowsFormsApplicationBase
    {
        private OneInstanceApp()
        {
            base.IsSingleInstance = true;
        }

        public static void Run(Form form)
        {
            OneInstanceApp oneInstanceApp = new OneInstanceApp();
            oneInstanceApp.MainForm = form;
            oneInstanceApp.StartupNextInstance += StartupNextInstanceHandler;
            oneInstanceApp.Run(Environment.GetCommandLineArgs());
        }

        private static void StartupNextInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            ((sender as OneInstanceApp).MainForm as Form1).ModifyStateForm(true);
        }
    }
}
