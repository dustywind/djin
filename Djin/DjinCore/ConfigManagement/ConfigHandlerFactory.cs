using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Core.ConfigManagement
{
    class ConfigHandlerFactory
    {
        public static IConfigHandler GetHandler(string configPath)
        {
            return new XmlConfigHandler(configPath);
        }
    }
}
