using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace WebControl
{
    class ServerControl : IServerControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private static Form _mainForm;

        private static Panel _mainPanel;

        public void Initializer(string ip, int port)
        {
            _connectionTable = ConnectionTable.GetRemote(ip, port);
            _messageQueue = MessageQueue.GetRemote(ip, port);
        }
        

        public static void OpenBrowser(object sender, EventArgs e)
        {
            var clientInformations = _connectionTable.GetChecked();
            foreach (ConnectionTable.ClientInformation clientInformation in clientInformations)
            {
                var p = new string[,] { { "p1", "v1" }, { "p2", "v2" } };
                var m = new Message(p, true, "open_browser", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void CloseBrowser(object sender, EventArgs e)
        {
            var p = new string[,] { { "p1", "v1" }, { "p2", "v2" } };
            foreach (var m in from ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked() select new Message(p, true, "close_browser", clientInformation.GetName()))
            {
                _messageQueue.SetMessage(m);
            }
        }

        public static void ChangeUrl(object sender, EventArgs e)
        {
            var changeUrlForm = new ServerChangeUrlForm() { TopLevel = true };
            if (changeUrlForm.ShowDialog() == DialogResult.OK)
            {
                if (changeUrlForm.textBoxUrl.Text != null)
                {
                    var url = changeUrlForm.textBoxUrl.Text;
                    var p = new string[,] { { "p1", "v1" }, { "p2", "v2" } };
                
                    foreach (var m in from ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked() select new Message(p, true, "start_browser", clientInformation.GetName()))
                    {
                        _messageQueue.SetMessage(m);
                    }
                }
            }
        }

        public static void OpenPreferences(object sender, EventArgs e)
        {
            
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
