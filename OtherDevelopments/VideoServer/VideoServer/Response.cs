using System;
using System.IO;
using System.Net.Sockets;

namespace VideoServer
{
    class Response
    {
        byte[] data;
        string status;
        string mime;

        private Response(string status, string mime, byte[] data)
        {
            this.data = data;
            this.status = status;
            this.mime = mime;
        }

        public static Response From(Request request)
        {
            if (request == null)
            {
                return NotWork("400.html", "400 bad request");
            }
            if (request.Type == "GET")
            {
                string file = Environment.CurrentDirectory + HttpServer.Web_dir + request.URL.Replace('/', '\\');
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Exists && !fileInfo.Extension.Contains("*.*"))
                {
                    return MakeFromFile(fileInfo);
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(file);
                    if (!directoryInfo.Exists)
                    {
                        return NotWork("404.html", "404 Page Not Found");
                    }
                    FileInfo[] files = directoryInfo.GetFiles();
                    foreach (FileInfo item in files)
                    {
                        if (item.Name.Contains("default.html") || item.Name.Contains("index.html"))
                        {
                            return MakeFromFile(item);
                        }
                    }
                }
            }
            else
            {
                return NotWork("405.html", "405 Method Not Allowed");
            }
            return NotWork("404.html", "404 Page Not Found");
        }

        private static Response MakeFromFile(FileInfo fileInfo)
        {
            FileStream fileStream = fileInfo.OpenRead();
            byte[] data = new byte[fileStream.Length];
            BinaryReader reader = new BinaryReader(fileStream);
            reader.Read(data, 0, data.Length);
            return new Response("200 OK", "text/html", data);
        }

        private static Response NotWork(string filename, string status)
        {
            string file = Environment.CurrentDirectory + HttpServer.Message_dir + filename;
            FileInfo fileInfo = new FileInfo(file);
            FileStream fs = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            byte[] data = new byte[fs.Length];
            reader.Read(data, 0, data.Length);
            return new Response(status, "text/html", data);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("{0} {1}\r\nServer: {2}Content-Language: ru\r\nContent-type: {3}\r\nAccept-Ranges: bytes\r\nContentLength: {4}\r\nConnection: close\r\n", HttpServer.Version, status, HttpServer.ServerName, mime, data.Length);
            writer.Flush();
            stream.Write(data, 0, data.Length);
        }
    }
}
