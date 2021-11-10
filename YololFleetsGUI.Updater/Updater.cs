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
using System.IO.Compression;
using System.Net;
using System.IO;

namespace YololFleetsGUI.Updater
{
    public partial class frmUpdateWindow : Form
    {
        private class AppDetails
        {
            public Release releaseInfo;
            public System.Windows.Forms.Label statusLabel;
            public Button InstallUpdateButton;
            public AutoUpdater.GitHubIds gitHubId;

            public int LatestAvailableVersionId 
            { 
                get
                {
                    return gitHubId switch
                    {
                        AutoUpdater.GitHubIds.Player => Program.preferences.LatestPlayerId,
                        AutoUpdater.GitHubIds.Simulator => Program.preferences.LatestSimulatorId,
                        AutoUpdater.GitHubIds.GUI => Program.preferences.LatestGuiId,
                        _ => 0
                    };
                }
                set
                {
                    switch (gitHubId)
                    {
                        case AutoUpdater.GitHubIds.Player:
                            Program.preferences.LatestPlayerId = value;
                            break;
                        case AutoUpdater.GitHubIds.Simulator:
                            Program.preferences.LatestSimulatorId = value;
                            break;
                        case AutoUpdater.GitHubIds.GUI:
                            Program.preferences.LatestGuiId = value;
                            break;
                    }
                }
            }
            public int LastInstalledVersionId
            {
                get
                {
                    return gitHubId switch
                    {
                        AutoUpdater.GitHubIds.Player => Program.preferences.LastInstalledPlayerId,
                        AutoUpdater.GitHubIds.Simulator => Program.preferences.LastInstalledSimulatorId,
                        AutoUpdater.GitHubIds.GUI => Program.preferences.LatestGuiId,
                        _ => 0
                    };
                }
            }
            public string InstallationFolder
            {
                get
                {
                    return gitHubId switch
                    {
                        AutoUpdater.GitHubIds.Player => Program.preferences.PlayerPath,
                        AutoUpdater.GitHubIds.Simulator => Program.preferences.CombatSimulatorPath,
                        AutoUpdater.GitHubIds.GUI => Directory.GetCurrentDirectory(),
                        _ => string.Empty
                    };
                }
            }
            public string AssetName
            {
                get
                {
                    return gitHubId switch
                    {
                        AutoUpdater.GitHubIds.Player => "SpaceCombatPlayer.Win.zip",
                        AutoUpdater.GitHubIds.Simulator => "SpaceShipCombatSimulator.Win.zip",
                        AutoUpdater.GitHubIds.GUI => "YololFleetsGUI.Win.zip",
                        _ => string.Empty
                    };
                }
            }
            public string TempDownloadFolder
            {
                get
                {
                    return gitHubId switch
                    {
                        AutoUpdater.GitHubIds.Player => Path.Combine(Program.preferences.PlayerPath, "TempPlayer"),
                        AutoUpdater.GitHubIds.Simulator => Path.Combine(Program.preferences.CombatSimulatorPath, "TempSimulator"),
                        AutoUpdater.GitHubIds.GUI => Path.Combine(Directory.GetCurrentDirectory(), "TempGui"),
                        _ => string.Empty
                    };
                }
            }
            public string TempDownloadFilePath 
            { 
                get 
                {
                    return TempDownloadFolder != string.Empty ? Path.Combine(TempDownloadFolder, "temp.zip") : string.Empty;
                }
            }
        }

        AppDetails playerDetails;
        AppDetails simulatorDetails;
        AppDetails guiDetails;

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
                gitHubId = AutoUpdater.GitHubIds.Player
            };
            simulatorDetails = new AppDetails()
            {
                statusLabel = lblSimulatorUpdateStatus,
                InstallUpdateButton = btnInstallSimulatorUpdate,
                gitHubId = AutoUpdater.GitHubIds.Simulator
            };
            guiDetails = new AppDetails()
            {
                statusLabel = lblGuiUpdateStatus,
                InstallUpdateButton = btnInstallGuiUpdate,
                gitHubId = AutoUpdater.GitHubIds.GUI
            };
        }

        private static async void CheckForUpdate(AppDetails app)
        {
            try
            {
                app.statusLabel.Text = checkingForUpdatesMessage;
                app.releaseInfo = await AutoUpdater.GetLatestReleaseInfo(app.gitHubId);

                // update id of latest available release
                app.LatestAvailableVersionId = app.releaseInfo.Id;
                
                if (app.releaseInfo.Id == app.LastInstalledVersionId)
                {   // Up to date
                    app.statusLabel.Text = upToDateMessage;
                    app.statusLabel.ForeColor = Color.Green;
                }
                else
                {   // Update found
                    app.statusLabel.Text = updatePendingMessage;
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

        private static async void Update(AppDetails app)
        {
            // Find binary to install
            string downloadUrl = string.Empty;
            foreach (ReleaseAsset asset in app.releaseInfo.Assets)
            {
                if (asset.Name == app.AssetName)
                {
                    // downloadURL may need to be manually assembled (user/project/assetId)
                    downloadUrl = asset.BrowserDownloadUrl;

                    app.statusLabel.Text = updateFoundMessage;

                    break;
                }
            }

            if (downloadUrl == string.Empty)
            {
                app.statusLabel.Text = updateFailedMessage;
                app.statusLabel.ForeColor = Color.Red;

                return;
            }

            // clean out temporary folder
            await Task.Run(() =>
            {
                if (Directory.Exists(app.TempDownloadFolder))
                {
                    Directory.Delete(app.TempDownloadFolder, true);
                }

                Directory.CreateDirectory(app.TempDownloadFolder);
            });

            // download files
            using (WebClient client = new WebClient())
            {
                app.statusLabel.Text = downloadingUpdateMessage;
                await client.DownloadFileTaskAsync(new Uri(downloadUrl), app.TempDownloadFilePath);
            }

            // extract files
            app.statusLabel.Text = extractingFilesMessage;
            await Task.Run(() => ZipFile.ExtractToDirectory(app.TempDownloadFilePath, app.TempDownloadFolder));

            // copy files
            // if copying Preferences.dll or Octokit.dll, rename them first
            // if copying items whose name starts with YololFleetsGui.Updater, copy with an alternative name
            // main app on startup will handle deleting old versions, and renaming alternative versions to the correct ones

            throw new NotImplementedException();
        }

        private void frmUpdateWindow_Load(object sender, EventArgs e)
        {
            CheckForUpdate(playerDetails);
            CheckForUpdate(simulatorDetails);
            CheckForUpdate(guiDetails);
        }
    }
}
