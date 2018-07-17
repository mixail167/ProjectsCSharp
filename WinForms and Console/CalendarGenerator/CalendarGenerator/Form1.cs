using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using PDF = Spire.Pdf;
using PDFGraphics = Spire.Pdf.Graphics;
using PDFFind = Spire.Pdf.General.Find;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
            BackColor = Color.Gold,
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204))),
            SelectionBackColor = Color.Gold,
            SelectionForeColor = Color.Black,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };
        private DataGridViewCellStyle cellDefaultStyle = new DataGridViewCellStyle
        {
            BackColor = Color.Silver,
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204))),
            SelectionBackColor = Color.Silver,
            SelectionForeColor = Color.Black,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };

        private DataGridViewCellStyle cellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.White,
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204))),
            SelectionBackColor = Color.OrangeRed,
            SelectionForeColor = Color.Black,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };

        private DataGridViewCellStyle cellWeekEndStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Red,
            BackColor = Color.White,
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204))),
            SelectionBackColor = Color.OrangeRed,
            SelectionForeColor = Color.Black,
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
                if (indexRow >= 6)
                {
                    dataGridView[indexColumn, indexRow].Style = cellWeekEndStyle;
                }
                else
                {
                    dataGridView[indexColumn, indexRow].Style = cellStyle;
                }
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

        private Paragraph CreateParagraph(Section section, Spire.Doc.Documents.HorizontalAlignment alignment = Spire.Doc.Documents.HorizontalAlignment.Left)
        {
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.HorizontalAlignment = alignment;
            return paragraph;
        }

        private bool WriteToPDFDocument(string text, string path)
        {
            try
            {
                PDF.PdfDocument doc = new PDF.PdfDocument();
                doc.XmpMetaData.SetAuthor(Environment.UserName);
                doc.XmpMetaData.SetCreateDate(DateTime.Now);
                doc.XmpMetaData.SetCreator(Environment.UserName);
                doc.XmpMetaData.SetKeywords(string.Format("Календарь, {0}", this.dateTime.Year));
                doc.XmpMetaData.SetProducer(Environment.UserName);
                doc.XmpMetaData.SetSubject("Календари");
                doc.XmpMetaData.SetTitle(string.Format("Календарь на {0} г.", this.dateTime.Year));
                PDFGraphics.PdfMargins margin = new PDFGraphics.PdfMargins();
                PDFGraphics.PdfUnitConvertor unitConvertor = new PDFGraphics.PdfUnitConvertor();
                margin.Top = unitConvertor.ConvertUnits(0.71f, PDFGraphics.PdfGraphicsUnit.Centimeter, PDFGraphics.PdfGraphicsUnit.Point);
                margin.Bottom = margin.Top;
                margin.Left = unitConvertor.ConvertUnits(1.76f, PDFGraphics.PdfGraphicsUnit.Centimeter, PDFGraphics.PdfGraphicsUnit.Point);
                margin.Right = margin.Left;
                PDF.PdfPageBase page = doc.Pages.Add(PDF.PdfPageSize.A4, margin);
                PDFGraphics.PdfTrueTypeFont timesNewRoman = new PDFGraphics.PdfTrueTypeFont(@"C:\Windows\Fonts\times.ttf", 16.0f);
                PDFGraphics.PdfTrueTypeFont courierNew = new PDFGraphics.PdfTrueTypeFont(@"C:\Windows\Fonts\cour.ttf", 12.0f);
                PDFGraphics.PdfStringFormat alignCenter = new PDFGraphics.PdfStringFormat(PDFGraphics.PdfTextAlignment.Center);
                page.Canvas.DrawString(string.Format("Календарь на {0} г.", this.dateTime.Year), timesNewRoman, new PDFGraphics.PdfSolidBrush(Color.Blue), page.Canvas.ClientSize.Width / 2, 0, alignCenter);
                float y = 43;
                string[] lines = text.Split(new char[] { '\n' }, StringSplitOptions.None);
                foreach (string item in lines)
                {
                    bool isFound = false;
                    if (item.Trim().StartsWith(@"Дн\Нед"))
                    {
                        page.Canvas.DrawRectangle(new PDFGraphics.PdfSolidBrush(Color.Yellow), new RectangleF(0, y, courierNew.MeasureString(item, courierNew.Size).Width * item.Length, 13));
                    }
                    else
                    {
                        for (int i = 0; i < daysOfWeek.Length; i++)
                        {
                            if (item.Trim().StartsWith(daysOfWeek[i]))
                            {
                                string temp = item.Substring(4, 3);
                                SizeF size = courierNew.MeasureString(temp, courierNew.Size);
                                page.Canvas.DrawRectangle(new PDFGraphics.PdfSolidBrush(Color.Yellow), new RectangleF(size.Width * 4, y, size.Width * temp.Length, 13));
                                if (i >= 5)
                                {
                                    page.Canvas.DrawString(item, courierNew, new PDFGraphics.PdfSolidBrush(Color.Red), 0, y);
                                }
                                else
                                {
                                    page.Canvas.DrawString(item, courierNew, new PDFGraphics.PdfSolidBrush(Color.Black), 0, y);
                                }
                                isFound = true;
                                break;
                            }
                        }
                    }
                    if (!isFound)
                    {
                        page.Canvas.DrawString(item, courierNew, new PDFGraphics.PdfSolidBrush(Color.Black), 0, y);
                    }
                    y += 13;
                }
                doc.SaveToFile(path, PDF.FileFormat.PDF);
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
                doc.BuiltinDocumentProperties.Title = string.Format("Календарь на {0} г.", this.dateTime.Year);
                doc.BuiltinDocumentProperties.Author = Environment.UserName;
                doc.BuiltinDocumentProperties.Category = "Календари";
                doc.BuiltinDocumentProperties.Keywords = string.Format("Календарь, {0}", this.dateTime.Year);
                doc.BuiltinDocumentProperties.Comments = string.Format("Календарь на {0} г.", this.dateTime.Year);
                Section section = doc.AddSection();

                Paragraph paragraph = CreateParagraph(section, Spire.Doc.Documents.HorizontalAlignment.Center);
                TextRange textRange = paragraph.AppendText(string.Format("Календарь на {0} г.\n\n", this.dateTime.Year));
                textRange.CharacterFormat.Font = new Font("Times New Roman", 16.0f, FontStyle.Regular);
                textRange.CharacterFormat.TextColor = Color.Blue;

                paragraph = CreateParagraph(section);
                string[] lines = text.Split(new char[] { '\n' }, StringSplitOptions.None);
                foreach (string item in lines)
                {
                    textRange = paragraph.AppendText(item + "\n");
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
                        textSelection.GetAsOneRange().CharacterFormat.HighlightColor = Color.Yellow;
                    }
                }

                doc.SaveToFile(path, Spire.Doc.FileFormat.Docx);
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
                    writeInFile = WriteToPDFDocument;
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
                saveFileDialog1.Filter = "Portable Document Format (*.pdf)|*.pdf";
            else if (radioButton2.Checked)
                saveFileDialog1.Filter = "Документ Word (*.docx)|*.docx";
            else saveFileDialog1.Filter = "Текстовый документ (*.txt)|*.txt";
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