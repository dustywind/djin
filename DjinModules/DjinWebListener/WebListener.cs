using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Modules.WebListener
{
    public class WebListener : global::Djin.Shared.Interfaces.IDjinModule
    {
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
            return;
        }

        public void Run()
        {
            ProxyConfig pConf = new ProxyConfig { NumberOfThreads = 1, ServerBacklog = 5, ServerPort = 11000};
            new Proxy(pConf).Test();
            return;
        }

        public void OnStop()
        {
            return;
        }
    }
}
