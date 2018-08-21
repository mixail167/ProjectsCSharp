using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GamehacklabTrainerEngine.Cheat
{
    class CheatManager // класс, управляющий читами
    {
        private Dictionary<int, Cheat> cheats = new Dictionary<int, Cheat>();
        private static CheatManager instance = null;
        private int ProcessID;

        public int GetProcessID() { return ProcessID; }

        private CheatManager() { }

        public static CheatManager getInstance()
        {
            if (instance == null)
            {
                instance = new CheatManager();
            }

            return instance;

        }

        

    }
}
