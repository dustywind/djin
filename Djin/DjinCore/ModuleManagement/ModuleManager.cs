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
    class ModuleManager : AbstractModuleManager
    {

        private Dictionary<ModuleDescription, IDjinModule> LoadedModuleInstances = new Dictionary<ModuleDescription,IDjinModule>();
        private Dictionary<ModuleDescription, ModuleThread> RunningModuleInstances = new Dictionary<ModuleDescription,ModuleThread>();

        private static object Lock = new object();

        private static ModuleManager _Instance = null;
        internal static ModuleManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ModuleManager();
                }
                return _Instance;
            }
        }

        private ModuleManager() { }

        override internal void AddModule(ModuleDescription description)
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

        override internal void RunLoadedModule(ModuleDescription description)
        {
            try
            {
                IDjinModule instance = GetLoadedModule(description);

                lock (ModuleManager.Lock)
                {
                    if (RunningModuleInstances.ContainsKey(description) == false)
                    {
                        var thread = new ModuleThread(description, instance);
                        RunningModuleInstances.Add(description, thread);
                        thread.Start();
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Requested instance of: " + description + " hasn't been loaded yet.");
            }
        }

        internal override void HaltLoadedModule(ModuleDescription description)
        {
            lock (ModuleManager.Lock)
            {
                if (RunningModuleInstances.ContainsKey(description) == false)
                {
                    // nothing to do
                    return;
                }
                ModuleThread mt = RunningModuleInstances[description];
            }
        }

        override internal void AddAndRunModule(ModuleDescription description)
        {
            AddModule(description);
            RunLoadedModule(description);
        }

        private ModuleThread GetRunningModule(ModuleDescription description)
        {
            return RunningModuleInstances[description];
        }

        private IDjinModule GetLoadedModule(ModuleDescription description)
        {
            return LoadedModuleInstances[description];
        }

        private bool ModuleAlreadyRunning(ModuleDescription description)
        {
            return RunningModuleInstances.ContainsKey(description);
        }

        private bool ModuleAlreadyLoaded(ModuleDescription description)
        {
            return LoadedModuleInstances.ContainsKey(description);
        }

        private void AddModule(ModuleDescription description, IDjinModule instance)
        {
            LoadedModuleInstances.Add(description, instance);
        }

    }
}
