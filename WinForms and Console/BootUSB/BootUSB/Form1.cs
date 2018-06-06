using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BootUSB
{
    public partial class Form1 : Form
    {
        ProcessStartInfo psi;
        Process process;
        string text;
        int stage = 0;
        string letter;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            psi = new ProcessStartInfo();
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.StandardOutputEncoding = Encoding.GetEncoding(866);
            psi.UseShellExecute = false;
            text = string.Empty;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        /// <summary>
        /// Немедленная остановка процесса
        /// </summary>
        /// <returns>Значение, определяющее, следовало ли останавливать процесс</returns>
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

        /// <summary>
        /// Сброс параметров
        /// </summary>
        void Reset(bool flag)
        {
            if (tableLayoutPanel1.InvokeRequired)
            {
                tableLayoutPanel1.BeginInvoke(new Action<bool>(Reset), flag);
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

        /// <summary>
        /// Инитиализация и запуск процесса
        /// </summary>
        /// <param name="fileName">Имя процесса</param>
        /// <param name="arguments">Аргументы процесса</param>
        /// <returns>Значение, определяющее, был ли процесс успешно инициализирован</returns>
        bool InitProcess(string fileName, string arguments)
        {
            try
            {
                psi.FileName = fileName;
                psi.Arguments = arguments;
                process = new Process();
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += new DataReceivedEventHandler(process1_OutputDataReceived);
                process.Exited += new EventHandler(process1_Exited);
                process.StartInfo = psi;
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

        /// <summary>
        /// Обновление консоли ошибок
        /// </summary>
        /// <param name="data">Текст для вывода</param>
        void RefreshMainConsole(string data)
        {
            if (!String.IsNullOrEmpty(data) && !text.Equals(data))
            {
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.BeginInvoke(new Action<string>(RefreshMainConsole), new[] { data });
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
                    text = data;
                    richTextBox1.Text += text + "\n";
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.ScrollToCaret();
                }
            }
        }

        /// <summary>
        /// Проверка наличия диска по его букве, введенной пользователем
        /// </summary>
        /// <returns>Результат проверки</returns>
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
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            stage = 2;
                            RefreshMainConsole("Этап 2 из 3: Обновление загрузочного кода.");
                            InitProcess(openFileDialog1.FileName, string.Format("/nt60 {0}:", letter));
                        }
                        else
                        {
                            Reset(true);
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
                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (!folderBrowserDialog1.SelectedPath.EndsWith("\\"))
                            {
                                folderBrowserDialog1.SelectedPath += "\\";
                            }
                            stage = 3;
                            RefreshMainConsole("Этап 3 из 3: Копирование файлов.");
                            InitProcess("cmd.exe", string.Format("/c xcopy \"{0}*.*\" {1}:\\ /E /H", folderBrowserDialog1.SelectedPath, letter));
                        }
                        else
                        {
                            Reset(true);
                        }
                    }
                    break;
                case 3:
                case 0:
                    Reset(true);
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
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
        }
    }
}
