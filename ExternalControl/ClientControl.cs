using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaceCommon;

namespace ExternalControl
{
    class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private List<Parameter> _emptyList;

        public void Initializer(string ip, int port)
        {
            _emptyList = new List<Parameter> { new Parameter("parameter", "value") };
            _connectionTable = ConnectionTable.GetRemote("localhost", 9090);
            _messageQueue = MessageQueue.GetRemote("localhost", 9090);
        }
    }
}
