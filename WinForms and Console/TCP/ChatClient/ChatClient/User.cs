using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    class User
    {
        Guid id;
        string name;

        public User(string text)
        {
            try
            {
                string[] tmp = text.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                id = Guid.Parse(tmp[0]);
                name = tmp[1];
            }
            catch (Exception)
            {
                
            }
        }

        public Guid ID
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}