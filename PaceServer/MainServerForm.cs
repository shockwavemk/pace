using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
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

        public MainServerForm(object[] plugins)
        {
            InitializeComponent();
            LoadPlugIns(plugins);
            this.FormClosing += new FormClosingEventHandler(MainServerForm_FormClosing);
        }

        private void MainServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                Services.PrepareSetService(9090);
                Services.SetService(typeof(ConnectionTable));
                Services.SetService(typeof(MessageQueue));

                _name = "Server";

                _connectionTable = (ConnectionTable)System.Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
                
                _messageQueue = (MessageQueue)System.Activator.GetObject(typeof(MessageQueue), "http://localhost:9090/MessageQueue.rem");

                _taskManager = new TaskManager(ref _messageQueue, ref _name);
                _taskManager.Task += TaskManagerOnTask;

                //Set Own Information
                var ci = new ConnectionTable.ClientInformation(_name);
                ci.SetGroup("server");
                _connectionTable.Set(_name, ci);
                
                //Open ClientsTable by Default
                LoadClientsTable();
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
            var clientsTableForm = new ClientsTable { TopLevel = false, FormBorderStyle = FormBorderStyle.Sizable, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, };
            mainPanel.Controls.Add(clientsTableForm);
            clientsTableForm.Visible = true;
        }

        private void LoadPlugIns(Object[] plugins)
        {
            // Take each plugin object and start initialization methods
            foreach (Type plugin in plugins)
            {
                if(plugin != null)
                {
                    // Load New Main Menu Entries
                    var mainMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateMainMenu", new object[] { });
                    // Load New Clients-Table Menu
                    var clientsTableMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateClientsTableMenu", new object[] { });
                    // Load Additional entries to existing standard-menu
                    var fileMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateMainMenuEntryFile", new object[] { });
                    var editMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateMainMenuEntryEdit", new object[] { });
                    var runMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateMainMenuEntryRun", new object[] { });
                    var viewMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateMainMenuEntryView", new object[] { });
                    var helpMenu = (ToolStripMenuItem)DllLoader.ViewInvoke(plugin, "CreateMainMenuEntryHelp", new object[] { });

                    menuStrip1.Items.Add(mainMenu);

                    // Assign Listener
                    fileMenu.Click += ItemOnClick(plugin, "File");
                    editMenu.Click += ItemOnClick(plugin, "Edit");
                    runMenu.Click += ItemOnClick(plugin, "Run");
                    viewMenu.Click += ItemOnClick(plugin, "View");
                    helpMenu.Click += ItemOnClick(plugin, "Help");

                    // Assign new entries to existing menu
                    ToolStripMenuItem item;

                    item = (ToolStripMenuItem) menuStrip1.Items["File"];
                    item.DropDownItems.Add(fileMenu);

                    item = (ToolStripMenuItem)menuStrip1.Items["Edit"];
                    item.DropDownItems.Add(editMenu);

                    item = (ToolStripMenuItem)menuStrip1.Items["Run"];
                    item.DropDownItems.Add(runMenu);

                    item = (ToolStripMenuItem)menuStrip1.Items["View"];
                    item.DropDownItems.Add(viewMenu);

                    item = (ToolStripMenuItem)menuStrip1.Items["Help"];
                    item.DropDownItems.Add(helpMenu);
                }
            }
        }

        private EventHandler ItemOnClick(Type plugin, string action)
        {
            return delegate(object sender, EventArgs args)
            {
                var temp = (string)DllLoader.ControlInvoke(plugin, action, new object[] { });
                var m = DllLoader.SoapToObject<Message>(temp);
                _messageQueue.SetMessage(m);
            };
        }
        

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
