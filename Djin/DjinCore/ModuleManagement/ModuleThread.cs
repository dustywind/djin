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
        private bool Loop;

        private Thread ThreadInstance;
        private int MillisecondTimeout = 500;

        private static SuicideCollector collector = SuicideCollector.Instance;

        internal ModuleThread(ModuleDescription description, IDjinModule moduleInstance)
        {
            this.ModuleInstance = moduleInstance;
            this.Description = description;
            this.Loop = Description.Loop;
            ThreadInstance = new Thread(new ThreadStart(this.ThreadProcessing));
        }

        internal void Start()
        {
            ThreadInstance.Start();
        }

        internal void Stop()
        {
            Stop(MillisecondTimeout);
        }

        internal void Stop(int milliSeconds)
        {
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

                // let the modulemanager know that the Thread want's to exit.
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

    class SuicideCollector : IDisposable
    {
        /**
         * The SuicideCollector will tell the ModuleManager
         * which DjinModule wants to be joined.
         *  
         * How it works:
         * SuicideCollector will create a Thread which checks the 
         * CorpseCollection-queue to gather DjinModules that terminated
         * For each DjinModule in CorpseCollection the ModuleManager will be
         * called to halt the given DjinModule
         */
        private static SuicideCollector _instance = null;
        internal static SuicideCollector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SuicideCollector();
                }
                return _instance;
            }
        }

        private BlockingCollection<ModuleDescription> CorpseCollection = new BlockingCollection<ModuleDescription>();
        private Thread Looper;

        private bool _Shutdown = false;
        private int LooperTimeout = 500;
        private int LoopInterval = 200;

        internal SuicideCollector()
        {
            Looper = new Thread(new ThreadStart(this.SuicideCollectLoop));
            Looper.Start();
        }

        internal void Stop()
        {
            Shutdown();
            ShutdownLooper();
        }

        internal void Suicide(ModuleDescription description)
        {
            if (WillShutdown())
            {
                CorpseCollection.Add(description);
            }
        }

        internal void SuicideCollectLoop()
        {
            while( WillShutdown() == false) {
                ModuleDescription killMe = null;
                if (CorpseCollection.TryTake(out killMe, LoopInterval))
                {
                    ModuleManager.Instance.HaltLoadedModule(killMe);
                }

            }
        }

        private void Shutdown()
        {
            this._Shutdown = true;
        }

        private bool WillShutdown()
        {
            return this._Shutdown;
        }

        private void ShutdownLooper()
        {
            if (Looper.Join(LooperTimeout) == false)
            {
                Looper.Abort();
            }

        }

        void IDisposable.Dispose()
        {
            // TODO
            // free BlockingQCollection
            CorpseCollection.Dispose();
            throw new NotImplementedException();
        }
    }
}
