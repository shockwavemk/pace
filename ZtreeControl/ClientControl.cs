using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using PaceCommon;

namespace ZtreeControl
{
    class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private static Process _processZLeaf;
        private Form _mainForm;
        private string _name;
        private Panel _mainPanel;

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

        public void StartZLeaf()
        {
            try
            {
                var exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");
                var ci = _connectionTable.Get("Server");
                var arguments = ClientModel.BuildCommandLineOptionsZLeaf(ci.GetIp(), _name, ClientModel.W, ClientModel.H, ClientModel.X, ClientModel.Y);

                ProcessControl.FindDeleteFileAndStartAgain(exeToRun, "zleaf", true, false, Properties.Resources.zleaf, arguments);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }
        
        public void StopZLeaf()
        {
            ProcessControl.FindAndKillProcess("zleaf");
        }
    }
}
