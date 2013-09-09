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

        private static List<Parameter> _emptyList;
        private static Panel _mainPanel;

        delegate void PluginCallback();

        public void Initializer(string ip, int port, ref MessageQueue messageQueue, ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
            _messageQueue = messageQueue;

            _emptyList = new List<Parameter> { new Parameter("parameter", "value")};
        }
        

        public static void OpenBrowser(object sender, EventArgs e)
        {

            var d = new PluginCallback(SetMessage);
            _mainForm.Invoke(d, new object[] { });

            /*
            foreach (var m in from ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked() select new Message(_emptyList, true, "open_browser", clientInformation.GetName()))
            {
                _messageQueue.SetMessage(m);
            }
             */
        }

        public static void CloseBrowser(object sender, EventArgs e)
        {
            foreach (var m in from ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked() select new Message(_emptyList, true, "close_browser", clientInformation.GetName()))
            {
                _messageQueue.SetMessage(m);
            }
        }

        public static void SetMessage()
        {
            _messageQueue.SetMessage(new Message());
        }

        public static void ChangeUrl(object sender, EventArgs e)
        {
            var changeUrlForm = new ServerChangeUrlForm() { TopLevel = true };
            if (changeUrlForm.ShowDialog() == DialogResult.OK)
            {
                if (changeUrlForm.textBoxUrl.Text != null)
                {
                    var url = changeUrlForm.textBoxUrl.Text;
                    var p = new List<Parameter> { new Parameter("url", url)};
                
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
