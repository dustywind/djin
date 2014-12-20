using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Shared
{
    public class ThreadPool
    {
        public void AddToQueu(IThreadPoolWorkPackage workPackage)
        {

        }
    }

    public class IThreadPoolWorkPackage
    {
        public void Work();
    }
}
