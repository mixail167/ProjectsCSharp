using System;
using System.Net.Sockets;

namespace ChatServer
{
    public class Client
    {
        private Guid id;
        private NetworkStream networkStream;
        private string user;
        private TcpClient client;

        public Client(TcpClient client)
        {
            this.id = Guid.NewGuid();
            this.client = client;
            this.networkStream = this.client.GetStream();
        }

        public Guid ID
        {
            get { return id; }
            private set { id = value; }
        }

        public NetworkStream NetworkStream
        {
            get { return networkStream; }
            set { networkStream = value; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        internal void Close()
        {
            if (this.networkStream != null)
                this.networkStream.Close();
            if (this.client != null)
                this.client.Close();
        }
    }
}
