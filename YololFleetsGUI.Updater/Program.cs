using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YololFleetsGUI.Preferences;

namespace YololFleetsGUI.Updater
{
    static class Program
    {
        public static UserPreferences preferences;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                preferences = UserPreferences.FromDefaultSettingsFile();
            }
            catch
            {
                preferences = new UserPreferences();
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmUpdateWindow());
        }
    }
}
