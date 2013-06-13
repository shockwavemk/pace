using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using PaceServer;
using Message = PaceCommon.Message;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private const int Threshold = 1;
        private ContextMenu tray_menu;
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;
        private NetworkClient _networkClient;
        private bool _running = true;
        private string _name;
        private Thread _threadWorker;
        private TaskManager _taskManager;

        public MainClientForm()
        {
            InitializeComponent();
            tray_menu = new ContextMenu();
            tray_menu.MenuItems.Add(0, new MenuItem("Show", new EventHandler(notifyIcon_DoubleClick)));
            tray_menu.MenuItems.Add(0, new MenuItem("Exit", new EventHandler(MainClientForm_Exit)));

            this.FormClosing += new FormClosingEventHandler(MainClientForm_FormClosing);
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            Services.PrepareGetService();
            Services.GetService("localhost", 9090, typeof(ConnectionTable));
            Services.GetService("localhost", 9090, typeof(MessageQueue));
            _connectionTable = (ConnectionTable)System.Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
            _messageQueue = (MessageQueue)System.Activator.GetObject(typeof(MessageQueue), "http://localhost:9090/MessageQueue.rem");
            _name = HashOps.GetFqdn();

            Resize += new EventHandler(MainClientForm_Resize);
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            notifyIcon.ContextMenu = tray_menu;
            notifyIcon.MouseClick += new MouseEventHandler(notifyIcon_MouseUp);

            try
            {
                _networkClient = new NetworkClient(ref _messageQueue, ref _connectionTable, _name);
                _taskManager = new TaskManager(ref _messageQueue);
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
            var m = new Message(rlist, true, "ping", "server");
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
