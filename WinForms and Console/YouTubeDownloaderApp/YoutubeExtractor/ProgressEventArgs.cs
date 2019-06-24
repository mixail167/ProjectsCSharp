﻿using System;

namespace YoutubeExtractor
{
    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(double progressPercentage, int bytesReceived = 0, int totalBytesReceived = 0)
        {
            this.ProgressPercentage = progressPercentage;
            this.BytesReceived = bytesReceived;
            this.TotalBytesReceived = totalBytesReceived;
        }

        /// <summary>
        /// Gets or sets a token whether the operation that reports the progress should be canceled.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the progress percentage in a range from 0.0 to 100.0.
        /// </summary>
        public double ProgressPercentage { get; private set; }

        /// <summary>
        ///  Gets the received bytes
        /// </summary>
        public int TotalBytesReceived { get; private set; }

        public int BytesReceived { get; private set; }
    }
}