using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;

namespace ChatServer
{
    public class OneInstanceApp : WindowsFormsApplicationBase
    {
        private OneInstanceApp()
        {
            base.IsSingleInstance = true;
        }

        public static void Run(Form form)
        {
            OneInstanceApp oneInstanceApp =
                new OneInstanceApp();
            oneInstanceApp.MainForm = form;
            oneInstanceApp.StartupNextInstance += StartupNextInstanceHandler;
            try
            {
                oneInstanceApp.Run(Environment.GetCommandLineArgs());
            }
            catch (ObjectDisposedException)
            {
                
            }
        }

        private static void StartupNextInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            ((sender as OneInstanceApp).MainForm as Form1).ModifyStateForm(true);
        }
    }
}
