using System;

namespace YoutubeExtractor
{
    public class DecipherSignatureException : Exception
    {
        public DecipherSignatureException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
