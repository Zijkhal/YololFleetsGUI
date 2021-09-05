using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace YololFleetsGUI
{
    /// <summary>
    /// Handles automatically updating the Simulator, Player, and the GUI
    /// </summary>
    static class AutoUpdater
    {
        private class LatestReleaseInfos
        {
            public Release simulator;
            public Release player;
            public Release gui;
        }

        private static readonly GitHubClient client = new GitHubClient(new ProductHeaderValue("YololFleetsGUI"));
        private static readonly long simulatorID = 366504584L;
        private static readonly long playerID = 367688805L;
        private static readonly long guiID = 376849707L;
        private static readonly TimeSpan timeout = TimeSpan.FromSeconds(15);


        /// <summary>
        /// check for available updates, and apply them unless the user opts out
        /// </summary>
        public static void AutoUpdate()
        {
            Task<LatestReleaseInfos> latestReleaseTask = GetLatestReleaseInfos();
            latestReleaseTask.Wait(timeout);
            LatestReleaseInfos latestReleaseInfos = latestReleaseTask.Result;
        }

        private static async Task<LatestReleaseInfos> GetLatestReleaseInfos()
        {
            Task<Release> simReleaseTask = client.Repository.Release.GetLatest(simulatorID);
            Task<Release> playerReleaseTask = client.Repository.Release.GetLatest(playerID);
            Task<Release> guiReleaseTask = client.Repository.Release.GetLatest(guiID);

            LatestReleaseInfos latestReleaseInfos = new LatestReleaseInfos
            {
                simulator = await simReleaseTask,
                player = await playerReleaseTask,
                gui = await guiReleaseTask
            };

            return latestReleaseInfos;
        }
    }
}
