using System.Collections.Generic;
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
        public List<IStreamInfo> Streams { get; private set; }
        public string Extension { get => Streams[0].Container.Name; }
        public long Size
        {
            get
            {
                long size = 0;
                foreach (IStreamInfo item in Streams)
                {
                    size += item.Size.Bytes;
                }
                return size;
            }
        }

        public VideoData(string title, List<IStreamInfo> streams, ClosedCaptionTrackInfo trackInfo, string path)
        {
            Title = title;
            Streams = streams;
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
