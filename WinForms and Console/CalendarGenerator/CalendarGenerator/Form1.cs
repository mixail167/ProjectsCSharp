using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalendarGenerator
{
    public partial class Form1 : Form
    {
        private int year;
        private string[] daysOfWeek = new string[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };

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
                this.year = Convert.ToInt32(numericUpDown1.Value);
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitDataGridView();
            numericUpDown1.Value = Convert.ToDecimal(DateTime.Now.Year);
            this.year = Convert.ToInt32(numericUpDown1.Value);
        }

        private void GenerateCalendar(DataGridView dataGridView, int monthNumber, int year)
        {
            DateTime dateTime = new DateTime(year, monthNumber, 1);
            int indexRow;
            int indexColumn = 1;
            int monthCount = DateTime.DaysInMonth(year, monthNumber);
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    indexRow = 5;
                    break;
                case DayOfWeek.Monday:
                    indexRow = 1;
                    break;
                case DayOfWeek.Saturday:
                    indexRow = 6;
                    break;
                case DayOfWeek.Sunday:
                    indexRow = 7;
                    break;
                case DayOfWeek.Thursday:
                    indexRow = 4;
                    break;
                case DayOfWeek.Tuesday:
                    indexRow = 2;
                    break;
                default:
                    indexRow = 3;
                    break;
            }

            dataGridView[indexColumn, 0].Value = new GregorianCalendar().GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            for (int i = 1; i <= monthCount; i++)
            {
                dataGridView[indexColumn, indexRow].Value = i.ToString();
                dataGridView[indexColumn, indexRow].Style = cellStyle;
                if (indexRow == dataGridView1.RowCount - 1)
                {
                    indexColumn++;
                    indexRow = 1;
                    try
                    {
                        dataGridView[indexColumn, 0].Value = new GregorianCalendar().GetWeekOfYear(new DateTime(year, monthNumber, i + 1), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
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
                int year = Convert.ToInt32(numericUpDown1.Value);
                this.year = year;
                if (year == DateTime.Now.Year)
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
                    GenerateCalendar(dataGridView, i, Convert.ToInt32(numericUpDown1.Value));
                }
            }
            catch (Exception)
            {
                numericUpDown1.Value = this.year;
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
    }
}
