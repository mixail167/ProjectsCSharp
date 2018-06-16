using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;

namespace Algorithms
{
    public partial class Form1 : Form
    {
        #region Vars

        private int oldValue;
        private int oldValueCount;
        private Graph graph;
        private DataGridCell cell;
        private bool semaphore;
        private bool semaphore2;

        #endregion

        public Form1()
        {
            InitializeComponent();
            oldValueCount = 3;
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowCount = 2;
            dataGridView1.Rows[1].ReadOnly = true;
            dataGridView2.ColumnCount = oldValueCount + 1;
            dataGridView2.RowCount = oldValueCount + 1;
            dataGridView2.Rows[0].ReadOnly = true;
            dataGridView2.Columns[0].ReadOnly = true;
            graph = new Graph("graph");
            AddCellsAndNodes(0, dataGridView2.ColumnCount);
            cell = new DataGridCell(0, 0);
            semaphore = false;
            semaphore2 = false;
        }

        /// <summary>
        /// Алгоритм Бойера-Мура
        /// </summary>
        /// <param name="text"> Исходный текст</param>
        /// <param name="substring">Образ</param>
        /// <param name="index">Индекс первого вхождения образа в исходном тексте</param>
        /// <returns>Значение, показывающее, содержится ли образ в тексте</returns>
        private bool AlgBM(string text, string substring, out int index)
        {
            int n = text.Length;
            int m = substring.Length;
            int ind;
            int[] table = new int[9999];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = m;
            }
            for (int i = m - 1; i >= 0; i--)
            {
                if (table[(short)(substring[i])] == m)
                {
                    table[(short)(substring[i])] = m - i - 1;
                }
            }
            ind = m - 1;
            while (ind < n)
            {
                if (substring[m - 1] != text[ind])
                {
                    ind += table[(short)(text[ind])];
                }
                else
                {
                    for (int i = m - 2; i >= 0; i--)
                    {
                        if (substring[i] != text[ind - m + i + 1])
                        {
                            ind += table[(short)(text[ind - m + i + 1])] - 1;
                            break;
                        }
                        else if (i == 0)
                        {
                            index = ind - m + 1;
                            return true;
                        }
                    }
                }
            }
            index = 0;
            return false;
        }

        /// <summary>
        /// Алгоритм Кнута-Морриса-Пратта
        /// </summary>
        /// <param name="text"> Исходный текст</param>
        /// <param name="substring">Образ</param>
        /// <param name="index">Индекс первого вхождения образа в исходном тексте</param>
        /// <returns>Значение, показывающее, содержится ли образ в тексте</returns>
        private bool AlgKMP(string text, string substring, out int index)
        {
            int[] pi = new int[substring.Length];
            int j = 0, i = 1;
            pi[0] = 0;
            while (i != substring.Length)
            {
                if (!substring[i].Equals(substring[j]))
                {
                    if (j == 0)
                    {
                        pi[i] = 0;
                        i++;
                    }
                    else
                    {
                        j = pi[j - 1];
                    }
                }
                else
                {
                    pi[i] = j + 1;
                    i++;
                    j++;
                }
            }
            i = 0;
            j = 0;
            while (i != text.Length)
            {
                if (text[i].Equals(substring[j]))
                {
                    i++;
                    j++;
                    if (j == substring.Length)
                    {
                        index = i - substring.Length;
                        return true;
                    }
                }
                else if (j != 0)
                {
                    j = pi[j - 1];
                }
                else
                {
                    i++;
                    if (i == text.Length)
                    {
                        index = 0;
                        return false;
                    }
                }
            }
            index = 0;
            return false;
        }

        /// <summary>
        /// Сортировка вставками
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private int[] InsertionSort(int[] massiv, bool revers)
        {
            for (int i = 1; i < massiv.Length; i++)
            {
                int x = massiv[i];
                int j = i;
                while (j > 0 && ((revers && massiv[j - 1] < x) || (!revers && massiv[j - 1] > x)))
                {
                    massiv[j] = massiv[j - 1];
                    j--;
                }
                massiv[j] = x;
            }
            return massiv;
        }

        /// <summary>
        /// Сортировка пузырьком
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private int[] BubbleSort(int[] massiv, bool revers)
        {
            for (int i = 0; i < massiv.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < massiv.Length - i - 1; j++)
                {
                    if ((revers && massiv[j] < massiv[j + 1]) ||
                        (!revers && massiv[j] > massiv[j + 1]))
                    {
                        massiv = Swap(massiv, j, j + 1);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    break;
                }
            }
            return massiv;
        }

