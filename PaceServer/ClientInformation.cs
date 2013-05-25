using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceServer
{
    class ClientInformation
    {
        public static Dictionary<int, ClientInformation> ClientList = new Dictionary<int, ClientInformation>();
        public string Name { get; set; }

        public ClientInformation(string name)
        {
            this.Name = name;
        }

        public static ClientInformation GetClientInformation(int clientId)
        {
            //return (ClientList.ContainsKey(clientId)) ? ClientList[clientId] : null;
            return new ClientInformation("TestClient"+clientId);
        }

        
    }
}
