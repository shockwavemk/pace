using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Windows.Forms;

namespace PaceCommon
{
    public class DllLoader
    {
        public static string[] GetDllsPath(string path)
        {
            var filePaths = Directory.GetFiles(path, "*.dll");
            return filePaths;
        }

        public Object[] LoadDlls(string[] strings)
        {
            var list = new List<Object>();
            foreach (var s in strings)
            {
                list.AddRange(DllLoad(s)); 
            }
            return list.ToArray();
        }

        public Object[] DllLoad(string path)
        {
            var pluginLibrary = Assembly.LoadFrom(path);
            IEnumerable<Type> types = pluginLibrary.GetTypes();

            var list = new List<Object>();

            foreach (var type in types)
            {
                if (type.BaseType != null && type.Name == "Plugin")
                {
                    list.Add(type);
                }
            }
            return list.ToArray();
        }

        public static object TypeInvoke(Type type, string method, object[] p)
        {
            var classInst = Activator.CreateInstance(type);
            return Invoke(classInst, method, p);
        }

        public static object Invoke(object o, string s, object[] p)
        {
            var inst = o.GetType();
            
            var methodInfo = inst.GetMethod(s);
            return methodInfo.Invoke(o, p);
        }

        public static object ViewInvoke(Type type, string method, object[] p)
        {
            var classInst = Activator.CreateInstance(type);
            var o = Invoke(classInst, "GetView", p);
            return TypeInvoke(o.GetType(), method, p);
        }

        public static object ControlInvoke(Type type, string method, object[] p)
        {
            var classInst = Activator.CreateInstance(type);
            var o = Invoke(classInst, "GetControl", p);
            return TypeInvoke(o.GetType(), method, p);
        }

        public static object ModelInvoke(Type type, string method, object[] p)
        {
            var classInst = Activator.CreateInstance(type);
            var o = Invoke(classInst, "GetModel", p);
            return TypeInvoke(o.GetType(), method, p);
        }

        public static T SoapToObject<T>(string SOAP)
        {
            if (string.IsNullOrEmpty(SOAP))
            {
                throw new ArgumentException("SOAP can not be null/empty");
            }
            using (var Stream = new MemoryStream(Encoding.UTF8.GetBytes(SOAP)))
            {
                var formatter = new SoapFormatter();
                return (T)formatter.Deserialize(Stream);
            }
        }
   
        public static string ObjectToSoap(object Object)
        {
            if (Object == null)
            {
                throw new ArgumentException("Object can not be null");
            }
            using (var stream = new MemoryStream())
            {
                var serializer = new SoapFormatter();
                serializer.Serialize(stream, Object);
                stream.Flush();
                return Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Position);
            }
        }
    }
}
