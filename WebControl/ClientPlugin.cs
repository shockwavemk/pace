using System;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace WebControl
{
    class ClientPlugin : IClientPlugin
    {
        private Form _mainForm;
        private MessageQueue _messageQueue;
        private string _name;
        private ClientControl _control;
        private ClientModel _model;
        private ClientView _view;
        private bool _browserRunning = true;
        private Thread _thread;
        private Browser _browserForm;
        private Panel _mainPanel;
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
            TraceOps.Out("WebControl Client Plugin");
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
            try
            {
                var thread = new Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            TraceOps.Out("WebControl Client recived Message: "+ message.GetCommand());
                            if (message.GetCommand() == "open_browser")
                            {
                                var d = new PluginCallback(_control.StartBrowser);
                                _mainForm.Invoke(d, new object[] { });
                            }

                            if (message.GetCommand() == "open_custom_browser")
                            {
                                var d = new PluginCallback(_control.StartCustomBrowser);
                                _mainForm.Invoke(d, new object[] { });
                            }

                            if (message.GetCommand() == "close_browser")
                            {
                                var d = new PluginCallback(_control.StopBrowser);
                                _mainForm.Invoke(d, new object[] { });
                            }

                            if (message.GetCommand() == "change_url")
                            {
                                var p = message.GetParameter();
                                var value = "";
                                
                                if (p.GetLength(0) > 0 && p.GetLength(1) > 1)
                                {
                                    value = Message.GetAttribute(p, "url");
                                }

                                if (value != "") _control.SetUrl(value);

                                var d = new PluginCallback(_control.ChangeUrl);
                                _mainForm.Invoke(d, new object[] { });
                            }
                            }
                            catch (Exception e)
                            {
                                TraceOps.Out(e.ToString());
                            }
                    }));
                thread.Start();
            }
            catch (Exception e)
            {
                TraceOps.Out(e.ToString());
            }
        }

        public EventHandler SetEventHandler(object sender, EventArgs args)
        {
            return delegate {
                //MessageBox.Show("Test" + sender.ToString());
            };
        }
    }
}
