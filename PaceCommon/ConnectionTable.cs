﻿using System;
using System.Collections;

namespace PaceCommon
{
    [Serializable]
    public class ConnectionTable : MarshalByRefObject
    {
        private Hashtable _hashTable;

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
                //ConnectionRegistration.Invoke(clientInformation);
                _hashTable.Add(name, clientInformation);
            }
            return clientInformation;
        }

        public void Set(string name, ClientInformation clientInformation)
        {
            _hashTable.Add(name, clientInformation);
        }

        public Array GetAll()
        {
            return new ArrayList(_hashTable.Values).ToArray();
        }

        [Serializable]
        public class ClientInformation
        {
            private string _name;
            private string _group;

            public ClientInformation(string name)
            {
                _name = name;
                _group = "Clients";
            }

            public string GetName()
            {
                return _name;
            }

            public void SetGroup(string group)
            {
                _group = group;
            }

            public string GetGroup()
            {
                return _group;
            }
        }
    }
}
