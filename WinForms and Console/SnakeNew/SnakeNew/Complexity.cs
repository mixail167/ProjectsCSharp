using System;
using System.Configuration;
using System.Windows.Forms;

namespace SnakeNew
{
    class Complexity
    {
        string key;
        string level;
        uint score;
        int bombCount;
        int interval;

        public Complexity(string key, string level, int bombCount, int interval)
        {
            this.key = key;
            this.level = level;
            this.bombCount = bombCount;
            this.interval = interval;
            try
            {
                this.score = Convert.ToUInt16(ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings[this.key].Value);
            }
            catch (Exception)
            {
                this.score = 0;
            }
        }

        public string Key
        {
            get { return key; }
        }

        public string Level
        {
            get { return level; }
        }

        public uint Score
        {
            get { return score; }
            set { score = value; }
        }

        public int BombCount
        {
            get { return bombCount; }
        }

        public int Interval
        {
            get { return interval; }
        }

        public override string ToString()
        {
            return string.Format("Сложность: {0}; Счет: {1}.", this.level, this.score);
        }
    }
}
