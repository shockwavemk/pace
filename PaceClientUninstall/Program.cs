using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaceClientUninstall.Properties;
using PaceCommon;

namespace PaceClientUninstall
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        private static Mutex instanceMutex;

        [STAThread]
        static void Main()
        {
            try
            {
                bool createdNew;
                instanceMutex = new Mutex(true, @"Local\" + Assembly.GetExecutingAssembly().GetType().GUID, out createdNew);
                if (!createdNew)
                {
                    instanceMutex = null;
                    return;
                }

                if (MessageBox.Show(Resources.Uninstall_Question, Resources.Uninstall + Globals.ProductName,
                                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var clickOnceHelper = new ClickOnceHelper(Globals.PublisherName, Globals.ProductName, Globals.ProductSuite);
                    clickOnceHelper.Uninstall();

                    //Delete all files from publisher folder and folder itself on uninstall
                    var publisherFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Globals.PublisherName);
                    if (Directory.Exists(publisherFolder))
                        Directory.Delete(publisherFolder, true);
                }

                ReleaseMutex();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static void ReleaseMutex()
        {
            if (instanceMutex == null)
                return;
            instanceMutex.ReleaseMutex();
            instanceMutex.Close();
            instanceMutex = null;
        }
    }
}
