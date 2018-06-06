using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LocalizationApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ChangeLanguage("ru-RU");
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-RU"));
            }
            else
            {
                ChangeLanguage("en-US");
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-US"));
            }
        }

        private void ChangeLanguage(string newLanguageString)
        {
            var resources = new ComponentResourceManager(typeof(Form1));

            CultureInfo newCultureInfo = new CultureInfo(newLanguageString);

            //Применяем для каждого контрола на форме новую культуру
            foreach (Control c in this.Controls)
            {
                resources.ApplyResources(c, c.Name, newCultureInfo);
            }

            //Отдельно меняем для тайтла самой формы локализацию
            resources.ApplyResources(this, "$this", newCultureInfo);
        }

        private void Form1_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            if (e.Culture.Name == "ru-RU")
            {
                radioButton1.Checked = true;
            }
            else radioButton2.Checked = true;
        }
    }
}
