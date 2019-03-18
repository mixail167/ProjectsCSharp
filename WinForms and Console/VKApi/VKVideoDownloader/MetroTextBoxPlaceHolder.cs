using MetroFramework.Controls;
using System.Drawing;

namespace VKVideoDownloader
{
    class MetroTextBoxPlaceHolder : MetroTextBox
    {
        private string placeHolder;

        public MetroTextBoxPlaceHolder()
            : base()
        {

        }

        public string PlaceHolder
        {
            get { return placeHolder; }
            set
            {
                if (value != string.Empty)
                {
                    placeHolder = value;
                    bool useSystemPasswordChar = this.UseSystemPasswordChar;
                    char passwordChar = this.PasswordChar;
                    this.ForeColor = Color.Gray;
                    this.SetText(string.Empty, placeHolder, false, '\0', Color.Gray);
                    this.Enter += (s, e) =>
                    {
                        this.SetText(placeHolder, string.Empty, useSystemPasswordChar, passwordChar, Color.Black);
                    };
                    this.Leave += (s, e) =>
                    {
                        this.SetText(string.Empty, placeHolder, false, '\0', Color.Gray);
                    };
                }
            }
        }

        public bool isPlaceHolder()
        {
            return (this.Text == this.placeHolder) ? true : false;
        }

        private void SetText(string value1, string value2, bool useSystemPasswordChar, char passwordChar, Color foreColor)
        {
            if (this.Text == value1)
            {
                this.Text = value2;
                this.UseSystemPasswordChar = useSystemPasswordChar;
                this.PasswordChar = passwordChar;
                this.ForeColor = foreColor;
            }
        }
    }
}
