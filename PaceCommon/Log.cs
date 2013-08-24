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
            if (LogFile.InvokeRequired)
            {
                var d = new UpdateLogFileCallback(UpdateLogFile);
                Invoke(d, new object[] { });
            }
            else
            {
                LogFile.Text = TraceOps.GetLog();
            }
        }

        private void UpdateLogFileInternal()
        {
            var d = new UpdateLogFileCallback(UpdateLogFileCB);
            while (_running)
            {
                Thread.Sleep(1000);
                Invoke(d, new object[] { });
            }
        }

        private void UpdateLogFileCB()
        {
            LogFile.Text = TraceOps.GetLog();
        }



        private void Log_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(UpdateLogFileInternal);
        }
    }
}
