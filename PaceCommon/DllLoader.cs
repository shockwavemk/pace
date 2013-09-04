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
            TraceOps.Out("Looking for Plugins in: "+path);
            var filePaths = Directory.GetFiles(path, "*.dll");
            return filePaths;
        }

        public IServerPlugin[] LoadServerDlls(string[] strings)
        {
            var list = new List<IServerPlugin>();
            foreach (var s in strings)
            {
                TraceOps.Out("Lade Plugin: "+s);
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
                    TraceOps.Out("Lade ServerPlugin");
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

        public static IServerPlugin[] LoadServerPlugInsExternal(string path)
        {
            var dllLoader = new DllLoader();
            var plugins = dllLoader.LoadServerDlls(GetDllsPath(path));
            return plugins;
        }


        public IClientPlugin[] LoadClientDlls(string[] strings)
        {
            var list = new List<IClientPlugin>();
            foreach (var s in strings)
            {
                TraceOps.Out("Lade Plugin: " + s);
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
                    TraceOps.Out("Lade ClientPlugin");
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

        public static IClientPlugin[] LoadClientPlugInsExternal(string path)
        {
            var dllLoader = new DllLoader();
            var plugins = dllLoader.LoadClientDlls(GetDllsPath(path));
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
