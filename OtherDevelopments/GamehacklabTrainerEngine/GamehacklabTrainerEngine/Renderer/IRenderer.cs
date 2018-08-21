using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamehacklabTrainerEngine.Renderer
{
    interface IRenderer
    {
        void DrawText(String text, int x, int y);
        void DrawImage(ref Bitmap image, int x, int y, int width, int height);
        void DrawBackground();

    }
}
