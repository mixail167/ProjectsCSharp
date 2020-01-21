﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace YoutubeExtractor
{
    internal static class HttpHelper
    {
        public static string DownloadString(string url)
        {
#if PORTABLE
            var request = WebRequest.Create(url);
            request.Method = "GET";

            System.Threading.Tasks.Task<WebResponse> task = System.Threading.Tasks.Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result)).Result;
#else
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                return client.DownloadString(url);
            }
#endif
        }

        public static string HtmlDecode(string value)
        {
#if PORTABLE
            return System.Net.WebUtility.HtmlDecode(value);
#else
            return System.Web.HttpUtility.HtmlDecode(value);
#endif
        }

        public static IDictionary<string, string> ParseQueryString(string s)
        {
            // remove anything other than query string from url
            if (s.Contains("?"))
            {
                s = s.Substring(s.IndexOf('?') + 1);
            }

            var dictionary = new Dictionary<string, string>();

            foreach (string vp in Regex.Split(s, "&"))
            {
                string[] strings = Regex.Split(vp, "=");

                string key = strings[0];
                string value = string.Empty;

                if (strings.Length == 2)
                    value = strings[1];
                else if (strings.Length > 2)
                    value = string.Join("=", strings.Skip(1).ToArray());

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static string ReplaceQueryStringParameter(string currentPageUrl, string paramToReplace, string newValue)
        {
            var query = ParseQueryString(currentPageUrl);

            query[paramToReplace] = newValue;

            var resultQuery = new StringBuilder();
            bool isFirst = true;

            foreach (KeyValuePair<string, string> pair in query)
            {
                if (!isFirst)
                {
                    resultQuery.Append("&");
                }

                resultQuery.Append(pair.Key);
                resultQuery.Append("=");
                resultQuery.Append(pair.Value);

                isFirst = false;
            }

            var uriBuilder = new UriBuilder(currentPageUrl)
            {
                Query = resultQuery.ToString()
            };

            return uriBuilder.ToString();
        }

        public static string UrlDecode(string url)
        {
#if PORTABLE
            return System.Net.WebUtility.UrlDecode(url);
#else
            return System.Web.HttpUtility.UrlDecode(url);
#endif
        }

        public static string UrlEncode(string url)
        {
#if PORTABLE
            return System.Net.WebUtility.UrlEncode(url);
#else
            return System.Web.HttpUtility.UrlEncode(url);
#endif
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(responseStream))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}