using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Djin.Shared.Interfaces;

namespace Djin.Core.ModuleManagement
{
    class ModuleThread
    {
        private IDjinModule ModuleInstance;
        private ModuleDescription Description;
        private volatile bool Loop;

        private Thread ThreadInstance;
        private const int MillisecondTimeout = 500;

        private static SuicideCollector collector = SuicideCollector.Instance;

        internal ModuleThread(ModuleDescription description, IDjinModule moduleInstance)
        {
            ModuleInstance = moduleInstance;
            Description = description;
            Loop = Description.Loop;
            ThreadInstance = new Thread(new ThreadStart(this.ThreadProcessing)){
                Name = description.ToString()
            };
        }

        internal void Start()
        {
            ThreadInstance.Start();
        }

        internal void Stop(int milliSeconds=MillisecondTimeout)
        {
            Loop = false;
            if (ThreadInstance.Join(milliSeconds) == false)
            {
                ThreadInstance.Abort();
            }
        }

        internal void ThreadProcessing()
        {
            try
            {
                do
                {
                    // call all the functions provided by the module
                    ModuleInstance.OnStart();
                    ModuleInstance.Run();
                    ModuleInstance.OnStop();
                } while (this.Loop);

                // let the modulemanager know that the Thread wants to exit.
                this.Suicide();
            }
            catch (ThreadAbortException) // Thread-execution was aborted
            {
                // TODO LOG error
            }
            catch (Exception e)
            {
                // TODO
                throw new NotImplementedException(e.Message);
            }
            
        }

        private void Suicide()
        {
            collector.Suicide(Description);
        }
    }

    
}
