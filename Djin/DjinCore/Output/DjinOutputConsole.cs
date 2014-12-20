using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Shared.Interfaces;

namespace Djin.DjinCore.Output
{
    class DjinOutputConsole : IDjinOutput
    {
        public IDjinOutput Init(string connectionString)
        {
            return this;
        }

        public void Write(byte[] message)
        {
            Console.Write(message);
        }

        public void Write(byte[] message, int offset, int length)
        {
            Console.Write(message.Skip<byte>(offset).Take<byte>(length).ToArray<byte>());
        }

        public void Write(string message)
        {
            Console.Write(string);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Flush()
        {
            // do nothing
        }

        public void Close()
        {
            // also do nothing
        }
    }
}
