using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceServer
{
    class OutQueue
    {
        private static ConcurrentQueue<ServerAction> _serverActions;
    }
}
