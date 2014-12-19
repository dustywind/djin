using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Shared.Interfaces;

namespace Djin.Modules.DjinModuleTest
{
    public class DjinModuleTest : IDjinModule
    {
        private string INSTALL = "Djin.Install";
        private string UNINSTALL = "Djin.Uninstall";
        private string ON_START= "Djin.OnStart";
        private string RUN = "Djin.Run";
        private string ON_STOP = "Djin.OnStop";

        private IDjinOutput output;
        private Dictionary<string, string> parameters;

        public DjinModuleTest(IDjinOutput output, Dictionary<string, string> parameters)
        {
            this.output = output;
            this.parameters = parameters;
        }

        public void Install()
        {
            Console.WriteLine(this.INSTALL);
            return;
        }

        public void Uninstall()
        {
            Console.WriteLine(this.UNINSTALL);
            return;
        }

        public void OnStart()
        {
            Console.WriteLine(this.ON_START);
            return;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine(this.RUN);
            }
        }

        public void OnStop()
        {
            Console.WriteLine(this.ON_STOP);
            return;
        }

    }
}
