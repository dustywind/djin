using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Shared.Interfaces;

namespace Djin.Core.Output
{
    class DjinOutputSocket : IDjinOutput
    {
        public IDjinOutput Init(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] message)
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] message, int offset, int length)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string message)
        {
            throw new NotImplementedException();
        }

        public void Write(string message)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
