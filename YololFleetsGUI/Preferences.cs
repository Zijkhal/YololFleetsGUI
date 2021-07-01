using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YololFleetsGUI
{
    class Preferences
    {
        public static Preferences current;

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
        public static readonly string settingsFileName = "settings.json";
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

        [JsonIgnore]
        public string CombatSimulatorFilePath { get { return Path.Combine(CombatSimulatorPath ?? string.Empty, combatSimulatorFileName); } }
        [JsonIgnore]
        public string ReplayPlayerFilePath { get { return Path.Combine(PlayerPath ?? string.Empty, playerFileName); } }
        [JsonIgnore]
        public string DefaultReplayPath { get { return Path.Combine(CombatSimulatorPath ?? string.Empty, defaultReplayFileName); } }

        public string CombatSimulatorPath { get; set; }
        public string PlayerPath { get; set; }
        public string DefaultReplayFolder
        {
            get { return (defaultReplayFolder ?? string.Empty) != string.Empty ? defaultReplayFolder : Path.Combine(Directory.GetCurrentDirectory(), "Replays"); }
            set { defaultReplayFolder = value ?? string.Empty; }
        }
        private string defaultReplayFolder = string.Empty;

        static Preferences()
        {
            current = File.Exists(settingsFileName) ? JsonSerializer.Deserialize<Preferences>(File.ReadAllText(settingsFileName)) : new Preferences();
        }
    }
}
