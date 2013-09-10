using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;

namespace ExternalControl
{
    class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
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
    }
}
