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
        private static MainServerForm msf;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _dllLoader = new DllLoader();
            IPlugin[] plugins = _dllLoader.LoadDlls(DllLoader.GetDllsPath(AppDomain.CurrentDomain.BaseDirectory));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            msf = new MainServerForm(plugins);
            Application.Run(msf);
        }
    }
}
