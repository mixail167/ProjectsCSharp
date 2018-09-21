using System.Collections.Generic;
using System.Drawing;
using Un4seen.Bass.AddOn.Tags;

namespace AudioPlayer
{
    public class Tag
    {
        private int bitRate;
        private int freq;
        private string channels;
        private string album;
        private string artist;
        private string title;
        private string year;
        private string genre;
        private string path;
        private string fileName;
        private bool error;
        private Image image;

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
                if (CommonInterface.IsValid(path, CommonInterface.URLPattern) && !CommonInterface.IsValid(path, CommonInterface.PathPattern2))
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
                bitRate = tagInfo.bitrate;
                freq = tagInfo.channelinfo.freq;
                channels = ChannelsDict[tagInfo.channelinfo.chans];
                artist = tagInfo.artist;
                album = tagInfo.album;
                genre = tagInfo.genre;
                title = tagInfo.title;
                year = tagInfo.year;
                this.path = path;
                fileName = System.IO.Path.GetFileName(path);
                image = tagInfo.PictureGetImage(0);
                error = false;
            }
            else
            {
                error = true;
            }
        }

        public Image Image
        {
            get { return image; }
        }

        public int BitRate
        {
            get { return bitRate; }
        }

        public int Freq
        {
            get { return freq; }
        }

        public bool Error
        {
            get { return error; }
        }

        public string Channels
        {
            get { return channels; }
        }

        public string Artist
        {
            get { return artist; }
        }

        public string Album
        {
            get { return album; }
        }

        public string Genre
        {
            get { return genre; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Year
        {
            get { return year; }
        }

        public string Path
        {
            get { return path; }
        }

        public string FileName
        {
            get { return fileName; }
        }
    }
}
