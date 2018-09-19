using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;

namespace AudioPlayer
{
    public class OneInstanceApp : WindowsFormsApplicationBase
    {
        private OneInstanceApp()
        {
            base.IsSingleInstance = true;
        }

        public static void Run(Form form, StartupNextInstanceEventHandler startupHandler)
        {
            OneInstanceApp oneInstanceApp =
                new OneInstanceApp();
            oneInstanceApp.MainForm = form;
            oneInstanceApp.StartupNextInstance += startupHandler;
            oneInstanceApp.Run(Environment.GetCommandLineArgs());
        }
    }
}
