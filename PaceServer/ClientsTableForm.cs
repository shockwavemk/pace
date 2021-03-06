﻿using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;

namespace PaceServer
{
    public partial class ClientsTableForm : Form
    {
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private ListViewGroup _local, _remote, _server, _group;
        private IServerPlugin[] _plugins;
        private bool _running;
        private ConnectionForm _newConnectionForm;
        private string _ip = NetworkOps.GetIpString(HashOps.GetFqdn());
        private int _port = 9090;

        delegate void UpdateClientTableCallback();


        public ClientsTableForm(ref IServerPlugin[] plugins, int port)
        {
            _plugins = plugins;
            _port = port;
            _ip = HashOps.GetFqdn();
            InitializeComponent();
            handle.MouseDown += handle_MouseDown;
            handle.MouseMove += handle_MouseMove;
            handle.MouseUp += handle_MouseUp;

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
            _connectionTable = ConnectionTable.GetRemote(_ip, _port);
            _messageQueue = MessageQueue.GetRemote(_ip, _port);
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

        private void clientListView_CheckedIndexChanged(object sender, EventArgs e)
        {
            var strings = new string[clientListView.CheckedItems.Count];

            for (var index = 0; index < clientListView.CheckedItems.Count; index++)
            {
                strings[index] = clientListView.CheckedItems[index].Text;
            }
            _connectionTable.SetChecked(strings);
        }

        bool mouseClicked = false;

        private void handle_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseClicked = true;
        }

        private void handle_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseClicked = false;
        }

        private void handle_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mouseClicked)
            {
                this.Height = handle.Top + 70 + e.Y;
                this.Width = handle.Left + 20 + e.X;
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
            foreach (IServerPlugin plugin in _plugins)
            {
                if (plugin != null)
                {
                    // Load New Main Menu Entries
                    var clientsTableMenuView = (IServerView)plugin.GetView();
                    var clientsTableMenu = clientsTableMenuView.CreateClientsTableMenu();
                    if (clientsTableMenu != null) menuStrip1.Items.Add(clientsTableMenu);
                }
            }
        }

        private void LoadConnections(Connections connections)
        {
            foreach (Connection connection in connections.ConnectionList)
            {
                if (connection.name != "Server" && connection.ip != "unknown" && connection.port != 0)
                {
                    IPHostEntry he = Dns.GetHostEntry(connection.ip);
                    var dns = he.AddressList[1].ToString();
                    var ip = NetworkOps.GetIpString(dns);
                    NetworkOps.SetUpClientConnectionConfig(ip, connection.port, _ip, _port);
                }
            }
        }


        private ArrayList CreateConnections()
        {
            var ctall = _connectionTable.GetAll();
            var connections = new ArrayList();

            foreach (ConnectionTable.ClientInformation ct in ctall)
            {
                if (ct.GetName() != "Server")
                {
                    var connection = new Connection {ip = ct.GetIp(), name = ct.GetName(), port = ct.GetPort()};
                    connections.Add(connection);
                }
            }
            return connections;
        }
        
        private bool ConnectionsIsValid()
        {
            return true; //TODO
        }

        //NetworkOps.SetUpClientConnectionConfig("131.234.150.135", 9091, _ip, _port);

        private void clientListView_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var strings = new string[clientListView.SelectedItems.Count];

            for (var index = 0; index < clientListView.SelectedItems.Count; index++)
            {
                strings[index] = clientListView.SelectedItems[index].Text;
            }
            _connectionTable.SetSelection(strings);
        }

        private void newConnectionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _newConnectionForm = new ConnectionForm() { TopLevel = true };
            if (_newConnectionForm.ShowDialog() == DialogResult.OK)
            {
                var ip = NetworkOps.GetIpString(_newConnectionForm.textBoxUri.Text);
                var port = NetworkOps.GetPort(_newConnectionForm.textBoxPort.Text);

                var connected = NetworkOps.SetUpClientConnectionConfig(ip, port, _ip, _port);
                if (!connected)
                {
                    MessageBox.Show("Cannot establish connection to network. Please try again.\r\n");
                }
            }
        }

        private void saveConnectionsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.ConnectionsIsValid())
            {
                var connections = new Connections { ConnectionList = CreateConnections() };

                try
                {
                    var openFileDialog = new SaveFileDialog
                    {
                        RestoreDirectory = true,
                        Filter = "All Connection Xml files (*.PXML)|*.PXML"
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileName = openFileDialog.FileName;

                        ObjectToXml<Connections>.Save(connections, fileName);
                        MessageBox.Show("Customer saved to XML file '" + fileName + "'!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("File can not be saved. Please try again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to save connection object!" + Environment.NewLine + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadConnectionsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = "All Connection Xml files (*.PXML)|*.PXML"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;

                if (File.Exists(fileName) == true)
                {
                    Connections connections = ObjectToXml<Connections>.Load(fileName);

                    if (connections == null)
                    {
                        MessageBox.Show("Unable to load customer object from file '" + fileName + "'!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.LoadConnections(connections);
                        MessageBox.Show("Customer loaded from file '" + fileName + "'!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("File does not exist. Please try again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
