using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PaceCommon
{
    public class DllLoader
    {
        public static string[] GetDllsPath(string path)
        {
            var filePaths = Directory.GetFiles(path, "*.dll");
            return filePaths;
        }

        public IPlugin[] LoadDlls(string[] strings)
        {
            var list = new List<IPlugin>();
            foreach (var s in strings)
            {
                list.AddRange(DllLoad(s)); 
            }
            return list.ToArray();
        }

        public IPlugin[] DllLoad(string path)
        {
            var pluginLibrary = Assembly.LoadFrom(path);
            IEnumerable<Type> types = pluginLibrary.GetTypes();

            var list = new List<IPlugin>();

            foreach (var type in types)
            {
                if (type.BaseType != null && type.Name == "Plugin")
                {
                    
                    var plugin = (IPlugin) Activator.CreateInstance(type);
                    list.Add(plugin);
                }
            }
            return list.ToArray();
        }
    }
}
