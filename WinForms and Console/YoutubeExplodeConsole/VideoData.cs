using System.IO;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplodeConsole
{
    class VideoData
    {
        public ClosedCaptionTrackInfo TrackInfo { get; private set; }
        public string SavePath { get; private set; }
        public string Title { get; private set; }
        public string TitleReplaced { get; private set; }
        public IVideoStreamInfo StreamInfo { get; private set; }
        public string Extension { get => StreamInfo.Container.Name; }
        public long Size { get => StreamInfo.Size.Bytes; }

        public VideoData(string title, IVideoStreamInfo streamInfo, ClosedCaptionTrackInfo trackInfo, string path)
        {
            Title = title;
            StreamInfo = streamInfo;
            TrackInfo = trackInfo;
            SavePath = ReplaceChars(Path.GetInvalidPathChars(), path);
            TitleReplaced = ReplaceChars(Path.GetInvalidFileNameChars(), Title);
        }

        private string ReplaceChars(char[] chars, string title)
        {
            foreach (char item in chars)
            {
                if (title.IndexOf(item) != -1)
                {
                    title = title.Replace(item, '_');
                }
            }
            return title;
        }
    }
}
