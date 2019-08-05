using Accord.Video;
using Accord.Video.DirectShow;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WebCam
{
    public partial class MainWindow : Window
    {
        private FilterInfoCollection availableWebcams;
        private VideoCaptureDevice webcam;

        public MainWindow()
        {
            InitializeComponent();
            FillWebcams();
            SetFirstWebcam();
        }

        private void FillWebcams()
        {
            availableWebcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (var webcam in availableWebcams)
            {
                ComboBoxWebcams.Items.Add(webcam.Name);
            }
        }

        private void SetFirstWebcam()
        {
            if (ComboBoxWebcams.Items.Count > 0)
            {
                ComboBoxWebcams.SelectedIndex = 0;
            }
        }

        private void ComboBoxWebcams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StopWebcam();
            StartWebcam();
        }

        private void StartWebcam()
        {
            webcam = new VideoCaptureDevice(availableWebcams[ComboBoxWebcams.SelectedIndex].MonikerString);
            webcam.Start();
            webcam.NewFrame += new NewFrameEventHandler(webcam_NewFrame);
        }

        private void StopWebcam()
        {
            if (webcam != null && webcam.IsRunning)
            {
                webcam.SignalToStop();
                webcam.NewFrame -= new NewFrameEventHandler(webcam_NewFrame);
                webcam = null;
            }
        }

        private void webcam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            eventArgs.Frame.RotateFlip(RotateFlipType.Rotate180FlipY);
            ImageWebcamFrame.Source = BitmapToBitmapImage(eventArgs.Frame);
        }

        public BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void ButtonPhoto_Click(object sender, RoutedEventArgs e)
        {
            const string pathImageFolder = @"D:\\Photo\";
            string pathToImage = string.Format("{0}{1}.png", pathImageFolder, Guid.NewGuid());

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageWebcamFrame.Source));
            using (FileStream fileStream = new FileStream(pathToImage, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StopWebcam();
        }
    }
}
