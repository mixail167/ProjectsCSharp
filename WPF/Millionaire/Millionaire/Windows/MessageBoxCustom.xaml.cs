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
    /// Логика взаимодействия для MessageBoxErrorCustom.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        public MessageBoxCustom(string title, string message)
        {
            InitializeComponent();
            Title = title;
            Message.Text = message;
        }
    }
}
