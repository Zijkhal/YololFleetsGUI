
namespace YololFleetsGUI
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSetSimulatorInstallationLocation = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SimulatorInstallationBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lblSimulatorInstallation = new System.Windows.Forms.Label();
            this.lblPlayerInstallation = new System.Windows.Forms.Label();
            this.btnSetPlayerInstallation = new System.Windows.Forms.Button();
            this.PlayerInstallationBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lblSimulatorInstallationError = new System.Windows.Forms.Label();
            this.lblPlayerInstallationError = new System.Windows.Forms.Label();
            this.lblSimulatorInstallationSetting = new System.Windows.Forms.Label();
            this.lblPlayerInstallationPathSetting = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSetSimulatorInstallationLocation
            // 
            this.btnSetSimulatorInstallationLocation.Location = new System.Drawing.Point(12, 27);
            this.btnSetSimulatorInstallationLocation.Name = "btnSetSimulatorInstallationLocation";
            this.btnSetSimulatorInstallationLocation.Size = new System.Drawing.Size(75, 23);
            this.btnSetSimulatorInstallationLocation.TabIndex = 1;
            this.btnSetSimulatorInstallationLocation.Text = "Browse";
            this.btnSetSimulatorInstallationLocation.UseVisualStyleBackColor = true;
            this.btnSetSimulatorInstallationLocation.Click += new System.EventHandler(this.btnSetSimulatorInstallationLocation_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(713, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SimulatorInstallationBrowser
            // 
            this.SimulatorInstallationBrowser.ShowNewFolderButton = false;
            // 
            // lblSimulatorInstallation
            // 
            this.lblSimulatorInstallation.AutoSize = true;
            this.lblSimulatorInstallation.ForeColor = System.Drawing.Color.Gray;
            this.lblSimulatorInstallation.Location = new System.Drawing.Point(93, 31);
            this.lblSimulatorInstallation.Name = "lblSimulatorInstallation";
            this.lblSimulatorInstallation.Size = new System.Drawing.Size(38, 15);
            this.lblSimulatorInstallation.TabIndex = 3;
            this.lblSimulatorInstallation.Text = "label1";
            // 
            // lblPlayerInstallation
            // 
            this.lblPlayerInstallation.AutoSize = true;
            this.lblPlayerInstallation.ForeColor = System.Drawing.Color.Gray;
            this.lblPlayerInstallation.Location = new System.Drawing.Point(94, 84);
            this.lblPlayerInstallation.Name = "lblPlayerInstallation";
            this.lblPlayerInstallation.Size = new System.Drawing.Size(38, 15);
            this.lblPlayerInstallation.TabIndex = 5;
            this.lblPlayerInstallation.Text = "label1";
            // 
            // btnSetPlayerInstallation
            // 
            this.btnSetPlayerInstallation.Location = new System.Drawing.Point(13, 80);
            this.btnSetPlayerInstallation.Name = "btnSetPlayerInstallation";
            this.btnSetPlayerInstallation.Size = new System.Drawing.Size(75, 23);
            this.btnSetPlayerInstallation.TabIndex = 4;
            this.btnSetPlayerInstallation.Text = "Browse";
            this.btnSetPlayerInstallation.UseVisualStyleBackColor = true;
            this.btnSetPlayerInstallation.Click += new System.EventHandler(this.btnSetPlayerInstallation_Click);
            // 
            // PlayerInstallationBrowser
            // 
            this.PlayerInstallationBrowser.ShowNewFolderButton = false;
            // 
            // lblSimulatorInstallationError
            // 
            this.lblSimulatorInstallationError.AutoSize = true;
            this.lblSimulatorInstallationError.ForeColor = System.Drawing.Color.Red;
            this.lblSimulatorInstallationError.Location = new System.Drawing.Point(210, 9);
            this.lblSimulatorInstallationError.Name = "lblSimulatorInstallationError";
            this.lblSimulatorInstallationError.Size = new System.Drawing.Size(0, 15);
            this.lblSimulatorInstallationError.TabIndex = 6;
            // 
            // lblPlayerInstallationError
            // 
            this.lblPlayerInstallationError.AutoSize = true;
            this.lblPlayerInstallationError.ForeColor = System.Drawing.Color.Red;
            this.lblPlayerInstallationError.Location = new System.Drawing.Point(183, 62);
            this.lblPlayerInstallationError.Name = "lblPlayerInstallationError";
            this.lblPlayerInstallationError.Size = new System.Drawing.Size(0, 15);
            this.lblPlayerInstallationError.TabIndex = 7;
            // 
            // lblSimulatorInstallationSetting
            // 
            this.lblSimulatorInstallationSetting.AutoSize = true;
            this.lblSimulatorInstallationSetting.Location = new System.Drawing.Point(12, 9);
            this.lblSimulatorInstallationSetting.Name = "lblSimulatorInstallationSetting";
            this.lblSimulatorInstallationSetting.Size = new System.Drawing.Size(192, 15);
            this.lblSimulatorInstallationSetting.TabIndex = 8;
            this.lblSimulatorInstallationSetting.Text = "Combat Simulator installation path";
            // 
            // lblPlayerInstallationPathSetting
            // 
            this.lblPlayerInstallationPathSetting.AutoSize = true;
            this.lblPlayerInstallationPathSetting.Location = new System.Drawing.Point(12, 62);
            this.lblPlayerInstallationPathSetting.Name = "lblPlayerInstallationPathSetting";
            this.lblPlayerInstallationPathSetting.Size = new System.Drawing.Size(165, 15);
            this.lblPlayerInstallationPathSetting.TabIndex = 9;
            this.lblPlayerInstallationPathSetting.Text = "Replay Player installation path";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPlayerInstallationPathSetting);
            this.Controls.Add(this.lblSimulatorInstallationSetting);
            this.Controls.Add(this.lblPlayerInstallationError);
            this.Controls.Add(this.lblSimulatorInstallationError);
            this.Controls.Add(this.lblPlayerInstallation);
            this.Controls.Add(this.btnSetPlayerInstallation);
            this.Controls.Add(this.lblSimulatorInstallation);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSetSimulatorInstallationLocation);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSetSimulatorInstallationLocation;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog SimulatorInstallationBrowser;
        private System.Windows.Forms.Label lblSimulatorInstallation;
        private System.Windows.Forms.Label lblPlayerInstallation;
        private System.Windows.Forms.Button btnSetPlayerInstallation;
        private System.Windows.Forms.FolderBrowserDialog PlayerInstallationBrowser;
        private System.Windows.Forms.Label lblSimulatorInstallationError;
        private System.Windows.Forms.Label lblPlayerInstallationError;
        private System.Windows.Forms.Label lblSimulatorInstallationSetting;
        private System.Windows.Forms.Label lblPlayerInstallationPathSetting;
    }
}