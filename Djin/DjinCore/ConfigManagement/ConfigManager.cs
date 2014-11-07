using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Djin.Core.ModuleManagement;

namespace Djin.Core.ConfigManagement
{
    class ConfigManager
    {

        private string djinModuleConfigPath;
        private IConfigHandler configHandler;

        internal ConfigManager()
        {
#if DEBUG
            djinModuleConfigPath = ConfigurationManager.AppSettings["DjinModuleTestConfigPath"]
               .ToString();
#else
            djinModuleConfigPath = ConfigurationManager.AppSettings["DjinModuleConfigPath"]
               .ToString();
#endif
            configHandler = ConfigHandlerFactory.GetHandler(djinModuleConfigPath);
        }

        private void CreateNewConfig()
        {
            this.configHandler.CreateNewConfig();
        }

        internal void StartModulesFromConfig()
        {
            List<ModuleDescription> modules = configHandler.GetModuleListFromConfig();
            foreach (var module in modules)
            {
                ModuleManager.Instance.AddAndRunModule(module);
            }
        }
    }
}
