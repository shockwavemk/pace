using System;
using System.Collections.Concurrent;
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
        private ConcurrentQueue<PaceCommon.Message> _inQueue;
        private ConcurrentQueue<PaceCommon.Message> _outQueue;

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
                _inQueue = new ConcurrentQueue<PaceCommon.Message>();
                _outQueue = new ConcurrentQueue<PaceCommon.Message>();

                var tempServer = new NetworkServer();
                tempServer.SetIpAddress("127.0.0.1");
                tempServer.SetPort(1987);
                tempServer.SetInQueue(ref _inQueue);
                tempServer.SetOutQueue(ref _outQueue);
                NetworkServer.ClientChange += tempServer_ClientChange;
                tempServer.Start();

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
                TraceOps.Out("Server: Start to work on Messages");
                while (_running)
                {
                    Thread.Sleep(500);
                    Message m;
                    var message = _outQueue.TryDequeue(out m) ? m : null;

                    if (message != null)
                    {
                        MessageBox.Show("Command: " + message.GetCommand() + " Destination: " +  message.GetDestination(), "Message from Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
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

        private void tempServer_ClientChange(object sender, ClientChangeEventArgs e)
        {
           Invoke(new UpdateStatusCallback(UpdateStatus), new object[] { e.EventMessage });
        }

        private void UpdateStatus(string strMessage)
        {
            // changes in system
            TraceOps.Out(strMessage);
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
