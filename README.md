# Yolol Fleets GUI

This is a simple GUI for the [Saturn's Envy](https://github.com/martindevans/Yolol-SpaceShipCombatSimulator) game created by [Yolathothep](https://github.com/martindevans). It provides a GUI to select the fleets that battle against each other, and open the [Replay Player](https://github.com/martindevans/Yolol-SpaceCombatPlayer) to view the battle. It also automatically saves the replays and the captain's log files of each battle.

### How to Use

##### Saved Replays
All replays and captain's log files are automatically saved to a new folder inside a specified folder. This folder is named "[date]\_[time]\_[name of fleet A]\_vs\_[name of fleet B]". Inside this folder there are the two captain's log files named "CaptainsLog\_A\_[name of fleet A]" and "CaptainsLog\_B\_[name of fleet B]" respectively. The replay itself is the "replay.json.deflate" file.

##### First Run
At first launch go to the settings, and select the folder where the [Combat Simulator](https://github.com/martindevans/Yolol-SpaceShipCombatSimulator) and the [Replay Player](https://github.com/martindevans/Yolol-SpaceCombatPlayer) are located.
Optionally, select a folder where the replays of each battle will be saved. By default they are saved to the folder YololFleetsGUI is run from.

##### Running a Battle
At the main window specify two fleets to battle each other, to do this, select the fleet's "Root Folder" as described in [Combat Simulator](https://github.com/martindevans/Yolol-SpaceShipCombatSimulator/blob/master/readme.md#Folder-Structure). When both fleets are selected, the "Simulate!" button becomes enabled. Press it to run the battle. While the battle simulator is running, the "Watch Replay" button is disabled - it is enabled once the battle simulator has finished.

##### Viewing the Latest Replay
After the battle has finished, press the "Watch Replay" button to view it in the [Replay Player](https://github.com/martindevans/Yolol-SpaceCombatPlayer).
