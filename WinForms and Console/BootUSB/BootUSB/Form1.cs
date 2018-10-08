using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading;

namespace BootUSB
{
    public partial class Form1 : Form
    {
        ProcessStartInfo processStartInfo;
        Process process;
        string text;
        int stage = 0;
        string letter;
        Thread thread;

        public Form1()
        {
            InitializeComponent();
            processStartInfo = new ProcessStartInfo();
            processStartInfo.Verb = "runas";
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            processStartInfo.UseShellExecute = false;
            text = string.Empty;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            if (Environment.Is64BitOperatingSystem)
            {
                SevenZipExtractor.SetLibraryPath(Path.Combine(Application.StartupPath, "7z_x64.dll"));
            }
            else
            {
                SevenZipExtractor.SetLibraryPath(Path.Combine(Application.StartupPath, "7z_x32.dll"));
            }
        }

        bool KillProcess()
        {
            try
            {
                if (!process.HasExited)
                {
                    process.Kill();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                Reset(true);
            }
        }

        void Reset(bool flag)
        {
            if (tableLayoutPanel1.InvokeRequired)
            {
                tableLayoutPanel1.Invoke(new Action<bool>(Reset), flag);
            }
            else
            {
                if (flag)
                {
                    stage = 0;
                }
                tableLayoutPanel1.Enabled = flag;
                button2.Enabled = flag;
                button3.Enabled = !flag;
            }
        }

        bool InitProcess(string fileName, string arguments)
        {
            try
            {
                processStartInfo.FileName = fileName;
                processStartInfo.Arguments = arguments;
                process = new Process();
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += new DataReceivedEventHandler(process1_OutputDataReceived);
                process.Exited += new EventHandler(process1_Exited);
                process.StartInfo = processStartInfo;
                process.Start();
                process.BeginOutputReadLine();
                Reset(false);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Reset(true);
                return false;
            }
        }

        void RefreshMainConsole(string data)
        {
            if (!String.IsNullOrEmpty(data) && !text.Equals(data))
            {
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new Action<string>(RefreshMainConsole), new[] { data });
                }
                else
                {
                    if (data.Trim().StartsWith("Завершено") && text.Trim().StartsWith("Завершено"))
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 2), richTextBox1.Lines[richTextBox1.Lines.Length - 2].Length + 1);
                        }
                        catch
                        {

                        }
                    }
                    else if (data.Trim().StartsWith("Извлечение файлов") && text.Trim().StartsWith("Извлечение файлов"))
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.GetFirstCharIndexFromLine(richTextBox1.Lines.Length - 2), richTextBox1.Lines[richTextBox1.Lines.Length - 2].Length + 1);
                        }
                        catch
                        {

                        }
                    }
                    if (!data.Trim().StartsWith("Поток") && !data.Trim().StartsWith("DISKPART"))
                    {
                        text = data;
                        richTextBox1.Text += text + "\n";
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.ScrollToCaret();
                    }
                }
            }
        }

        bool CheckDrive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo item in drives)
            {
                if (item.Name[0] == letter[0])
                {
                    return true;
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InitProcess("diskpart.exe", ""))
            {
                try
                {
                    process.StandardInput.WriteLine("list disk");
                    process.StandardInput.WriteLine("exit");
                }
                catch (Exception exception)
                {
                    KillProcess();
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void process1_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            RefreshMainConsole(e.Data);
        }

        private void process1_Exited(object sender, EventArgs e)
        {
            switch (stage)
            {
                case 1:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new EventHandler(process1_Exited), new[] { sender, e });
                    }
                    else
                    {
                        RefreshMainConsole("Этап 2 из 3: Обновление загрузочного кода.");
                        if (!checkBox1.Checked)
                        {
                            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                stage = 2;
                                InitProcess(openFileDialog1.FileName, string.Format("/nt60 {0}:", letter));
                            }
                            else
                            {
                                RefreshMainConsole("Действия отменены пользователем.");
                            }
                        }
                        else
                        {
                            openFileDialog2.FileName = string.Empty;
                            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                            {
                                thread = new Thread(() =>
                                    {
                                        using (SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(openFileDialog2.FileName))
                                        {
                                            RefreshMainConsole("Проверка ISO-образа");
                                            if (sevenZipExtractor.Check())
                                            {
                                                FileStream fileStream = null;
                                                try
                                                {
                                                    fileStream = new FileStream(Path.Combine(Application.StartupPath, "bootsect.exe"), FileMode.Create);
                                                    sevenZipExtractor.ExtractFile(sevenZipExtractor.ArchiveFileNames.FirstOrDefault(x => x.EndsWith("bootsect.exe")), fileStream);
                                                    fileStream.Close();
                                                    fileStream = null;
                                                    stage = 2;
                                                    InitProcess(Path.Combine(Application.StartupPath, "bootsect.exe"), string.Format("/nt60 {0}:", letter));
                                                }
                                                catch (ArgumentOutOfRangeException)
                                                {
                                                    RefreshMainConsole("Файл 'bootsect.exe' не найден");
                                                    Reset(true);
                                                }
                                                catch (Exception exception)
                                                {
                                                    RefreshMainConsole(exception.Message);
                                                    Reset(true);
                                                }
                                                finally
                                                {
                                                    if (fileStream != null)
                                                    {
                                                        fileStream.Close();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                RefreshMainConsole("ISO-образ поврежден");
                                                Reset(true);
                                            }
                                        }
                                    })
                                    {
                                        IsBackground = true
                                    };
                                thread.Start();
                            }
                            else
                            {
                                RefreshMainConsole("Действия отменены пользователем.");
                                Reset(true);
                            }
                        }
                    }
                    break;
                case 2:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new EventHandler(process1_Exited), new[] { sender, e });
                    }
                    else
                    {
                        RefreshMainConsole("Этап 3 из 3: Копирование файлов.");
                        if (!checkBox1.Checked)
                        {
                            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                            {
                                if (!folderBrowserDialog1.SelectedPath.EndsWith("\\"))
                                {
                                    folderBrowserDialog1.SelectedPath += "\\";
                                }
                                stage = 3;
                                InitProcess("cmd.exe", string.Format("/c xcopy \"{0}*.*\" {1}:\\ /E /H", folderBrowserDialog1.SelectedPath, letter));
                            }
                            else
                            {
                                RefreshMainConsole("Действия отменены пользователем.");
                            }
                        }
                        else
                        {
                            File.Delete(Path.Combine(Application.StartupPath, "bootsect.exe"));
                            thread = new Thread(() =>
                                {
                                    using (SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(openFileDialog2.FileName))
                                    {
                                        sevenZipExtractor.ExtractionFinished += sevenZipExtractor_ExtractionFinished;
                                        sevenZipExtractor.Extracting += sevenZipExtractor_Extracting;
                                        try
                                        {
                                            sevenZipExtractor.ExtractArchive(string.Format("{0}:\\", letter));
                                        }
                                        catch (Exception exception)
                                        {
                                            RefreshMainConsole(exception.Message);
                                            Reset(true);
                                        }
                                    }
                                })
                                {
                                    IsBackground = true
                                };
                            thread.Start();
                        }
                    }
                    break;
                case 3:
                    RefreshMainConsole("Готово.");
                    Reset(true);
                    break;
                default:
                    Reset(true);
                    break;
            }
        }

        void sevenZipExtractor_Extracting(object sender, ProgressEventArgs e)
        {
            RefreshMainConsole(string.Format("Извлечение файлов: {0}%", e.PercentDone));
        }

        private void sevenZipExtractor_ExtractionFinished(object sender, EventArgs e)
        {
            stage = 3;
            process1_Exited(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                if (thread.ThreadState != System.Threading.ThreadState.Aborted || thread.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    thread.Abort();
                    RefreshMainConsole("Действия отменены пользователем.");
                }
                thread = null;
            }
            if (KillProcess())
            {
                RefreshMainConsole("Действия отменены пользователем.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            letter = textBox1.Text.ToUpper();
            if (letter == string.Empty)
            {
                MessageBox.Show("Введите букву диска!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CheckDrive() && MessageBox.Show("Диск с такой буквой уже существует. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }
            if (InitProcess("diskpart.exe", ""))
            {
                try
                {
                    stage = 1;
                    RefreshMainConsole("Этап 1 из 3: Форматирование диска.");
                    process.StandardInput.WriteLine("select disk {0}", numericUpDown1.Value);
                    process.StandardInput.WriteLine("clean");
                    process.StandardInput.WriteLine("create partition primary");
                    process.StandardInput.WriteLine("active");
                    if (comboBox1.SelectedIndex == 0)
                        process.StandardInput.WriteLine("format fs={0}", comboBox2.Items[comboBox2.SelectedIndex].ToString());
                    else process.StandardInput.WriteLine("format fs={0} quick", comboBox2.Items[comboBox2.SelectedIndex].ToString());
                    process.StandardInput.WriteLine("assign letter={0}", letter);
                    process.StandardInput.WriteLine("exit");
                }
                catch (Exception exception)
                {
                    KillProcess();
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (process != null)
            {
                try
                {
                    process.Kill();
                }
                catch
                {

                }
                finally
                {
                    process.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && (textBox1.Text.ToUpper()[0] < 65 || textBox1.Text.ToUpper()[0] > 90))
            {
                textBox1.Text = string.Empty;
            }
            else textBox1.Text = textBox1.Text.ToUpper();
        }
    }
}
