using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

        public IServerPlugin[] LoadServerDlls(string[] strings)
        {
            var list = new List<IServerPlugin>();
            foreach (var s in strings)
            {
                list.AddRange(ServerDllLoad(s)); 
            }
            return list.ToArray();
        }

        public IServerPlugin[] ServerDllLoad(string path)
        {
            var pluginLibrary = Assembly.LoadFrom(path);
            IEnumerable<Type> types = pluginLibrary.GetTypes();

            var list = new List<IServerPlugin>();

            foreach (var type in types)
            {
                if (type.BaseType != null && type.Name == "ServerPlugin")
                {
                    
                    var plugin = (IServerPlugin) Activator.CreateInstance(type);
                    list.Add(plugin);
                }
            }
            return list.ToArray();
        }

        public static IServerPlugin[] LoadServerPlugIns()
        {
            var dllLoader = new DllLoader();
            var plugins = dllLoader.LoadServerDlls(GetDllsPath(AppDomain.CurrentDomain.BaseDirectory));
            return plugins;
        }


        public IClientPlugin[] LoadClientDlls(string[] strings)
        {
            var list = new List<IClientPlugin>();
            foreach (var s in strings)
            {
                list.AddRange(ClientDllLoad(s));
            }
            return list.ToArray();
        }

        public IClientPlugin[] ClientDllLoad(string path)
        {
            var pluginLibrary = Assembly.LoadFrom(path);
            IEnumerable<Type> types = pluginLibrary.GetTypes();

            var list = new List<IClientPlugin>();

            foreach (var type in types)
            {
                if (type.BaseType != null && type.Name == "ClientPlugin")
                {
                    var plugin = (IClientPlugin)Activator.CreateInstance(type);
                    list.Add(plugin);
                }
            }
            return list.ToArray();
        }

        public static IClientPlugin[] LoadClientPlugIns()
        {
            var dllLoader = new DllLoader();
            var plugins = dllLoader.LoadClientDlls(GetDllsPath(AppDomain.CurrentDomain.BaseDirectory));
            return plugins;
        }

        public static void InitializeClientPlugIns(IEnumerable<IPlugin> plugins, ref MessageQueue messageQueue, string name, Form mainForm)
        {
            if (plugins != null)
            {
                foreach (IClientPlugin plugin in plugins)
                {
                    if (plugin != null)
                    {
                        plugin.SetQueue(ref messageQueue);
                        plugin.SetForm(mainForm);
                        plugin.Start(name);
                    }
                }
            }
        }
    }
}
