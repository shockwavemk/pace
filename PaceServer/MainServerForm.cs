using System;
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
        private IPlugin[] _plugins;
        private MainServerForm _msf;
        private ClientsTable _clientsTableForm;
        private int _port;

        public delegate void FormResizeEventHandler();

        public MainServerForm()
        {
            _msf = this;
            _port = 9090;
            //_plugins = plugins;
            InitializeComponent();
            LoadPlugIns();
            FormClosing += MainServerForm_FormClosing;
            Resize += OnResize;
            LocationChanged += OnLocation;
        }

        private void OnResize(object sender, EventArgs e)
        {
            if (_clientsTableForm.WindowState == FormWindowState.Maximized)
            {
                
            }
        }

        private void OnLocation(object sender, EventArgs e)
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
                _connectionTable.Set(_name, ci);

                LoadClientsTable();

                if (_plugins != null)
                {
                    foreach (IServerPlugin plugin in _plugins)
                    {
                        if (plugin != null)
                        {
                            plugin.SetQueue(ref _messageQueue);
                            plugin.SetForm(_msf);
                            plugin.Start(plugin.Name());
                        }
                    }

                    _taskManager.SetListener(_plugins);
                }
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
            _clientsTableForm = new ClientsTable(ref _plugins, _port) { TopLevel = false };
               
            mainPanel.Controls.Add(_clientsTableForm);
            _clientsTableForm.Visible = true;
        }

        private void LoadPlugIns()
        {
            /*
            // Take each plugin object and start initialization methods
            foreach (IServerPlugin plugin in _plugins)
            {
                if (plugin != null)
                {
                    // Load New Main Menu Entries
                    var mainMenu = (ToolStripMenuItem)plugin.GetView().CreateMainMenu();
                    // Load Additional entries to existing standard-menu
                    var fileMenu = (ToolStripMenuItem)plugin.GetView().CreateMainMenuEntryFile();
                    var editMenu = (ToolStripMenuItem)plugin.GetView().CreateMainMenuEntryEdit();
                    var runMenu = (ToolStripMenuItem)plugin.GetView().CreateMainMenuEntryRun();
                    var viewMenu = (ToolStripMenuItem)plugin.GetView().CreateMainMenuEntryView();
                    var helpMenu = (ToolStripMenuItem)plugin.GetView().CreateMainMenuEntryHelp();

                    menuStrip1.Items.Add(mainMenu);

                    // Assign new entries to existing menu
                    ToolStripMenuItem item;
                    if (fileMenu.DropDown != null)
                    {
                        //fileMenu.Click += ItemOnClick(plugin, "File");
                        item = (ToolStripMenuItem)menuStrip1.Items["File"];
                        item.DropDownItems.Add(fileMenu);
                    }
                    if (editMenu.DropDown != null)
                    {
                        //editMenu.Click += ItemOnClick(plugin, "Edit");
                        item = (ToolStripMenuItem)menuStrip1.Items["Edit"];
                        item.DropDownItems.Add(editMenu);
                    }
                    if (runMenu.DropDown != null)
                    {
                        //runMenu.Click += ItemOnClick(plugin, "Run");
                        item = (ToolStripMenuItem)menuStrip1.Items["Run"];
                        item.DropDownItems.Add(runMenu);
                    }
                    if (viewMenu.DropDown != null)
                    {
                        //viewMenu.Click += ItemOnClick(plugin, "View");
                        item = (ToolStripMenuItem)menuStrip1.Items["View"];
                        item.DropDownItems.Add(viewMenu);
                    }
                    if (helpMenu.DropDown != null)
                    {
                        //helpMenu.Click += ItemOnClick(plugin, "Help");
                        item = (ToolStripMenuItem)menuStrip1.Items["Help"];
                        item.DropDownItems.Add(helpMenu);
                    }
                }
            }
             * */
        }

        private EventHandler ItemOnClick(Type plugin, string action)
        {
            
            return delegate(object sender, EventArgs args)
            {
                /*
                var temp = (string)DllLoader.ControlInvoke(plugin, action, new object[] { });
                var m = DllLoader.SoapToObject<Message>(temp);
                _messageQueue.SetMessage(m);
                 */
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
    }

    
}
