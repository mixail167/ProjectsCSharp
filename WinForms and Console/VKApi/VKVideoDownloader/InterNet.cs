using System.Runtime.InteropServices;

namespace VKVideoDownloader
{
    class InterNet
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsConnected
        {
            get
            {
                return InternetGetConnectedState(out _, 0);
            }
        }
    }
}
