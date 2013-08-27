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
        private static IServerPlugin[] _plugins;
        
        [STAThread]
        static void Main()
        {
            _dllLoader = new DllLoader();
            _plugins = _dllLoader.LoadServerDlls(DllLoader.GetDllsPath(AppDomain.CurrentDomain.BaseDirectory));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _msf = new MainServerForm(_plugins);
            Application.Run(_msf);
        }
    }
}
