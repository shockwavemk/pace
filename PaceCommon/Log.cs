using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaceCommon
{
    public partial class Log : Form
    {
        private bool _running = true;

        delegate void UpdateLogFileCallback();
        
        public Log()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void UpdateLogFile()
        {
            if (LogFile != null && LogFile.InvokeRequired)
            {
                var d = new UpdateLogFileCallback(UpdateLogFile);
                Invoke(d, new object[] { });
            }
            else
            {
                if (LogFile != null) LogFile.Text = TraceOps.GetLog();
            }
        }

        private void UpdateLogFileInternal()
        {
            var d = new UpdateLogFileCallback(UpdateLogFileCB);
            while (_running)
            {
                Thread.Sleep(1000);
                if (_running && this.Visible && LogFile != null)
                {
                    Invoke(d, new object[] {});
                }
            }
        }

        private void UpdateLogFileCB()
        {
            if (LogFile != null) LogFile.Text = TraceOps.GetLog();
        }


        private void Log_Load(object sender, EventArgs e)
        {
            FormClosing += Log_FormClosing;
            _running = true;
            Task.Factory.StartNew(UpdateLogFileInternal);
        }

        private void Log_FormClosing(object sender, EventArgs e)
        {
            _running = false;
        }
    }
}
