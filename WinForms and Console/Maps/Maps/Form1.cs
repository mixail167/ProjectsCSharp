using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Net;
using System.Windows.Forms;

namespace Maps
{
    public partial class Form1 : Form
    {
        bool lockFlag;

        public Form1()
        {
            InitializeComponent();
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
            comboBox1.ValueMember = "Name";
            comboBox1.DataSource = GMapProviders.List;
            comboBox1.SelectedItem = GMapProviders.OpenStreetMap;
            trackBar1.Maximum = gMapControl1.MaxZoom;
            trackBar1.Minimum = gMapControl1.MinZoom;
            lockFlag = true;
            trackBar1.Value = (int)Math.Floor(gMapControl1.Zoom);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = Convert.ToDouble(textBox1.Text);
                double lng = Convert.ToDouble(textBox2.Text);
                gMapControl1.Position = new PointLatLng(lat, lng);
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != string.Empty)
            {
                GeoCoderStatusCode status = gMapControl1.SetPositionByKeywords(textBox3.Text);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = comboBox1.SelectedItem as GMapProvider;
        }

        private void gMapControl1_OnMapZoomChanged()
        {
            if (!lockFlag)
            {
                trackBar1.Value = (int)Math.Floor(gMapControl1.Zoom);
            }
            lockFlag = !lockFlag;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (!lockFlag)
            {
                gMapControl1.Zoom = trackBar1.Value;
            }
            lockFlag = !lockFlag;
        }
    }
}
