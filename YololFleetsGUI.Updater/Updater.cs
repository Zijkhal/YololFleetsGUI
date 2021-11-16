using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Net;
using System.IO;

namespace YololFleetsGUI.Updater
{
    public partial class frmUpdateWindow : Form
    {
        UpdateLogic updateLogic;

        static readonly string notCheckedMessage = "not checked";
        static readonly string checkingForUpdatesMessage = "checking for updates";
        static readonly string updatePendingMessage = "update pending";
        static readonly string upToDateMessage = "up to date";
        static readonly string updateFoundMessage = "update download found";
        static readonly string downloadingUpdateMessage = "downloading update";
        static readonly string extractingFilesMessage = "extracting files";
        static readonly string copyingFilesMessage = "copying files";
        static readonly string updateSuccessfulMessage = "update successful";
        static readonly string updateFailedMessage = "update failed";
        static readonly string afterUpdateCleanupFailedMessage = "an error has occured while deleting temporary files";

        public frmUpdateWindow()
        {
            InitializeComponent();
            lblPlayerUpdateStatus.Text = notCheckedMessage;
            lblSimulatorUpdateStatus.Text = notCheckedMessage;
            lblGuiUpdateStatus.Text = notCheckedMessage;

            updateLogic = new UpdateLogic(
                // Player
                new AppDetails()
                {
                    statusLabel = lblPlayerUpdateStatus,
                    InstallUpdateButton = btnInstallPlayerUpdate,
                    gitHubId = GitHubIds.Player
                },
                // Simulator
                new AppDetails()
                {
                    statusLabel = lblSimulatorUpdateStatus,
                    InstallUpdateButton = btnInstallSimulatorUpdate,
                    gitHubId = GitHubIds.Simulator
                },
                // GUI
                new AppDetails()
                {
                    statusLabel = lblGuiUpdateStatus,
                    InstallUpdateButton = btnInstallGuiUpdate,
                    gitHubId = GitHubIds.GUI
                });

            updateLogic.UpdateFailed += app =>
            {
                app.statusLabel.Text = updateFailedMessage;
                app.statusLabel.ForeColor = Color.Red;
            };

            updateLogic.UpdateCheckStart += app => app.statusLabel.Text = checkingForUpdatesMessage;

            updateLogic.UpdateCheckComplete += app =>
            {
                if (app.releaseInfo.Id == app.LastInstalledVersionId)
                {   // Up to date
                    app.statusLabel.Text = upToDateMessage;
                    app.statusLabel.ForeColor = Color.Green;
                }
                else
                {   // Update found
                    app.updateFound = true;
                    app.statusLabel.Text = updatePendingMessage;

                    if (app.AutoDownloadUpdates || updateLogic.InstallAll)
                    {
                        updateLogic.UpdateApp(app.gitHubId);
                    }
                    else
                    {
                        app.InstallUpdateButton.Visible = true;
                        app.InstallUpdateButton.Enabled = true;
                    }
                }
            };

            updateLogic.UpdateFound += app => app.statusLabel.Text = updateFoundMessage;

            updateLogic.UpdateDownloadStart += app => app.statusLabel.Text = downloadingUpdateMessage;

            updateLogic.UpdateExtractionStart += app => app.statusLabel.Text = extractingFilesMessage;

            updateLogic.UpdateCopyFilesStart += app => app.statusLabel.Text = copyingFilesMessage;

            updateLogic.UpdateCompleted += app =>
            {
                app.statusLabel.Text = updateSuccessfulMessage;
                app.statusLabel.ForeColor = Color.Green;
            };

            updateLogic.AfterUpdateCleanupFailed += app =>
            {
                lblMessages.Text = afterUpdateCleanupFailedMessage;
                lblMessages.Visible = true;
            };
        }

        private void frmUpdateWindow_Load(object sender, EventArgs e)
        {
            updateLogic.CheckForUpdate(GitHubIds.Player);
            updateLogic.CheckForUpdate(GitHubIds.Simulator);
            updateLogic.CheckForUpdate(GitHubIds.GUI);
        }

        private void btnInstallSimulatorUpdate_Click(object sender, EventArgs e)
        {
            updateLogic.UpdateApp(GitHubIds.Simulator);
        }

        private void btnInstallPlayerUpdate_Click(object sender, EventArgs e)
        {
            updateLogic.UpdateApp(GitHubIds.Player);
        }

        private void btnInstallGuiUpdate_Click(object sender, EventArgs e)
        {
            updateLogic.UpdateApp(GitHubIds.GUI);
        }

        private void btnInstallAllUpdates_Click(object sender, EventArgs e)
        {
            updateLogic.InstallAll = true;

            // the UpdateApp functions checks to make sure there are no conflicting update attempts
            updateLogic.UpdateApp(GitHubIds.Player);
            updateLogic.UpdateApp(GitHubIds.Simulator);
            updateLogic.UpdateApp(GitHubIds.GUI);
        }
    }
}
