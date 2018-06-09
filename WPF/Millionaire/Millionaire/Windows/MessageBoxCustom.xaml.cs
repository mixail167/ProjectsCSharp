using System.Windows;

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
