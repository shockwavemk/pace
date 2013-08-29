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
        private ContextMenu tray_menu;
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private string _name;
        private TaskManager _taskManager;
        private NetworkClient _networkClient;
        private ConfigServer _configServer;
        private IClientPlugin[] _plugins;

        
        
        public MainClientForm()
        {
            _plugins = LoadPlugins();

            InitializeComponent();
            

            LocationChanged += OnLocation;
            FormClosing += MainClientForm_FormClosing;
        }

        private static IClientPlugin[] LoadPlugins()
        {
            var dllLoader = new DllLoader();
            var plugins = dllLoader.LoadClientDlls(DllLoader.GetDllsPath(AppDomain.CurrentDomain.BaseDirectory));
            return plugins;
        }

        private void LoadTray()
        {
            tray_menu = new ContextMenu();
            tray_menu.MenuItems.Add(0, new MenuItem("Show", notifyIcon_DoubleClick));
            tray_menu.MenuItems.Add(0, new MenuItem("Exit", MainClientForm_Exit));
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            TraceOps.LoadLog();
            //Services.PrepareGetService();

            //Resize += MainClientForm_Resize;
            //notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            //notifyIcon.ContextMenu = tray_menu;
            //notifyIcon.MouseClick += notifyIcon_MouseUp;
            
            //_configServer = new ConfigServer();
            //_configServer.Changed += ConfigServerOnChanged;

            //_plugins[0].Start("TODO");
            //TraceOps.Out("test");
            //LoadPlugIns();
        }

        private void LoadPlugIns()
        {
            if (_plugins != null)
            {
                var threads = new Thread[_plugins.Length];
                var i = 0;
                
                foreach (IClientPlugin plugin in _plugins)
                {
                    if (plugin != null)
                    {
                        plugin.SetQueue(ref _messageQueue);
                        plugin.SetForm(this);
                        threads[i] = new Thread(LoadPlugIn);
                        threads[i].Start(plugin);
                    }
                }
            }
        }

        private static void LoadPlugIn(object o)
        {
            var oplugin = (IClientPlugin) o;
            oplugin.Start("TODO");
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "ping", "Server");
            _messageQueue.SetMessage(m);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
