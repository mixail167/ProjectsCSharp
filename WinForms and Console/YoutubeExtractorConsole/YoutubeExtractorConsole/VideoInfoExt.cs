using YoutubeExtractor;

namespace YoutubeExtractorConsole
{
    struct VideoInfoExt
    {
        public VideoInfoExt(VideoInfo videoInfo, string path)
        {
            VideoInfo = videoInfo.Clone() as VideoInfo;
            Path = path;
        }

        public VideoInfo VideoInfo { get; private set; }
        public string Path { get; private set; }
        public string Title { get => VideoInfo.Title; }
        public string VideoExtension { get => VideoInfo.VideoExtension; }
    }
}
