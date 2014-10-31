using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Core.ModuleManagement;

namespace Djin.Core.Playground
{
    class CorePlayground
    {
        public void Play()
        {
            var mm = new ModuleManager();
            var mdesc = new ModuleDescription();

            mdesc.AssemblyPath = @"E:\Projects\djin\DjinModules\DjinModuleTest\bin\Debug\DjinModuleTest.dll";
            mdesc.ClassName = "DjinModuleTest";
            mdesc.Namespace = "Djin.Modules.DjinModuleTest";


            mm.AddModule(mdesc);
            mm.RunLoadedModule(mdesc);
            "breakpoint".Split(',');
        }
    }
}
