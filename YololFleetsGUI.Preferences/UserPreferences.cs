using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YololFleetsGUI.Preferences
{
    public class UserPreferences
    {
        /// <summary>
        /// name of the combat simulator executable
        /// </summary>
        public static readonly string combatSimulatorFileName = "SpaceShipCombatSimulator.exe";
        /// <summary>
        /// name of the replay player executable
        /// </summary>
        public static readonly string playerFileName = "SaturnsEnvy.exe";
        /// <summary>
        /// name of the settings file
        /// </summary>
        public static readonly string defaultSettingsFileName = "settings.json";
        /// <summary>
        /// file name used when saving replays
        /// </summary>
        public static readonly string defaultReplayFileName = "replay.json.deflate";
        /// <summary>
        /// used to display winner / loser
        /// </summary>
        public static readonly string winnerMessageMarker = " (VictoryMarker)";
        /// <summary>
        /// file name of the captains log of fleet A
        /// </summary>
        public static readonly string captainsLogAFileName = "CaptainsLog_A.txt";
        /// <summary>
        /// file name of the captains log of fleet B
        /// </summary>
        public static readonly string captainsLogBFileName = "CaptainsLog_B.txt";
        /// <summary>
        /// file name of the replay file
        /// </summary>
        public static readonly string replayFileName = "output.json.deflate";

        /// <summary>
        /// File path to the combat simulator executable
        /// </summary>
        [JsonIgnore]
        public string CombatSimulatorFilePath { get { return Path.Combine(CombatSimulatorPath ?? string.Empty, combatSimulatorFileName); } }
        /// <summary>
        /// File path to the replay player executable
        /// </summary>
        [JsonIgnore]
        public string ReplayPlayerFilePath { get { return Path.Combine(PlayerPath ?? string.Empty, playerFileName); } }
        /// <summary>
        /// Default location of the replay file (if replay destination is not overwritten in launch params of the simulator)
        /// </summary>
        [JsonIgnore]
        public string DefaultReplayPath { get { return Path.Combine(CombatSimulatorPath ?? string.Empty, defaultReplayFileName); } }

        /// <summary>
        /// Path to the folder in which the comat simulator executable is located
        /// </summary>
        public string CombatSimulatorPath { get; set; } = string.Empty;
        /// <summary>
        /// Path to the folder in which the replay player executable is located
        /// </summary>
        public string PlayerPath { get; set; } = string.Empty;
        /// <summary>
        /// Default folder in which to save new replays
        /// </summary>
        public string DefaultReplayFolder
        {
            get { return (defaultReplayFolder ?? string.Empty) != string.Empty ? defaultReplayFolder : Path.Combine(Directory.GetCurrentDirectory(), "Replays"); }
            set { defaultReplayFolder = value ?? string.Empty; }
        }
        private string defaultReplayFolder = string.Empty;

        private string pathToFile = string.Empty;


        #region Auto Update Settings
        private static readonly TimeSpan minTimeBetweenUpdateChecks = TimeSpan.FromHours(3);

        /// <summary>
        /// Whether the installed version of the replay player is the same as the latest available version
        /// </summary>
        [JsonIgnore]
        public bool PlayerUpdatePending
        {
            get
            {
                return LastInstalledPlayerId != LatestPlayerId;
            }
        }
        /// <summary>
        /// Whether the installed version of the simulator is the same as the latest available version
        /// </summary>
        [JsonIgnore]
        public bool SimulatorUpdatePending
        {
            get
            {
                return LastInstalledSimulatorId != LatestSimulatorId;
            }
        }
        /// <summary>
        /// Whether the installed version of the GUI is the same as the latest available version
        /// </summary>
        [JsonIgnore]
        public bool GuiUpdatePending
        {
            get
            {
                return LastInstalledGuiId != LatestGuiId;
            }
        }

        /// <summary>
        /// Whether it is time to check for updates again
        /// </summary>
        [JsonIgnore]
        public bool UpdateCheckDue
        {
            get
            {
                return CheckForUpdatesOnLaunch
                    && ((AutoUpdatePlayer && DateTime.Now - LastPlayerUpdateCheck > minTimeBetweenUpdateChecks)
                        || (AutoUpdateSimulator && DateTime.Now - LastSimulatorUpdateCheck > minTimeBetweenUpdateChecks)
                        || (AutoUpdateGui && DateTime.Now - LastGuiUpdateCheck > minTimeBetweenUpdateChecks))
                    ;
            }
        }

        /// <summary>
        /// Update process should stop itself automatically after this time
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(15);

        /// <summary>
        /// Whether to check for updates when the GUI launches
        /// </summary>
        public bool CheckForUpdatesOnLaunch { get; set; } = true;
        /// <summary>
        /// Whether to check for updates to the replay player
        /// </summary>
        public bool AutoUpdatePlayer { get; set; } = true;
        /// <summary>
        /// Whether to check for updates to the simulator
        /// </summary>
        public bool AutoUpdateSimulator { get; set; } = true;
        /// <summary>
        /// Whether to check for updates to the GUI
        /// </summary>
        public bool AutoUpdateGui { get; set; } = true;

        /// <summary>
        /// Whether to automatically download latest update for the replay player
        /// </summary>
        public bool AutoDownloadPlayerUpdates { get; set; } = false;
        /// <summary>
        /// Whether to automatically download latest update for the simulator
        /// </summary>
        public bool AutoDownloadSimulatorUpdates { get; set; } = false;
        /// <summary>
        /// Whether to automatically download latest update for the GUI
        /// </summary>
        public bool AutoDownloadGuiUpdates { get; set; } = false;

        /// <summary>
        /// Last time the replay player was checked for updates
        /// </summary>
        public DateTime LastPlayerUpdateCheck { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Last time the simulator was checked for updates
        /// </summary>
        public DateTime LastSimulatorUpdateCheck { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Last time the GUI was checked for updates
        /// </summary>
        public DateTime LastGuiUpdateCheck { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Version of the replay player which was last installed by the updater
        /// </summary>
        public int LastInstalledPlayerId { get; set; } = 0;
        /// <summary>
        /// Version of the simulator which was last installed by the updater
        /// </summary>
        public int LastInstalledSimulatorId { get; set; } = 0;
        /// <summary>
        /// Version of the GUI which was last installed by the updater
        /// </summary>
        public int LastInstalledGuiId { get; set; } = 0;

        /// <summary>
        /// Latest available version of the replay player on GitHub
        /// </summary>
        public int LatestPlayerId { get; set; } = 0;
        /// <summary>
        /// Latest available version of the simulator on GitHub
        /// </summary>
        public int LatestSimulatorId { get; set; } = 0;
        /// <summary>
        /// Latest available version of the GUI on GitHub
        /// </summary>
        public int LatestGuiId { get; set; } = 0;
        #endregion



        public static UserPreferences FromJsonText(string jsonText)
        {
            return JsonSerializer.Deserialize<UserPreferences>(jsonText);
        }

        /// <summary>
        /// Initialize a new Preferences class from a json file
        /// </summary>
        /// <param name="settingsFilePath">path to json file</param>
        /// <returns></returns>
        public static UserPreferences FromJsonFile(string settingsFilePath)
        {
            UserPreferences p = File.Exists(settingsFilePath)
                ? FromJsonText(File.ReadAllText(settingsFilePath))
                : new UserPreferences();

            p.pathToFile = settingsFilePath;

            return p;
        }

        /// <summary>
        /// Initialize a new Preferences class from the default json file
        /// </summary>
        /// <returns></returns>
        public static UserPreferences FromDefaultSettingsFile()
        {
            return FromJsonFile(defaultSettingsFileName);
        }

        /// <summary>
        /// Save current preferences to the given file
        /// </summary>
        /// <param name="destination">file to save to</param>
        public void Save(string destination)
        {
            File.WriteAllText(destination, JsonSerializer.Serialize(this));
        }

        /// <summary>
        /// Save current preferences to the file it was loaded from, default file if it was freshly created
        /// </summary>
        public void Save()
        {
            Save(pathToFile != string.Empty ? pathToFile : defaultSettingsFileName);
        }
    }
}
