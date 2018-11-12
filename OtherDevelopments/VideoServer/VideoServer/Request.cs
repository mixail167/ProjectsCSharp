using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoServer
{
    class Request
    {
        public string URL { get; set; }
        public string Type { get; set; }
        public string Host { get; set; }

        private Request(string type, string url, string host)
        {
            Type = type;
            URL = url;
            Host = host;
        }

        public static Request GetRequest(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }
            string[] tokens = message.Split(' ', '\n');
            return new Request(tokens[0], tokens[1], tokens[4]);
        }
    }
}
