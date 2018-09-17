using System.Collections.Generic;
using Un4seen.Bass.AddOn.Tags;

namespace AudioPlayer
{
    public class Tag
    {
        public int BitRate;
        public int Freq;
        public string Channels;
        public string Album;
        public string Artist;
        public string Title;
        public string Year;
        public string Genre;
        public string Path;
        public string FileName;
        public bool Error;

        Dictionary<int, string> ChannelsDict = new Dictionary<int, string>()
        {
            {0,"Null"},
            {1,"Mono"},
            {2,"Stereo"}
        };

        public Tag(string path, bool isRadio)
        {
            TAG_INFO tagInfo = null;
            if (isRadio)
            {
                if (CommonInterface.IsValid(path))
                {
                    tagInfo = new TAG_INFO(path);
                }
            }
            else
            {
                tagInfo = BassTags.BASS_TAG_GetFromFile(path);
            }
            if (tagInfo != null)
            {
                BitRate = tagInfo.bitrate;
                Freq = tagInfo.channelinfo.freq;
                Channels = ChannelsDict[tagInfo.channelinfo.chans];
                Artist = tagInfo.artist;
                Album = tagInfo.album;
                Genre = tagInfo.genre;
                Title = tagInfo.title;
                Year = tagInfo.year;
                Path = path;
                string[] tmp = path.Split('\\');
                FileName = tmp[tmp.Length - 1];
                Error = false;
            }
            else
            {
                Error = true;
            }
        }
    }
}
