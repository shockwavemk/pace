using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace PaceServer
{
    public partial class ClientsTable : Form
    {
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private ListViewGroup _local, _remote, _server, _group;
        private IPlugin[] _plugins;
        public ListView.SelectedListViewItemCollection SelectedListViewItemCollection;

        public ClientsTable(IPlugin[] plugins)
        {
            _plugins = plugins;
            InitializeComponent();
            LoadPlugIns();
        }

        private void ClientsTable_Load(object sender, EventArgs e)
        {
            _connectionTable = ConnectionTable.GetRemote();
            _messageQueue = MessageQueue.GetRemote();

            CreateGroups();
            UpdateClientTable();
        }

        private void clientListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem si in clientListView.SelectedItems)
            {
                _connectionTable.SetSelected(si.Text);
            }
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

        private void LoadPlugIns()
        {
            // Take each plugin object and start initialization methods
            foreach (IPlugin plugin in _plugins)
            {
                if (plugin != null)
                {
                    // Load New Main Menu Entries
                    var clientsTableMenu = (ToolStripMenuItem)plugin.GetView().CreateClientsTableMenu();
                    menuStrip1.Items.Add(clientsTableMenu);
                }
            }
        }
    }
}
