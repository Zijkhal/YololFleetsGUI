﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using YololFleetsGUI.Preferences;

namespace YololFleetsGUI
{
    public partial class MainWindow : Form
    {
        private bool fleet1Selected = false;
        private bool fleet2Selected = false;

        private Process combatSimulator = null;
        private Process replayPlayer = null;
        private bool combatSimulatorRunning = false;
        private bool replayPlayerRunning = false;

        private string latestReplayPath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Control Events
        private void tbFleet1_Click(object sender, EventArgs e)
        {
            Fleet1Browser.ShowDialog();

            fleet1Selected = true;
            tbFleet1.Text = Fleet1Browser.SelectedPath;

            btnRunBattleSimulation.Enabled = fleet1Selected && fleet2Selected;
        }

        private void tbFleet2_Click(object sender, EventArgs e)
        {
            Fleet2Browser.ShowDialog();

            fleet2Selected = true;
            tbFleet2.Text = Fleet2Browser.SelectedPath;

            btnRunBattleSimulation.Enabled = fleet1Selected && fleet2Selected;
        }

        private void btnRunBattleSimulation_Click(object sender, EventArgs e)
        {
            lblWinner.Text = string.Empty;

            string simulatorPath = Program.preferences.CombatSimulatorFilePath;

            if (File.Exists(simulatorPath))
            {
                try
                {
                    btnRunBattleSimulation.Enabled = false;

                    btnWatchReplay.Enabled = false;

                    StopProcess(combatSimulator, combatSimulatorRunning);

                    combatSimulator = new Process();
                    combatSimulator.StartInfo.FileName = simulatorPath;
                    combatSimulator.StartInfo.Arguments = $"-a {Fleet1Browser.SelectedPath} -b {Fleet2Browser.SelectedPath}";
                    combatSimulator.StartInfo.RedirectStandardOutput = true;
                    combatSimulator.StartInfo.CreateNoWindow = true;
                    combatSimulator.EnableRaisingEvents = true;

                    combatSimulator.OutputDataReceived += new DataReceivedEventHandler(SimulationOutputHandler);
                    combatSimulator.Exited += new EventHandler(CombatSimulatorExited);

                    combatSimulatorRunning = combatSimulator.Start();
                    combatSimulator.BeginOutputReadLine();
                }
                catch (Exception ex)
                {
                    if (combatSimulatorRunning)
                    {
                        combatSimulator?.Kill();
                    }

                    MessageBox.Show($"An error occured while simulating the battle:{Environment.NewLine}{ex.Message}");

                    btnRunBattleSimulation.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Combat simulator not found, go to settings to specify its location!");
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form settings = new Settings();

            settings.Show();
            settings.BringToFront();
            settings.Focus();
        }

        private void btnWatchReplay_Click(object sender, EventArgs e)
        {
            string playerPath = Program.preferences.ReplayPlayerFilePath;

            if (File.Exists(playerPath))
            {
                try
                {
                    StopProcess(replayPlayer, replayPlayerRunning);

                    replayPlayer = new Process();
                    replayPlayer.StartInfo.FileName = playerPath;
                    replayPlayer.StartInfo.Arguments = Path.Combine(latestReplayPath, UserPreferences.defaultReplayFileName);
                    replayPlayer.EnableRaisingEvents = true;

                    replayPlayer.Exited += new EventHandler(ReplayPlayerExited);

                    replayPlayerRunning = replayPlayer.Start();
                }
                catch (Exception ex)
                {
                    if (replayPlayerRunning)
                    {
                        replayPlayer?.Kill();
                    }

                    MessageBox.Show($"An error has occured while trying to open the replay player:{Environment.NewLine}{ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Replay player not found, go to settings to specify its location!");
            }
        }
        #endregion

        #region Helper functions
        private static void StopProcess(Process p, bool running)
        {
            if(running && p!= null && !p.HasExited)
            {
                if (p.Responding)
                {
                    p.CloseMainWindow();

                    p.Close();
                }
                else
                {
                    p.Kill();
                }
            }
        }

        private static string DateTimeToString (DateTime dt)
        {
            return $"{dt.Year}-{dt.Month:00}-{dt.Day:00}_{dt.Hour:00}-{dt.Minute:00}-{dt.Second:00}";
        }

        private void SaveTempReplayToDefaultReplayFolder()
        {
            try
            {
                string fleet1Name = Path.GetFileNameWithoutExtension(tbFleet1.Text);
                string fleet2Name = Path.GetFileNameWithoutExtension(tbFleet2.Text);

                latestReplayPath = Path.Combine(Program.preferences.DefaultReplayFolder, $"{DateTimeToString(DateTime.Now)}_{fleet1Name}_vs_{fleet2Name}");
                Directory.CreateDirectory(latestReplayPath);

                string workingDirectory = Directory.GetCurrentDirectory();

                File.Copy(Path.Combine(workingDirectory, UserPreferences.captainsLogAFileName), Path.Combine(latestReplayPath, $"CaptainsLog_A_{fleet1Name}.txt"));

                File.Copy(Path.Combine(workingDirectory, UserPreferences.captainsLogBFileName), Path.Combine(latestReplayPath, $"CaptainsLog_B_{fleet2Name}.txt"));

                File.Copy(Path.Combine(workingDirectory, UserPreferences.replayFileName), Path.Combine(latestReplayPath,UserPreferences.defaultReplayFileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occured while saving the replay:{Environment.NewLine}{ex.Message}");
            }
        }
        #endregion

        #region Process Event Handlers
        private void SimulationOutputHandler(object sender, DataReceivedEventArgs e)
        {
            string msg = e.Data ?? string.Empty;

            this.BeginInvoke(new MethodInvoker(() =>
            {
                if(msg.Contains("Winner: Draw"))
                {
                    lblWinner.Text = "Draw";
                }
                if (msg.Contains(UserPreferences.winnerMessageMarker))
                {
                    lblWinner.Text = msg.Replace(UserPreferences.winnerMessageMarker, string.Empty).Replace(" - ", string.Empty);
                }
                rtbConsoleOutput.AppendText(msg + Environment.NewLine);
            }));
        }

        private void CombatSimulatorExited(object sender, EventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                btnWatchReplay.Enabled = true;
                combatSimulatorRunning = false;

                SaveTempReplayToDefaultReplayFolder();

                btnRunBattleSimulation.Enabled = true;
            }));
        }

        private void ReplayPlayerExited(object sender, EventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                replayPlayerRunning = false;
            }));
        }
        #endregion

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopProcess(combatSimulator, combatSimulatorRunning);
            //StopProcess(replayPlayer, replayPlayerRunning);
        }
    }
}
