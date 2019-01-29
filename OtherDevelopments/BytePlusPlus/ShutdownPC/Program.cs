using System.Diagnostics;

namespace ShutdownPC
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = new Process();
            p.StartInfo.FileName = "shutdown.exe";
            p.StartInfo.Arguments = "/s /t 0";
            p.Start();
        }
    }
}
