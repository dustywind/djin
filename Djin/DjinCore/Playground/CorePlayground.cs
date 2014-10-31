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

            mdesc.Path = @"E:\Projects\djin\DjinModules\DjinWebListener\bin\Debug\DjinModuleTest.dll";
        }
    }
}
