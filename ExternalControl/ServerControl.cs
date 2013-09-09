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

        public void Initializer(string ip, int port, ref MessageQueue messageQueue, ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
            _messageQueue = messageQueue;
            _emptyList = new List<Parameter> { new Parameter("parameter", "value") };
        }
    }
}
