using PaceCommon;

namespace WebControl
{
    public class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;

        public void Initializer(string ip, int port)
        {
            _connectionTable = ConnectionTable.GetRemote(ip, port);
            _messageQueue = MessageQueue.GetRemote(ip, port);
        }
    }
}
