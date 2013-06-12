using System;
using System.Windows.Forms;
using PaceCommon;

namespace PaceClient
{
    public partial class MainClientForm : Form
    {
        private ConnectionTable _connectionTable;
        private MessageQueue _messageQueue;

        public MainClientForm()
        {
            InitializeComponent();
        }

        private void MainClientForm_Load(object sender, EventArgs e)
        {
            Services.PrepareGetService();
            Services.GetService("localhost", 9090, typeof(ConnectionTable));
            Services.GetService("localhost", 9090, typeof(MessageQueue));
            _connectionTable = new ConnectionTable();
            _messageQueue = new MessageQueue();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           Console.WriteLine("Hash: {0}.", _connectionTable.GetHashCode());
           Console.WriteLine("MessageQueue: {0}.", _messageQueue.Test());
        }
    }
}
