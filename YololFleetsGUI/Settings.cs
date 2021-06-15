using System;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace YololFleetsGUI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            lblSimulatorInstallation.Text = File.Exists(Preferences.current.CombatSimulatorFilePath) ? Preferences.current.CombatSimulatorFilePath : string.Empty;
            lblPlayerInstallation.Text = File.Exists(Preferences.current.ReplayPlayerFilePath) ? Preferences.current.ReplayPlayerFilePath : string.Empty;
            lblDefaultReplayFolder.Text = Preferences.current.DefaultReplayFolder;
        }

        #region Control Events
        private void btnSetSimulatorInstallationLocation_Click(object sender, EventArgs e)
        {
            SimulatorInstallationBrowser.ShowDialog();

            string path = Path.Combine(SimulatorInstallationBrowser.SelectedPath, Preferences.combatSimulatorFileName);

            if (File.Exists(path))
            {
                lblSimulatorInstallation.Text = path;
                lblSimulatorInstallationError.Text = string.Empty;
            }
            else
            {
                lblSimulatorInstallationError.Text = $"The file {Preferences.combatSimulatorFileName} could not be found in the selected folder";
            }
        }

        private void btnSetPlayerInstallation_Click(object sender, EventArgs e)
        {
            PlayerInstallationBrowser.ShowDialog();

            string path = Path.Combine(PlayerInstallationBrowser.SelectedPath, Preferences.playerFileName);

            if (File.Exists(path))
            {
                lblPlayerInstallation.Text = path;
                lblPlayerInstallationError.Text = string.Empty;
            }
            else
            {
                lblPlayerInstallationError.Text = $"The file {Preferences.playerFileName} could not be found in the selected folder";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Preferences.current.CombatSimulatorPath = Path.GetDirectoryName(lblSimulatorInstallation.Text);
            Preferences.current.PlayerPath = Path.GetDirectoryName(lblPlayerInstallation.Text);
            Preferences.current.DefaultReplayFolder = lblDefaultReplayFolder.Text;

            File.WriteAllText(Preferences.settingsFileName, JsonSerializer.Serialize(Preferences.current));

            this.Close();
        }
        #endregion

        private void btnSetDefaultReplayFolder_Click(object sender, EventArgs e)
        {
            DefaultReplayFolderBrowser.ShowDialog();

            lblDefaultReplayFolder.Text = DefaultReplayFolderBrowser.SelectedPath;
        }
    }
}
