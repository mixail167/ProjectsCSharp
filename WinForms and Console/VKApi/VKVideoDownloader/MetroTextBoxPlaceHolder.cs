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
                    bool useSystemPasswordChar = UseSystemPasswordChar;
                    char passwordChar = PasswordChar;
                    ForeColor = Color.Gray;
                    SetText(string.Empty, placeHolder, false, '\0', Color.Gray);
                    Enter += (s, e) =>
                    {
                        SetText(placeHolder, string.Empty, useSystemPasswordChar, passwordChar, Color.Black);
                    };
                    Leave += (s, e) =>
                    {
                        SetText(string.Empty, placeHolder, false, '\0', Color.Gray);
                    };
                }
            }
        }

        public bool IsPlaceHolder()
        {
            return Text == placeHolder;
        }

        private void SetText(string value1, string value2, bool useSystemPasswordChar, char passwordChar, Color foreColor)
        {
            if (Text == value1)
            {
                Text = value2;
                UseSystemPasswordChar = useSystemPasswordChar;
                PasswordChar = passwordChar;
                ForeColor = foreColor;
            }
        }
    }
}
