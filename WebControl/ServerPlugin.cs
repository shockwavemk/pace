using System;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace WebControl
{
    class ServerPlugin : IServerPlugin
    {
        private Form _mainPanel;
        private MessageQueue _messageQueue;
        private string _name;
        private ServerControl _control;
        private ServerModel _model;
        private ServerView _view;

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

        public ServerPlugin()
        {
            _control = new ServerControl();
            _model = new ServerModel();
            _view = new ServerView();
        }

        public void Start(string name)
        {
            _name = name;
        }

        public void Test()
        {
            MessageBox.Show("Test Server Plugin");
        }

        public string Name()
        {
            return _name;
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void SetForm(Form mainPanel)
        {
            _mainPanel = mainPanel;
        }

        public void SetTask(Message message)
        {
            TraceOps.Out("WebControl Server recived Message: " + message.GetCommand());
        }
    }
}
