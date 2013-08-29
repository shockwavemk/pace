using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaceCommon;

namespace WebControl
{
    class ServerControl : IServerControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;

        public void Initializer(string ip, int port)
        {
            _connectionTable = ConnectionTable.GetRemote(ip, port);
            _messageQueue = MessageQueue.GetRemote(ip, port);
        }

        public static void StartWebControlToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            foreach (ConnectionTable.ClientInformation clientInformation in _connectionTable.GetChecked())
            {
                TraceOps.Out(clientInformation.GetName());
                var rlist = new List<string> { "" };
                var m = new PaceCommon.Message(rlist, true, "start_webcontrol", clientInformation.GetName());
                _messageQueue.SetMessage(m);
            }
        }
    }
}
