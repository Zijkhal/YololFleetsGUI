using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static readonly string winnerMessageMarker = " (VictoryMarker)";

        [JsonIgnore]
        public string CombatSimulatorFilePath { get { string path = $@"{CombatSimulatorPath}\{combatSimulatorFileName}"; return File.Exists(path) ? path : string.Empty; } }
        [JsonIgnore]
        public string ReplayPlayerFilePath { get { string path = $@"{PlayerPath}\{playerFileName}"; return File.Exists(path) ? path : string.Empty; } }
        [JsonIgnore]
        public string DefaultReplayPath { get { return $@"{CombatSimulatorPath}\{defaultReplayFileName}"; } }

        public string CombatSimulatorPath { get; set; }
        public string PlayerPath { get; set; }

        static Preferences()
        {
            current = File.Exists(settingsFileName) ? JsonSerializer.Deserialize<Preferences>(File.ReadAllText(settingsFileName)) : new Preferences();
        }

        public bool SetCombatSimulatorPath (string path)
        {
            bool validPath = File.Exists($@"{path}\{combatSimulatorFileName}");

            if (validPath)
            {
                CombatSimulatorPath = path;
            }
            return validPath;
        }

        public bool SetReplayPayerPath(string path)
        {
            bool validPath = File.Exists($@"{path}\{playerFileName}");

            if (validPath)
            {
                PlayerPath = path;
            }
            return validPath;
        }
    }
}
