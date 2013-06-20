using System.Diagnostics;
using System.IO;

namespace ZtreeControl
{
    class Control
    {
        public Control()
        {}
        
        public void StartZLeaf()
        {
            byte[] exeBytes = Properties.Resources.zleaf;
            string exeToRun = Path.Combine(Path.GetTempPath(), "zleaf.exe");

            using (FileStream exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                exeFile.Write(exeBytes, 0, exeBytes.Length);
            Process.Start(exeToRun);
        }

        public void StartZTree()
        {
            byte[] exeBytes = Properties.Resources.ztree;
            string exeToRun = Path.Combine(Path.GetTempPath(), "ztree.exe");

            using (FileStream exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                exeFile.Write(exeBytes, 0, exeBytes.Length);

            Process.Start(exeToRun);
        }

        public string Test2()
        {
            StartZTree();
            return "Test33333";
        }
    }
}
