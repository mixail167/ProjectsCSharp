using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace VideoServer
{
    class HttpServer
    {
        public const string Message_dir = @"\root\message\";
        public const string Web_dir = @"\root\web";
        public const string ServerName = "myserv/1.1";
        public const string Version = "HTTP/1.0";

        TcpListener listener;
        bool running = false;

        public HttpServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        private void Run()
        {
            listener.Start();
            running = true;
            while (running)
            {
                TcpClient client = listener.AcceptTcpClient();
                HandleClient(client);
                client.Close();
            }
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());
            string message = string.Empty;
            while (reader.Peek() != -1)
            {
                message += reader.ReadLine() + "\n";
            }
            Request request = Request.GetRequest(message);
            Response response = Response.From(request);
            response.Post(client.GetStream());
        }
    }
}
