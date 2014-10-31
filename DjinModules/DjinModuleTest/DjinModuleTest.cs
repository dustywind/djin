using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Modules.DjinModuleTest
{
    public class DjinModuleTest : global::Djin.Shared.Interfaces.IDjinModule
    {

        public void Install()
        {
            Console.WriteLine("DjinModuleTest.Install");
            return;
        }

        public void Uninstall()
        {
            Console.WriteLine("Djin.ModuleTest.Uninstall");
            return;
        }

        public void OnStart()
        {
            Console.WriteLine("Djin.OnStart");
            return;
        }

        public void Run()
        {
            Console.WriteLine("Djin.Run");
            return;
        }

        public void OnStop()
        {
            Console.WriteLine("Djin.OnStop");
            return;
        }

    }
}
