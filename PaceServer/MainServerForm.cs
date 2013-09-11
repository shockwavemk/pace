using System;
using System.Collections.Generic;
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
        private string _name;
        private IServerPlugin[] _plugins;
        private MainServerForm _msf;
        private ClientsTableForm _clientsTableForm;
        private int _port;

        public delegate void FormResizeEventHandler();

        public MainServerForm()
        {
            _msf = this;
            _port = 9090;
            _plugins = DllLoader.LoadServerPlugIns();
            if (_plugins.Length < 1)
            {
                _plugins = DllLoader.LoadServerPlugInsExternal("C:\\Plugins\\");
            }

            InitializeComponent();
            LoadWindowFunctions();
        }

        private void LoadWindowFunctions()
        {
            Resize += MainServerForm_Resize;
            LocationChanged += MainServerForm_Location;
            FormClosing += MainServerForm_FormClosing;
        }

        private void MainServerForm_Resize(object sender, EventArgs e)
        {
            if (_clientsTableForm.WindowState == FormWindowState.Maximized)
            {
                
            }
        }

        private void MainServerForm_Location(object sender, EventArgs e)
        {
            var location = Location;
            location.X += Width;
            TraceOps.SetLogPosition(location);
        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            TraceOps.LoadLog();
            try
            {
                _name = "Server";

                Services.PrepareSetService(_port);
                Services.SetService(typeof(ConnectionTable));
                Services.SetService(typeof(MessageQueue));

                _connectionTable = ConnectionTable.GetRemote("localhost",_port);
                _messageQueue = MessageQueue.GetRemote("localhost", _port);

                _taskManager = new TaskManager(ref _messageQueue, ref _name);
                _taskManager.Task += TaskManagerOnTask;

                var ci = new ConnectionTable.ClientInformation(_name);
                ci.SetGroup("server");
                ci.SetIp(NetworkOps.GetIpString(HashOps.GetFqdn()));
                ci.SetPort(_port);
                _connectionTable.Set(_name, ci);

                LoadClientsTable();
                LoadPlugIns();
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
            _clientsTableForm = new ClientsTableForm(ref _plugins, _port);
            _clientsTableForm.TopLevel = false;
            _clientsTableForm.Visible = true;
            
            mainPanel.Controls.Add(_clientsTableForm);
            
        }

        private void LoadPlugIns()
        {
            if (_plugins != null)
            {
                foreach (IServerPlugin plugin in _plugins)
                {
                    if (plugin != null)
                    {
                        plugin.SetQueue(ref _messageQueue);
                        plugin.SetTable(ref _connectionTable);
                        plugin.SetForm(_msf);
                        plugin.SetPanel(mainPanel);
                        plugin.SetName(ref _name);
                        plugin.Start();

                        var view = (IServerView)plugin.GetView();
                        var mainMenu = view.CreateMainMenu();
                        var fileMenu = view.CreateMainMenuEntryFile();
                        var editMenu = view.CreateMainMenuEntryEdit();
                        var runMenu = view.CreateMainMenuEntryRun();
                        var viewMenu = view.CreateMainMenuEntryView();
                        var helpMenu = view.CreateMainMenuEntryHelp();

                        if (mainMenu != null) menuStrip1.Items.Add(mainMenu);

                        
                        // Assign new entries to existing menu
                        ToolStripMenuItem item;
                        if (fileMenu != null && fileMenu.DropDown != null)
                        {
                            fileMenu.Click += plugin.SetEventHandler(fileMenu, null);
                            item = (ToolStripMenuItem)menuStrip1.Items["File"];
                            item.DropDownItems.Add(fileMenu);
                        }

                        if (editMenu != null && editMenu.DropDown != null)
                        {
                            editMenu.Click += ItemOnClick(plugin, "Edit");
                            item = (ToolStripMenuItem)menuStrip1.Items["Edit"];
                            item.DropDownItems.Add(editMenu);
                        }
                        if (runMenu != null && runMenu.DropDown != null)
                        {
                            runMenu.Click += ItemOnClick(plugin, "Run");
                            item = (ToolStripMenuItem)menuStrip1.Items["Run"];
                            item.DropDownItems.Add(runMenu);
                        }
                        if (viewMenu != null && viewMenu.DropDown != null)
                        {
                            viewMenu.Click += ItemOnClick(plugin, "View");
                            item = (ToolStripMenuItem)menuStrip1.Items["View"];
                            item.DropDownItems.Add(viewMenu);
                        }
                        if (helpMenu != null && helpMenu.DropDown != null)
                        {
                            helpMenu.Click += ItemOnClick(plugin, "Help");
                            item = (ToolStripMenuItem)menuStrip1.Items["Help"];
                            item.DropDownItems.Add(helpMenu);
                        }
                    }
                }

                _taskManager.SetListener(_plugins);
            }
        }

        private EventHandler ItemOnClick(IPlugin plugin, string action)
        {
            return delegate(object sender, EventArgs args)
            {
                //MessageBox.Show("Test");
            };
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void File_Click(object sender, EventArgs e)
        {
            
        }

        private void logFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TraceOps.LoadLog();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var preferencesForm = new PreferencesForm() { TopLevel = true };
            if (preferencesForm.ShowDialog() == DialogResult.OK)
            {
                var startOnSystemStart = preferencesForm.checkBoxRunOnStartup.Checked;
                if (startOnSystemStart)
                {
                    TraceOps.Out("Set PaceServer Preferences.\r\n");
                }
            }
        }

        private void securityOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var securityOptionsForm = new SecurityOptionsForm() { TopLevel = true };
            if (securityOptionsForm.ShowDialog() == DialogResult.OK)
            {
                    TraceOps.Out("Set PaceServer Security.\r\n");
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }
    }

    
}
