using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace YololFleetsGUI.Updater
{
    static class AutoUpdater
    {
        public enum GitHubIds : long
        {
            Simulator = 366504584L,
            Player = 367688805L,
            GUI = 376849707L
        }

        private static readonly GitHubClient client = new GitHubClient(new ProductHeaderValue("YololFleetsGUI"));
        private static readonly TimeSpan timeout = TimeSpan.FromSeconds(15);

        static AutoUpdater()
        {
            client.SetRequestTimeout(timeout);
        }

        public static Task<Release> GetLatestReleaseInfo(GitHubIds gitHubId)
        {
            return client.Repository.Release.GetLatest((long)gitHubId);
        }
    }
}
