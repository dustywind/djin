using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Djin.Core.ModuleManagement
{
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
            while (WillShutdown() == false)
            {
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
