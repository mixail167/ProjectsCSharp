using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VKVideoDownloader
{
    class Request
    {
        string url;

        public Request(string url)
        {
            this.url = url;
        }

        public async Task<string> GetAsync()
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }


        public string Get()
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
