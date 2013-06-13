using System;
using System.Windows.Forms;
using PaceCommon;

namespace PaceServer
{
    public partial class ClientsTable : Form
    {
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;

        public ClientsTable()
        {
            InitializeComponent();
        }

        private void ClientsTable_Load(object sender, EventArgs e)
        {
            _connectionTable = (ConnectionTable)System.Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
            _messageQueue = (MessageQueue)System.Activator.GetObject(typeof(MessageQueue), "http://localhost:9090/MessageQueue.rem");
            
            UpdateClientTable();
        }

        private void clientListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdateClientTable()
        {
            var ctall = _connectionTable.GetAll();
            clientListView.Items.Clear();
            foreach (ConnectionTable.ClientInformation ct in ctall)
            {
                clientListView.Items.Add(new ListViewItem(ct.GetName(), 0));
            }
            clientListView.Update();
        }
    }
}