        /// <summary>
        /// Сортировка выбором
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private int[] SelectionSort(int[] massiv, bool revers)
        {
            for (int i = 0; i < massiv.Length - 1; i++)
            {
                int index = i;
                for (int j = i + 1; j < massiv.Length; j++)
                {
                    if ((revers && massiv[j] > massiv[index]) ||
                        (!revers && massiv[j] < massiv[index]))
                    {
                        index = j;
                    }
                }
                if (index != i)
                {
                    massiv = Swap(massiv, i, index);
                }
            }
            return massiv;
        }

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        /// <param name="massiv">Исходный массив элементов</param>
        /// <param name="revers">Значение true, если требуется отсортировать массив по убыванию, иначе - false</param>
        /// <returns>Отсортированный массив элементов</returns>
        private int[] MergeSort(int[] massiv, bool revers)
        {
            if (massiv.Length == 1)
                return massiv;
            return Merge(MergeSort(massiv.Take(massiv.Length / 2).ToArray(), revers), MergeSort(massiv.Skip(massiv.Length / 2).ToArray(), revers), revers);
        }

        private int[] Merge(int[] massiv1, int[] massiv2, bool revers)
        {
            int a = 0, b = 0;
            int[] massiv = new int[massiv1.Length + massiv2.Length];
            for (int i = 0; i < massiv1.Length + massiv2.Length; i++)
            {
                if (b < massiv2.Length && a < massiv1.Length)
                {
                    if ((!revers && massiv1[a] > massiv2[b]) ||
                        (revers && massiv1[a] < massiv2[b]))
                    {
                        massiv[i] = massiv2[b++];
                    }
                    else massiv[i] = massiv1[a++];
                }
                else
                {
                    if (b < massiv2.Length)
                    {
                        massiv[i] = massiv2[b++];
                    }
                    else
                    {
                        massiv[i] = massiv1[a++];
                    }
                }
            }
            return massiv;
        }

        private int[] Swap(int[] massiv, int i, int j)
        {
            int temp = massiv[i];
            massiv[i] = massiv[j];
            massiv[j] = temp;
            return massiv;
        }

        private void AddCellsAndNodes(int left, int right)
        {
            for (int i = left + 1; i < right; i++)
            {
                DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();
                buttonCell.Value = i;
                dataGridView2[0, i] = buttonCell;
                buttonCell = new DataGridViewButtonCell();
                buttonCell.Value = i;
                dataGridView2[i, 0] = buttonCell;
                dataGridView2[i, i].Value = 0;
                dataGridView2[i, i].Style.BackColor = System.Drawing.Color.Yellow;
                dataGridView2[i, i].ReadOnly = true;
                graph.AddNode(i.ToString());
            }
        }

