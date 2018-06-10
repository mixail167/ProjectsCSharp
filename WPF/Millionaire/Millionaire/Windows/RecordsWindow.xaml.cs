using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Millionaire
{
    /// <summary>
    /// Логика взаимодействия для Records.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        List<Record> records;

        public RecordsWindow(List<Record> records)
        {
            InitializeComponent();
            this.records = records.OrderByDescending(item => item.Score).ToList();
        }

        private void RecordsWindow1_ContentRendered(object sender, EventArgs e)
        {
            for (int i = 0; i < records.Count; i++)
            {
                records[i].Index = i+1;
                ListView.Items.Add(records[i]);
            }
        }
    }
}
