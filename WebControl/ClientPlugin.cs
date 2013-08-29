﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace WebControl
{
    class ClientPlugin : IClientPlugin
    {
        private Form _mainPanel;
        private MessageQueue _messageQueue;
        private string _name;
        private ClientControl _control;
        private ClientModel _model;
        private ClientView _view;
        private bool _browserRunning = true;
        private Thread _thread;

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

        public void Start(string name)
        {
            _name = name;
            _control = new ClientControl();
            _model = new ClientModel();
            _view = new ClientView();

            ShowBrowser();
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

        public void SetForm(Form mainPanel)
        {
            _mainPanel = mainPanel;
        }

        public void SetTask(Message message)
        {
            TraceOps.Out("WebControl Client recived Message: "+ message.GetCommand());
            if (message.GetCommand() == "start webcontrol")
            {
                var task = new Thread(ShowBrowser);
                task.Start();
            }
        }

        public void ShowBrowser()
        {
            var browserForm = new Browser() {TopLevel = true};
            browserForm.Show();
        }

        public EventHandler SetEventHandler(object sender, EventArgs args)
        {
            return delegate {
                MessageBox.Show("Test" + sender.ToString());
            };
        }
    }
}
