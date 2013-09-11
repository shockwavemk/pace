using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using Message = PaceCommon.Message;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private const int Threshold = 100;
        private ContextMenu _trayMenu;
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private string _name;
        private TaskManager _taskManager;
        private NetworkClient _networkClient;
        private ConfigServer _configServer;
        private IClientPlugin[] _plugins;
        
        public MainClientForm()
        {
            _plugins = DllLoader.LoadClientPlugIns();
            if (_plugins.Length < 1)
            {
                _plugins = DllLoader.LoadClientPlugInsExternal("C:\\Plugins\\");
            }

            InitializeComponent();
            LoadWindowFunctions();
            LoadTray();
        }
        
        private void LoadTray()
        {
            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add(0, new MenuItem("Show", notifyIcon_DoubleClick));
            _trayMenu.MenuItems.Add(0, new MenuItem("Exit", MainClientForm_Exit));

            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.ContextMenu = _trayMenu;
            notifyIcon.MouseClick += notifyIcon_MouseUp;
        }

        private void LoadWindowFunctions()
        {
            Resize += MainClientForm_Resize;
            LocationChanged += MainClientForm_Location;
            FormClosing += MainClientForm_FormClosing;
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            TraceOps.LoadLog();
            Services.PrepareGetService();
            SystemOps.SetAutoStart();
            
            _configServer = new ConfigServer();
            _configServer.Changed += ConfigServerOnChanged;

            DllLoader.InitializeClientPlugIns(_plugins);
        }



        private void MainClientForm_Location(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                var location = Location;
                location.X += Width;
                TraceOps.SetLogPosition(location);
            }
            else
            {
                TraceOps.SetLogHidden();
            }
        }

        private void ConfigServerOnChanged(object sender, ChangedEventArgs eventArgs)
        {
            ConnectToServer(eventArgs.Ip, eventArgs.Port);
        }

        private void ConnectToServer(string ip, int port)
        {
            TraceOps.Out("Connect to: " + ip + " : " + port);
            Services.GetService(ip, port, typeof(ConnectionTable));
            Services.GetService(ip, port, typeof(MessageQueue));
            _connectionTable = ConnectionTable.GetRemote(ip, port);
            _messageQueue = MessageQueue.GetRemote(ip, port);
            _name = HashOps.GetFqdn();
            
            try
            {
                foreach (IClientPlugin plugin in _plugins)
                {
                    plugin.SetForm(this);
                    plugin.SetName(ref _name);
                    plugin.SetQueue(ref _messageQueue);
                    plugin.SetTable(ref _connectionTable);
                }
                
                _networkClient = new NetworkClient(ref _messageQueue, ref _connectionTable, _name);
                _taskManager = new TaskManager(ref _messageQueue, ref _name);
                _taskManager.Task += TaskManagerOnTask;
                if (_plugins != null) _taskManager.SetListener(_plugins);
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
                default:
                    TraceOps.Out(message.GetCommand());
                    break;
            }
        }
        
        private void MainClientForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
            }

        }

        protected void MainClientForm_Exit(Object sender, EventArgs e)
        {
            Close();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Show();
                TraceOps.SetLogVisible();
                WindowState = FormWindowState.Normal;
            }
        }

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var btext = "No network connection";
                try
                {
                    btext = NetworkOps.GetIpString(HashOps.GetFqdn());
                }
                catch (Exception)
                {

                    btext = "No network connection";
                }
                
                
                notifyIcon.ShowBalloonTip(1000, "Pace Configuration", btext, ToolTipIcon.None);
            }

            if (e.Button == MouseButtons.Right)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
        }

        private void MainClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var p = new string[,] { { "p1", "v1" }, { "p2", "v2" } };
            var m = new Message(p, true, "ping", "Server");
            
            if (_messageQueue != null)
            {
                _messageQueue.SetMessage(m);
            }
            else
            {
                TraceOps.Out("no connection to server message queue");
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
