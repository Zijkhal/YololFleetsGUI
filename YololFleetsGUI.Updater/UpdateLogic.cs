using Octokit;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace YololFleetsGUI.Updater
{
    class UpdateLogic
    {
        public delegate void UpdateEventsEventHandler(AppDetails app);

        /// <summary>
        /// invoked right before checking for updates
        /// </summary>
        public event UpdateEventsEventHandler UpdateCheckStart;
        /// <summary>
        /// invoked right after checking for updates has completed
        /// </summary>
        public event UpdateEventsEventHandler UpdateCheckComplete;
        /// <summary>
        /// invoked when the new version to download has been found among the assets
        /// </summary>
        public event UpdateEventsEventHandler UpdateFound;
        /// <summary>
        /// invoked right before beginning to download an update
        /// </summary>
        public event UpdateEventsEventHandler UpdateDownloadStart;
        /// <summary>
        /// invoked right after finished downloading an update
        /// </summary>
        public event UpdateEventsEventHandler UpdateDowloadFinished;
        /// <summary>
        /// invoked right before beginning to extract downloaded update
        /// </summary>
        public event UpdateEventsEventHandler UpdateExtractionStart;
        /// <summary>
        /// invoked right after finished extracting downloaded update
        /// </summary>
        public event UpdateEventsEventHandler UpdateExtractionFinished;
        /// <summary>
        /// invoked right before beginning to copy extracted files to installation folder
        /// </summary>
        public event UpdateEventsEventHandler UpdateCopyFilesStart;
        /// <summary>
        /// invoked right after finished copying extracted files to installation folder
        /// </summary>
        public event UpdateEventsEventHandler UpdateCopyFilesFinished;
        /// <summary>
        /// invoked if the update process has failed for some reason
        /// </summary>
        public event UpdateEventsEventHandler UpdateFailed;
        /// <summary>
        /// invoked if update has been completed succesfully
        /// </summary>
        public event UpdateEventsEventHandler UpdateCompleted;
        /// <summary>
        /// invoked if failed to clean up the temporary files and folders used during the update process
        /// </summary>
        public event UpdateEventsEventHandler AfterUpdateCleanupFailed;

        AppDetails playerDetails;
        AppDetails simulatorDetails;
        AppDetails guiDetails;

        public bool InstallAll { get; set; }

        private static readonly TimeSpan timeout = TimeSpan.FromSeconds(15);

        private readonly GitHubClient client = new GitHubClient(new ProductHeaderValue("YololFleetsGUI"));

        public UpdateLogic(AppDetails playerDetails, AppDetails simulatorDetails, AppDetails guiDetails)
        {
            this.playerDetails = playerDetails;
            this.simulatorDetails = simulatorDetails;
            this.guiDetails = guiDetails;

            client.SetRequestTimeout(timeout);
        }

        /// <summary>
        /// Check the relevant GitHub repo for new releases
        /// </summary>
        /// <param name="gitHubId">GitHub repo to check for an update</param>
        public void CheckForUpdate(GitHubIds gitHubId)
        {
            CheckForUpdate(gitHubId switch
            {
                GitHubIds.Player => playerDetails,
                GitHubIds.Simulator => simulatorDetails,
                GitHubIds.GUI => guiDetails,
                _ => throw new ArgumentException()
            });
        }

        /// <summary>
        /// Check the relevant GitHub repo for new releases
        /// </summary>
        /// <param name="app">the app to check for updates</param>
        private async void CheckForUpdate(AppDetails app)
        {
            try
            {
                UpdateCheckStart?.Invoke(app);
                app.releaseInfo = await client.Repository.Release.GetLatest((long)app.gitHubId);

                // update id of latest available release
                app.LatestAvailableVersionId = app.releaseInfo.Id;
                app.LastUpdateCheck = DateTime.Now;

                UpdateCheckComplete?.Invoke(app);
            }
            catch
            {
                UpdateFailed?.Invoke(app);
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
                    if (recursive && fsi is DirectoryInfo)
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

        /// <summary>
        /// Updates the specified app to the newest version previously found on GitHub
        /// </summary>
        /// <param name="gitHubId">app to update</param>
        public void UpdateApp(GitHubIds gitHubId)
        {
            UpdateApp(
                gitHubId switch
                {
                    GitHubIds.Player => playerDetails,
                    GitHubIds.Simulator => simulatorDetails,
                    GitHubIds.GUI => guiDetails,
                    _ => throw new ArgumentException()
                });
        }

        /// <summary>
        /// Updates the specified app to the newest version previously found on GitHub
        /// </summary>
        /// <param name="app">app to update</param>
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
                // will throw an exception of can not find the asset to download
                string downloadUrl = app.releaseInfo.Assets.First(x => x.Name == app.AssetName).BrowserDownloadUrl;
                UpdateFound?.Invoke(app);

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
                    UpdateDownloadStart?.Invoke(app);
                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), app.TempDownloadFilePath);
                    UpdateDowloadFinished?.Invoke(app);
                }

                // extract files
                UpdateExtractionStart?.Invoke(app);
                await Task.Run(() => ZipFile.ExtractToDirectory(app.TempDownloadFilePath, app.TempExtractionFolder));
                UpdateExtractionFinished?.Invoke(app);

                // copy files
                // if copying Preferences.dll or Octokit.dll, rename them first
                // if copying items whose name starts with YololFleetsGui.Updater, copy with an alternative name
                // main app on startup will handle deleting old versions, and renaming alternative versions to the correct ones
                UpdateCopyFilesStart?.Invoke(app);
                await Task.Run(() => CopyExtractedFilesToInstallationFolder(app));
                UpdateCopyFilesFinished?.Invoke(app);

                // Update succesful
                app.updateCompleted = true;
                app.LastInstalledVersionId = app.LatestAvailableVersionId;
                UpdateCompleted?.Invoke(app);
            }
            catch
            {
                UpdateFailed?.Invoke(app);
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
                AfterUpdateCleanupFailed?.Invoke(app);
            }

            app.updateInProgress = false;
        }
    }
}
