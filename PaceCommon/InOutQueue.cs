using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCommon
{
    public class InOutQueue
    {
        public ConcurrentQueue<Message> ServerToClientQueue, ClientToServerQueue;

        public InOutQueue()
        {
            ServerToClientQueue = new ConcurrentQueue<Message>();
            ClientToServerQueue = new ConcurrentQueue<Message>();
        }
    }
}
