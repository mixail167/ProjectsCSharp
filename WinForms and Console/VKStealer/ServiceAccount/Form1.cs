using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using IO = System.IO;

namespace ServiceAccount
{
    public partial class Form1 : Form
    {
        DriveService service;

        public Form1()
        {
            InitializeComponent();
            try
            {
                service = new DriveService(new BaseClientService.Initializer()
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

            }
            finally
            {
                InitTree(service);
            }
        }


        private async void InitTree(DriveService service)
        {
            listView1.Items.Clear();
            treeView1.Nodes.Clear();
            if (service != null)
            {
                FileList fileList = await GetFileList(service, "items(id, title, parents)", "mimeType = 'application/vnd.google-apps.folder'");
                if (fileList != null && fileList.Items.Count > 0)
                {
                    try
                    {
                        List<File> listFile = fileList.Items.Reverse().ToList();
                        TreeNode rootNode = new TreeNode("Root")
                        {
                            Tag = "root",
                            ContextMenuStrip = contextMenuStrip1
                        };
                        foreach (File item in listFile)
                        {
                            TreeNode node = new TreeNode(item.Title)
                            {
                                Tag = item.Id,
                                ContextMenuStrip = contextMenuStrip1
                            };
                            if (item.Parents[0].IsRoot == true)
                            {
                                rootNode.Nodes.Add(node);
                            }
                            else
                            {
                                File file = listFile.FirstOrDefault(x => x.Id == item.Parents[0].Id);
                                if (file != null)
                                {
                                    TreeNode nodeParent = SearchParent(rootNode, file.Id);
                                    if (nodeParent != null)
                                    {
                                        nodeParent.Nodes.Add(node);
                                    }
                                }
                            }
                        }
                        treeView1.Nodes.Add(rootNode);
                        treeView1.ExpandAll();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private TreeNode SearchParent(TreeNode beginNode, string id)
        {
            TreeNode node = null;
            while (beginNode != null)
            {
                if (beginNode.Tag.ToString().Equals(id))
                {
                    node = beginNode;
                    break;
                };
                if (beginNode.Nodes.Count != 0)
                {
                    node = SearchParent(beginNode.Nodes[0], id);
                    if (node != null)
                    {
                        break;
                    };
                };
                beginNode = beginNode.NextNode;
            };
            return node;
        }

        private async Task<FileList> GetFileList(DriveService driveService, string fields, string q)
        {
            try
            {
                FilesResource.ListRequest request = driveService.Files.List();
                request.Q = q;
                request.Fields = fields;
                return await request.ExecuteAsync();
            }
            catch
            {
                return null;
            }
        }

        private async Task<File> GetFile(DriveService driveService, string fields, string id)
        {
            try
            {
                FilesResource.GetRequest request = driveService.Files.Get(id);
                request.Fields = fields;
                return await request.ExecuteAsync();
            }
            catch
            {
                return null;
            }
        }

        private async void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string id = treeView1.SelectedNode.Tag.ToString();
            if (e.ClickedItem == toolStripMenuItem1)
            {
                DownloadFolder(service, id, treeView1.SelectedNode.Text);
            }
            else
            {
                string response = await Delete(service, id);
                if (response != null && response == string.Empty)
                {
                    treeView1.Nodes.Remove(treeView1.SelectedNode);
                    listView1.Items.Clear();
                }
            }
        }

        private async Task<string> Delete(DriveService service, string id)
        {
            try
            {
                return await service.Files.Delete(id).ExecuteAsync();
            }
            catch
            {
                return null;
            }
        }

        private async void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode == null || !treeView1.SelectedNode.Equals(e.Node))
            {
                treeView1.SelectedNode = e.Node;
                listView1.Items.Clear();
                FileList fileList = await GetFileList(service, "items(id, title, description, createdDate, modifiedDate, lastModifyingUserName, mimeType, fullFileExtension, fileSize)", string.Format("'{0}' in parents and mimeType != 'application/vnd.google-apps.folder'", treeView1.SelectedNode.Tag.ToString()));
                if (fileList != null && fileList.Items.Count > 0)
                {
                    foreach (File item in fileList.Items)
                    {
                        listView1.Items.Add(new ListViewItem(new string[]
                        {
                            item.Title,
                            item.Description,
                            item.CreatedDate.HasValue ? item.CreatedDate.Value.ToString() : "",
                            item.ModifiedDate.HasValue ? item.ModifiedDate.Value.ToString() : "",
                            item.LastModifyingUserName,
                            item.MimeType,
                            item.FullFileExtension,
                            item.FileSize.HasValue ? item.FileSize.Value.ToString() : ""
                        }) { Tag = item });
                    }
                }
            }
        }

        private async void DownloadFolder(DriveService service, string id, string folderName)
        {
            FileList fileList = await GetFileList(service, "items(id, title, fileSize, mimeType)", string.Format("'{0}' in parents and mimeType != 'application/vnd.google-apps.folder'", id));
            if (fileList != null && fileList.Items.Count > 0 && folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Downloading(service, fileList.Items, folderBrowserDialog1.SelectedPath, folderName);
            }
        }

        private async void Downloading(DriveService service, IList<File> fileList, string path, string folderName)
        {
            progressBar2.Maximum = fileList.Count;
            label2.Text = string.Format("Загружено файлов: {0}/{1}", progressBar2.Value, progressBar2.Maximum);
            groupBox1.Visible = true;
            foreach (File item in fileList)
            {
                label1.Text = string.Format("Загрузка файла: {0}", item.Title);
                IO.MemoryStream stream = new IO.MemoryStream();
                FilesResource.GetRequest request = service.Files.Get(item.Id);
                if (item.FileSize > int.MaxValue)
                {
                    progressBar1.Maximum = (int)(item.FileSize * int.MaxValue / long.MaxValue);
                }
                else
                {
                    progressBar1.Maximum = (int)(item.FileSize);
                }
                if (progressBar1.Maximum >= 100)
                {
                    request.MediaDownloader.ChunkSize = progressBar1.Maximum / 100;
                }
                else
                {
                    request.MediaDownloader.ChunkSize = 1;
                }
                request.MediaDownloader.ProgressChanged += ((progress) =>
                {
                    progressBar1.Invoke(new MethodInvoker(() =>
                    {
                        if (progress.Status == DownloadStatus.Downloading)
                        {
                            if (item.FileSize > int.MaxValue)
                            {
                                progressBar1.Value = (int)(progress.BytesDownloaded * int.MaxValue / long.MaxValue);
                            }
                            else
                            {
                                progressBar1.Value = (int)progress.BytesDownloaded;
                            }
                            progressBar1.Refresh();
                        }
                    }));
                });
                IDownloadProgress downloadProgress = await request.DownloadAsync(stream);
                if (downloadProgress.Status == DownloadStatus.Completed && folderName != null)
                {
                    string fullPath = IO.Path.Combine(path, folderName);
                    if (!IO.Directory.Exists(fullPath))
                    {
                        IO.Directory.CreateDirectory(fullPath);
                    }
                    fullPath = IO.Path.Combine(fullPath, string.Concat(item.Title, GetExtension(item.MimeType, item.FullFileExtension)));
                    using (IO.FileStream fileStream = new IO.FileStream(fullPath, IO.FileMode.Create, IO.FileAccess.Write))
                    {
                        stream.WriteTo(fileStream);
                    }
                }
                progressBar1.Value = 0;
                progressBar2.PerformStep();
                progressBar2.Refresh();
                label2.Text = string.Format("Загружено файлов: {0}/{1}", progressBar2.Value, progressBar2.Maximum);
            }
            progressBar2.Value = 0;
            groupBox1.Visible = false;
        }

        private string GetExtension(string mimeType, string fullFileExtension)
        {
            string extension = string.Empty;
            if (fullFileExtension == null || fullFileExtension == string.Empty)
            {
                if (mimeType != null)
                {
                    switch (mimeType)
                    {
                        case "application/pdf":
                            extension = ".pdf";
                            break;
                        default:
                            break;
                    }
                }
            }
            return extension;
        }

        private async void Downloading(DriveService service, IList<File> fileList, string path, bool isFolder = false)
        {
            progressBar2.Maximum = fileList.Count;
            label2.Text = string.Format("Загружено файлов: {0}/{1}", progressBar2.Value, progressBar2.Maximum);
            groupBox1.Visible = true;
            foreach (File item in fileList)
            {
                label1.Text = string.Format("Загрузка файла: {0}", item.Title);                
                IO.MemoryStream stream = new IO.MemoryStream();
                FilesResource.GetRequest request = service.Files.Get(item.Id);
                if (item.FileSize > int.MaxValue)
                {
                    progressBar1.Maximum = (int)(item.FileSize * int.MaxValue / long.MaxValue);
                }
                else
                {
                    progressBar1.Maximum = (int)(item.FileSize);
                }
                if (progressBar1.Maximum >= 100)
                {
                    request.MediaDownloader.ChunkSize = progressBar1.Maximum/100;
                }
                else
                {
                    request.MediaDownloader.ChunkSize = 1;
                }
                request.MediaDownloader.ProgressChanged += ((progress) =>
                {
                    progressBar1.Invoke(new MethodInvoker(() =>
                    {
                        if (progress.Status == DownloadStatus.Downloading)
                        {
                            if (item.FileSize > int.MaxValue)
                            {
                                progressBar1.Value = (int)(progress.BytesDownloaded * int.MaxValue / long.MaxValue);
                            }
                            else
                            {
                                progressBar1.Value = (int)progress.BytesDownloaded;
                            }
                            progressBar1.Refresh();
                        }
                    }));
                });
                IDownloadProgress downloadProgress = await request.DownloadAsync(stream);
                if (downloadProgress.Status == DownloadStatus.Completed)
                {
                    string fullPath = null;
                    if (isFolder)
                    {
                        fullPath = IO.Path.Combine(path, string.Concat(item.Title, GetExtension(item.MimeType, item.FullFileExtension)));
                    }
                    else
                    {
                        fullPath = path;
                    }
                    using (IO.FileStream fileStream = new IO.FileStream(fullPath, IO.FileMode.Create, IO.FileAccess.Write))
                    {
                        stream.WriteTo(fileStream);
                    }
                }
                progressBar1.Value = 0;
                progressBar2.PerformStep();
                progressBar2.Refresh();
                label2.Text = string.Format("Загружено файлов: {0}/{1}", progressBar2.Value, progressBar2.Maximum);
            }
            progressBar2.Value = 0;
            groupBox1.Visible = false;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            toolStripMenuItem1.Visible = !groupBox1.Visible;
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripMenuItem3)
            {
                if (listView1.SelectedItems.Count > 1 && folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    List<File> fileList = new List<File>(listView1.SelectedItems.Count);
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        fileList.Add(item.Tag as File);
                    }
                    Downloading(service, fileList, folderBrowserDialog1.SelectedPath, true);
                }
                else
                {
                    File file = listView1.SelectedItems[0].Tag as File;
                    saveFileDialog1.Tag = file;
                    saveFileDialog1.FileName = file.Title;
                    saveFileDialog1.ShowDialog();
                }
            }
            else
            {
                DeleteItems();
            }
        }

        private async void DeleteItems()
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                string response = await Delete(service, (item.Tag as File).Id);
                if (response != null && response == string.Empty)
                {
                    listView1.Items.Remove(item);
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Downloading(service, new List<File>() { saveFileDialog1.Tag as File }, saveFileDialog1.FileName);
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            switch (listView1.SelectedItems.Count)
            {
                case 0:
                    e.Cancel = true;
                    break;
                case 1:
                    toolStripMenuItem3.Text = "Скачать файл";
                    toolStripMenuItem4.Text = "Удалить файл";
                    break;
                default:
                    toolStripMenuItem3.Text = "Скачать выделенные файлы";
                    toolStripMenuItem4.Text = "Удалить выделенные файлы";
                    break;
            }
        }

        private void contextMenuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            InitTree(service);
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItems();
            }
            else if (e.KeyCode == Keys.A && e.Control)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    item.Selected = true;
                }
            }
        }
    }
}
