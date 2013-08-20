using System;
using System.Collections.Generic;
using System.Threading;
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
        private bool _running;
        delegate void UpdateClientTableCallback();


        public ClientsTable(IPlugin[] plugins)
        {
            _plugins = plugins;
            InitializeComponent();
            LoadPlugIns();
        }

        private void OnResize(object sender, EventArgs eventArgs)
        {
            clientListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            clientListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ClientsTable_Load(object sender, EventArgs e)
        {
            _running = true;
            _connectionTable = ConnectionTable.GetRemote();
            _messageQueue = MessageQueue.GetRemote();
            clientListView.ItemChecked += clientListView_CheckedIndexChanged;
            
            CreateGroups();

            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (_running)
                {
                    UpdateClientTable();
                    Thread.Sleep(2000);
                }
            }));
            thread.Start();
        }

        private void clientListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var strings = new string[clientListView.SelectedItems.Count];

            for (var index = 0; index < clientListView.SelectedItems.Count; index++)
            {
                strings[index] = clientListView.SelectedItems[index].Text;
            }
            _connectionTable.SetSelection(strings);
        }

        private void clientListView_CheckedIndexChanged(object sender, EventArgs e)
        {
            var strings = new string[clientListView.CheckedItems.Count];

            for (var index = 0; index < clientListView.CheckedItems.Count; index++)
            {
                strings[index] = clientListView.CheckedItems[index].Text;
            }
            _connectionTable.SetChecked(strings);
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
            if (clientListView.InvokeRequired)
            {
                var d = new UpdateClientTableCallback(UpdateClientTable);
                this.Invoke(d, new object[] {});
            }
            else
            {
                clientListView.BeginUpdate();
                var ctall = _connectionTable.GetAll();

                clientListView.Items.Clear();

                foreach (ConnectionTable.ClientInformation ct in ctall)
                {
                    ListViewItem listViewItem;
                    switch (ct.GetGroup())
                    {
                        case "server":
                            listViewItem =
                                new ListViewItem(
                                    new string[]
                                        {
                                            ct.GetName(), ct.GetPerformance(), ct.GetMessagesInQueue().ToString(),
                                            ct.GetApplicationNames()
                                        }, 0) { Group = _server, Checked = ct.GetChecked(), Selected = ct.GetSelected() };
                            break;
                        case "remote":
                            listViewItem =
                                new ListViewItem(
                                    new string[]
                                        {
                                            ct.GetName(), ct.GetPerformance(), ct.GetMessagesInQueue().ToString(),
                                            ct.GetApplicationNames()
                                        }, 0) { Group = _remote, Checked = ct.GetChecked(), Selected = ct.GetSelected() };
                            break;
                        default:
                            listViewItem =
                                new ListViewItem(
                                    new string[]
                                        {
                                            ct.GetName(), ct.GetPerformance(), ct.GetMessagesInQueue().ToString(),
                                            ct.GetApplicationNames()
                                        }, 0) { Group = _local, Checked = ct.GetChecked(), Selected = ct.GetSelected()};
                            break;
                    }
                    
                    clientListView.Items.Add(listViewItem);
                }
                clientListView.EndUpdate();
            }
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

        private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
