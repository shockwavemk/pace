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

        public ClientsTable(IPlugin[] plugins)
        {
            _plugins = plugins;
            InitializeComponent();
            LoadPlugIns();
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

        private void LoadPlugIns()
        {
            // Take each plugin object and start initialization methods
            foreach (IPlugin plugin in _plugins)
            {
                if (plugin != null)
                {
                    // Load New Main Menu Entries
                    //var clientsTableMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateClientsTableMenu", new object[] { });
                    //clientsTableMenu.Click += ItemOnClick(plugin, "File");
                    //menuStrip1.Items.Add(clientsTableMenu);
                }
            }
        }

        private EventHandler ItemOnClick(Type plugin, string action)
        {
            return delegate(object sender, EventArgs args)
            {
                //var temp = (string)DllLoader.ControlInvoke(plugin, action, new object[] { });
                //var m = DllLoader.SoapToObject<Message>(temp);
                //_messageQueue.SetMessage(m);
            };
        }
    }
}
