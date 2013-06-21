using System;
using System.Diagnostics;
using System.IO;
using PaceCommon;

namespace ZtreeControl
{
    class Control
    {
        public Control()
        {}
        
        public void StartZLeaf()
        {
            try
            {
                var exeBytes = Properties.Resources.zleaf;
                var exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");

                using (var exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                    exeFile.Write(exeBytes, 0, exeBytes.Length);
                Process.Start(exeToRun);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public void StartZTree()
        {
            try
            {
                var exeBytes = Properties.Resources.ztree;
                var exeToRun = Path.Combine(Path.GetTempPath(), "ztree.exe");
            
                using (var exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                    exeFile.Write(exeBytes, 0, exeBytes.Length);
                Process.Start(exeToRun);
            }
            catch (Exception exception)
            {
                TraceOps.Out(exception.ToString());
            }
        }

        public string Test2()
        {
            StartZTree();
            return "Start zTree";
        }
    }
}
