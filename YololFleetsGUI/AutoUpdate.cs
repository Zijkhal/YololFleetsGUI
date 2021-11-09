using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;

namespace YololFleetsGUI
{
    /// <summary>
    /// starter window which handles automatic updates
    /// </summary>
    public partial class AutoUpdate : Form
    {
        private static readonly TimeSpan timeout = TimeSpan.FromSeconds(15);

        private static readonly ProgramDetails simulatorDetails = new ProgramDetails(
            366504584L, 
            Program.preferences.CombatSimulatorPath,
            Program.preferences.lastSimulatorReleaseID,
            "SpaceShipCombatSimulator.Win.zip",
            "SimulatorTemp"
        );
        private static readonly ProgramDetails playerDetails = new ProgramDetails(
            367688805L,
            Program.preferences.PlayerPath,
            Program.preferences.lastPlayerReleaseID,
            "SpaceCombatPlayer.Win.zip",
            "PlayerTemp"
        );
        private static readonly ProgramDetails guiDetails = new ProgramDetails(
            376849707L,
            Directory.GetCurrentDirectory(),
            Program.preferences.lastGUIReleaseID,
            "YololFleetsGUI.Win.zip",
            "GUITemp"
        );

        private class ProgramDetails
        {
            /// <summary>
            /// id of the github repository of the program to update
            /// </summary>
            public readonly long RepositoryID;
            /// <summary>
            /// folder of the currently installed version
            /// </summary>
            public readonly string folder;
            /// <summary>
            /// asset id of the last downloaded release
            /// </summary>
            public readonly long latestDownloadedID;
            /// <summary>
            /// name of the asset to find in the release files
            /// </summary>
            public readonly string assetName;
            /// <summary>
            /// temporary name to use for downloading and extracting
            /// </summary>
            public readonly string tempname;

            public string TempDownloadFilePath { get { 
                    return Path.Combine(Directory.GetParent(folder).FullName, tempname + ".zip"); 
                } }
            public string TempExtractionFolder { get
                {
                    return Path.Combine(Directory.GetParent(folder).FullName, tempname);
                } }
            public string TempExtractedMainFolder { get
                {
                    return Path.Combine(TempExtractionFolder, Path.GetFileName(folder));
                } }

            public ProgramDetails(long ID, string folder, long latestDownloadedID, 
                string assetName, string tempname)
            {
                this.RepositoryID = ID;
                this.folder = folder;
                this.latestDownloadedID = latestDownloadedID;
                this.assetName = assetName;
                this.tempname = tempname;
            }
        }

        // token source used to cancel the update tasks
        private CancellationTokenSource updateCancellationSource = new CancellationTokenSource();
        private WebClient[] downloadClient = new WebClient[3];

        public AutoUpdate()
        {
            InitializeComponent();
        }

        // check for updates and apply them if user does not opt out
        private void AutoUpdate_Load(object sender, EventArgs e)
        {
            PerformUpdates();
        }

        private void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private async Task CheckAndUpdateProgram(ProgramDetails programDetails, int downloadClientIdx)
        {
            GitHubClient ghClient = new GitHubClient(new ProductHeaderValue("YololFleetsGUI"));
            ghClient.SetRequestTimeout(timeout);

            Task<Release> releaseTask = ghClient.Repository.Release.GetLatest(programDetails.RepositoryID);
            Release latestReleaseInfo = await releaseTask;

            if (updateCancellationSource.Token.IsCancellationRequested)
            {
                return;
            }

            if (latestReleaseInfo.Id == programDetails.latestDownloadedID)
            {
                // no need to update
                return;
            }

            // download and apply update

            // Find binary to install
            string downloadUrl = string.Empty;
            foreach (ReleaseAsset asset in latestReleaseInfo.Assets)
            {
                if (asset.Name == programDetails.assetName)
                {
                    // downloadURL may need to be manually assembled (user/project/assetId)
                    downloadUrl = asset.BrowserDownloadUrl;
                    break;
                }
            }

            if (updateCancellationSource.Token.IsCancellationRequested)
            {
                return;
            }

            if (downloadUrl == string.Empty)
            {
                // asset not found
                return;
            }

            // Correct binary is found, install it

            using (WebClient client = new WebClient())
            {
                downloadClient[downloadClientIdx] = client;

                await Task.Run(() => DeleteFileIfExists(programDetails.TempDownloadFilePath));

                await client.DownloadFileTaskAsync(new Uri(downloadUrl),
                    programDetails.TempDownloadFilePath);

                downloadClient[downloadClientIdx] = null;
            }

            if (!updateCancellationSource.Token.IsCancellationRequested)
            {
                if (Directory.Exists(programDetails.TempExtractionFolder))
                {
                    Directory.Delete(programDetails.TempExtractionFolder, true);
                }

                await Task.Run(() => ZipFile.ExtractToDirectory(programDetails.TempDownloadFilePath, 
                                                                programDetails.TempExtractionFolder));

                Directory.Delete(programDetails.folder, true);
                Directory.Move(programDetails.TempExtractedMainFolder, programDetails.folder);
                Directory.Delete(programDetails.TempExtractionFolder, true);
            }            
        }

        private async void PerformUpdates()
        {
            await CheckAndUpdateProgram(simulatorDetails, 0);
            await CheckAndUpdateProgram(playerDetails, 1);
            await CheckAndUpdateProgram(guiDetails, 2);
        }

        private void CancelTasks()
        {
            foreach (WebClient wc in downloadClient)
            {
                wc?.CancelAsync();
            }
        }
    }
}
