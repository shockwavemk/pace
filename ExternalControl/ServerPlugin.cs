using System;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace ExternalControl
{
    class ServerPlugin : IServerPlugin
    {
        private Panel _mainPanel;
        private MessageQueue _messageQueue;
        private string _name;
        private ServerControl _control;
        private ServerModel _model;
        private ServerView _view;
        private Form _mainForm;
        private ConnectionTable _connectionTable;

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

        public void Start()
        {
            
        }

        public void Test()
        {
            MessageBox.Show("Test Server Plugin");
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
            TraceOps.Out("ExternalControl Server recived Message: " + message.GetCommand());
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
