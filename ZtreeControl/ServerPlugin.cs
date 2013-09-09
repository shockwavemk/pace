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
        public ServerControl Control;
        public ServerView View;
        public ServerModel Model;
        private TaskManager _taskManager;
        private string _name;
        private MessageQueue _messageQueue;
        private Panel _mainPanel;
        private Form _mainForm;

        public ServerPlugin()
        {
            Control = new ServerControl();
            View = new ServerView();
            Model = new ServerModel();
        }

        public IView GetView()
        {
            return View;
        }

        public IControl GetControl()
        {
            return Control;
        }

        public IModel GetModel()
        {
            return Model;
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void SetForm(Form mainForm)
        {
            _mainForm = mainForm;
            Control.SetForm(mainForm);
        }

        public void SetPanel(Panel mainPanel)
        {
            _mainPanel = mainPanel;
            Control.SetPanel(mainPanel);
        }

        public void SetTask(Message message)
        {
            throw new NotImplementedException();
        }

        public EventHandler SetEventHandler(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            
        }

        public void Start(string name)
        {
            _name = name;
            _taskManager = new TaskManager(ref _messageQueue, ref _name);
            _taskManager.Task += TaskManagerOnTask;
        }
        
        public string Name()
        {
            return "ztree";
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
