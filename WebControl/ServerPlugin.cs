using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace WebControl
{
    class ServerPlugin : IServerPlugin
    {
        private Form _mainPanel;

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
            throw new NotImplementedException();
        }

        public void Test()
        {
            MessageBox.Show("Test Server Plugin");
        }

        public string Name()
        {
            throw new NotImplementedException();
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            throw new NotImplementedException();
        }

        public void SetForm(Form mainPanel)
        {
            _mainPanel = mainPanel;
        }

        public void SetTask(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
