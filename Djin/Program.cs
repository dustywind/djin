using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
#if DEBUG
                if (args[0].CompareTo("--test") == 0)
                {
                    Djin.Playground.Playground.Play();
                }
#endif
            }
            return;
        }
    }
}

