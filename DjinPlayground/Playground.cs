using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Modules.WebListener;
using Djin.Shared.Interfaces;

namespace Djin.Playground
{
    public class Playground
    {
        public void Play()
        {
            Console.WriteLine("Playground");

            IDjinModule module = new Djin.Modules.WebListener.WebListener();

            module.Run();
            
        }
    }
}
