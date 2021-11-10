using System;
using System.Windows.Forms;
using YololFleetsGUI.Preferences;

namespace YololFleetsGUI
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
                MessageBox.Show($"Unable to load user preferences. Initializing with default values");
                preferences = new UserPreferences();
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
