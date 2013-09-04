﻿using System;
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
        private ClientsTable _clientsTableForm;
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
            _clientsTableForm = new ClientsTable(ref _plugins, _port) { TopLevel = false };
               
            mainPanel.Controls.Add(_clientsTableForm);
            _clientsTableForm.Visible = true;
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
                        plugin.SetForm(_msf);
                        plugin.Start(_name);


                        var plugInControl = (IServerControl) plugin.GetControl();
                        plugInControl.Initializer("localhost", _port);
                        
                        
                        var mainMenuView = (IServerView)plugin.GetView();
                        var mainMenu = mainMenuView.CreateMainMenu();


                        var fileMenuView = (IServerView)plugin.GetView();
                        var fileMenu = fileMenuView.CreateMainMenuEntryFile();

                        var editMenuView = (IServerView)plugin.GetView();
                        var editMenu = editMenuView.CreateMainMenuEntryEdit();

                        var runMenuView = (IServerView)plugin.GetView();
                        var runMenu = runMenuView.CreateMainMenuEntryEdit();

                        var viewMenuView = (IServerView)plugin.GetView();
                        var viewMenu = viewMenuView.CreateMainMenuEntryEdit();

                        var helpMenuView = (IServerView)plugin.GetView();
                        var helpMenu = helpMenuView.CreateMainMenuEntryEdit();

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
                MessageBox.Show("Test");
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
