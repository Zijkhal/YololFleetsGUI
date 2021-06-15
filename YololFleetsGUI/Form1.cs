using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace YololFleetsGUI
{
    public partial class Form1 : Form
    {
        private bool fleet1Selected = false;
        private bool fleet2Selected = false;

        private Process combatSimulator = null;
        private Process replayPlayer = null;

        public Form1()
        {
            InitializeComponent();
        }

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

        private void SimulationOutputHandler(object sender, DataReceivedEventArgs e)
        {
            string msg = e.Data ?? string.Empty;

            if (msg.Contains(Preferences.winnerMessageMarker))
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    lblWinner.Text = msg.Replace(Preferences.winnerMessageMarker, string.Empty).Replace(" - ", string.Empty);
                }));
            }

            this.BeginInvoke(new MethodInvoker(() =>
            {
                rtbConsoleOutput.AppendText(msg + Environment.NewLine);
            }));
        }

        private void btnRunBattleSimulation_Click(object sender, EventArgs e)
        {
            lblWinner.Text = string.Empty;

            string simulatorPath = Preferences.current.CombatSimulatorFilePath;

            if(File.Exists(simulatorPath))
            {
                try
                {
                    combatSimulator?.Kill();

                    combatSimulator = new Process();
                    combatSimulator.StartInfo.FileName = simulatorPath;
                    combatSimulator.StartInfo.Arguments = $"-a {Fleet1Browser.SelectedPath} -b {Fleet2Browser.SelectedPath} -o {Preferences.current.DefaultReplayPath}";
                    combatSimulator.StartInfo.RedirectStandardOutput = true;
                    combatSimulator.StartInfo.CreateNoWindow = true;

                    combatSimulator.OutputDataReceived += new DataReceivedEventHandler(SimulationOutputHandler);

                    combatSimulator.Start();

                    combatSimulator.BeginOutputReadLine();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured while simulating the battle:{Environment.NewLine}{ex.Message}");

                    combatSimulator?.Kill();
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

        private void btnSaveReplay_Click(object sender, EventArgs e)
        {
            if (File.Exists(Preferences.current.DefaultReplayPath))
            {
                saveReplayDialog.ShowDialog();

                File.Copy(Preferences.current.DefaultReplayPath, saveReplayDialog.FileName);

                saveReplayDialog.FileName = string.Empty;
            }
            else
            {
                MessageBox.Show("replay not found");
            }
        }

        private void saveReplayDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!saveReplayDialog.FileName.EndsWith(saveReplayDialog.DefaultExt))
            {
                MessageBox.Show($"The replay file must have the extension {saveReplayDialog.DefaultExt}");
                e.Cancel = true;
            }
        }

        private void btnCopyReplayPath_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Preferences.current.DefaultReplayPath);
        }

        private void btnOpenPlayer_Click(object sender, EventArgs e)
        {
            string playerPath = Preferences.current.ReplayPlayerFilePath;

            if(File.Exists(playerPath))
            {
                try
                {
                    replayPlayer?.CloseMainWindow();

                    replayPlayer = new Process();
                    replayPlayer.StartInfo.FileName = playerPath;

                    replayPlayer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error has occured while trying to open the replay player:{Environment.NewLine}{ex.Message}");

                    replayPlayer?.Kill();
                }
            }
            else
            {
                MessageBox.Show("Replay player not found, go to settings to specify its location!");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            combatSimulator?.Kill();
            replayPlayer?.Kill();
        }
    }
}
