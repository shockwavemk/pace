using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;

namespace PaceCommon
{
    [Serializable]
    public class ConnectionTable : MarshalByRefObject
    {
        private ConcurrentDictionary<string, ClientInformation> _concurrentDictionary;

        public ConnectionTable()
        {
            _concurrentDictionary = new ConcurrentDictionary<string,ClientInformation>();
        }

        public static ConnectionTable GetRemote()
        {
            return (ConnectionTable)Activator.GetObject(typeof(ConnectionTable), "http://localhost:9090/ConnectionTable.rem");
        }

        public ClientInformation Get(string name)
        {
            return _concurrentDictionary.GetOrAdd(name, new ClientInformation(name));
        }

        public Array GetSelected()
        {
            return _concurrentDictionary.Where(pair => pair.Value.GetSelected()).Select(pair => pair.Value).ToArray();
        }


        public void Set(string name, ClientInformation clientInformation)
        {
            _concurrentDictionary.AddOrUpdate(name, clientInformation, (s, information) => clientInformation);
        }

        public Array GetAll()
        {
            return _concurrentDictionary.Values.ToArray();
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

        public void SetSelection(string[] strings)
        {
            foreach (ClientInformation clientInformation in _concurrentDictionary.Values)
            {
                clientInformation.SetSelected(false);
            }
            
            foreach (var s in strings)
            {
                _concurrentDictionary[s].SetSelected(true);
            }
        }
    }
}
