using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer(8080);
            server.Start();
            Console.ReadKey();
        }
    }
}
