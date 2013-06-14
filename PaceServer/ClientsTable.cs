using System;
using System.Windows.Forms;
using PaceCommon;

namespace PaceServer
{
    public partial class ClientsTable : Form
    {
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private ListViewGroup _local, _remote, _server, _group;

        public ClientsTable()
        {
            InitializeComponent();
        }

        private void ClientsTable_Load(object sender, EventArgs e)
        {
            _connectionTable = (ConnectionTable)System.Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
            _messageQueue = (MessageQueue)System.Activator.GetObject(typeof(MessageQueue), "http://localhost:9090/MessageQueue.rem");

            CreateGroups();
            UpdateClientTable();
        }

        private void clientListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreateGroups()
        {
            _local = new ListViewGroup("Local clients", HorizontalAlignment.Left);
            _remote = new ListViewGroup("Remote clients", HorizontalAlignment.Left);
            _server = new ListViewGroup("Server", HorizontalAlignment.Left);

            clientListView.Groups.AddRange(new[] { _local, _remote, _server });
        }

        private void UpdateClientTable()
        {
            var ctall = _connectionTable.GetAll();
            clientListView.Items.Clear();
            
            foreach (ConnectionTable.ClientInformation ct in ctall)
            {
                ListViewItem listViewItem;
                switch (ct.GetGroup())
                {
                    case "server":
                        listViewItem = new ListViewItem(ct.GetName(), 0) {Group = _server};
                        break;
                    case "remote":
                        listViewItem = new ListViewItem(ct.GetName(), 0) { Group = _remote };
                        break;
                    default:
                        listViewItem = new ListViewItem(ct.GetName(), 0) { Group = _local };
                        break;
                }
                clientListView.Items.Add(listViewItem);
            }
            clientListView.Update();
        }
    }
}
