using System;
using System.Diagnostics;
using System.IO;
using PaceCommon;

namespace ZtreeControl
{
    class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private static Process _processZLeaf;
        
        public void Initializer(string ip, int port)
        {
            _connectionTable = ConnectionTable.GetRemote(ip, port);
            _messageQueue = MessageQueue.GetRemote(ip, port);
        }

        public void StartZLeaf()
        {
            try
            {
                var exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");
                ProcessControl.FindDeleteFileAndStartAgain(exeToRun, "zleaf", true, false, Properties.Resources.zleaf);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }
        
        public void StopZLeaf()
        {
            ProcessControl.FindAndKillProcess("zleaf");
        }
    }
}
