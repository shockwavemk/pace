using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaceCommon;

namespace WebControl
{
    public class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;

        public void Initializer(string ip, int port, ref MessageQueue messageQueue, ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
            _messageQueue = messageQueue;
        }
    }
}
