using System.Reflection;
using System.Runtime.Loader;

namespace ReportPOC.Services
{
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public CustomAssemblyLoadContext() : base(isCollectible: true)
        {
        }

        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            // Load the unmanaged library from the specified path
            return LoadUnmanagedDllFromPath(absolutePath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            // If LoadUnmanagedLibrary is called, this method won't be used.
            return IntPtr.Zero;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            // No managed assembly loading required
            return null;
        }
    }
}
