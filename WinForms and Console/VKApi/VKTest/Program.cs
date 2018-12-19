using Newtonsoft.Json.Linq;
using System;
using xNet;

namespace VKTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRequest request = new HttpRequest();
            string login = Console.ReadLine();
            string password = Console.ReadLine();
            try
            {
                dynamic json = JObject.Parse(request.Get("https://oauth.vk.com/token?grant_type=password&client_id=2274003&client_secret=hHbZxrka2uZ6jB1inYsH&username=" + login + "&password=" + password).ToString());
                string id = json.user_id.ToString();
                if (id.Length > 1)
                {
                    json = JObject.Parse(request.Get("https://api.vk.com/method/account.getProfileInfo?access_token=" + json.access_token).ToString());
                    Console.WriteLine(json.response.first_name + " " + json.response.last_name);
                }
            }
            catch (Exception)
            {

            }
            Console.ReadKey();
        }
    }
}
