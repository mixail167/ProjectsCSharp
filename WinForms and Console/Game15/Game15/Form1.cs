using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game15
{
    public partial class Form1 : Form
    {
        private bool isMouseDown;
        private Point mouseCoordinates;
        private List<Tuple<int, int, Image>> data;
        private ComponentResourceManager resourceManager;
        private bool run;

        public Form1()
        {
            InitializeComponent();
            resourceManager = new ComponentResourceManager(this.GetType());
            SetMessageBoxProperties(resourceManager, Properties.Settings.Default.Language);
            run = false;
        }

        public static void SetMessageBoxProperties(ComponentResourceManager resourceManager, CultureInfo cultureInfo)
        {
            MessageBoxManager.Unregister();
            MessageBoxManager.OK = resourceManager.GetString("ok", cultureInfo);
            MessageBoxManager.Cancel = resourceManager.GetString("cancel", cultureInfo);
            MessageBoxManager.Retry = resourceManager.GetString("retry", cultureInfo);
            MessageBoxManager.Ignore = resourceManager.GetString("ignore", cultureInfo);
            MessageBoxManager.Abort = resourceManager.GetString("abort", cultureInfo);
            MessageBoxManager.Yes = resourceManager.GetString("yes", cultureInfo);
            MessageBoxManager.No = resourceManager.GetString("no", cultureInfo);
            MessageBoxManager.Register();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mouseCoordinates = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (e.X > mouseCoordinates.X)
                {
                    this.Location = new Point(this.Location.X + 1, this.Location.Y);
                }
                else if (e.X < mouseCoordinates.X)
                {
                    this.Location = new Point(this.Location.X - 1, this.Location.Y);
                }
                if (e.Y > mouseCoordinates.Y)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y + 1);
                }
                else if (e.Y < mouseCoordinates.Y)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y - 1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(resourceManager.GetString("message", Properties.Settings.Default.Language), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestroyPictureBoxes();
                run = NewGame();
            }
        }

        private void DestroyPictureBoxes()
        {
            panel1.Controls.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string gameMode = Properties.Settings.Default.GameMode;
            Form2 form2 = new Form2();
            form2.ShowDialog();
            if (run && gameMode!=Properties.Settings.Default.GameMode && 
                MessageBox.Show(resourceManager.GetString("message", Properties.Settings.Default.Language), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestroyPictureBoxes();
                run = NewGame();
            }
        }

        private bool NewGame()
        {
            switch (Properties.Settings.Default.GameMode)
            {
                case "Classic":
                    data = GenerateData();
                    break;
                default:
                    openFileDialog1.FileName = string.Empty;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        data = InitData(new Bitmap(openFileDialog1.FileName));
                    }
                    else return false;
                    break;
            }
            Random rand = new Random();
            PictureBox[] pictureBoxes = new PictureBox[16];
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                bool visible;
                int index;
                if (i == 15)
                {
                    visible = false;
                    index = 0;
                }
                else
                {
                    visible = true;
                    index = rand.Next(data.Count - 1);
                    //index = 0;
                }
                int x = (int)(i * 1.0 / 4) * 75 + ((int)(i * 1.0 / 4) + 1) * 3;
                int y = (i % 4) * 75 + ((i % 4) + 1) * 3;
                pictureBoxes[i] = new PictureBox
                {
                    Location = new Point(x, y),
                    Image = data[index].Item3,
                    Tag = data[index],
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(75, 75),
                    Visible = visible,
                    Cursor = Cursors.Hand
                };
                pictureBoxes[i].MouseClick += pictureBox_MouseClick;
                data.RemoveAt(index);
            }
            panel1.Controls.AddRange(pictureBoxes);
            return true;
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
            {
                PictureBox pictureBox = sender as PictureBox;
                PictureBox pictureBox16 = panel1.Controls[panel1.Controls.Count - 1] as PictureBox;
                int dx = Math.Abs(pictureBox.Location.X - pictureBox16.Location.X);
                int dy = Math.Abs(pictureBox.Location.Y - pictureBox16.Location.Y);
                if ((dx == 0 && dy < 2 * pictureBox.Height) || (dy == 0 && dx < 2 * pictureBox.Width))
                {
                    panel1.Controls.Remove(pictureBox);
                    panel1.Controls.Remove(pictureBox16);
                    Point location = pictureBox.Location;
                    pictureBox.Location = pictureBox16.Location;
                    pictureBox16.Location = location;
                    panel1.Controls.Add(pictureBox);
                    panel1.Controls.Add(pictureBox16);
                    if (CheckWin())
                    {
                        panel1.Controls[panel1.Controls.Count - 1].Visible = true;
                        MessageBox.Show(resourceManager.GetString("messageWin", Properties.Settings.Default.Language));
                        DestroyPictureBoxes();
                        run = false;
                    }
                }
            }
        }

        private bool CheckWin()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                PictureBox pictureBox = panel1.Controls[i] as PictureBox;
                int x = (int)(pictureBox.Location.X * 1.0 / pictureBox.Width);
                int y = (int)(pictureBox.Location.Y * 1.0 / pictureBox.Height);
                Tuple<int, int, Image> tag = pictureBox.Tag as Tuple<int, int, Image>;
                if (x != tag.Item1 || y != tag.Item2)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Tuple<int, int, Image>> GenerateData()
        {
            List<Tuple<int, int, Image>> data = new List<Tuple<int, int, Image>>();
            Font font = new Font("Courier New", 30f);
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Bitmap block = new Bitmap(75, 75);
                    string text = (i * 4 + j + 1).ToString();
                    using (Graphics g = Graphics.FromImage(block))
                    {
                        SizeF size = g.MeasureString(text, font);
                        g.Clear(Color.White);
                        g.DrawString(text, font, Brushes.Black, new PointF((float)((block.Width * 1.0f - size.Width) / 2), (float)((block.Height * 0.5f) / 2)));
                    }
                    data.Add(new Tuple<int, int, Image>(j, i, block));
                }
            }
            return data;
        }

        private List<Tuple<int, int, Image>> InitData(Bitmap bitmap)
        {
            int widthBlock = (int)(bitmap.Width * 1.0 / 4);
            int heightBlock = (int)(bitmap.Height * 1.0 / 4);
            List<Tuple<int, int, Image>> data = new List<Tuple<int, int, Image>>();
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Bitmap block = new Bitmap(widthBlock, heightBlock);
                    using (Graphics g = Graphics.FromImage(block))
                    {
                        g.DrawImage(bitmap, new Rectangle(0, 0, widthBlock, heightBlock), new Rectangle(j * widthBlock, i * heightBlock, widthBlock, heightBlock), GraphicsUnit.Pixel);
                    }
                    data.Add(new Tuple<int, int, Image>(j, i, block));
                }
            }
            return data;
        }
    }
}
