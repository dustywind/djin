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
                var assembly = GetAssembly();
                var module = assembly.GetModule(Description.ModuleName);
                var type = module.GetType(Description.FullName);
                ConstructorInfo constructorInfo = type.GetConstructor(new Type[0]);
                var instance = constructorInfo.Invoke(null) as IDjinModule;

                if (instance == null)
                {
                    throw new Exception("Could not create a instanceof: " + Description);
                }
                return instance;
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
            return Assembly.LoadFile(Description.AssemblyPath);
        }
    }
}
