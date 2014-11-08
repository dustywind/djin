using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Modules.DjinModuleTest
{
    public class DjinModuleTest : global::Djin.Shared.Interfaces.IDjinModule
    {
        private string INSTALL = "Djin.Install";
        private string UNINSTALL = "Djin.Uninstall";
        private string ON_START= "Djin.OnStart";
        private string RUN = "Djin.Run";
        private string ON_STOP = "Djin.OnStop";

        private Dictionary<string, object> parameters;

        public DjinModuleTest(Dictionary<string, object> parameters)
        {
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
            Console.WriteLine(this.RUN);
            return;
        }

        public void OnStop()
        {
            Console.WriteLine(this.ON_STOP);
            return;
        }

    }
}
