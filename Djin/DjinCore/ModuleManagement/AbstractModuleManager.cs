using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Core.ModuleManagement
{
    abstract class AbstractModuleManager
    {
        internal abstract void AddModule(ModuleDescription description);

        internal abstract void RunLoadedModule(ModuleDescription description);

        internal abstract void AddAndRunModule(ModuleDescription description);

        internal abstract void HaltLoadedModule(ModuleDescription description);
    }
}
