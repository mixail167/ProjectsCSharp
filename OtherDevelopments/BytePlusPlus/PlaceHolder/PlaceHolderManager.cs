using System.Drawing;
using System.Windows.Forms;

namespace PlaceHolder
{
    public static class PlaceHolderManager
    {
        public static void AddPlaceHolder(this TextBox tb, string placeHolderText)
        {
            tb.ForeColor = Color.Gray;
            tb.Text = placeHolderText;
            tb.Enter += (s, e) =>
                {
                    if (tb.Text == placeHolderText)
                    {
                        tb.Text = string.Empty;
                        tb.ForeColor = Color.Black;
                    }
                };
            tb.Leave += (s, e) =>
            {
                if (tb.Text == string.Empty)
                {
                    tb.Text = placeHolderText;
                    tb.ForeColor = Color.Gray;
                }
            };

        }
    }
}
