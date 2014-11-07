using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Core.ModuleManagement;

namespace Djin.Core.ConfigManagement
{
    interface IConfigHandler
    {
        void CreateNewConfig();

        List<ModuleDescription> GetModuleListFromConfig();

        void AddModuleToConfig(ModuleDescription md);

        void RemoveModuleFromConfig(ModuleDescription md);
    }
}
