using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
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

            if (msg.Contains(Preferences.winnerMessagePrefix))
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    lblWinner.Text = msg;
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

            if(simulatorPath != string.Empty)
            {
                Process simulation = new Process();
                simulation.StartInfo.FileName = simulatorPath;
                string outputFilePath = $@"{Preferences.current.CombatSimulatorPath}\{Preferences.defaultReplayFileName}";
                simulation.StartInfo.Arguments = $"-a {Fleet1Browser.SelectedPath} -b {Fleet2Browser.SelectedPath} -o {outputFilePath}";
                simulation.StartInfo.RedirectStandardOutput = true;
                simulation.StartInfo.CreateNoWindow = true;

                simulation.OutputDataReceived += new DataReceivedEventHandler(SimulationOutputHandler);

                simulation.Start();

                simulation.BeginOutputReadLine();
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
            string replayToSave = $@"{Preferences.current.CombatSimulatorPath}\{Preferences.defaultReplayFileName}";

            if (File.Exists(replayToSave))
            {
                saveReplayDialog.ShowDialog();

                File.Copy(replayToSave, saveReplayDialog.FileName);

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
    }
}
