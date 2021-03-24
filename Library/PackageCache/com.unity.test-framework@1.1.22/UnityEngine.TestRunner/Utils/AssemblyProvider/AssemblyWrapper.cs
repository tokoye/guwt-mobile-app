<<<<<<< HEAD:Library/PackageCache/com.unity.test-framework@1.1.22/UnityEngine.TestRunner/Utils/AssemblyProvider/AssemblyWrapper.cs
using System;
using System.Reflection;

namespace UnityEngine.TestTools.Utils
{
    internal class AssemblyWrapper : IAssemblyWrapper
    {
        public AssemblyWrapper(Assembly assembly)
        {
            Assembly = assembly;
            Name = assembly.GetName();
        }

        public Assembly Assembly { get; }

        public AssemblyName Name { get; }

        public virtual string Location
        {
            get
            {
                //Some platforms dont support this
                throw new NotImplementedException();
            }
        }

        public virtual AssemblyName[] GetReferencedAssemblies()
        {
            //Some platforms dont support this
            throw new NotImplementedException();
        }
    }
}
=======
using System;
using System.Reflection;

namespace UnityEngine.TestTools.Utils
{
    internal class AssemblyWrapper : IAssemblyWrapper
    {
        public AssemblyWrapper(Assembly assembly)
        {
            Assembly = assembly;
            Name = assembly.GetName();
        }

        public Assembly Assembly { get; }

        public AssemblyName Name { get; }

        public virtual string Location
        {
            get
            {
                //Some platforms dont support this
                throw new NotImplementedException();
            }
        }

        public virtual AssemblyName[] GetReferencedAssemblies()
        {
            //Some platforms dont support this
            throw new NotImplementedException();
        }
    }
}
>>>>>>> 311c70c6976bc5ea11bafc9e95d3e0e4ee456de5:Library/PackageCache/com.unity.test-framework@1.1.20/UnityEngine.TestRunner/Utils/AssemblyProvider/AssemblyWrapper.cs
