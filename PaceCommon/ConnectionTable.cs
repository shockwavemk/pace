using System;
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

        public static ConnectionTable GetRemote()
        {
            return (ConnectionTable)Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
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

        public Array GetSelected()
        {
            var al = new ArrayList(_hashTable.Values);
            var alnew = new ArrayList();
            foreach (ClientInformation ci in al)
            {
                if (ci.GetSelected())
                {
                    alnew.Add(ci);
                }
            }
            
            var alta = alnew.ToArray();
            return alta;
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
            private bool _selected;

            public ClientInformation(string name)
            {
                _name = name;
                _group = "Clients";
                _selected = false;
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

            public void SetSelected(bool selected)
            {
                _selected = selected;
            }

            public bool GetSelected()
            {
                return _selected;
            }
        }

        public void SetSelected(string text)
        {
            var citemp = Get(text);
            citemp.SetSelected(true);
        }
    }
}
