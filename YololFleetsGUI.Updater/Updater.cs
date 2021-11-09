using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;

namespace YololFleetsGUI.Updater
{
    public partial class frmUpdateWindow : Form
    {
        private class AppDetails
        {
            public Release releaseInfo;
            public System.Windows.Forms.Label statusLabel;
            public Button InstallUpdateButton;
            public int lastInstalledVersionId = 0;
        }

        AppDetails playerDetails;
        AppDetails simulatorDetails;
        AppDetails guiDetails;

        static string notCheckedMessage = "not checked";
        static string checkingForUpdatesMessage = "checking for updates";
        static string updateFoundMessage = "update found";
        static string updateFailedMessage = "update failed";
        static string updateSuccessfulMessage = "update successful";
        static string upToDateMessage = "up to date";

        public frmUpdateWindow()
        {
            InitializeComponent();
            lblPlayerUpdateStatus.Text = notCheckedMessage;
            lblSimulatorUpdateStatus.Text = notCheckedMessage;
            lblGuiUpdateStatus.Text = notCheckedMessage;

            playerDetails = new AppDetails()
            {
                statusLabel = lblPlayerUpdateStatus,
                InstallUpdateButton = btnInstallPlayerUpdate,
                lastInstalledVersionId = Program.preferences.LastInstalledPlayerId
            };
            simulatorDetails = new AppDetails()
            {
                statusLabel = lblSimulatorUpdateStatus,
                InstallUpdateButton = btnInstallSimulatorUpdate,
                lastInstalledVersionId = Program.preferences.LastInstalledSimulatorId
            };
            guiDetails = new AppDetails()
            {
                statusLabel = lblGuiUpdateStatus,
                InstallUpdateButton = btnInstallGuiUpdate,
                lastInstalledVersionId = Program.preferences.LastInstalledGuiId
            };
        }

        private async void CheckForUpdate(AutoUpdater.GitHubIds gitHubId)
        {
            AppDetails app = gitHubId switch
            {
                AutoUpdater.GitHubIds.Player => playerDetails,
                AutoUpdater.GitHubIds.Simulator => simulatorDetails,
                AutoUpdater.GitHubIds.GUI => guiDetails,
                _ => throw new ArgumentException($"The app {gitHubId} could not be resolved"),
            };

            try
            {
                app.statusLabel.Text = checkingForUpdatesMessage;
                app.releaseInfo = await AutoUpdater.GetLatestReleaseInfo(gitHubId);
                
                if (app.releaseInfo.Id == app.lastInstalledVersionId)
                {   // Up to date
                    app.statusLabel.Text = upToDateMessage;
                }
                else
                {   // Update found
                    app.statusLabel.Text = updateFoundMessage;
                    app.InstallUpdateButton.Visible = true;
                    app.InstallUpdateButton.Enabled = true;
                }
            }
            catch
            {
                app.statusLabel.Text = updateFailedMessage;
                app.statusLabel.ForeColor = Color.Red;
            }
        }

        private void frmUpdateWindow_Load(object sender, EventArgs e)
        {
            CheckForUpdate(AutoUpdater.GitHubIds.Player);
            CheckForUpdate(AutoUpdater.GitHubIds.Simulator);
            CheckForUpdate(AutoUpdater.GitHubIds.GUI);



        }
    }
}
