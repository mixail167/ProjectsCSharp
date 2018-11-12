using System.ComponentModel;
using System.IO;
using System.Media;

namespace MineSweeper.Classes
{
    class WAVPlayer
    {
        public static bool Sound = true;
        static SoundPlayer soundPlayer = null;

        public static void PlaySound(UnmanagedMemoryStream ums)
        {
            if (Sound)
            {
                soundPlayer = new SoundPlayer(ums);
                soundPlayer.LoadCompleted += soundPlayer_LoadCompleted;
                soundPlayer.LoadAsync();
            }
        }

        static void soundPlayer_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            soundPlayer.Play();
        }

        public static void StopSound()
        {
            if (soundPlayer != null)
            {
                soundPlayer.Stop();
            }
        }
    }
}
