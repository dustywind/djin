using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djin
{
    class Program
    {
        private static string Usage = "Type:\n./djin.exd (to run normally)\n./djin.exe --test (for debugging modules)";

        static void Main(string[] args)
        {
            /**
             * DEBUG-MODE
             * Compile as Debug to gain acces to Playground
             */
            if (args.Length > 0)
            {
#if DEBUG
                foreach (var arg in args)
                {
                    if (arg.CompareTo("--test") == 0)
                    {
                        new Djin.Playground.Playground().Play();
                    }
                    else if( arg.CompareTo("--core-test") == 0) {
                        new Djin.Core.Playground.CorePlayground().Play();
                    }
                }
#endif
            }
            else
            {
            }
            return;
        }
    }
}