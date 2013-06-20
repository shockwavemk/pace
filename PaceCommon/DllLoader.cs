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

        public void LoadDlls(string[] strings)
        {
            foreach (var s in strings)
            {

                TraceOps.Out(s);
                DllLoad(s); 
            }
        }

        public void DllLoad(string path)
        {
            var pluginLibrary = Assembly.LoadFrom(path);
            IEnumerable<Type> types = pluginLibrary.GetTypes();

            foreach (Type type in types)
            {
                if (type.Name != "Resources")
                {
                    var classInst = Activator.CreateInstance(type);
                    MethodInfo methodInfo = type.GetMethod("Test2");
                    if (methodInfo != null)
                    {
                        var result = methodInfo.Invoke(classInst, new object[] {});
                        TraceOps.Out(result.ToString());
                    }
                }
            }
        }
    }
}
