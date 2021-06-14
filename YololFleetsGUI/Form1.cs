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

        private void btnRunBattleSimulation_Click(object sender, EventArgs e)
        {
            string simulatorPath = Preferences.current.CombatSimulatorFilePath;

            if(simulatorPath != string.Empty)
            {
                Process simulation = new Process();
                simulation.StartInfo.FileName = simulatorPath;
                simulation.StartInfo.Arguments = $"-a {Fleet1Browser.SelectedPath} -b {Fleet2Browser.SelectedPath}";

                simulation.Start();
            }
            else
            {
                MessageBox.Show("Combat simulator is not found, go to settings to specify it's location!");
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form settings = new Settings();

            settings.Show();
            settings.BringToFront();
            settings.Focus();
        }
    }
}
