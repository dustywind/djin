using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Djin.Modules.WebListener
{
    class Proxy : IDisposable
    {
        private ProxyConfig Config;
        private Thread ProxyThread;

        private Socket listener;

        internal Proxy(ProxyConfig config)
        {
            Config = config;
            CreateProxyServer();
        }

        private void CreateProxyServer()
        {
            listener = PrepareListenSocket();
            ProxyThread = new Thread(new ThreadStart(RunProxyServer));
        }

        private Socket PrepareListenSocket()
        {
            IPAddress host = new IPAddress(new byte[] { 127, 0, 0, 1 });
            IPEndPoint endpoint = new IPEndPoint(host, Config.ServerPort);

            Socket ListenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenSock.Bind(endpoint);
            ListenSock.Listen(Config.ServerBacklog);
            return ListenSock;
        }

        internal void Close()
        {
            listener.Close();
            ProxyThread.Join(Config.ShutdownGraceTime);
        }

        internal void StartProxyServer()
        {
            ProxyThread.Start();
        }

        internal void RunProxyServer()
        {
            try
            {
                while (true)
                {
                    Socket con = listener.Accept();
                    new Thread(new ThreadStart(
                            new ProxyRequestHandler(con).ProcessProxyRequest
                        )
                    ).Start();
                }
            }
            catch (ObjectDisposedException)
            {
                // simply shut down
            }
        }


        private class ProxyRequestHandler
        {
            private Socket client;

            public ProxyRequestHandler(Socket client)
            {
                this.client = client;
                //this.client.Blocking = false;
            }

            public void ProcessProxyRequest()
            {
                try
                {
                    ProxyRequest pRequest = ReceiveRequestFromClient();
                    ProxyResponse pResponse = ProcessRequest(pRequest);
                    SendResponseToClient(pResponse);

                }
                catch (Exception)
                {
                    // Suppress all exceptions. Do not let them interfere with the original process
                    // maybe we should log smthing
                }
                finally
                {
                    client.Close();
                }
            }

            private ProxyRequest ReceiveRequestFromClient()
            {
                ProxyRequest request = new ProxyRequest();
                try
                {
                    //Djin.Modules.WebListener.ProxyHttpHelper.ProxyHttpFactory.CreateRequestFromString("");

                }
                catch (SocketException se)
                {
                    throw new Exception(se.Message);
                }

                throw new NotImplementedException();
            }

            private ProxyResponse ProcessRequest(ProxyRequest pr)
            {
                throw new NotImplementedException();
            }

            private void SendResponseToClient(ProxyResponse pr)
            {
                throw new NotImplementedException();
            }
        }


        public void Dispose()
        {
            this.Close();
        }
    }

    class ProxyConfig
    {
        // TODO impl Threadpool
        public int NumberOfThreads = 1;

        public int ServerPort = 11000;
        public int ServerBacklog = 5;

        public int ShutdownGraceTime = 500;

        public bool LogTextOnly = true;
    }


    class ProxyRequest
    {
        public string HttpMethod;
        public string HttpVersion;

    }

    class ProxyResponse 
    {

    }
}