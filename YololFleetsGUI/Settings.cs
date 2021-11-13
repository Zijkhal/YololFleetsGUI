using System;
using System.Windows.Forms;
using System.IO;
using YololFleetsGUI.Preferences;

namespace YololFleetsGUI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        #region Control Events
        private void btnSetSimulatorInstallationLocation_Click(object sender, EventArgs e)
        {
            SimulatorInstallationBrowser.ShowDialog();

            string path = Path.Combine(SimulatorInstallationBrowser.SelectedPath, UserPreferences.combatSimulatorFileName);

            if (File.Exists(path))
            {
                lblSimulatorInstallation.Text = path;
                lblSimulatorInstallationError.Text = string.Empty;
            }
            else
            {
                lblSimulatorInstallationError.Text = $"The file {UserPreferences.combatSimulatorFileName} could not be found in the selected folder";
            }
        }

        private void btnSetPlayerInstallation_Click(object sender, EventArgs e)
        {
            PlayerInstallationBrowser.ShowDialog();

            string path = Path.Combine(PlayerInstallationBrowser.SelectedPath, UserPreferences.playerFileName);

            if (File.Exists(path))
            {
                lblPlayerInstallation.Text = path;
                lblPlayerInstallationError.Text = string.Empty;
            }
            else
            {
                lblPlayerInstallationError.Text = $"The file {UserPreferences.playerFileName} could not be found in the selected folder";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Program.preferences.CombatSimulatorPath = Path.GetDirectoryName(lblSimulatorInstallation.Text);
            Program.preferences.PlayerPath = Path.GetDirectoryName(lblPlayerInstallation.Text);
            Program.preferences.DefaultReplayFolder = lblDefaultReplayFolder.Text;

            Program.preferences.Save();

            this.Close();
        }
        #endregion

        private void btnSetDefaultReplayFolder_Click(object sender, EventArgs e)
        {
            DefaultReplayFolderBrowser.ShowDialog();

            lblDefaultReplayFolder.Text = DefaultReplayFolderBrowser.SelectedPath;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            lblSimulatorInstallation.Text = File.Exists(Program.preferences.CombatSimulatorFilePath) ? Program.preferences.CombatSimulatorFilePath : string.Empty;
            lblPlayerInstallation.Text = File.Exists(Program.preferences.ReplayPlayerFilePath) ? Program.preferences.ReplayPlayerFilePath : string.Empty;
            lblDefaultReplayFolder.Text = Program.preferences.DefaultReplayFolder;
        }
    }
}
