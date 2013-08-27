using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceCommon;

namespace PaceClient
{
    static class Program
    {
        private static DllLoader _dllLoader;
        private static MainClientForm _mcf;

        [STAThread]
        static void Main()
        {
            _dllLoader = new DllLoader();
            IClientPlugin[] plugins = _dllLoader.LoadClientDlls(DllLoader.GetDllsPath(AppDomain.CurrentDomain.BaseDirectory));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mcf = new MainClientForm(plugins);
            Application.Run(_mcf);
        }
    }
}
