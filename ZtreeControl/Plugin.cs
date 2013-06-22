﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;
using PaceServer;
using Message = PaceCommon.Message;

namespace ZtreeControl
{
    class Plugin : IPlugin
    {
        public Control Control;
        public View View;
        public Model Model;
        private bool _running = true;
        private int Threshold = 1000;
        private TaskManager _taskManager;
        private string _name;
        private MessageQueue _messageQueue;

        public Plugin()
        {
            Control = new Control();
            View = new View();
            Model = new Model();
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

        public string Test()
        {
            return "Plugin Test";
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
