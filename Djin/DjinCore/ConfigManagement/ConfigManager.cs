using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace Djin.Core.ConfigManagement
{
    class ConfigManager
    {
        private string DjinModuleConfigPath;

        internal ConfigManager()
        {
#if DEBUG
            DjinModuleConfigPath = ConfigurationManager.AppSettings["DjinModuleTestConfigPath"]
               .ToString();
#else
            DjinModuleConfigPath = ConfigurationManager.AppSettings["DjinModuleConfigPath"]
               .ToString();
#endif
        }
    }
}
