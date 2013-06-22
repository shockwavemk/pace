using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using PaceCommon;
using Message = PaceCommon.Message;

namespace ZtreeControl
{
    public class Control : PaceCommon.IControl
    {
        private static Process _processZTree;
        private static IntPtr hcalc;

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
                hcalc = IntPtr.Zero;
                
                var exeBytes = Properties.Resources.ztree;
                var exeToRun = Path.Combine(Path.GetTempPath(), "ztree.exe");

                _processZTree = new Process();
                _processZTree.StartInfo.FileName = exeToRun;
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    while (hcalc == IntPtr.Zero)
                    {
                        Thread.Sleep(10);
                        //hcalc = FindWindow(null, "zTree");
                    }
                }));
                thread.Start();



                //if(_processZTree._processZTree.Kill();
                

                if (System.IO.File.Exists(exeToRun))
                {
                    System.IO.File.Delete(exeToRun);
                }

                using (var exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                {
                    exeFile.Write(exeBytes, 0, exeBytes.Length);
                }
                _processZTree.Start();
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
            return ""; //DllLoader.ObjectToSoap(m);
        }

        public string StopRemoteZLeaf()
        {
            /*
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "stop_zleaf", "server");
            return DllLoader.ObjectToSoap(m);
             */
            return "";
        }

        public string Test()
        {
            return "Test";
        }

        public string File()
        {
            //var rlist = new List<string> { "" };
            //var m = new Message(rlist, true, "file", "Server");
            //return DllLoader.ObjectToSoap(m);
            return "File";
        }

        public string Edit()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "edit", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }

        public string View()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "view", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }

        public string Run()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "run", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }

        public string Help()
        {
            var rlist = new List<string> { "" };
            var m = new Message(rlist, true, "help", "Server");
            return ""; // DllLoader.ObjectToSoap(m);
        }
    }
}
