using System;
using System.Drawing;
using System.Linq;
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
        private enum GitHubIds : long
        {
            Simulator = 366504584L,
            Player = 367688805L,
            GUI = 376849707L
        }

        private class AppDetails
        {
            public Release releaseInfo;
            public bool updateFound = false;
            public bool updateInProgress = false;
            public bool updateCompleted = false;
            public System.Windows.Forms.Label statusLabel;
            public Button InstallUpdateButton;
            public GitHubIds gitHubId;

            public int LatestAvailableVersionId 
            { 
                get
                {
                    return gitHubId switch
                    {
                        GitHubIds.Player => Program.preferences.LatestPlayerId,
                        GitHubIds.Simulator => Program.preferences.LatestSimulatorId,
                        GitHubIds.GUI => Program.preferences.LatestGuiId,
                        _ => 0
                    };
                }
                set
                {
                    switch (gitHubId)
                    {
                        case GitHubIds.Player:
                            Program.preferences.LatestPlayerId = value;
                            break;
                        case GitHubIds.Simulator:
                            Program.preferences.LatestSimulatorId = value;
                            break;
                        case GitHubIds.GUI:
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
                        GitHubIds.Player => Program.preferences.LastInstalledPlayerId,
                        GitHubIds.Simulator => Program.preferences.LastInstalledSimulatorId,
                        GitHubIds.GUI => Program.preferences.LatestGuiId,
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
                        GitHubIds.Player => Program.preferences.PlayerPath,
                        GitHubIds.Simulator => Program.preferences.CombatSimulatorPath,
                        GitHubIds.GUI => Directory.GetCurrentDirectory(),
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
                        GitHubIds.Player => "SpaceCombatPlayer.Win.zip",
                        GitHubIds.Simulator => "SpaceShipCombatSimulator.Win.zip",
                        GitHubIds.GUI => "YololFleetsGUI.Win.zip",
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
                        GitHubIds.Player => Path.Combine(Program.preferences.PlayerPath, "TempPlayer"),
                        GitHubIds.Simulator => Path.Combine(Program.preferences.CombatSimulatorPath, "TempSimulator"),
                        GitHubIds.GUI => Path.Combine(Directory.GetCurrentDirectory(), "TempGui"),
                        _ => string.Empty
                    };
                }
            }
            public string TempExtractionFolder
            {
                get
                {
                    return gitHubId switch
                    {
                        GitHubIds.Player => Path.Combine(Program.preferences.PlayerPath, "TempExtractedPlayer"),
                        GitHubIds.Simulator => Path.Combine(Program.preferences.CombatSimulatorPath, "TempExtractedSimulator"),
                        GitHubIds.GUI => Path.Combine(Directory.GetCurrentDirectory(), "TempExtractedGui"),
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
            public bool AutoDownloadUpdates
            {
                get
                {
                    return gitHubId switch
                    {
                        GitHubIds.Player => Program.preferences.AutoDownloadPlayerUpdates,
                        GitHubIds.Simulator => Program.preferences.AutoDownloadSimulatorUpdates,
                        GitHubIds.GUI => Program.preferences.AutoDownloadGuiUpdates,
                        _ => false
                    };
                }
            }
        }

        AppDetails playerDetails;
        AppDetails simulatorDetails;
        AppDetails guiDetails;

        bool installAll = false;

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

        private static readonly GitHubClient client = new GitHubClient(new ProductHeaderValue("YololFleetsGUI"));
        private static readonly TimeSpan timeout = TimeSpan.FromSeconds(15);

        static frmUpdateWindow()
        {
            client.SetRequestTimeout(timeout);
        }

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
                gitHubId = GitHubIds.Player
            };
            simulatorDetails = new AppDetails()
            {
                statusLabel = lblSimulatorUpdateStatus,
                InstallUpdateButton = btnInstallSimulatorUpdate,
                gitHubId = GitHubIds.Simulator
            };
            guiDetails = new AppDetails()
            {
                statusLabel = lblGuiUpdateStatus,
                InstallUpdateButton = btnInstallGuiUpdate,
                gitHubId = GitHubIds.GUI
            };
        }

        private async void CheckForUpdate(AppDetails app)
        {
            try
            {
                app.statusLabel.Text = checkingForUpdatesMessage;
                app.releaseInfo = await client.Repository.Release.GetLatest((long)app.gitHubId);

                // update id of latest available release
                app.LatestAvailableVersionId = app.releaseInfo.Id;
                
                if (app.releaseInfo.Id == app.LastInstalledVersionId)
                {   // Up to date
                    app.statusLabel.Text = upToDateMessage;
                    app.statusLabel.ForeColor = Color.Green;
                }
                else
                {   // Update found

                    app.updateFound = true;
                    app.statusLabel.Text = updatePendingMessage;

                    if (app.AutoDownloadUpdates || installAll)
                    {
                        UpdateApp(app);
                    }
                    else
                    {
                        app.InstallUpdateButton.Visible = true;
                        app.InstallUpdateButton.Enabled = true;
                    }
                }
            }
            catch
            {
                UpdateFailed(app);
            }
        }

        private static void CopyFiles(DirectoryInfo from, DirectoryInfo to, bool recursive = true)
        {
            // rename old dll files which are used by the updater
            foreach (FileInfo old in to.GetFiles())
            {
                if (old.Name.Contains("Octokit.dll") || old.Name.Contains("Preferences.dll"))
                {
                    old.MoveTo(Path.Combine(to.FullName, "OLD" + old.Name), true);
                }
            }

            // copy files
            // files of the Updater are copied with the "NEW" prefix
            foreach (FileInfo file in from.GetFiles())
            {
                if (file.Name.StartsWith(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name))
                {
                    file.CopyTo(Path.Combine(to.FullName, "NEW" + file.Name), true);
                }
                else
                {
                    file.CopyTo(Path.Combine(to.FullName, file.Name), true);
                }
            }

            // do the same for all subDirectories
            if (recursive)
            {
                foreach (DirectoryInfo subFrom in from.GetDirectories())
                {
                    //Get or create destination directory
                    DirectoryInfo subTo = Directory.CreateDirectory(Path.Combine(to.FullName, subFrom.Name));

                    CopyFiles(subFrom, subTo, recursive);
                }
            }
        }

        private static void UpdateFailed(AppDetails app)
        {
            app.statusLabel.Text = updateFailedMessage;
            app.statusLabel.ForeColor = Color.Red;
        }

        private static void CopyExtractedFiles(AppDetails app)
        {
            DirectoryInfo downloadDirectory = new DirectoryInfo(app.TempDownloadFolder);
            DirectoryInfo installDirectory = new DirectoryInfo(app.InstallationFolder);
            
            CopyFiles(downloadDirectory, installDirectory, true);
        }

        private static void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        private static void CleanOutFolder(DirectoryInfo directory, bool recursive = true)
        {
            foreach (FileInfo fi in directory.GetFiles())
            {
                fi.Delete();
            }

            if (recursive)
            {
                foreach(DirectoryInfo subDir in directory.GetDirectories())
                {
                    CleanOutFolder(subDir, true);
                }
            }
        }

        private async void UpdateApp(AppDetails app)
        {
            // if update is not found, is already complete, or in progress, return
            if (!app.updateFound || app.updateCompleted || app.updateInProgress)
            {
                return;
            }

            app.updateInProgress = true;

            try
            {
                // Find binary to install
                string downloadUrl = app.releaseInfo.Assets.First(x => x.Name == app.AssetName).BrowserDownloadUrl;

                app.statusLabel.Text = updateFoundMessage;


                // clean out temporary folders
                await Task.Run(() =>
                {
                    CleanOutFolder(Directory.CreateDirectory(app.TempDownloadFolder));
                    CleanOutFolder(Directory.CreateDirectory(app.TempExtractionFolder));
                });

                // download files
                using (WebClient client = new WebClient())
                {
                    app.statusLabel.Text = downloadingUpdateMessage;
                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), app.TempDownloadFilePath);
                }

                // extract files
                app.statusLabel.Text = extractingFilesMessage;
                await Task.Run(() => ZipFile.ExtractToDirectory(app.TempDownloadFilePath, app.TempExtractionFolder));

                // copy files
                // if copying Preferences.dll or Octokit.dll, rename them first
                // if copying items whose name starts with YololFleetsGui.Updater, copy with an alternative name
                // main app on startup will handle deleting old versions, and renaming alternative versions to the correct ones
                app.statusLabel.Text = copyingFilesMessage;
                await Task.Run(() => CopyExtractedFiles(app));

                app.statusLabel.Text = updateSuccessfulMessage;
                app.statusLabel.ForeColor = Color.Green;
                app.updateCompleted = true;
            }
            catch
            {
                UpdateFailed(app);
            }

            try
            {
                // clean up temporary folders
                await Task.Run(() =>
                {
                    DeleteFolder(app.TempDownloadFolder);
                    DeleteFolder(app.TempExtractionFolder);
                });
            }
            catch
            {
                lblMessages.Text = afterUpdateCleanupFailedMessage;
                lblMessages.Visible = true;
            }

            app.updateInProgress = false;
        }

        private void frmUpdateWindow_Load(object sender, EventArgs e)
        {
            CheckForUpdate(playerDetails);
            CheckForUpdate(simulatorDetails);
            CheckForUpdate(guiDetails);
        }

        private void btnInstallSimulatorUpdate_Click(object sender, EventArgs e)
        {
            UpdateApp(simulatorDetails);
        }

        private void btnInstallPlayerUpdate_Click(object sender, EventArgs e)
        {
            UpdateApp(playerDetails);
        }

        private void btnInstallGuiUpdate_Click(object sender, EventArgs e)
        {
            UpdateApp(guiDetails);
        }

        private void btnInstallAllUpdates_Click(object sender, EventArgs e)
        {
            installAll = true;

            UpdateApp(playerDetails);
            UpdateApp(simulatorDetails);
            UpdateApp(guiDetails);
        }
    }
}
