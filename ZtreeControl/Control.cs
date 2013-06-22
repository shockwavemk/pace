using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PaceCommon;

namespace ZtreeControl
{
    public class Control : PaceCommon.IControl
    {
        private static MessageQueue _messageQueue;

        public static Message GetMessage()
        {
            return _messageQueue.GetMessage("ZtreeControl");
        }

        public static bool SetMessage(Message message)
        {
            _messageQueue.SetMessage(message);
            return true;
        }

        public void StartZLeaf(object sender, EventArgs eventArgs)
        {
            try
            {
                var exeBytes = Properties.Resources.zleaf;
                var exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");



                if (System.IO.File.Exists(exeToRun))
                {
                    System.IO.File.Delete(exeToRun);
                }

                using (var exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                {
                    exeFile.Write(exeBytes, 0, exeBytes.Length);
                }
                Process.Start(exeToRun);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public static void StartZTree(object sender, EventArgs eventArgs)
        {
            try
            {
                var exeBytes = Properties.Resources.ztree;
                var exeToRun = Path.Combine(Path.GetTempPath(), "ztree.exe");

                if (System.IO.File.Exists(exeToRun))
                {
                    System.IO.File.Delete(exeToRun);
                }

                using (var exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                {
                    exeFile.Write(exeBytes, 0, exeBytes.Length);
                }
                Process.Start(exeToRun);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public string StartRemoteZLeaf()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "start_zleaf", "server");
            return DllLoader.ObjectToSoap(m);
        }

        public string StopRemoteZLeaf()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "stop_zleaf", "server");
            return DllLoader.ObjectToSoap(m);
        }

        public string Test()
        {
            return "Test";
        }

        public string File()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "file", "Server");
            return DllLoader.ObjectToSoap(m);
        }

        public string Edit()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "edit", "Server");
            return DllLoader.ObjectToSoap(m);
        }

        public string View()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "view", "Server");
            return DllLoader.ObjectToSoap(m);
        }

        public string Run()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "run", "Server");
            return DllLoader.ObjectToSoap(m);
        }

        public string Help()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "help", "Server");
            return DllLoader.ObjectToSoap(m);
        }
    }
}
