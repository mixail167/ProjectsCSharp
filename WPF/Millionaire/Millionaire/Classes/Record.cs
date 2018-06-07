using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Millionaire
{
    public class Record
    {
        int index = 0;
        string name;
        int score;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public static Record Parse(string[] parts)
        {
            try
            {
                if (parts.Length == 2)
                {
                    Record record = new Record();
                    record.name = parts[0];
                    record.score = Convert.ToInt32(parts[1]);
                    return record;
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}#{1}", name, score);
        }
    }
}
