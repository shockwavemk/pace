using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PaceCommon;

namespace ZtreeControl
{
    class ProcessControl
    {
        private static List<Process> _process = new List<Process>();

        public static string[] GetGsfPaths(string path)
        {
            var filePaths = Directory.GetFiles(path, "*.gsf");
            return filePaths;
        }

        public static bool FindAndKillProcess(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.StartsWith(name))
                {
                    try
                    {
                        clsProcess.Kill();
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public static bool FindProcess(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.StartsWith(name))
                {
                    return true;
                }
            }
            return false;
        }

        public static void FindAndDeleteGsf(string path)
        {
            var gsfs = GetGsfPaths(path);
            foreach (var gsf in gsfs)
            {
                if (File.Exists(gsf))
                {
                    File.Delete(gsf);
                }
            }
        }

        public static void FindDeleteFileAndStartAgain(string path, string process, bool panel, bool deletegsf, byte[] ressource)
        {
            try
            {
                var thread = new Thread(new ThreadStart(() =>
                {
                    var found = true;
                    while (found)
                    {
                        Thread.Sleep(10);
                        found = false;
                        foreach (Process clsProcess in Process.GetProcesses())
                        {
                            if (clsProcess.ProcessName.StartsWith(process))
                            {
                                found = true;
                                try
                                {
                                    clsProcess.Kill();
                                }
                                catch (Exception e)
                                {
                                    TraceOps.Out(e.ToString());
                                }
                            }
                        }
                    }

                    while (File.Exists(path))
                    {
                        File.Delete(path);
                        FindAndDeleteGsf(AppDomain.CurrentDomain.BaseDirectory);
                    }

                    var exeBytes = ressource;
                    var p = new Process {StartInfo = {FileName = path}};
                    _process.Add(p);

                    using (var exeFile = new FileStream(path, FileMode.CreateNew))
                    {
                        exeFile.Write(exeBytes, 0, exeBytes.Length);
                    }

                    p.Start();

                }));
                thread.Start();
            }
            catch (Exception e)
            {
                TraceOps.Out(e.ToString());
            }

        }
    }
}
