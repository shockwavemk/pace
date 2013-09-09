using System;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace WebControl
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

        public void Start(string name)
        {
            _name = name;
        }

        public void Test()
        {
            //MessageBox.Show("Test Server Plugin");
        }

        public string Name()
        {
            return _name;
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;

        }

        public void SetTask(Message message)
        {
            TraceOps.Out("WebControl Server recived Message: " + message.GetCommand());
        }

        public EventHandler SetEventHandler(object sender, EventArgs args)
        {
            return delegate 
            {
                
                
                
                //MessageBox.Show("Test"+ sender.ToString());
            };
        }
    }
}
