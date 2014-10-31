using System;
using System.Collections.Generic;
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
        private Thread ThreadInstance;
        private int MillisecondTimeout = 500;

        internal ModuleThread(IDjinModule moduleInstance)
        {
            this.ModuleInstance = moduleInstance;
            ThreadInstance = new Thread(new ThreadStart(this.ThreadProcessing));
        }

        internal void Start()
        {
            ThreadInstance.Start();
        }

        internal void Stop()
        {
            if (ThreadInstance.Join(MillisecondTimeout) == false)
            {
                ThreadInstance.Abort();
            }
        }

        internal void ThreadProcessing()
        {
            try
            {
                ModuleInstance.OnStart();
                ModuleInstance.Run();
                ModuleInstance.OnStop();
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

    }
}
