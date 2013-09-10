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
        private string _name;

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
        

        public static void OpenBrowser(object sender, EventArgs e)
        {
            var clientInformations = _connectionTable.GetChecked();
            foreach (ConnectionTable.ClientInformation clientInformation in clientInformations)
            {
                var p = new string[,] { };
                var m = new Message(p, true, "open_browser", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void OpenCustomBrowser(object sender, EventArgs e)
        {
            var clientInformations = _connectionTable.GetChecked();
            foreach (ConnectionTable.ClientInformation clientInformation in clientInformations)
            {
                var p = new string[,] { };
                var m = new Message(p, true, "open_custom_browser", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }

        public static void CloseBrowser(object sender, EventArgs e)
        {
            var p = new string[,] { };
            foreach (var m in from ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked() select new Message(p, true, "close_browser", clientInformation.GetName()))
            {
                _messageQueue.SetMessage(m);
            }
        }

        public static void ChangeUrl(object sender, EventArgs e)
        {
            var changeUrlForm = new ServerChangeUrlForm() { TopLevel = true };

            TraceOps.Out("result"+changeUrlForm.ShowDialog().ToString());
                if (changeUrlForm.textBoxUrl.Text != null)
                {
                    var url = changeUrlForm.textBoxUrl.Text;
                    var p = new string[,] { { "url", url } };
                
                    foreach (var m in from ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked() select new Message(p, true, "change_url", clientInformation.GetName()))
                    {
                        _messageQueue.SetMessage(m);
                    }
                }
            
        }

        public static void OpenPreferences(object sender, EventArgs e)
        {
            
        }
    }
}
