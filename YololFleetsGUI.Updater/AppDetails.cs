using Octokit;
using System;
using System.IO;
using System.Windows.Forms;

namespace YololFleetsGUI.Updater
{
    enum GitHubIds : long
    {
        Simulator = 366504584L,
        Player = 367688805L,
        GUI = 376849707L
    }

    class AppDetails
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
}
