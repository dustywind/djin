using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Djin.Core.ModuleManagement;
using global::Djin.Shared.Interfaces;

namespace Djin.Core.Output
{
    class OutputFactory
    {
        public static IDjinOutput CreateOutput(ModuleDescription description)
        {
            return new DjinOutputFile().Init(description.ToString() + "_" + DateTime.Now.Ticks);
        }

        public static IDjinOutput CreateOutput(string connectionString)
        {
            throw new NotImplementedException();
        }

        public static IDjinOutput CreateOutputSocket(string connectionString)
        {
            throw new NotImplementedException();
        }

        public static IDjinOutput CreateOutputFile(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
