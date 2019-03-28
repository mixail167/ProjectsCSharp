using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text.RegularExpressions;
using VKVideoDownloader.Properties;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace VKVideoDownloader
{
    public partial class Form1 : MetroForm
    {
        bool firstForm;
        string id;
        string access_token;

        public Form1(bool firstForm)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            metroCheckBox1.Checked = Settings.Default.Remember;
            if (metroCheckBox1.Checked)
            {
                metroTextBoxPlaceHolder1.Text = Settings.Default.Login;
            }
            this.firstForm = firstForm;
        }

        private async Task Autorization()
        {
            metroProgressSpinner1.Visible = true;
            metroButton1.Enabled = false;
            this.Refresh();
            if (InterNet.IsConnected)
            {
                if (Functions.CheckValid(metroTextBoxPlaceHolder1.Text, "^[0-9]{12}$") && Functions.CheckValid(metroTextBoxPlaceHolder2.Text, "^[0-9a-zA-Z]{3,20}$"))
                {                    
                    try
                    {
                        metroLabel3.Visible = false;
                        string url = string.Format(Resources.Autorization, metroTextBoxPlaceHolder1.Text, metroTextBoxPlaceHolder2.Text);
                        Request request = new Request(url);
                        dynamic json = JObject.Parse(await request.GetAsync());
                        id = json.user_id.ToString();
                        if (id.Length > 1)
                        {
                            access_token = json.access_token.ToString();
                            if (metroCheckBox1.Checked)
                            {
                                Settings.Default.Login = metroTextBoxPlaceHolder1.Text;
                            }
                            Settings.Default.Remember = metroCheckBox1.Checked;
                            Settings.Default.Save(); 
                            this.DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                    catch (WebException exception)
                    {
                        if (exception.Status == WebExceptionStatus.ProtocolError)
                        {
                            switch ((exception.Response as HttpWebResponse).StatusCode)
                            {
                                case System.Net.HttpStatusCode.Unauthorized:
                                    Error("Неверный логин и/или пароль!");
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Error(exception.Message);
                        }
                    }
                    catch (Exception exception)
                    {
                        Error(exception.Message);
                    }
                }
                else
                {
                    Error("Неверный логин и/или пароль!");
                }
            }
            else
            {
                Error("Отсутствует соединение с сетью Интернет.");
            }
        }

        private async void metroButton1_Click(object sender, EventArgs e)
        {
           await Autorization();
        }       

        private void Error(string text)
        {
            metroLabel3.Visible = true;
            metroLabel3.Text = text;
            metroProgressSpinner1.Visible = false;
            metroButton1.Enabled = true;
            this.Refresh();
        }

        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && firstForm)
            {
                this.Hide();
                (new Form2(access_token, id)).ShowDialog();
            }
        }

        public string ID
        {
            get { return id; }
        }

        public string AccessToken
        {
            get { return access_token; }
        }

        private void metroLabel3_MouseEnter(object sender, EventArgs e)
        {
            metroToolTip1.SetToolTip(metroLabel3, metroLabel3.Text);
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!metroTextBoxPlaceHolder1.isPlaceHolder())
            {
                if (Functions.CheckValid(metroTextBoxPlaceHolder1.Text, "^[0-9]{0,12}$"))
                {
                    metroTextBoxPlaceHolder1.Tag = metroTextBoxPlaceHolder1.Text;
                }
                else if (metroTextBoxPlaceHolder1.Tag != null)
                {
                    metroTextBoxPlaceHolder1.Text = metroTextBoxPlaceHolder1.Tag.ToString();
                } 
            }
        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Autorization();
            }
        }

        private void metroTextBoxPlaceHolder2_TextChanged(object sender, EventArgs e)
        {
            if (!metroTextBoxPlaceHolder2.isPlaceHolder())
            {
                if (Functions.CheckValid(metroTextBoxPlaceHolder2.Text, "^[0-9a-zA-Z]{0,20}$"))
                {
                    metroTextBoxPlaceHolder2.Tag = metroTextBoxPlaceHolder2.Text;
                }
                else if (metroTextBoxPlaceHolder2.Tag != null)
                {
                    metroTextBoxPlaceHolder2.Text = metroTextBoxPlaceHolder2.Tag.ToString();
                }
            }
        }
    }
}
