using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using PaceCommon;
using PaceServer;
using Message = PaceCommon.Message;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private const int Threshold = 100;
        private ContextMenu tray_menu;
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private string _name;
        private TaskManager _taskManager;
        private NetworkClient _networkClient;
        private ConfigServer _configServer;

        delegate void UpdateLogFileCallback();

        public MainClientForm()
        {
            InitializeComponent();
            tray_menu = new ContextMenu();
            tray_menu.MenuItems.Add(0, new MenuItem("Show", notifyIcon_DoubleClick));
            tray_menu.MenuItems.Add(0, new MenuItem("Exit", MainClientForm_Exit));
            LocationChanged += OnLocation;

            FormClosing += MainClientForm_FormClosing;
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            TraceOps.LoadLog();
            Services.PrepareGetService();

            Resize += MainClientForm_Resize;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.ContextMenu = tray_menu;
            notifyIcon.MouseClick += notifyIcon_MouseUp;
            
            _configServer = new ConfigServer();
            _configServer.Changed += ConfigServerOnChanged;
        }

        private void OnLocation(object sender, EventArgs e)
        {
            var location = Location;
            location.X += Width;
            TraceOps.SetLogPosition(location);
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
                _networkClient = new NetworkClient(ref _messageQueue, ref _connectionTable, _name);
                _taskManager = new TaskManager(ref _messageQueue, ref _name);
                _taskManager.Task += TaskManagerOnTask;
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

        private void button1_Click(object sender, EventArgs e)
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "ping", "Server");
            _messageQueue.SetMessage(m);
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
                WindowState = FormWindowState.Normal;
            }
        }

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
        }

        private void MainClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
