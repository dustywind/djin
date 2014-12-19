using Djin.Core.ConfigManagement;
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
#if DEBUG
            PrintWelcomeMessage();
#endif
            if (args.Length > 0 && args[0].CompareTo("--demonize") == 0)
            {
                Thread djin = SetupDaemon();
                djin.Start((object)args.Skip<string>(1).ToArray<string>());
            }
            else
            {
                Init(args);
            }
            Idle();
            return;
        }

        private static Thread SetupDaemon()
        {
            Thread daemon = new System.Threading.Thread(
                new ParameterizedThreadStart(Djin.Program.InitForDaemon)
            );
            daemon.IsBackground = true;
            return daemon;
        }

        private static void InitForDaemon(object argo)
        {
            Init(argo as string[]);
        }

        private static void Init(string[] args)
        {
            if (args != null)
            {
                ComputeArgs(args);
            }

            /**
             * __TODO__:
             * [X] call ConfigManagement
             * [X] call ModuleManagement
             * [ ] implement DjinOutput / DjinSocket
             * [ ] implement DjinLog
             * [ ] call CommunicationCenter // communicate with remote host
             *
             * __Further explanation__:
             * CommunicationCenter will block and wait for
             * commands from a non-local host
             * As soon as a command was received (in form of a DjinModule), CC will
             * open a new Thread and execute the DjinModule.
             * Afterwards CC will wait for further Commands
             */
            ConfigManager cm = new ConfigManager();
            cm.StartModulesFromConfig();

            return;
        }

        private static void ComputeArgs(string[] args)
        {
#if DEBUG
            PrintDebugMode();
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

        /* Pretty print all input in a box like:
         * +---------+
         * | Example |
         * +---------+
         */
        private static void PrettyPrint(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            int maxLineLength = 0;
            foreach (string s in args)
            {
                maxLineLength = s.Length > maxLineLength ? s.Length : maxLineLength;
            }
            var sb = new StringBuilder();
            // +---------+
            sb.Append('+');
            for (int i = 0; i < maxLineLength +2; i++) { sb.Append('-'); }
            sb.AppendLine("+");

            // | Example |
            foreach (var s in args)
            {
                sb.Append("| ");
                sb.Append(s);
                for (int i = 0; i < maxLineLength - s.Length; i++)
                {
                    sb.Append(' ');
                }
                sb.AppendLine(" |");
            }

            // +---------+
            sb.Append('+');
            for (int i = 0; i < maxLineLength +2; i++) { sb.Append('-'); }
            sb.Append('+');

            Console.WriteLine(sb.ToString());
        }

        private static void Idle()
        {
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

#if DEBUG
        private static void PrintWelcomeMessage()
        {
            PrettyPrint(new string[] {
                "Herzlich Willkommen zu Djin <3",
                "Bitte waehlen Sie ueber die Config die gewuenschten Module aus",
                "Zur Auswahl stehen:",
                "- DjinWebListener"
            });
        }
#endif

#if DEBUG
        private static void PrintDebugMode()
        {
            PrettyPrint(new string[] { "DEBUG-Mode" });
        }
#endif
    }
}