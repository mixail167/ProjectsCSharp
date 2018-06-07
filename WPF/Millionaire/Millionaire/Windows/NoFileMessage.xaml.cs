using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Millionaire
{
    /// <summary>
    /// Логика взаимодействия для NoFileMessage.xaml
    /// </summary>
    public partial class NoFileMessage : Window
    {
        public bool next;
        public NoFileMessage()
        {
            InitializeComponent();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            DownloadFile downloadFile = new DownloadFile(false);
            Hide();
            downloadFile.ShowDialog();
            next = downloadFile.next;
            Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            next = false;
            Close();
        }
        
    }
}
