using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using ZtreeControl.Properties;
using Message = PaceCommon.Message;

namespace ZtreeControl
{
    public class ServerControl : IServerControl
    {
        [DllImport("USER32.DLL")] static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("USER32.dll")] private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private static Process _processZTree;
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private static Form _mainForm;
        private static Panel _mainPanel;
        private List<Parameter> _emptyList;

        delegate void PluginCallback();

        public void Initializer(string ip, int port)
        {
            _emptyList = new List<Parameter> { new Parameter("parameter", "value") };
            _connectionTable = ConnectionTable.GetRemote("localhost", 9090);
            _messageQueue = MessageQueue.GetRemote("localhost", 9090);
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
                                    catch(Exception e)
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

                        var exeBytes = Properties.Resources.ztree;
                        _processZTree = new Process {StartInfo = {FileName = path}};

                        using (var exeFile = new FileStream(path, FileMode.CreateNew))
                        {
                            exeFile.Write(exeBytes, 0, exeBytes.Length);
                        }

                        _processZTree.Start();

                            if (panel)
                            {
                                var d = new PluginCallback(SetWindowToPanel);
                                _mainForm.Invoke(d, new object[] { });
                                
                            }
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
                TraceOps.Out("1:"+e.ToString());
            }

        }

        public static void SetWindowToPanel()
        {
            Thread.Sleep(500);
            SetParent(_processZTree.MainWindowHandle, _mainPanel.Handle);
                                MoveWindow(_processZTree.MainWindowHandle, 0, 0, _mainPanel.Width - 90,
                                           _mainPanel.Height, true);
        }

        public void StartZLeaf()
        {
            //TODO
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
                try
                {
                    FindDeleteFileAndStartAgain(exeToRun, "ztree", true);
                }
                catch (Exception innerException)
                {
                    TraceOps.Out(innerException.ToString());
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
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
                var m = new PaceCommon.Message(p, true, "start_zleaf", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public void SetForm(Form mainForm)
        {
            _mainForm = mainForm;
        }

        public void SetPanel(Panel mainPanel)
        {
            _mainPanel = mainPanel;
        }
    }
}
