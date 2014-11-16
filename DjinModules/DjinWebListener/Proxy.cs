using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Djin.Modules.WebListener
{
    class Proxy
    {
        private int listenPort = 11000;

        internal void Test()
        {
            IPHostEntry ipHostInfo = Dns.Resolve("localhost" /*Dns.GetHostName()*/);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, listenPort);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            listener.Bind(localEndPoint);


            listener.Listen(5);
            Socket con = listener.Accept();

            byte[] buffer = new byte[4096 * 2];

            int read = con.Receive(buffer);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < read; i++)
            {
                sb.Append((char)buffer[i]);
            }
            string s = sb.ToString();

            return;
        }

        private ProxyConfig Config;

        internal Proxy(ProxyConfig config)
        {
            Config = config;
        }

        private void CreateProxyServer()
        {

        }

    }

    class ProxyConfig
    {
        public int NumberOfThreads = 1;

        public int ServerPort = 11000;
        public int ServerBacklog = 5;

        public bool LogTextOnly = true;

    }
}