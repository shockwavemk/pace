using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceServer
{
    [Serializable]
    class ServerAction: PaceCommon.Message
    {
        public ServerAction(List<string> parameter, bool acknowledgement, string command, string destination) : base(parameter, acknowledgement, command, destination)
        {

        }
    }
}
