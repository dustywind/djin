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
            if (args.Length == 1)
            {
#if DEBUG
                if (args[0].CompareTo("--test") == 0)
                {
                    Djin.Playground.Playground.Play();
                }
                else
                {
                    Console.WriteLine(Usage);
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

