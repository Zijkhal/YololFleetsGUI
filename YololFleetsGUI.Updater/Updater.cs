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
            public DateTime LastUpdateCheck
            {
                get
                {
                    return gitHubId switch
                    {
                        GitHubIds.Player => Program.preferences.LastPlayerUpdateCheck,
                        GitHubIds.Simulator => Program.preferences.LastSimulatorUpdateCheck,
                        GitHubIds.GUI => Program.preferences.LastGuiUpdateCheck,
                        _ => DateTime.MinValue
                    };
                }
                set
                {
                    switch (gitHubId)
                    {
                        case GitHubIds.Player:
                            Program.preferences.LastPlayerUpdateCheck = value;
                            break;
                        case GitHubIds.Simulator:
                            Program.preferences.LastSimulatorUpdateCheck = value;
                            break;
                        case GitHubIds.GUI:
                            Program.preferences.LastGuiUpdateCheck = value;
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
                set
                {
                    switch (gitHubId)
                    {
                        case GitHubIds.Player:
                            Program.preferences.LastInstalledPlayerId = value;
                            break;
                        case GitHubIds.Simulator:
                            Program.preferences.LastInstalledSimulatorId = value;
                            break;
                        case GitHubIds.GUI:
                            Program.preferences.LastInstalledGuiId = value;
                            break;
                    }
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

        /// <summary>
        /// Check the relevant GitHub repo for new releases
        /// </summary>
        /// <param name="app">the app to check for updates</param>
        private async void CheckForUpdate(AppDetails app)
        {
            try
            {
                app.statusLabel.Text = checkingForUpdatesMessage;
                app.releaseInfo = await client.Repository.Release.GetLatest((long)app.gitHubId);

                // update id of latest available release
                app.LatestAvailableVersionId = app.releaseInfo.Id;
                app.LastUpdateCheck = DateTime.Now;

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

        /// <summary>
        /// Iterate through all items in directory, and perform action on them if they are not Ignored
        /// </summary>
        /// <param name="directory">Directory which contains the items to iterate through</param>
        /// <param name="recursive">Whether to go into subdirectories</param>
        /// <param name="Ignore">Tells the program whether to ignore the current file or folder (true = ignore)</param>
        /// <param name="Action">Action to perform on each file and directory</param>
        private static void ForAllItemsInDirectory(DirectoryInfo directory, bool recursive, Func<FileSystemInfo, bool> Ignore, Action<FileSystemInfo> Action)
        {
            if (!directory.Exists)
            {
                return;
            }

            foreach (FileSystemInfo fsi in directory.GetFileSystemInfos())
            {
                if (fsi.Exists && !Ignore(fsi))
                {
                    if(recursive && fsi is DirectoryInfo)
                    {
                        ForAllItemsInDirectory(fsi as DirectoryInfo, recursive, Ignore, Action);
                    }

                    Action(fsi);
                }
            }
        }

        /// <summary>
        /// Delete files of the currently installed (outdated) version
        /// </summary>
        /// <param name="directory">Directory containing the files to delete</param>
        /// <param name="Ignore">Return true if file or directory is to be ignored (not deleted)</param>
        /// <param name="recursive">Whether to go into subdirectories</param>
        private static void CleanOutFolder(DirectoryInfo directory, Func<FileSystemInfo, bool> Ignore, bool recursive = true)
        {
            ForAllItemsInDirectory(directory,
                recursive,
                Ignore,
                fsi =>
                {
                    if (fsi is DirectoryInfo && (fsi as DirectoryInfo).GetFileSystemInfos().Length != 0)
                    {
                        return;
                    }
                    fsi.Delete();
                });
        }

        /// <summary>
        /// Copy all files from source directory to destination directory
        /// </summary>
        /// <param name="source">Move files from here</param>
        /// <param name="destination">Move files to here</param>
        /// <param name="recursive">Whether to go into subdirectories</param>
        private static void CopyFiles(DirectoryInfo source, DirectoryInfo destination, bool recursive = true)
        {
            // copy files, add "NEW" prefix to all file names
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(destination.FullName, "NEW" + file.Name), true);
            }

            // do the same for all subDirectories
            if (recursive)
            {
                foreach (DirectoryInfo subDir in source.GetDirectories())
                {
                    //Get or create destination directory
                    DirectoryInfo subTo = Directory.CreateDirectory(Path.Combine(destination.FullName, subDir.Name));

                    CopyFiles(subDir, subTo, recursive);
                }
            }

            // rename old dll files which are used by the updater
            foreach (FileInfo old in destination.GetFiles())
            {
                if (old.Name.Contains("Octokit.dll") || old.Name.Contains("Preferences.dll"))
                {
                    old.MoveTo(Path.Combine(destination.FullName, "OLD" + old.Name), true);
                }
            }
        }

        private static void UpdateFailed(AppDetails app)
        {
            app.statusLabel.Text = updateFailedMessage;
            app.statusLabel.ForeColor = Color.Red;
        }

        /// <summary>
        /// Remove the "NEW" prefix from the file names of the newly installed version
        /// </summary>
        /// <param name="directory">directory in which to rename the files</param>
        /// <param name="Ignore">return true if current file or directory is to be ignored</param>
        /// <param name="recursive">Whether to go into subdirectories</param>
        private static void RenameNewFiles(DirectoryInfo directory, Func<FileSystemInfo, bool> Ignore, bool recursive = true)
        {
            ForAllItemsInDirectory(directory,
                recursive,
                Ignore,
                fsi =>
                {
                    if (fsi.Name.StartsWith("NEW") && fsi is FileInfo)
                    {
                        (fsi as FileInfo).MoveTo(Path.Combine(directory.FullName, fsi.Name.Replace("NEW", "")));
                    }
                });
        }

        /// <summary>
        /// Copy files from the temporary extraction folder to the installation folder
        /// </summary>
        /// <param name="app">move the files of this app</param>
        private static void CopyExtractedFilesToInstallationFolder(AppDetails app)
        {
            DirectoryInfo downloadDirectory = new DirectoryInfo(app.TempDownloadFolder);
            DirectoryInfo installDirectory = new DirectoryInfo(app.InstallationFolder);
            
            // copy the extracted files to the installation folder, add the "NEW" prefic to their names
            CopyFiles(downloadDirectory, installDirectory, true);

            // delete old installation files
            // ignore files which:
            //   - start with "OLD" (dlls used by the updater)
            //   - start with "NEW" (files of the new version)
            //   - start with the name of this assembly (YololFleetsGUI.Updater - files of the updater)
            //   - are on the ignore list in preferences (for example the settings.json file and the replays folder)
            CleanOutFolder(installDirectory, fsi =>
            {
                return
                    fsi.Name.StartsWith("OLD")
                    || fsi.Name.StartsWith("NEW")
                    || fsi.Name.StartsWith(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)
                    || Program.preferences.IgnoreList.Contains(fsi.Name);
            });

            // remove the new prefix of the files
            // ignore files which:
            //   - start with the name of this assembly (YololFleetsGUI.Updater - files of the updater)
            //   - are on the ignore list in preferences (for example the settings.json file and the replays folder)
            RenameNewFiles(installDirectory, fsi =>
            {
                return
                    fsi.Name.StartsWith(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)
                    || Program.preferences.IgnoreList.Contains(fsi.Name);
            });
        }

        private static void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
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


                // Ensure that temporary folders are empty
                await Task.Run(() =>
                {
                    DeleteFolder(app.TempDownloadFolder);
                    DeleteFolder(app.TempExtractionFolder);

                    Directory.CreateDirectory(app.TempDownloadFolder);
                    Directory.CreateDirectory(app.TempExtractionFolder);
                    
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
                await Task.Run(() => CopyExtractedFilesToInstallationFolder(app));

                // Update succesful
                app.statusLabel.Text = updateSuccessfulMessage;
                app.statusLabel.ForeColor = Color.Green;
                app.updateCompleted = true;

                app.LastInstalledVersionId = app.LatestAvailableVersionId;
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
