using Crypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Millionaire
{
    /// <summary>
    /// Логика взаимодействия для EndOfGameWindow.xaml
    /// </summary>
    public partial class EndOfGameWindow : Window
    {
        public List<Record> records;
        Record player;

        public EndOfGameWindow(List<Record> records, Record player)
        {
            InitializeComponent();
            this.player = player;
            this.records = records;
            Label.Content = string.Format("Ваш счет: {0} р.", player.Score);
        }

        private string ReadData(string fileName)
        {
            try
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    if (fileStream.Read(buffer, 0, buffer.Length) > 0)
                    {
                        return AesCrypt.DecryptStringFromBytes(buffer, Encoding.ASCII.GetBytes("zxcvqwerasdfqazx"), Encoding.ASCII.GetBytes("qazxcvbnmlpoiuyt"));
                    }
                    else throw new Exception();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void ReadDataRecords()
        {
            string temp = ReadData("records.bin");
            if (temp != string.Empty)
            {
                try
                {
                    string[] parts = temp.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        foreach (string item in parts)
                        {
                            Record record = Record.Parse(item.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));
                            if (record != null)
                            {
                                records.Add(record);
                            }
                        }
                        records = records.OrderByDescending(item => item.Score).ToList();
                        if (records.Count > 10)
                        {
                            records.RemoveRange(10, records.Count - 10);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void EndOfGameWindow1_ContentRendered(object sender, EventArgs e)
        {
            if (records.Count == 0 && File.Exists("records.bin"))
            {
                ReadDataRecords();
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (!TextBox.Text.Equals("Введите имя...") && !TextBox.Text.Equals(string.Empty))
            {
                player.Name = TextBox.Text;
                bool addRecords = true;
                bool rewrite = false;
                foreach (Record item in records)
                {
                    if (string.Compare(item.Name, player.Name, true) == 0)
                    {
                        if (item.Score < player.Score)
                        {
                            item.Score = player.Score;
                            rewrite = true;
                        }
                        addRecords = false;
                        break;
                    }
                }
                if (addRecords)
                {
                    records.Add(player);
                    rewrite = true;
                }
                if (rewrite)
                {
                    try
                    {
                        if (File.Exists("records.bin"))
                        {
                            FileAttributes fileAttributes = File.GetAttributes("records.bin");
                            if (fileAttributes.HasFlag(FileAttributes.ReadOnly))
                                File.SetAttributes("records.bin", fileAttributes & ~FileAttributes.ReadOnly);
                        }
                        byte[] bytes = AesCrypt.EncryptStringToBytes(string.Join<Record>("%", records.ToArray()), Encoding.ASCII.GetBytes("zxcvqwerasdfqazx"), Encoding.ASCII.GetBytes("qazxcvbnmlpoiuyt"));
                        using (FileStream fileStream = File.Create("records.bin"))
                        {
                            fileStream.Write(bytes, 0, bytes.Length);
                        }
                        File.SetAttributes("records.bin", File.GetAttributes("records.bin") | FileAttributes.ReadOnly);
                    }
                    catch (Exception exception)
                    {
                        Hide();
                        MainWindow.ShowError(exception.Message);
                    }
                }
                Close();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Equals("Введите имя..."))
            {
                TextBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Equals(string.Empty))
            {
                TextBox.Text = "Введите имя...";
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (TextBox.Text.Length >= 20)
            {
                e.Handled = true;
            }
        }
    }
}
