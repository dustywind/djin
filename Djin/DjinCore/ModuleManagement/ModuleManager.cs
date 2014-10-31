using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Shared.Interfaces;

namespace Djin.Core.ModuleManagement
{
    class ModuleManager
    {
        /**
         * This class is responsible for both
         * starting modules and keeping track of them.
         * For each Instance of a Module a new Thread will be started
         */
        private Dictionary<ModuleDescription, object> RunnningModuleInstances = new Dictionary<ModuleDescription,object>();

        private static object Lock = new object();

        internal ModuleManager() { }


        private bool ModuleAlreadyRunning(ModuleDescription description)
        {
            return RunnningModuleInstances.ContainsKey(description);
        }

        private void AddModule(ModuleDescription description, IDjinModule instance)
        {
            
        }

        internal void AddAndRunModule(ModuleDescription description)
        {
            lock (ModuleManager.Lock) // is this lock necessary?
            {
                if (ModuleAlreadyRunning(description) == false)
                {
                    try
                    {

                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        internal void AddModule(ModuleDescription description)
        {
            try
            {
                var moduleInstance = (IDjinModule)new InstanceCreator(description).GetNewInstance();
                AddModule(description, moduleInstance);
            }
            catch (Exception e) { throw e; }
        }

    }
}
