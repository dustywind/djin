using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Djin.Shared.Interfaces;
using Djin.Core.Output;

namespace Djin.Core.ModuleManagement
{
    class InstanceCreator
    {
        private ModuleDescription Description;

        private Assembly assembly;
        private Module module;
        private Type type;

        private Type[][] djinConstructors = new Type[][]{// The types are ordered by how suitable they are. The order is somewhat important!
            new Type[] {typeof(IDjinOutput), typeof(Dictionary<string, string>)},   // IDjinModule(IDjinOutput, Dictionary<>);
            new Type[] {typeof(IDjinOutput)},                                       // IDjinModule(IDjinOutput);
            new Type[] {typeof(Dictionary<string, string>)},                        // IDjinModule(Dictionary<>);
            Type.EmptyTypes,                                                        // IDjinModule();
        };

        private string InstantiationError
        {
            get
            {
                return "Could not create an instance of: " + Description;
            }
        }
        private string MissingConstructorError
        {
            get
            {
                return "The module " + Description + " does not contain a Constructor suitable for Djin";
            }
        }

        internal InstanceCreator(ModuleDescription description) {
            this.Description = description;
            this.assembly = GetAssemblyFromDescription();
            this.module = assembly.GetModule(Description.FileName);
            this.type = module.GetType(Description.FullName);
        }

        internal object GetNewInstance() 
        {
            try
            {
                var instance = CreateMatchingInstance();
                return instance;
            }
            catch (Exception e)
            {
                // TODO propper loging
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private Assembly GetAssemblyFromDescription()
        {
            return Assembly.LoadFile(Description.AbsoluteAssemblyPath);
        }

        private IDjinModule CreateMatchingInstance()
        {
            IDjinModule instance = null;
            if (HasDjinSupportedConstructor())
            {
                instance = GetInstanceFromDjinSupportedConstructor() as IDjinModule;
            }
            else {
                throw new Exception(MissingConstructorError);
            }

            if( instance == null) {
                throw new Exception(InstantiationError);
            }
            return instance;
        }

        private bool HasDjinSupportedConstructor()
        {
            foreach (Type[] types in djinConstructors)
            {
                var constructor = this.type.GetConstructor(types);
                if (constructor != null && constructor.IsPublic) { return true; }
            }
            return false;
        }

        private object GetInstanceFromDjinSupportedConstructor()
        {
            foreach (var types in djinConstructors)
            {
                var constructor = this.type.GetConstructor(types);
                if (constructor != null)
                {
                    return constructor.Invoke(CreateMatchingParameters(types));
                }
            }
            throw new Exception(MissingConstructorError);
        }

        private object[] CreateMatchingParameters(Type[] paramTemplate)
        {
            object[] p = new object[paramTemplate.Length];
            for (int i = 0; i < paramTemplate.Length; i++)
            {
                if (paramTemplate[i] == typeof(IDjinOutput))
                {
                    p[i] = OutputFactory.CreateOutput(Description) as object;
                }
                else if (paramTemplate[i] == typeof(Dictionary<string, string>))
                {
                    p[i] = Description.Parameters as object;
                }
            }
            return p;
        }

        private object GetInstanceFromDefaultconstructor()
        {
            var constructor = type.GetConstructor(Type.EmptyTypes);
            return constructor.Invoke(null);
        }
    }
}
