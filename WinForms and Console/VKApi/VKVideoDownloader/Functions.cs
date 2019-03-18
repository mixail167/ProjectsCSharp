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
    }
}
