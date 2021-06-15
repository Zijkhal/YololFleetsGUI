using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YololFleetsGUI
{
    class Preferences
    {
        public static Preferences current;

        public static readonly string combatSimulatorFileName = "SpaceShipCombatSimulator.exe";
        public static readonly string playerFileName = "SaturnsEnvy.exe";
        public static readonly string settingsFileName = "settings.json";
        public static readonly string defaultReplayFileName = "replay.json.deflate";
        public static string ReplayFileExtension { get { return Path.GetExtension(defaultReplayFileName); } }
        public static readonly string winnerMessageMarker = " (VictoryMarker)";
        public static readonly string captainsLogAFileName = "CaptainsLog_A.txt";
        public static readonly string captainsLogBFileName = "CaptainsLog_B.txt";

        [JsonIgnore]
        public string CombatSimulatorFilePath { get { return Path.Combine(CombatSimulatorPath, combatSimulatorFileName); } }
        [JsonIgnore]
        public string ReplayPlayerFilePath { get { return Path.Combine(PlayerPath, playerFileName); } }
        [JsonIgnore]
        public string DefaultReplayPath { get { return Path.Combine(CombatSimulatorPath, defaultReplayFileName); } }

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
