using System;
using System.Windows.Forms;

namespace PaceServer
{
    public partial class MainServerForm : Form
    {
        private delegate void UpdateStatusCallback(string strMessage);
        public MainServerForm()
        {
            InitializeComponent();
        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                var tempServer = new NetworkServer();
                tempServer.SetIpAddress("127.0.0.1");
                tempServer.SetPort(10111);
                NetworkServer.ClientChange += tempServer_ClientChange;
                tempServer.Start();
            }
            catch (Exception ex)
            {
                TraceOps.Out(ex.Message);
            }
        }

        private void tempServer_ClientChange(object sender, ClientChangeEventArgs e)
        {
            Invoke(new UpdateStatusCallback(UpdateStatus), new object[] { e.EventMessage });
        }
        private void UpdateStatus(string strMessage)
        {
            TraceOps.Out(strMessage);
        }
    }
}
