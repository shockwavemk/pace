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

        public static void StartRemoteZLeaf(object sender, EventArgs eventArgs)
        {
            
        }

        public string Test()
        {
            return "Test";
        }

        public string File()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "ping", "Server");
            return DllLoader.ObjectToSoap(m);
        }
    }
}
