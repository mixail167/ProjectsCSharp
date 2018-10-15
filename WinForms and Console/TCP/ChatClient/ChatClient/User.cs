using System;

namespace ChatClient
{
    class User
    {
        private Guid id;
        private string name;

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