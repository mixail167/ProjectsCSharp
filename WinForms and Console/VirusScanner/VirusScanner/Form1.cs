using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using VirusScanner.Properties;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace VirusScanner
{
    public partial class Form1 : Form
    {
        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void SetLabelText(string text)
        {
            Invoke(new Action<string>((labelText) => { label1.Text = labelText; }), text);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void Scan(string fileName)
        {
            SetLabelText("Выполняется запрос на сканирование...");
            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            WebRequest request = WebRequest.Create(string.Format(Resources.Scan, Resources.APIKey));
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            string postHeader = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", boundary, Path.GetFileName(fileName), MimeMapping.GetMimeMapping(fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                request.ContentLength = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        requestStream.Write(buffer, 0, bytesRead);
                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                }
            }
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dynamic json = JsonConvert.DeserializeObject(reader.ReadToEnd());
                        if (json != null)
                        {
                            switch ((int)json.response_code)
                            {
                                case 1:
                                    SetLabelText("Запрос на сканирование успешно выполнен.");
                                    break;
                                default:
                                    SetLabelText("Сканирование не выполнено");
                                    break;
                            }
                        }
                        else if ((response as HttpWebResponse).StatusCode == HttpStatusCode.NoContent)
                        {
                            SetLabelText("Превышен лимит запросов. Повторите запрос позже");
                        }
                        else
                        {
                            SetLabelText("Сканирование не выполнено. Повторите запрос позже");
                        }
                    }
                }
            }
        }

        private void Report(string fileName)
        {
            SetLabelText("Выполняется запрос на получение отчета...");
            string md5 = GetMD5(fileName);
            WebRequest request = WebRequest.Create(string.Format(Resources.Report, Resources.APIKey, md5));
            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dynamic json = JsonConvert.DeserializeObject(reader.ReadToEnd());
                        if (json != null)
                        {
                            switch ((int)json.response_code)
                            {
                                case 0:
                                    SetLabelText("Отчета по данному файлу не обнаружено");
                                    break;
                                case 1:
                                    SetLabelText(string.Format("Вирус обнаружило {0} из {1} антивирусов", json.positives, json.total));
                                    break;
                                case -2:
                                    SetLabelText("Файл находится в очереди на анализ. Повторите запрос позже");
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if ((response as HttpWebResponse).StatusCode == HttpStatusCode.NoContent)
                        {
                            SetLabelText("Превышен лимит запросов. Повторите запрос позже");
                        }
                        else
                        {
                            SetLabelText("Отчет не получен. Повторите запрос позже");
                        }
                    }
                }
            }
        }

        private string GetMD5(string filename)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        stringBuilder.Append(hashBytes[i].ToString("X2"));
                    }
                    return stringBuilder.ToString().ToLower();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (thread != null && thread.ThreadState == ThreadState.Running)
            {
                thread.Abort();
            }
            thread = new Thread(new ParameterizedThreadStart((obj) =>
            {
                try
                {
                    string fileName = obj.ToString();
                    if (File.Exists(fileName))
                    {
                        FileInfo file = new FileInfo(textBox1.Text);
                        if (file.Length <= 33554432)
                        {
                            if (radioButton1.Checked)
                            {
                                Report(fileName);
                            }
                            else
                            {
                                Scan(fileName);
                            }
                        }
                        else
                        {
                            SetLabelText("Длина файла должна быть не более 32 МБ");
                        }
                    }
                    else
                    {
                        SetLabelText("Файл не существует");
                    }
                }
                catch (ThreadAbortException)
                {

                }
            })) { IsBackground = true };
            thread.Start(textBox1.Text);
        }
    }
}
