using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            lblSimulatorInstallation.Text = Preferences.current.CombatSimulatorFilePath;
            lblPlayerInstallation.Text = Preferences.current.ReplayPlayerFilePath;
        }

        private void btnSetSimulatorInstallationLocation_Click(object sender, EventArgs e)
        {
            SimulatorInstallationBrowser.ShowDialog();

            if (Preferences.current.SetCombatSimulatorPath(SimulatorInstallationBrowser.SelectedPath))
            {
                lblSimulatorInstallation.Text = Preferences.current.CombatSimulatorFilePath;
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

            if (Preferences.current.SetReplayPayerPath(PlayerInstallationBrowser.SelectedPath))
            {
                lblPlayerInstallation.Text = Preferences.current.ReplayPlayerFilePath;
                lblPlayerInstallationError.Text = string.Empty;
            }
            else
            {
                lblPlayerInstallationError.Text = $"The file {Preferences.playerFileName} could not be found in the selected folder";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Preferences.settingsFileName, JsonSerializer.Serialize(Preferences.current));

            this.Close();
        }
    }
}
