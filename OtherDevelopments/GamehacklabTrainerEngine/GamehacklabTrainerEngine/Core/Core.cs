using GamehacklabTrainerEngine.Cheat;
using GamehacklabTrainerEngine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamehacklabTrainerEngine.Core
{
    class Core
    {
        CheatManager cheatManager;
        IRenderer renderer;
        bool isInterrupted = false;

        public Core()
        {
            cheatManager = CheatManager.getInstance();
            renderer = new DefaultRenderer();
        }

        public void Interrupt()
        {
            isInterrupted = true;
        }

    }
}
