using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;

namespace PaceServer
{
    static class Program
    {
        private static DllLoader _dllLoader;
        private static MainServerForm _msf;
        
        [STAThread]
        static void Main()
        {
            _dllLoader = new DllLoader();
            IPlugin[] plugins = _dllLoader.LoadDlls(DllLoader.GetDllsPath(AppDomain.CurrentDomain.BaseDirectory));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _msf = new MainServerForm(plugins);
            Application.Run(_msf);
        }
    }
}
