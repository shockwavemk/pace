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
        public void Start(string name)
        {
            _name = name;
            _control = new ClientControl();
            _model = new ClientModel();
            _view = new ClientView();
        }

        public void Test()
        {
            TraceOps.Out("WebControl Client Plugin");
        }

        public string Name()
        {
            return _name;
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void SetForm(Form mainForm)
        {
            _mainForm = mainForm;
        }

        public void SetPanel(Panel mainPanel)
        {
            _mainPanel = mainPanel;
        }

        public void SetTask(Message message)
        {
            TraceOps.Out("WebControl Client recived Message: " + message.GetCommand());
            if (message.GetCommand() == "start_webcontrol")
            {
                var d = new PluginCallback(StartExtern);
                _mainPanel.Invoke(d, new object[] { });
            }
        }

        public void StartExtern()
        {
            
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
