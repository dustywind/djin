using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Djin.Core.ModuleManagement
{
    class ModuleDescription
    {
        internal string Namespace;
        internal string ClassName;

        internal string Name { get { return this.ToString(); } }

        private string _Path;
        internal string Path {
            get {
                if (_Path == null){
                    throw new Exception("ModuleDescription.Path was not set");
                }
                return _Path;
            }
            set
            {
                if( File.Exists(value) == false) {
                    throw new FileNotFoundException("Could not find: " + value ?? "null");
                }
                _Path = value;
            }
        }

        internal List<object> Parameters = new List<object>();

        public override string ToString()
        {
            if (Namespace == null || ClassName == null)
            {
                throw new Exception("Instance of ModuleDescription is not ready for use");
            }
            return new StringBuilder()
                    .Append(Namespace)
                    .Append('.')
                    .Append(ClassName)
                    .ToString();
        }
    }
}
