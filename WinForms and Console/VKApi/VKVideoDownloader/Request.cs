using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace VKVideoDownloader
{
    class Request
    {
        readonly string url;

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
