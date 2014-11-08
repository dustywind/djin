using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Djin.Shared.Interfaces;

namespace Djin.Core.ModuleManagement
{
    class InstanceCreator
    {
        private ModuleDescription Description;

        private Assembly assembly;
        private Module module;
        private Type type;

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
                ConstructorInfo constructorInfo = type.GetConstructor(new Type[0]);
                var instance = constructorInfo.Invoke(null) as IDjinModule;

                if (instance == null)
                {
                    throw new Exception("Could not create a instanceof: " + Description);
                }
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
            else if (HasDefaultConstruktor())
            {
                instance = GetInstanceFromDefaultconstructor() as IDjinModule;
            }
            else {
                throw new Exception("The module " + Description + " does not contain a Constructor suitable for Djin");
            }

            if( instance == null) {
                throw new Exception("Could not create an instance of: " + Description);
            }
            return instance;
        }

        private bool HasDjinSupportedConstructor()
        {
            // try to get the constructor that accepts one argument (one dictionary)
            Type[] types = {typeof(Dictionary<string, object>)};
            var constructor = type.GetConstructor(types);
            if (constructor == null)
            {
                return false;
            }
            return constructor.IsPublic;
        }

        private bool HasDefaultConstruktor()
        {
            var constructor = type.GetConstructor(new Type[0]);
            if (constructor == null)
            {
                return false;
            }
            return constructor.IsPublic;
        }

        private object GetInstanceFromDjinSupportedConstructor()
        {
            var constructor = type.GetConstructor( new Type[]{typeof(Dictionary<string, object>)});
            return constructor.Invoke(new object[] {Description.Parameters});
        }

        private object GetInstanceFromDefaultconstructor()
        {
            var constructor = type.GetConstructor( new Type[0]);
            return constructor.Invoke(null);
        }

    }
}
