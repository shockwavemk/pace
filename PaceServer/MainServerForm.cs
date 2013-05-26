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
            this.FormClosing += new FormClosingEventHandler(MainServerForm_FormClosing);
        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                /*
                 * Starting Network-Server on local host
                 */
                var tempServer = new NetworkServer();
                tempServer.SetIpAddress("127.0.0.1");
                tempServer.SetPort(1987);
                NetworkServer.ClientChange += tempServer_ClientChange;
                tempServer.Start();

                /*
                 * Starting SubForm ClientsTable for start
                 */
                LoadClientsTable();
            }
            catch (Exception ex)
            {
                TraceOps.Out(ex.Message);
            }
        }

        private void MainServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void tempServer_ClientChange(object sender, ClientChangeEventArgs e)
        {
            Invoke(new UpdateStatusCallback(UpdateStatus), new object[] { e.EventMessage });
        }

        private void UpdateStatus(string strMessage)
        {
            TraceOps.Out(strMessage);
        }

        private void clientsTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadClientsTable();
        }

        private void LoadClientsTable()
        {
            var clientsTableForm = new ClientsTable { TopLevel = false, FormBorderStyle = FormBorderStyle.SizableToolWindow };
            mainPanel.Controls.Add(clientsTableForm);
            clientsTableForm.Visible = true;
        }
    }
}
