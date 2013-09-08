using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using ZtreeControl.Properties;

namespace ZtreeControl
{
    public class Control : IControl
    {
        private static Process _processZTree;
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;

        public void Initializer(string ip, int port)
        {
            _connectionTable = ConnectionTable.GetRemote(ip, port);
            _messageQueue = MessageQueue.GetRemote(ip, port);
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
                    DialogResult dialogResult = MessageBox.Show(Resources.Control_StartZTree_Are_you_sure_to_restart_z_Tree_, Resources.Control_StartZTree_z_Tree_is_still_running, MessageBoxButtons.YesNo);
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
            return "";
        }

        public string StopRemoteZLeaf()
        {
           return "";
        }

        public string Test()
        {
            return "Test";
        }

        public string File()
        {
            return "File";
        }

        public string Edit()
        {
            return "";
        }

        public string View()
        {
            return ""; 
        }

        public string Run()
        {
            return "";
        }

        public string Help()
        {
            return "";
        }

        // Actions performed by GUI

        public static void StartZLeafToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                TraceOps.Out(clientInformation.GetName());
                var p = new List<Parameter> { new Parameter("parameter", "value")};
                var m = new PaceCommon.Message(p, true, "start zleaf", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }
    }
}
