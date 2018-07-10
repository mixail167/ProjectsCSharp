using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CalendarGenerator
{
    public partial class Form1 : Form
    {
        private DateTime dateTime;
        private string[] daysOfWeek = new string[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
        private string[] months = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        private delegate bool WriteToFile(string text, string path);
        private WriteToFile writeInFile;

        private DataGridView oldDataGridView;
        private DataGridViewCellEventArgs dataGridViewCellEventArgs;

        private DataGridViewCellStyle firstRowStyle = new DataGridViewCellStyle
        {
            BackColor = System.Drawing.Color.Gold,
            Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
            SelectionBackColor = System.Drawing.Color.Gold,
            SelectionForeColor = System.Drawing.Color.Black,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };
        private DataGridViewCellStyle cellDefaultStyle = new DataGridViewCellStyle
        {
            BackColor = System.Drawing.Color.Silver,
            Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
            SelectionBackColor = System.Drawing.Color.Silver,
            SelectionForeColor = System.Drawing.Color.Black,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };

        private DataGridViewCellStyle cellStyle = new DataGridViewCellStyle
        {
            BackColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
            SelectionBackColor = System.Drawing.Color.OrangeRed,
            SelectionForeColor = System.Drawing.Color.Black,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };

        public Form1(string year)
        {
            InitializeComponent();
            InitDataGridView();
            try
            {
                numericUpDown1.Value = Convert.ToDecimal(year);
            }
            catch (Exception)
            {
                numericUpDown1.Value = Convert.ToDecimal(DateTime.Now.Year);
            }
            finally
            {
                this.dateTime = new DateTime(Convert.ToInt32(numericUpDown1.Value), 1, 1);
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitDataGridView();
            numericUpDown1.Value = Convert.ToDecimal(DateTime.Now.Year);
            this.dateTime = new DateTime(Convert.ToInt32(numericUpDown1.Value), 1, 1);
        }

        private void GenerateCalendar(DataGridView dataGridView, int monthNumber)
        {
            DateTime dateTime = new DateTime(this.dateTime.Year, monthNumber, 1);
            int indexRow = GetFirstDayOfMonth(dateTime);
            int indexColumn = 1;
            int daysInMonth = DateTime.DaysInMonth(this.dateTime.Year, monthNumber);
            dataGridView[indexColumn, 0].Value = new GregorianCalendar().GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            for (int i = 1; i <= daysInMonth; i++)
            {
                dataGridView[indexColumn, indexRow].Value = i.ToString();
                dataGridView[indexColumn, indexRow].Style = cellStyle;
                if (indexRow == dataGridView1.RowCount - 1)
                {
                    indexColumn++;
                    indexRow = 1;
                    try
                    {
                        dataGridView[indexColumn, 0].Value = new GregorianCalendar().GetWeekOfYear(new DateTime(this.dateTime.Year, monthNumber, i + 1), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    }
                    catch
                    {

                    }
                }
                else indexRow++;
            }
        }

        private void InitDataGridView()
        {
            foreach (DataGridView dataGridView in groupBox1.Controls.OfType<DataGridView>())
            {
                dataGridView.Rows.Clear();
                dataGridView.RowCount = 8;
                dataGridView.Rows[0].DefaultCellStyle = firstRowStyle;
                dataGridView[0, 0].Value = "Д/Н";
                for (int i = 1; i < dataGridView.RowCount; i++)
                {
                    dataGridView[0, i].Value = daysOfWeek[i - 1];
                    for (int j = 1; j < dataGridView.ColumnCount; j++)
                    {
                        dataGridView[j, i].Style = cellDefaultStyle;
                    }
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.dateTime = new DateTime(Convert.ToInt32(numericUpDown1.Value), 1, 1);
                if (this.dateTime.Year == DateTime.Now.Year)
                {
                    button1.Visible = false;
                }
                else
                {
                    button1.Visible = true;
                }
                InitDataGridView();
                for (int i = 1; i <= 12; i++)
                {
                    DataGridView dataGridView;
                    switch (i)
                    {
                        case 1:
                            dataGridView = dataGridView1;
                            break;
                        case 2:
                            dataGridView = dataGridView2;
                            break;
                        case 3:
                            dataGridView = dataGridView3;
                            break;
                        case 4:
                            dataGridView = dataGridView4;
                            break;
                        case 5:
                            dataGridView = dataGridView5;
                            break;
                        case 6:
                            dataGridView = dataGridView6;
                            break;
                        case 7:
                            dataGridView = dataGridView7;
                            break;
                        case 8:
                            dataGridView = dataGridView8;
                            break;
                        case 9:
                            dataGridView = dataGridView9;
                            break;
                        case 10:
                            dataGridView = dataGridView10;
                            break;
                        case 11:
                            dataGridView = dataGridView11;
                            break;
                        default:
                            dataGridView = dataGridView12;
                            break;

                    }
                    GenerateCalendar(dataGridView, i);
                }
            }
            catch (Exception)
            {
                numericUpDown1.Value = this.dateTime.Year;
            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToDecimal(DateTime.Now.Year);
        }

        private void numericUpDown1_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
            handledArgs.Handled = true;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex != 0 && e.ColumnIndex != 0)
            {
                if (this.oldDataGridView != null && dataGridView != this.oldDataGridView)
                {
                    oldDataGridView[dataGridViewCellEventArgs.ColumnIndex, dataGridViewCellEventArgs.RowIndex].Selected = false;
                }
                this.oldDataGridView = dataGridView;
                this.dataGridViewCellEventArgs = e;
            }
            else if (dataGridViewCellEventArgs != null)
            {
                oldDataGridView[dataGridViewCellEventArgs.ColumnIndex, dataGridViewCellEventArgs.RowIndex].Selected = true;
            }
        }

        private bool WriteToTextFile(string text, string path)
        {
            try
            {
                using (StreamWriter streamWriter = File.CreateText(path))
                {
                    streamWriter.WriteLine("{0,24}Календарь на {1} г.", " ", this.dateTime.Year);
                    streamWriter.WriteLine();
                    string[] lines = text.Split(new char[] { '\n' }, StringSplitOptions.None);
                    foreach (string item in lines)
                    {
                        streamWriter.WriteLine(item);
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool WriteToWordDocument(string text, string path)
        {
            try
            {
                Document doc = new Document();
                Section section = doc.AddSection();
                Paragraph paragraph = section.AddParagraph();
                paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                string[] lines = text.Split(new char[] { '\n' }, StringSplitOptions.None);
                foreach (string item in lines)
                {
                    TextRange textRange = paragraph.AppendText(item + "\n");
                    textRange.CharacterFormat.Font = new Font("Courier New", 12.0f, FontStyle.Regular);
                    if (item.Trim().StartsWith(@"Дн\Нед"))
                        textRange.CharacterFormat.TextBackgroundColor = Color.Yellow;
                    else if (item.Trim().StartsWith("СБ") || item.Trim().StartsWith("ВС"))
                        textRange.CharacterFormat.TextColor = Color.Red;
                }
                foreach (string item in daysOfWeek)
                {
                    TextSelection[] textSelections = doc.FindAllString(string.Format("{0}:", item), false, false);
                    foreach (TextSelection textSelection in textSelections)
                    {
                        textSelection.GetAsOneRange().CharacterFormat.HighlightColor = Color.FromArgb(0, 255, 255);
                    }
                }
                doc.SaveToFile(path, FileFormat.Docx);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            string path = GetFileName(this.dateTime.Year.ToString());
            if (path != null)
            {
                string text = GetCalendar();
                if (radioButton1.Checked)
                    writeInFile = WriteToWordDocument;
                else if (radioButton2.Checked)
                    writeInFile = WriteToWordDocument;
                else writeInFile = WriteToTextFile;
                if (writeInFile.Invoke(text, path))
                {
                    Process.Start(path);
                }
            }
        }

        private string GetFileName(string FileName)
        {
            saveFileDialog1.FileName = FileName;
            if (radioButton1.Checked)
                saveFileDialog1.Filter = "Portable Document Format(.pdf)|*.pdf";
            else if (radioButton2.Checked)
                saveFileDialog1.Filter = "Документ Word(.docx)|*.docx";
            else saveFileDialog1.Filter = "Текстовый документ(.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }
            return null;
        }

        private string GetCalendar()
        {
            string s = null;
            int firstDayOfYear = GetFirstDayOfMonth(dateTime);
            int[, ,] monthsArray = new int[12, 6, 7];
            for (int i = 0; i < 12; i++)
            {
                int daysInMonth = DateTime.DaysInMonth(dateTime.Year, i + 1);
                int st = 0;
                for (int j = 0; j < daysInMonth; j++)
                {
                    monthsArray[i, st, firstDayOfYear - 1] = j + 1;
                    if (firstDayOfYear == 7)
                    {
                        firstDayOfYear = 1;
                        st++;
                    }
                    else firstDayOfYear++;
                }
            }
            int start = 0;
            int stop;
            do
            {
                stop = start + 3;
                for (int i = start; i < stop; i++)
                {
                    s += string.Format("{0,20}", months[i]);
                }
                s += string.Format("\n\n{0, 7}", @"Дн\Нед");
                for (int i = start; i < stop; i++)
                {
                    s += string.Format("{0, 2}", " ");
                    for (int k = 0; k < 6; k++)
                    {
                        int day = 0;
                        for (int j = 0; j < 7 && day == 0; j++)
                        {
                            day = monthsArray[i, k, j];
                            if (day != 0)
                            {
                                s += string.Format("{0, 3}", new GregorianCalendar().GetWeekOfYear(new DateTime(this.dateTime.Year, i + 1, day), CalendarWeekRule.FirstDay, DayOfWeek.Monday));
                            }
                            else if (j == 6)
                            {
                                s += string.Format("{0, 3}", " ");
                            }
                        }
                    }
                }
                s += "\n\n";
                for (int j = 0; j < 7; j++)
                {
                    s += string.Format("{0, 6}:", daysOfWeek[j]);
                    for (int i = start; i < stop; i++)
                    {
                        s += string.Format("{0, 2}", " ");
                        for (int k = 0; k < 6; k++)
                        {
                            int day = monthsArray[i, k, j];
                            if (day != 0)
                                s += string.Format("{0, 3}", day);
                            else s += string.Format("{0, 3}", " ");
                        }
                    }
                    s += "\n";
                }
                s += "\n";
                if (stop < 12)
                    start += 3;
            } while (stop < 12);
            return s;
        }

        private int GetFirstDayOfMonth(DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Tuesday:
                    return 2;
                default:
                    return 3;
            }
        }
    }
}
