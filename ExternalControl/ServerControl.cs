using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaceCommon;

namespace ExternalControl
{
    public class ServerControl : IServerControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;

        private static List<Parameter> _emptyList;

        public void Initializer(string ip, int port)
        {
            _emptyList = new List<Parameter> { new Parameter("parameter", "value") };
            _connectionTable = ConnectionTable.GetRemote("localhost", 9090);
            _messageQueue = MessageQueue.GetRemote("localhost", 9090);
        }
    }
}
