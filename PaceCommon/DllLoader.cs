using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    class DllLoader
    {
        public static string[] GetDllsPath(string path)
        {
            var filePaths = Directory.GetFiles(path, "*.dll");
            return filePaths;
        }

        public void DllLoad(string path)
        {
            var pluginLibrary = Assembly.LoadFrom(path);
            IEnumerable<Type> types = pluginLibrary.GetTypes();

            foreach (Type type in types)
            {
                /*var typeIShellViewInterface = type.GetInterface(_NamespaceIShellView, false);
                if (typeIShellViewInterface != null)
                {
                    //here
                }
                 */
            }
        }
    }
}
