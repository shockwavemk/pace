using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace ZtreeControl
{
    class ServerPlugin : IServerPlugin
    {
        private ServerControl _control;
        private ServerView _view;
        private ServerModel _model;
        private TaskManager _taskManager;
        private string _name;
        private MessageQueue _messageQueue;
        private Panel _mainPanel;
        private Form _mainForm;
        private ConnectionTable _connectionTable;

        public ServerPlugin()
        {
            _control = new ServerControl();
            _view = new ServerView();
            _model = new ServerModel();
        }

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
            TraceOps.Out("ZtreeControl Server recived Message: " + message.GetCommand());
        }

        public EventHandler SetEventHandler(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            
        }

        public void Start()
        {
            _taskManager = new TaskManager(ref _messageQueue, ref _name);
            _taskManager.Task += TaskManagerOnTask;
        }
        
        public string Name()
        {
            return _name;
        }

        private void TaskManagerOnTask(Message message)
        {
            switch (message.GetCommand())
            {
                case "":
                    TraceOps.Out("Case 1");
                    break;
                default:
                    TraceOps.Out(message.GetCommand());
                    break;
            }
        }
    }
}
