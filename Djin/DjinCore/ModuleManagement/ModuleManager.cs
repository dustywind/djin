using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Shared.Interfaces;

namespace Djin.Core.ModuleManagement
{
    /**
     * This class is responsible for both
     * starting modules and keeping track of them.
     * For each Instance of a Module a new Thread will be started
     */
    class ModuleManager
    {

        private Dictionary<ModuleDescription, IDjinModule> LoadedModuleInstances = new Dictionary<ModuleDescription,IDjinModule>();
        private Dictionary<ModuleDescription, ModuleThread> RuningModuleInstances = new Dictionary<ModuleDescription,ModuleThread>();

        private static object Lock = new object();

        internal ModuleManager() { }

        private ModuleThread GetRunningModule(ModuleDescription description)
        {
            return RuningModuleInstances[description];
        }

        private IDjinModule GetLoadedModule(ModuleDescription description)
        {
            return LoadedModuleInstances[description];
        }


        private bool ModuleAlreadyRunning(ModuleDescription description)
        {
            return RuningModuleInstances.ContainsKey(description);
        }

        private bool ModuleAlreadyLoaded(ModuleDescription description)
        {
            return LoadedModuleInstances.ContainsKey(description);
        }


        internal void AddAndRunModule(ModuleDescription description)
        {
            AddModule(description);
            RunLoadedModule(description);
        }

        internal void AddModule(ModuleDescription description)
        {
                try
                {
                    lock (ModuleManager.Lock)
                    {
                        if (LoadedModuleInstances.ContainsKey(description) == false)
                        {
                            var moduleInstance = (IDjinModule)new InstanceCreator(description).GetNewInstance();
                            AddModule(description, moduleInstance);
                        }
                    }
                }
                catch (Exception e) { throw e; }
        }

        private void AddModule(ModuleDescription description, IDjinModule instance)
        {
            LoadedModuleInstances.Add(description, instance);
        }

        internal void RunLoadedModule(ModuleDescription description)
        {
            try
            {
                IDjinModule instance = GetLoadedModule(description);

                lock (ModuleManager.Lock)
                {
                    if (RuningModuleInstances.ContainsKey(description) == false)
                    {
                        var thread = new ModuleThread(instance);
                        RuningModuleInstances.Add(description, thread);
                        thread.Start();
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Requested instance of: " + description + " hasn't been loaded yet.");
            }
        }

    }
}
