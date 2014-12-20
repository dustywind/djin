using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Modules.WebListener
{
    public class WebListener : global::Djin.Shared.Interfaces.IDjinModule
    {
        private Proxy _Proxy;

        public void Install()
        {
            return;
        }

        public void Uninstall()
        {
            return;
        }

        public void OnStart()
        {
            _Proxy = new Proxy(new ProxyConfig
            {
                NumberOfThreads = 1,
                ServerBacklog = 5,
                ServerPort = 11000
            });
        }

        public void Run()
        {
            _Proxy.StartProxyServer();
            return;
        }

        public void OnStop()
        {
            _Proxy.Close();
            return;
        }
    }
}
