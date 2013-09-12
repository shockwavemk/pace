using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace ExternalControl
{
    public class ServerControl : IServerControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private string _name;
        private static Form _mainForm;
        private static Panel _mainPanel;

        delegate void PluginFormCallback(Form form);

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

        public static void RemoteStartProcess(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                var p = new string[,] { { "fileName", "" }, { "arguments", ""} };
                var m = new Message(p, true, "start_process", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void RemoteStopProcess(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                var p = new string[,] { { "fileName", "" }, { "arguments", "" } };
                var m = new Message(p, true, "stop_process", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void ShowClientDetails(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                TraceOps.Out("ClientInformation for " + clientInformation.GetName());
                try
                {
                    ConnectionTable.ClientInformation information = clientInformation;
                    var thread = new Thread(new ThreadStart(() =>
                    {
                        var clientDetailForm = new ServerClientDetailForm(information) { TopLevel = false };
                        var d = new PluginFormCallback(SetWindowToPanel);
                        _mainForm.Invoke(d, new object[] { clientDetailForm });
                    }));
                    thread.Start();
                }
                catch (Exception et)
                {
                    TraceOps.Out(et.ToString());
                }
                
            }
        }

        public static void SetWindowToPanel(Form form)
        {
            Thread.Sleep(500);
            _mainPanel.Controls.Add(form);
            form.Show();
        }

        public static void RemoteRestartWindows(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                var p = new string[,] { { "fileName", "cmd" }, { "arguments", ClientModel.BuildCommandLineOptionsRestart() } };
                var m = new Message(p, true, "start_process", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void RemoteLogOffWindows(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                var p = new string[,] { { "fileName", "cmd" }, { "arguments", ClientModel.BuildCommandLineOptionsLogOff() } };
                var m = new Message(p, true, "start_process", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void RemoteShutDownWindows(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                var p = new string[,] { { "fileName", "cmd" }, { "arguments", ClientModel.BuildCommandLineOptionsShutDown() } };
                var m = new Message(p, true, "start_process", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }
    }
}
