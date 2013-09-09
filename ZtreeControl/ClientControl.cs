using System;
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
    class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private static Process _processZLeaf;

        public void Initializer(string ip, int port, ref MessageQueue messageQueue, ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
            _messageQueue = messageQueue;
        }

        public void StartZLeaf()
        {
            try
            {
                var exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");
                FindDeleteFileAndStartAgain(exeToRun, "zleaf", true);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }


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
                if (System.IO.File.Exists(gsf))
                {
                    System.IO.File.Delete(gsf);
                }
            }
        }

        public static void FindDeleteFileAndStartAgain(string path, string process, bool panel)
        {
            try
            {

                var thread = new Thread(new ThreadStart(() =>
                {
                    try
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

                        while (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                            FindAndDeleteGsf(AppDomain.CurrentDomain.BaseDirectory);
                        }

                        var exeBytes = Properties.Resources.zleaf;
                        _processZLeaf = new Process { StartInfo = { FileName = path } };

                        using (var exeFile = new FileStream(path, FileMode.CreateNew))
                        {
                            exeFile.Write(exeBytes, 0, exeBytes.Length);
                        }

                        _processZLeaf.Start();
                    }
                    catch (Exception e)
                    {
                        TraceOps.Out(e.ToString());
                    }
                }));
                thread.Start();
            }
            catch (Exception e)
            {
                TraceOps.Out("1:" + e.ToString());
            }

        }
    }
}
