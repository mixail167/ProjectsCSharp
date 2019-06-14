using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace VKVideoDownloader
{
    class Functions
    {
        public static bool CheckValid(string text, string pattern)
        {
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(text))
            {
                return true;
            }
            return false;
        }

        public static int GetCount(string url, out string errorText)
        {
            Request request = new Request(url);
            int count = 0;
            errorText = string.Empty;
            if (InterNet.IsConnected)
            {
                try
                {
                    JObject json = JObject.Parse(request.Get());
                    if (json.ContainsKey("response"))
                    {
                        JObject response = json["response"] as JObject;
                        if (response.ContainsKey("count"))
                        {
                            count = Convert.ToInt32(response["count"]);
                        }
                    }
                    else if (json.ContainsKey("error"))
                    {
                        JObject error = json["error"] as JObject;
                        int code = Convert.ToInt32(error["error_code"]);
                        string message = error["error_msg"].ToString();
                        errorText = string.Format("Ошибка {0}: {1}", code, message);
                        count = -1;
                    }
                }
                catch (Exception e)
                {
                    count = -1;
                    errorText = e.Message;
                } 
            }
            else
            {
                count = -1;
                errorText = "Отсутствует соединение с сетью Интернет.";
            }
            return count;
        }
    }
}
