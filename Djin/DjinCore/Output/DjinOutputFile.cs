using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Shared.Interfaces;

namespace Djin.Core.Output
{
    class DjinOutputFile : IDjinOutput 
    {
        private string filePath;
        private FileStream output;

        public DjinOutputFile() { }

        public IDjinOutput Init(string filePath)
        {
            this.filePath = filePath;
            this.output = File.OpenWrite(filePath);
            return this;
        }

        public void WriteLine(string message)
        {
            Write(String.Format("{0}\n", message));
        }

        public void Write(string message)
        {
            byte[] byteMessage = Encoding.ASCII.GetBytes(message);
            Write(byteMessage);
        }

        public void Write(byte[] message)
        {
            Write(message, 0, message.Length);
        }

        public void Write(byte[] message, int offset, int count)
        {
            output.Write(message, offset, count);
        }

        public void Flush()
        {
            output.Flush();
        }

        public void Close()
        {
            Flush();
            output.Close();
        }

        public override string ToString()
        {
            return "DjinOutputFile: " + filePath;
        }
    }
}
