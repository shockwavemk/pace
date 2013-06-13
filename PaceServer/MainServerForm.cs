using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace PaceServer
{
    public partial class MainServerForm : Form
    {
        private Thread _threadWorker;
        private bool _running = true;
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private NetworkServer _networkServer;
        private InOutQueue _serverInOut;

        public MainServerForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainServerForm_FormClosing);
        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            Services.PrepareSetService(9090);
            Services.SetService(typeof(ConnectionTable));
            Services.SetService(typeof(MessageQueue));

            _connectionTable = (ConnectionTable)System.Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
            _messageQueue = (MessageQueue)System.Activator.GetObject(typeof(MessageQueue), "http://localhost:9090/MessageQueue.rem");

            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "dynamic test", "client1");
            _messageQueue.SetMessage(m);

            try
            {
                //_networkServer = new NetworkServer(ref _messageQueue);
                _threadWorker = new Thread(Tasks);
                _threadWorker.Start();

                LoadClientsTable();
            }
            catch (Exception ex)
            {
                TraceOps.Out(ex.Message);
            }
        }

        private void Tasks()
        {
            try
            {
                while (_running)
                {
                    Thread.Sleep(1000);
                    /*
                    var m = _messageQueue.ServerToClientTryDequeue(_messageQueue.Server);

                    if (m != null)
                    {
                        MessageBox.Show("Command: " + m.GetCommand() + " Destination: " + m.GetDestination(), "Message from Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                     */
                    
                }
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        private void MainServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void clientsTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadClientsTable();
        }

        private void LoadClientsTable()
        {
            var clientsTableForm = new ClientsTable { TopLevel = false, FormBorderStyle = FormBorderStyle.Sizable, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, };
            mainPanel.Controls.Add(clientsTableForm);
            clientsTableForm.Visible = true;
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
