using GMap.NET;
using GMap.NET.MapProviders;
using Maps.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            comboBox1.DataSource = GMapProviders.List.Where<GMapProvider>(x => x.Name != "None" && x.Name != "CustomMapProvider").ToList<GMapProvider>();
            comboBox1.SelectedItem = GMapProviders.OpenStreetMap;
            trackBar1.Maximum = gMapControl1.MaxZoom;
            trackBar1.Minimum = gMapControl1.MinZoom;
            lockFlag = true;
            trackBar1.Value = (int)Math.Floor(gMapControl1.Zoom);
            gMapControl1.Position = new PointLatLng(Settings.Default.lat, Settings.Default.lng);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = Convert.ToDouble(textBox1.Text);
                double lng = Convert.ToDouble(textBox2.Text);
                gMapControl1.Position = new PointLatLng(lat, lng);
                Settings.Default.lat = lat;
                Settings.Default.lng = lng;
                Settings.Default.Save();
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

        private string GetIP()
        {
            try
            {
                string ip = new WebClient().DownloadString(new Uri("https://api.ipify.org"));
                if (Regex.IsMatch(ip, @"(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}"))
                {
                    return ip;
                }
                else return null;
            }
            catch (WebException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async void Button3_ClickAsync(object sender, EventArgs e)
        {
            string ip = GetIP();
            if (ip != null)
            {
                try
                {
                    JObject json = await GetGPSInfoAsync(ip);
                    JToken child = json["latitude"];
                    if (child != null)
                    {
                        textBox1.Text = child.ToString();
                    }
                    child = json["longitude"];
                    if (child != null)
                    {
                        textBox2.Text = child.ToString();
                    }
                }
                catch (WebException exception)
                {
                    if (exception.Status == WebExceptionStatus.ProtocolError)
                    {
                        switch ((exception.Response as HttpWebResponse).StatusCode)
                        {
                            case HttpStatusCode.NotFound://404
                                break;
                            case HttpStatusCode.Found://302
                                break;
                            case HttpStatusCode.Moved://301
                                break;
                            case HttpStatusCode.RedirectMethod://303
                                break;
                            case HttpStatusCode.SwitchingProtocols://101
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не удалось определить IP-адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async Task<JObject> GetGPSInfoAsync(string ip)
        {
            WebRequest request = WebRequest.Create(string.Format("http://api.ipstack.com/{0}?access_key={1}&output=json&language=ru&fields=latitude,longitude", ip, Settings.Default.access_key));
            try
            {
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return JToken.ReadFrom(new JsonTextReader(reader)) as JObject;
                        }
                    }
                }
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.lat = gMapControl1.Position.Lat;
            Settings.Default.lng = gMapControl1.Position.Lng;
            Settings.Default.Save();
            gMapControl1.Manager.CancelTileCaching();
        }
    }
}