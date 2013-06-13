using System;
using System.Collections;

namespace PaceCommon
{
    [Serializable]
    public class ConnectionTable : MarshalByRefObject
    {
        private Hashtable _hashTable;

        public delegate void ConnectionRegistrationEventHandler(ClientInformation clientInformation);
        public event ConnectionRegistrationEventHandler ConnectionRegistration;

        public ConnectionTable()
        {
            _hashTable = new Hashtable();
        }

        public ClientInformation Get(string name)
        {
            var clientInformation = (ClientInformation)_hashTable[name];
            if (clientInformation == null)
            {
                clientInformation = new ClientInformation(name);
                ConnectionRegistration.Invoke(clientInformation);
                _hashTable.Add(name, clientInformation);
            }
            return clientInformation;
        }

        public ArrayList GetAll()
        {
            return new ArrayList(_hashTable.Values);
        }

        public class ClientInformation
        {
            public string Name { get; set; }

            public ClientInformation(string name)
            {
                Name = name;
            }
        }
    }
}
