using System;
using System.Collections.Generic;
using System.IO;
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
        private NewConnection _newConnectionForm;

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

        private void SetUpClientConnectionConfig(string uri, int port)
        {
            try
            {
                Services.GetService(uri, port, typeof(ConnectionConfig));
                var url = "http://" + uri + ":" + port + "/ConnectionConfig.rem";
                ConnectionConfig externalConfig = (ConnectionConfig) Activator.GetObject(typeof (ConnectionConfig), url);

                externalConfig.SetServer(uri,port);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Cannot establish connection to network. Please try again.");
                TraceOps.Out(exception.ToString());
            }
        }


        private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _newConnectionForm = new NewConnection() { TopLevel = true };
            if (_newConnectionForm.ShowDialog() == DialogResult.OK)
            {
                var portval = 0;
                try
                {
                     portval = Convert.ToInt32(_newConnectionForm.textBoxPort.Text);
                }
                catch (FormatException formatException)
                {
                    TraceOps.Out("Input string is not a sequence of digits.");
                }
                catch (OverflowException overflowException)
                {
                    TraceOps.Out("The number cannot fit in an Int32.");
                }
                finally
                {
                    SetUpClientConnectionConfig(_newConnectionForm.textBoxUri.Text, portval);
                }
            }
        }

        private void loadConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                {
                    RestoreDirectory = true,
                    Filter = "All Connection Xml files (*.XML)|*.XML"
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

        private void LoadConnections(Connections connections)
        {
            //this.EmailsListBox.Items.Clear();

            // Add EmailAddresses collection to the ListBox on the Form...
            /*
            foreach (EmailAddress emailAddress in customer.EmailAddresses)
            {
                // Convert the enumerated object into its string representation.
                string Destination = Enum.GetName(typeof(EmailDestination), emailAddress.Destination);

                this.EmailsListBox.Items.Add(emailAddress.Address + " - " + Destination);
            }
            */
        }
    }
}
