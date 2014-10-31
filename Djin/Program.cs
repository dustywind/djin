using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Djin
{
    class Program
    {
        /**
         * Usage:
         * djin.exe --demonize // create a daemon
         * // the '--demonize'-Argument must be passed as the first one
         *
         * djin.exe --test --test-core // Execute the Code in the test-environment Playground
         * // djin _MUST_ be compiled as DEBUG-Binary to use the test-environment
         * // the Arguments '--test' and '--test-core' can be in any order or be ommitted
         * // Nevertheless if '--demonize' is given, '--demonize' MUST be the very first argument
         */
        static void Main(string[] args)
        {
            /**
             * DEBUG-MODE
             * Compile as Debug to gain acces to Playground
             */
            if (args.Length > 0)
            {
                /**
                 * // TODO daemonize
                 * Thread.IsBackground = true
                 */
                if (args[0].CompareTo("--demonize") == 0)
                {
                    Thread djin = SetupDaemon();
                    djin.Start((object)args.Skip<string>(1).ToArray<string>());
                }
                else
                {
                    Init((object) args);
                }

            }
            return;
        }

        private static Thread SetupDaemon()
        {
            Thread daemon = new System.Threading.Thread(
                new ParameterizedThreadStart(Djin.Program.Init)
            );
            daemon.IsBackground = true;
            return daemon;
        }

        public static void Init(object argo)
        {
            string[] args = argo as string[];

            ComputeArgs(args);

            /**
             * TODO
             * - call ConfigManagement
             * - call ModuleManagement
             * - call CommunicationCenter // communicate with remote host
             */

            return;
        }

        private static void ComputeArgs(string[] args)
        {
#if DEBUG
            foreach (var arg in args)
            {
                if (arg.CompareTo("--test") == 0)
                {
                    new Djin.Playground.Playground().Play();
                }
                else if (arg.CompareTo("--core-test") == 0)
                {
                    new Djin.Core.Playground.CorePlayground().Play();
                }
            }
#endif

        }
    }
}