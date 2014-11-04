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
        internal string ModuleName { get { return Path.GetFileName(AssemblyPath); } }

        internal string FullName { get { return this.ToString(); } }

        internal bool Loop = false;

        private string _AssemblyPath;
        internal string AssemblyPath {
            get {
                if (_AssemblyPath == null){
                    throw new Exception("ModuleDescription.Path was not set");
                }
                return _AssemblyPath;
            }
            set
            {
                if( File.Exists(value) == false) {
                    throw new FileNotFoundException("Could not find: " + value ?? "null");
                }
                _AssemblyPath = value;
            }
        }

        internal Dictionary<string, string> Parameters = new Dictionary<string, string>();

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
