using System;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace ZtreeControl
{
    class ClientPlugin : IClientPlugin
    {
        private Panel _mainPanel;
        private MessageQueue _messageQueue;
        private string _name;
        private ClientControl _control;
        private ClientModel _model;
        private ClientView _view;
        private Thread _thread;
        private Form _mainForm;

        delegate void PluginCallback();

        public IView GetView()
        {
            throw new NotImplementedException();
        }

        public IControl GetControl()
        {
            throw new NotImplementedException();
        }

        public IModel GetModel()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            TraceOps.Out("ZtreeControl Client recived Message: " + message.GetCommand());
            if (message.GetCommand() == "start_zleaf")
            {
                var d = new PluginCallback(_control.StartZLeaf);
                _mainPanel.Invoke(d, new object[] { });
            }

            if (message.GetCommand() == "stop_zleaf")
            {
                var d = new PluginCallback(_control.StopZLeaf);
                _mainPanel.Invoke(d, new object[] { });
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