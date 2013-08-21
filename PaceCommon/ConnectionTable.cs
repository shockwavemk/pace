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

        public Array GetChecked()
        {
            return _concurrentDictionary.Where(pair => pair.Value.GetChecked()).Select(pair => pair.Value).ToArray();
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
            private int _messages_in_queue;
            private float _cpu;
            private string _applications;
            private bool _checked;
            private string _ip;
            private int _port;

            public ClientInformation(string name)
            {
                _name = name;
                _group = "Clients";
                _selected = false;
                _checked = false;
                _messages_in_queue = 0;
                _cpu = 0;
                _port = 0;
                _ip = "unknown";
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

            public void SetIp(string ip)
            {
                _ip = ip;
            }

            public string GetIp()
            {
                return _ip;
            }

            public void SetPort(int port)
            {
                _port = port;
            }

            public int GetPort()
            {
                return _port;
            }

            public void SetSelected(bool selected)
            {
                _selected = selected;
            }

            public bool GetSelected()
            {
                return _selected;
            }

            public void SetChecked(bool check)
            {
                _checked = check;
            }

            public bool GetChecked()
            {
                return _checked;
            }

            public int GetMessagesInQueue()
            {
                return _messages_in_queue;
            }

            public void SetMessagesInQueue(int n)
            {
                _messages_in_queue = n;
            }

            public string GetPerformance()
            {
                return Math.Round(_cpu, 0) + " %";
            }

            public void SetPerformance(float cpu)
            {
                _cpu = cpu;
            }

            public string GetApplicationNames()
            {
                return _applications;
            }

            public void SetApplicationNames(string applications)
            {
                _applications = applications;
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

        public void SetChecked(string[] strings)
        {
            foreach (ClientInformation clientInformation in _concurrentDictionary.Values)
            {
                clientInformation.SetChecked(false);
            }

            foreach (var s in strings)
            {
                _concurrentDictionary[s].SetChecked(true);
            }
        }
    }
}
