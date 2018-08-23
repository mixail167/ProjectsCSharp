using System.IO;
using System.Media;

namespace MineSweeper.Classes
{
    class WAVPlayer
    {
        public static bool sound = true;
        static SoundPlayer soundPlayer = new SoundPlayer();

        public static void PlaySound(UnmanagedMemoryStream ums)
        {
            if (sound)
            {
                soundPlayer = new SoundPlayer(ums);
                soundPlayer.Play();
            }
        }

        public static void StopSound()
        {
            soundPlayer.Stop();
        }
    }
}
