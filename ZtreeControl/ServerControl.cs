using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private string _name;

        delegate void PluginCallback();

        public void SetName(ref string name)
        {
            _name = name;
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void SetTable(ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
        }

        public void SetForm(Form mainForm)
        {
            _mainForm = mainForm;
        }

        public void SetPanel(Panel mainPanel)
        {
            _mainPanel = mainPanel;
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

                        while (File.Exists(path))
                        {
                            File.Delete(path);
                            ProcessControl.FindAndDeleteGsf(AppDomain.CurrentDomain.BaseDirectory);
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
                TraceOps.Out(e.ToString());
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
            try
            {
                var exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");
                var ci = _connectionTable.Get("Server");
                var arguments = ClientModel.BuildCommandLineOptionsZLeaf(ci.GetIp(), _name, 200, 200, 200, 200);

                ProcessControl.FindDeleteFileAndStartAgain(exeToRun, "zleaf", true, true, Resources.zleaf, arguments);
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
                if (ProcessControl.FindProcess("ztree"))
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

        // Actions performed by GUI

        public static void RemoteStartZLeaf(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                TraceOps.Out(clientInformation.GetName());
                var p = new string[,] { { }, { } };
                var m = new PaceCommon.Message(p, true, "start_zleaf", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void OpenPreferences(object sender, EventArgs e)
        {
            var serverPreferencesForm = new ServerPreferencesForm() { TopLevel = true };

            TraceOps.Out("result " + serverPreferencesForm.ShowDialog().ToString());
            if (serverPreferencesForm.XPos.Text != null && serverPreferencesForm.YPos.Text != null && serverPreferencesForm.Width.Text != null && serverPreferencesForm.Height.Text != null)
            {
                var xvalue = serverPreferencesForm.XPos.Text;
                var yvalue = serverPreferencesForm.YPos.Text;
                var wvalue = serverPreferencesForm.Width.Text;
                var hvalue = serverPreferencesForm.Height.Text;

                try
                {
                    ServerModel.X = Convert.ToInt32(xvalue);
                    ServerModel.Y = Convert.ToInt32(yvalue);
                    ServerModel.W = Convert.ToInt32(wvalue);
                    ServerModel.H = Convert.ToInt32(hvalue);
                }
                catch (Exception ie)
                {
                    TraceOps.Out(ie.ToString());
                }
                
                var p = new string[,] { { "X", xvalue }, { "Y", yvalue }, { "W", wvalue }, { "H", hvalue } };

                foreach (var m in from ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked() select new Message(p, true, "set_preferences", clientInformation.GetName()))
                {
                    _messageQueue.SetMessage(m);
                }
            }
        }

        public static void RemoteStopLeaf(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                TraceOps.Out(clientInformation.GetName());
                var p = new string[,] { { }, { } };
                var m = new Message(p, true, "stop_zleaf", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }
    }
}
