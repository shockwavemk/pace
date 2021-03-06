﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using PaceCommon;
using Keys = System.Windows.Forms.Keys;

namespace ExternalControl
{
    class ClientControl : IClientControl
    {
        private static ConnectionTable _connectionTable;
        private static MessageQueue _messageQueue;
        private Form _mainForm;
        private string _name;
        private Panel _mainPanel;
        
        public void SetName(ref string name)
        {
            _name = name;
        }

        public void SetQueue(ref MessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void SetTable(ref ConnectionTable connectionTable)
        {
            _connectionTable = connectionTable;
        }

        public void SetForm(Form mainForm)
        {
            _mainForm = mainForm;
        }

        public void SetPanel(Panel mainPanel)
        {
            _mainPanel = mainPanel;
        }

        public void RemoteKeyLogging()
        {
            TraceOps.Out("TODO: Remote Keylogging");
        }

        public void StartProcess(string fileName, string arguments)
        {
            var proc = new ProcessStartInfo();
            proc.FileName = fileName;
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Arguments = arguments;
            Process.Start(proc);
        }

        public void RemoteStartProcess()
        {
            TraceOps.Out("TODO: Remote Start");
        }

        public void RemoteStopProcess()
        {
            TraceOps.Out("TODO: Remote Start");
        }
    }
}
