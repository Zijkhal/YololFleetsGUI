
namespace YololFleetsGUI.Updater
{
    partial class frmUpdateWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSimulator = new System.Windows.Forms.Panel();
            this.btnInstallSimulatorUpdate = new System.Windows.Forms.Button();
            this.lblSimulatorUpdateStatus = new System.Windows.Forms.Label();
            this.lblSimulator = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInstallPlayerUpdate = new System.Windows.Forms.Button();
            this.lblPlayerUpdateStatus = new System.Windows.Forms.Label();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnInstallGuiUpdate = new System.Windows.Forms.Button();
            this.lblGuiUpdateStatus = new System.Windows.Forms.Label();
            this.lblGui = new System.Windows.Forms.Label();
            this.btnInstallAllUpdates = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblMessages = new System.Windows.Forms.Label();
            this.pnlSimulator.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSimulator
            // 
            this.pnlSimulator.Controls.Add(this.btnInstallSimulatorUpdate);
            this.pnlSimulator.Controls.Add(this.lblSimulatorUpdateStatus);
            this.pnlSimulator.Controls.Add(this.lblSimulator);
            this.pnlSimulator.Location = new System.Drawing.Point(12, 12);
            this.pnlSimulator.Name = "pnlSimulator";
            this.pnlSimulator.Size = new System.Drawing.Size(372, 27);
            this.pnlSimulator.TabIndex = 0;
            // 
            // btnInstallSimulatorUpdate
            // 
            this.btnInstallSimulatorUpdate.Enabled = false;
            this.btnInstallSimulatorUpdate.Location = new System.Drawing.Point(278, 0);
            this.btnInstallSimulatorUpdate.Name = "btnInstallSimulatorUpdate";
            this.btnInstallSimulatorUpdate.Size = new System.Drawing.Size(94, 23);
            this.btnInstallSimulatorUpdate.TabIndex = 2;
            this.btnInstallSimulatorUpdate.Text = "install ";
            this.btnInstallSimulatorUpdate.UseVisualStyleBackColor = true;
            this.btnInstallSimulatorUpdate.Click += new System.EventHandler(this.btnInstallSimulatorUpdate_Click);
            // 
            // lblSimulatorUpdateStatus
            // 
            this.lblSimulatorUpdateStatus.AutoSize = true;
            this.lblSimulatorUpdateStatus.ForeColor = System.Drawing.Color.Black;
            this.lblSimulatorUpdateStatus.Location = new System.Drawing.Point(70, 0);
            this.lblSimulatorUpdateStatus.Name = "lblSimulatorUpdateStatus";
            this.lblSimulatorUpdateStatus.Size = new System.Drawing.Size(118, 15);
            this.lblSimulatorUpdateStatus.TabIndex = 1;
            this.lblSimulatorUpdateStatus.Text = "checking for updates";
            // 
            // lblSimulator
            // 
            this.lblSimulator.AutoSize = true;
            this.lblSimulator.Location = new System.Drawing.Point(0, 0);
            this.lblSimulator.Name = "lblSimulator";
            this.lblSimulator.Size = new System.Drawing.Size(61, 15);
            this.lblSimulator.TabIndex = 0;
            this.lblSimulator.Text = "Simulator:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInstallPlayerUpdate);
            this.panel1.Controls.Add(this.lblPlayerUpdateStatus);
            this.panel1.Controls.Add(this.lblPlayer);
            this.panel1.Location = new System.Drawing.Point(12, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 27);
            this.panel1.TabIndex = 3;
            // 
            // btnInstallPlayerUpdate
            // 
            this.btnInstallPlayerUpdate.Enabled = false;
            this.btnInstallPlayerUpdate.Location = new System.Drawing.Point(278, 0);
            this.btnInstallPlayerUpdate.Name = "btnInstallPlayerUpdate";
            this.btnInstallPlayerUpdate.Size = new System.Drawing.Size(94, 23);
            this.btnInstallPlayerUpdate.TabIndex = 2;
            this.btnInstallPlayerUpdate.Text = "install ";
            this.btnInstallPlayerUpdate.UseVisualStyleBackColor = true;
            this.btnInstallPlayerUpdate.Click += new System.EventHandler(this.btnInstallPlayerUpdate_Click);
            // 
            // lblPlayerUpdateStatus
            // 
            this.lblPlayerUpdateStatus.AutoSize = true;
            this.lblPlayerUpdateStatus.Location = new System.Drawing.Point(70, 0);
            this.lblPlayerUpdateStatus.Name = "lblPlayerUpdateStatus";
            this.lblPlayerUpdateStatus.Size = new System.Drawing.Size(118, 15);
            this.lblPlayerUpdateStatus.TabIndex = 1;
            this.lblPlayerUpdateStatus.Text = "checking for updates";
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Location = new System.Drawing.Point(0, 0);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(42, 15);
            this.lblPlayer.TabIndex = 0;
            this.lblPlayer.Text = "Player:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnInstallGuiUpdate);
            this.panel2.Controls.Add(this.lblGuiUpdateStatus);
            this.panel2.Controls.Add(this.lblGui);
            this.panel2.Location = new System.Drawing.Point(12, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(372, 27);
            this.panel2.TabIndex = 4;
            // 
            // btnInstallGuiUpdate
            // 
            this.btnInstallGuiUpdate.Enabled = false;
            this.btnInstallGuiUpdate.Location = new System.Drawing.Point(278, 0);
            this.btnInstallGuiUpdate.Name = "btnInstallGuiUpdate";
            this.btnInstallGuiUpdate.Size = new System.Drawing.Size(94, 23);
            this.btnInstallGuiUpdate.TabIndex = 2;
            this.btnInstallGuiUpdate.Text = "install ";
            this.btnInstallGuiUpdate.UseVisualStyleBackColor = true;
            this.btnInstallGuiUpdate.Click += new System.EventHandler(this.btnInstallGuiUpdate_Click);
            // 
            // lblGuiUpdateStatus
            // 
            this.lblGuiUpdateStatus.AutoSize = true;
            this.lblGuiUpdateStatus.Location = new System.Drawing.Point(70, 0);
            this.lblGuiUpdateStatus.Name = "lblGuiUpdateStatus";
            this.lblGuiUpdateStatus.Size = new System.Drawing.Size(118, 15);
            this.lblGuiUpdateStatus.TabIndex = 1;
            this.lblGuiUpdateStatus.Text = "checking for updates";
            // 
            // lblGui
            // 
            this.lblGui.AutoSize = true;
            this.lblGui.Location = new System.Drawing.Point(0, 0);
            this.lblGui.Name = "lblGui";
            this.lblGui.Size = new System.Drawing.Size(29, 15);
            this.lblGui.TabIndex = 0;
            this.lblGui.Text = "GUI:";
            // 
            // btnInstallAllUpdates
            // 
            this.btnInstallAllUpdates.Enabled = false;
            this.btnInstallAllUpdates.Location = new System.Drawing.Point(290, 111);
            this.btnInstallAllUpdates.Name = "btnInstallAllUpdates";
            this.btnInstallAllUpdates.Size = new System.Drawing.Size(94, 23);
            this.btnInstallAllUpdates.TabIndex = 5;
            this.btnInstallAllUpdates.Text = "install all";
            this.btnInstallAllUpdates.UseVisualStyleBackColor = true;
            this.btnInstallAllUpdates.Click += new System.EventHandler(this.btnInstallAllUpdates_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(411, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "Skip Updates";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblMessages
            // 
            this.lblMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMessages.AutoSize = true;
            this.lblMessages.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblMessages.Location = new System.Drawing.Point(12, 221);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(274, 15);
            this.lblMessages.TabIndex = 7;
            this.lblMessages.Text = "an error has occured while deleting temporary files";
            this.lblMessages.Visible = false;
            // 
            // frmUpdateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 257);
            this.Controls.Add(this.lblMessages);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnInstallAllUpdates);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlSimulator);
            this.Name = "frmUpdateWindow";
            this.Text = "Yolol Fleets GUI Updater";
            this.Load += new System.EventHandler(this.frmUpdateWindow_Load);
            this.pnlSimulator.ResumeLayout(false);
            this.pnlSimulator.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSimulator;
        private System.Windows.Forms.Button btnInstallSimulatorUpdate;
        private System.Windows.Forms.Label lblSimulatorUpdateStatus;
        private System.Windows.Forms.Label lblSimulator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnInstallPlayerUpdate;
        private System.Windows.Forms.Label lblPlayerUpdateStatus;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnInstallGuiUpdate;
        private System.Windows.Forms.Label lblGuiUpdateStatus;
        private System.Windows.Forms.Label lblGui;
        private System.Windows.Forms.Button btnInstallAllUpdates;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblMessages;
    }
}

