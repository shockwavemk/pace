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
            TraceOps.Out("WebControl Client recived Message: "+ message.GetCommand());
            if (message.GetCommand() == "open_browser")
            {
                var d = new PluginCallback(ShowBrowser);
                _mainForm.Invoke(d, new object[] { });
            }

            if (message.GetCommand() == "close_browser")
            {
                var d = new PluginCallback(ShowBrowser);
                _mainForm.Invoke(d, new object[] { });
            }
        }

        public void ShowBrowser()
        {
            _browserForm = new Browser() {TopLevel = true};
            _browserForm.Show();
        }

        public EventHandler SetEventHandler(object sender, EventArgs args)
        {
            return delegate {
                //MessageBox.Show("Test" + sender.ToString());
            };
        }
    }
}