        private void RemoveNodes(int left, int right)
        {
            for (int i = left; i < right + 1; i++)
            {
                graph.RemoveNode(graph.FindNode(i.ToString()));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = -1;
            if (richTextBox1.Text == string.Empty)
            {
                label3.Text = "Ошибка: Исходный текст не задан.";
            }
            else if (textBox2.Text == string.Empty)
            {
                label3.Text = "Ошибка: Образ не задан.";
            }
            else if (richTextBox1.Text.Length < textBox2.Text.Length)
            {
                label3.Text = "Ошибка: Длина образа больше длины исходного текста.";
            }
            else if ((radioButton1.Checked && AlgKMP(richTextBox1.Text, textBox2.Text, out k)) ||
                     (radioButton2.Checked && AlgBM(richTextBox1.Text, textBox2.Text, out k)))
            {
                label3.Text = "Вывод: Образ найден. Индекс первого вхождения: " + k.ToString() + ".";
            }
            else
            {
                label3.Text = "Вывод: Образ не найден.";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 50 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView dataGridView = sender as DataGridView;
                oldValue = Convert.ToInt32(dataGridView[e.ColumnIndex, e.RowIndex].Value);
            }
            catch
            {
                oldValue = 0;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int temp = Convert.ToInt32(dataGridView1[e.ColumnIndex, e.RowIndex].Value);
            }
            catch
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = oldValue;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] massiv = new int[dataGridView1.ColumnCount];
            for (int i = 0; i < massiv.Length; i++)
            {
                massiv[i] = Convert.ToInt32(dataGridView1[i, 0].Value);
            }
            if (radioButton3.Checked)
            {
                massiv = SelectionSort(massiv, checkBox1.Checked);
            }
            else if (radioButton4.Checked)
            {
                massiv = InsertionSort(massiv, checkBox1.Checked);
            }
            else if (radioButton5.Checked)
            {
                massiv = BubbleSort(massiv, checkBox1.Checked);
            }
            else
            {
                massiv = MergeSort(massiv, checkBox1.Checked);
            }
            for (int i = 0; i < massiv.Length; i++)
            {
                dataGridView1[i, 1].Value = massiv[i];
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            dataGridView2.ColumnCount = Convert.ToInt32(numericUpDown2.Value) + 1;
            dataGridView2.RowCount = dataGridView2.ColumnCount;
            if (dataGridView2.ColumnCount - 1 > oldValueCount)
            {
                AddCellsAndNodes(oldValueCount, dataGridView2.ColumnCount);
            }
            else if (dataGridView2.ColumnCount - 1 < oldValueCount)
            {
                RemoveNodes(dataGridView2.ColumnCount, oldValueCount);
            }
            oldValueCount = dataGridView2.ColumnCount - 1;
        }

        private void numericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 51 && e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (semaphore2)
            {
                semaphore2 = !semaphore2;
                return;
            }
            try
            {
                if (e.RowIndex != e.ColumnIndex && !semaphore2)
                {
                    int temp = Convert.ToInt32(dataGridView2[e.ColumnIndex, e.RowIndex].Value);
                    semaphore2 = !semaphore2;
                    bool isFound = false;
                    dataGridView2[e.RowIndex, e.ColumnIndex].Value = temp;
                    if (temp != 0)
                    {
                        foreach (Edge item in graph.Edges)
                        {
                            if ((item.Source.Equals(e.ColumnIndex.ToString()) && item.Target.Equals(e.RowIndex.ToString()))
                                || (item.Source.Equals(e.RowIndex.ToString()) && item.Target.Equals(e.ColumnIndex.ToString())))
                            {
                                item.LabelText = temp.ToString();
                                isFound = true;
                                break;
                            }
                        }
                    }
                    if (!isFound)
                    {
                        if (temp != 0)
                        {
                            graph.AddEdge(e.RowIndex.ToString(), temp.ToString(), e.ColumnIndex.ToString()).Attr.ArrowheadAtTarget = ArrowStyle.None;

                        }
                        else
                        {
                            try
                            {
                                graph.RemoveEdge(graph.Edges.First(item => (item.Source.Equals(e.ColumnIndex.ToString()) && item.Target.Equals(e.RowIndex.ToString()))
                                                         || (item.Source.Equals(e.RowIndex.ToString()) && item.Target.Equals(e.ColumnIndex.ToString()))));
                            }
                            catch
                            {

                            }
                        }
                    }
                }

            }
            catch
            {
                dataGridView2[e.ColumnIndex, e.RowIndex].Value = oldValue;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!semaphore && cell.ColumnNumber != 0 && cell.RowNumber != 0)
            {
                dataGridView2[cell.ColumnNumber, 0].Style.BackColor = System.Drawing.Color.White;
                dataGridView2[0, cell.RowNumber].Style.BackColor = System.Drawing.Color.White;
                if (cell.ColumnNumber == cell.RowNumber)
                {
                    dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.White;
                }
                cell.ColumnNumber = 0;
                cell.RowNumber = 0;
            }
            if (e.RowIndex != e.ColumnIndex)
            {
                if (e.RowIndex == 0)
                {
                    if (semaphore)
                    {
                        if (cell.ColumnNumber != 0)
                        {
                            dataGridView2[cell.ColumnNumber, 0].Style.BackColor = System.Drawing.Color.White;
                            dataGridView2[e.ColumnIndex, 0].Style.BackColor = System.Drawing.Color.Red;
                            cell.ColumnNumber = e.ColumnIndex;
                        }
                        else
                        {
                            cell.ColumnNumber = e.ColumnIndex;
                            dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.Red;
                            semaphore = !semaphore;
                        }
                    }
                    else
                    {
                        dataGridView2[e.ColumnIndex, 0].Style.BackColor = System.Drawing.Color.Red;
                        cell.ColumnNumber = e.ColumnIndex;
                        semaphore = !semaphore;
                    }
                }
                else if (e.ColumnIndex == 0)
                {
                    if (semaphore)
                    {
                        if (cell.RowNumber != 0)
                        {
                            dataGridView2[0, cell.RowNumber].Style.BackColor = System.Drawing.Color.White;
                            dataGridView2[0, e.RowIndex].Style.BackColor = System.Drawing.Color.Red;
                            cell.RowNumber = e.RowIndex;
                        }
                        else
                        {
                            cell.RowNumber = e.RowIndex;
                            dataGridView2[cell.ColumnNumber, cell.RowNumber].Style.BackColor = System.Drawing.Color.Red;
                            semaphore = !semaphore;
                        }
                    }
                    else
                    {
                        dataGridView2[0, e.RowIndex].Style.BackColor = System.Drawing.Color.Red;
                        cell.RowNumber = e.RowIndex;
                        semaphore = !semaphore;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GraphForm graphForm = new GraphForm(graph);
            graphForm.ShowDialog();
        }
    }
}
