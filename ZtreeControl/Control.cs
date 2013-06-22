using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace ZtreeControl
{
    public class Control : PaceCommon.IControl
    {
        private static Process _processZTree;
        private static IntPtr hcalc;

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

        public static void FindDeleteFileAndStartAgain(string path, string process)
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
                                catch
                                {
                                
                                }
                            }
                        }
                    }

                    while (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        FindAndDeleteGsf(AppDomain.CurrentDomain.BaseDirectory);
                    }

                    var exeBytes = Properties.Resources.ztree;
                    _processZTree = new Process { StartInfo = { FileName = path } };

                    using (var exeFile = new FileStream(path, FileMode.CreateNew))
                    {
                        exeFile.Write(exeBytes, 0, exeBytes.Length);
                    }
                    
                    _processZTree.Start();
            }));
            thread.Start();
        }

        public void StartZLeaf(object sender, EventArgs eventArgs)
        {
            try
            {
                var exeBytes = Properties.Resources.zleaf;
                var exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");

                

                //FindAndKillProcess("zTree");

                /*
                if (System.IO.File.Exists(exeToRun))
                {
                    System.IO.File.Delete(exeToRun);
                }

                using (var exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                {
                    exeFile.Write(exeBytes, 0, exeBytes.Length);
                }
                Process.Start(exeToRun);
                */
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public static void StartZTree(object sender, EventArgs eventArgs)
        {
            try
            {
                var exeToRun = Path.Combine(Path.GetTempPath(), "ztree.exe");
                if (FindProcess("ztree"))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to restart z-Tree?", "z-Tree is still running", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                FindDeleteFileAndStartAgain(exeToRun, "ztree");
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
            TraceOps.Out("finished");
        }

        public string StartRemoteZLeaf()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "start_zleaf", "server");
            return ""; //DllLoader.ObjectToSoap(m);
        }

        public string StopRemoteZLeaf()
        {
            /*
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "stop_zleaf", "server");
            return DllLoader.ObjectToSoap(m);
             */
            return "";
        }

        public string Test()
        {
            return "Test";
        }

        public string File()
        {
            //var rlist = new List<string> { "" };
            //var m = new Message(rlist, true, "file", "Server");
            //return DllLoader.ObjectToSoap(m);
            return "File";
        }

        public string Edit()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "edit", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }

        public string View()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "view", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }

        public string Run()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "run", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }

        public string Help()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "help", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }
    }
}
