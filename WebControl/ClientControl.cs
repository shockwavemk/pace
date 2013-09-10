using System;
using System.Windows.Forms;
using PaceCommon;

namespace WebControl
{
    public class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private Browser _browserForm;
        private CustomBrowser _customBrowserForm;
        
        private Panel _mainPanel;
        private Form _mainForm;
        private string _name;

        //Browser Customizing
        private string _url;

        public void SetName(ref string name)
        {
            _name = name;
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void SetTable(ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
        }

        public void SetForm(Form mainForm)
        {
            _mainForm = mainForm;
        }

        public void SetPanel(Panel mainPanel)
        {
            _mainPanel = mainPanel;
        }


        //Set Browser Custom Options
        public void SetUrl(string url)
        {
            _url = url;
        }


        public void StartBrowser()
        {
            try
            {
                _browserForm = new Browser() { TopLevel = true };
                _browserForm.Show();
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void StopBrowser()
        {
            try
            {
                if (_customBrowserForm != null)
                {
                    _customBrowserForm.Visible = false;
                }

                if (_browserForm != null)
                {
                    _browserForm.Visible = false;
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void StartCustomBrowser()
        {
            try
            {
                _customBrowserForm = new CustomBrowser() { TopLevel = true };
                _customBrowserForm.Show();
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void ChangeUrl()
        {
            if (_browserForm != null)
            {
                _browserForm.addNewExTab(_url);
            }

            if (_customBrowserForm != null)
            {
                _customBrowserForm.BrowseTo(_url);
            }
        }
    }
}
