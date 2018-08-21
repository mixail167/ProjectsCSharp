using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BackpackTask
{
    public partial class Form1 : Form
    {
        private List<Item> items;

        public Form1()
        {
            InitializeComponent();

            AddItems();
            ShowItems(items);
        }

        private void AddItems()
        {
            items = new List<Item>();

            items.Add(new Item("Книга", 1, 600));
            items.Add(new Item("Бинокль", 2, 5000));
            items.Add(new Item("Аптечка", 4, 1500));
            items.Add(new Item("Ноутбук", 2, 40000));
            items.Add(new Item("Котелок", 1, 500));
        }

        private void ShowItems(List<Item> _items)
        {
            itemsListView.Items.Clear();

            foreach (Item i in _items)
            {
                itemsListView.Items.Add(new ListViewItem(new string[] { i.name, i.weigth.ToString(), 
                    i.price.ToString() }));
            }
        }

        //показать исходные данные
        private void ShowConditionsButton_Click(object sender, EventArgs e)
        {
            ShowItems(items);
        }

        //решить задачу
        private void solveButton_Click(object sender, EventArgs e)
        {
            Backpack bp = new Backpack(Convert.ToDouble(weightTextBox.Text));

            bp.MakeAllSets(items);

            List<Item> solve = bp.GetBestSet();

            if (solve == null)
            {
                MessageBox.Show("Нет решения!");
            }
            else
            {
                itemsListView.Items.Clear();

                ShowItems(solve);

                MessageBox.Show("Решение приведено в таблице");
            }
        }
    }
}