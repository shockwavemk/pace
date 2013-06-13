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
        private bool _running = true;
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private TaskManager _taskManager;

        public MainServerForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainServerForm_FormClosing);
        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                Services.PrepareSetService(9090);
                Services.SetService(typeof(ConnectionTable));
                Services.SetService(typeof(MessageQueue));

                _connectionTable = (ConnectionTable)System.Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
                
                _messageQueue = (MessageQueue)System.Activator.GetObject(typeof(MessageQueue), "http://localhost:9090/MessageQueue.rem");

                _taskManager = new TaskManager(ref _messageQueue);
                _taskManager.Task += TaskManagerOnTask;
                
                //Open ClientsTable by Default
                LoadClientsTable();
            }
            catch (Exception ex)
            {
                TraceOps.Out(ex.Message);
            }
        }

        private void TaskManagerOnTask(Message message)
        {
            switch (message.GetCommand())
            {
                case "":
                    Console.WriteLine("Case 1");
                    break;
                case "a":
                    Console.WriteLine("Case 2");
                    break;
                default:
                    TraceOps.Out(message.GetCommand());
                    break;
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
