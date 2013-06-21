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

            foreach (var type in types)
            {
                if (type.BaseType != null && type.BaseType.Name == "IPlugin")
                {
                    var classInst = Activator.CreateInstance(type);
                    var methodInfo = type.GetMethod("Test2");
                    if (methodInfo != null)
                    {
                        try
                        {
                            var result = methodInfo.Invoke(classInst, new object[] { });
                            TraceOps.Out(result.ToString());
                        }
                        catch (Exception exception)
                        {
                            TraceOps.Out(exception.ToString());
                        }
                    }
                }
            }
        }
    }
}
