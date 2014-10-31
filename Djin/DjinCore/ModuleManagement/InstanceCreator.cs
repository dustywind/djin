using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Djin.Shared.Interfaces;

namespace Djin.Core.ModuleManagement
{
    class InstanceCreator
    {
        private ModuleDescription Description;

        internal InstanceCreator(ModuleDescription description) {
            this.Description = description;
        }

        internal object GetNewInstance() 
        {
            try
            {
                IDjinModule module = GetAssembly().CreateInstance(Description.Name) as IDjinModule;
                if (module == null)
                {
                    throw new Exception("Could not create a instanceof: " + Description);
                }
                return module;
            }
            catch (Exception e)
            {
                // TODO propper loging
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        private Assembly GetAssembly()
        {
            return Assembly.LoadFile(Description.Path);
        }
    }
}
