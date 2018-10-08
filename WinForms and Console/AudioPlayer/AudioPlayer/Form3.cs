using System;
using System.Drawing;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CommonInterface.Link3 = this;
            CommonInterface.SetEffectFromSettings();
            checkBox1.Checked = Properties.Settings.Default.EQMode;
        }
        private void colorSlider1_MouseWheel(object sender, MouseEventArgs e)
        {
            CommonInterface.SetEffect();
            panel1.Invalidate();
        }

        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            CommonInterface.SetEffect();
            panel1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Audio.SetEffects();
            CommonInterface.SetEffectFromSettings();
            panel1.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                Audio.SetEffects(save: false);
            }
            else
            {
                CommonInterface.SetEffect();
            }
            Properties.Settings.Default.EQMode = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Chorus = (float)colorSlider5.Value;
            Properties.Settings.Default.Echo = (float)colorSlider4.Value;
            Properties.Settings.Default.EQ0 = -(float)colorSlider1.Value / 10f;
            Properties.Settings.Default.EQ1 = -(float)colorSlider2.Value / 10f;
            Properties.Settings.Default.EQ2 = -(float)colorSlider3.Value / 10f;
            Properties.Settings.Default.EQ3 = -(float)colorSlider6.Value / 10f;
            Properties.Settings.Default.EQ4 = -(float)colorSlider7.Value / 10f;
            Properties.Settings.Default.EQ5 = -(float)colorSlider8.Value / 10f;
            Properties.Settings.Default.EQ6 = -(float)colorSlider9.Value / 10f;
            Properties.Settings.Default.EQ7 = -(float)colorSlider10.Value / 10f;
            Properties.Settings.Default.EQ8 = -(float)colorSlider11.Value / 10f;
            Properties.Settings.Default.EQ9 = -(float)colorSlider12.Value / 10f;
            Properties.Settings.Default.EQ10 = -(float)colorSlider13.Value / 10f;
            Properties.Settings.Default.EQ11 = -(float)colorSlider14.Value / 10f;
            Properties.Settings.Default.EQ12 = -(float)colorSlider15.Value / 10f;
            Properties.Settings.Default.EQ13 = -(float)colorSlider16.Value / 10f;
            Properties.Settings.Default.EQ14 = -(float)colorSlider17.Value / 10f;
            Properties.Settings.Default.EQ15 = -(float)colorSlider18.Value / 10f;
            Properties.Settings.Default.EQ16 = -(float)colorSlider19.Value / 10f;
            Properties.Settings.Default.EQ17 = -(float)colorSlider20.Value / 10f;
            Properties.Settings.Default.VolumeFX = 4f - (float)colorSlider21.Value / 100f;
            Properties.Settings.Default.Save();
            CommonInterface.Link3 = null;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Red, 0, 50, panel1.Width - 1, 50);
            PointF[] points = new PointF[36];
            float[] values = new float[18]
            {
                (float)colorSlider1.Value,
                (float)colorSlider2.Value,
                (float)colorSlider3.Value,
                (float)colorSlider6.Value,
                (float)colorSlider7.Value,
                (float)colorSlider8.Value,
                (float)colorSlider9.Value,
                (float)colorSlider10.Value,
                (float)colorSlider11.Value,
                (float)colorSlider12.Value,
                (float)colorSlider13.Value,
                (float)colorSlider14.Value,
                (float)colorSlider15.Value,
                (float)colorSlider16.Value,
                (float)colorSlider17.Value,
                (float)colorSlider18.Value,
                (float)colorSlider19.Value,
                (float)colorSlider20.Value,
            };
            for (int i = 0, j = 0; i < points.Length; i += 2, j++)
            {
                points[i] = new PointF(j * 26, (values[j] + 150f) / 3f);
                points[i + 1] = new PointF(j * 26 + 26, (values[j] + 150f) / 3f);
            }
            e.Graphics.DrawCurve(Pens.Black, points, 0f);
        }
    }
}
