
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
            this.lblSimulatorInstallationTitle = new System.Windows.Forms.Label();
            this.lblPlayerInstallationTitle = new System.Windows.Forms.Label();
            this.lblDefaultReplaysFolderTitle = new System.Windows.Forms.Label();
            this.lblDefaultReplayFolder = new System.Windows.Forms.Label();
            this.btnSetDefaultReplayFolder = new System.Windows.Forms.Button();
            this.DefaultReplayFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSetSimulatorInstallationLocation
            // 
            this.btnSetSimulatorInstallationLocation.Location = new System.Drawing.Point(14, 36);
            this.btnSetSimulatorInstallationLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetSimulatorInstallationLocation.Name = "btnSetSimulatorInstallationLocation";
            this.btnSetSimulatorInstallationLocation.Size = new System.Drawing.Size(86, 31);
            this.btnSetSimulatorInstallationLocation.TabIndex = 1;
            this.btnSetSimulatorInstallationLocation.Text = "Browse";
            this.btnSetSimulatorInstallationLocation.UseVisualStyleBackColor = true;
            this.btnSetSimulatorInstallationLocation.Click += new System.EventHandler(this.btnSetSimulatorInstallationLocation_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(815, 553);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 31);
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
            this.lblSimulatorInstallation.Location = new System.Drawing.Point(106, 41);
            this.lblSimulatorInstallation.Name = "lblSimulatorInstallation";
            this.lblSimulatorInstallation.Size = new System.Drawing.Size(50, 20);
            this.lblSimulatorInstallation.TabIndex = 3;
            this.lblSimulatorInstallation.Text = "label1";
            // 
            // lblPlayerInstallation
            // 
            this.lblPlayerInstallation.AutoSize = true;
            this.lblPlayerInstallation.ForeColor = System.Drawing.Color.Gray;
            this.lblPlayerInstallation.Location = new System.Drawing.Point(107, 112);
            this.lblPlayerInstallation.Name = "lblPlayerInstallation";
            this.lblPlayerInstallation.Size = new System.Drawing.Size(50, 20);
            this.lblPlayerInstallation.TabIndex = 5;
            this.lblPlayerInstallation.Text = "label1";
            // 
            // btnSetPlayerInstallation
            // 
            this.btnSetPlayerInstallation.Location = new System.Drawing.Point(15, 107);
            this.btnSetPlayerInstallation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetPlayerInstallation.Name = "btnSetPlayerInstallation";
            this.btnSetPlayerInstallation.Size = new System.Drawing.Size(86, 31);
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
            this.lblSimulatorInstallationError.Location = new System.Drawing.Point(240, 12);
            this.lblSimulatorInstallationError.Name = "lblSimulatorInstallationError";
            this.lblSimulatorInstallationError.Size = new System.Drawing.Size(0, 20);
            this.lblSimulatorInstallationError.TabIndex = 6;
            // 
            // lblPlayerInstallationError
            // 
            this.lblPlayerInstallationError.AutoSize = true;
            this.lblPlayerInstallationError.ForeColor = System.Drawing.Color.Red;
            this.lblPlayerInstallationError.Location = new System.Drawing.Point(209, 83);
            this.lblPlayerInstallationError.Name = "lblPlayerInstallationError";
            this.lblPlayerInstallationError.Size = new System.Drawing.Size(0, 20);
            this.lblPlayerInstallationError.TabIndex = 7;
            // 
            // lblSimulatorInstallationTitle
            // 
            this.lblSimulatorInstallationTitle.AutoSize = true;
            this.lblSimulatorInstallationTitle.Location = new System.Drawing.Point(14, 12);
            this.lblSimulatorInstallationTitle.Name = "lblSimulatorInstallationTitle";
            this.lblSimulatorInstallationTitle.Size = new System.Drawing.Size(241, 20);
            this.lblSimulatorInstallationTitle.TabIndex = 8;
            this.lblSimulatorInstallationTitle.Text = "Combat Simulator installation path";
            // 
            // lblPlayerInstallationTitle
            // 
            this.lblPlayerInstallationTitle.AutoSize = true;
            this.lblPlayerInstallationTitle.Location = new System.Drawing.Point(14, 83);
            this.lblPlayerInstallationTitle.Name = "lblPlayerInstallationTitle";
            this.lblPlayerInstallationTitle.Size = new System.Drawing.Size(209, 20);
            this.lblPlayerInstallationTitle.TabIndex = 9;
            this.lblPlayerInstallationTitle.Text = "Replay Player installation path";
            // 
            // lblDefaultReplaysFolderTitle
            // 
            this.lblDefaultReplaysFolderTitle.AutoSize = true;
            this.lblDefaultReplaysFolderTitle.Location = new System.Drawing.Point(14, 152);
            this.lblDefaultReplaysFolderTitle.Name = "lblDefaultReplaysFolderTitle";
            this.lblDefaultReplaysFolderTitle.Size = new System.Drawing.Size(246, 20);
            this.lblDefaultReplaysFolderTitle.TabIndex = 13;
            this.lblDefaultReplaysFolderTitle.Text = "Automatically Saved Replays Folder";
            // 
            // lblDefaultReplayFolder
            // 
            this.lblDefaultReplayFolder.AutoSize = true;
            this.lblDefaultReplayFolder.ForeColor = System.Drawing.Color.Gray;
            this.lblDefaultReplayFolder.Location = new System.Drawing.Point(107, 182);
            this.lblDefaultReplayFolder.Name = "lblDefaultReplayFolder";
            this.lblDefaultReplayFolder.Size = new System.Drawing.Size(50, 20);
            this.lblDefaultReplayFolder.TabIndex = 11;
            this.lblDefaultReplayFolder.Text = "label1";
            // 
            // btnSetDefaultReplayFolder
            // 
            this.btnSetDefaultReplayFolder.Location = new System.Drawing.Point(15, 176);
            this.btnSetDefaultReplayFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetDefaultReplayFolder.Name = "btnSetDefaultReplayFolder";
            this.btnSetDefaultReplayFolder.Size = new System.Drawing.Size(86, 31);
            this.btnSetDefaultReplayFolder.TabIndex = 10;
            this.btnSetDefaultReplayFolder.Text = "Browse";
            this.btnSetDefaultReplayFolder.UseVisualStyleBackColor = true;
            this.btnSetDefaultReplayFolder.Click += new System.EventHandler(this.btnSetDefaultReplayFolder_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.Color.DarkGray;
            this.lblVersion.Location = new System.Drawing.Point(12, 571);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 20);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "Version: 3";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblDefaultReplaysFolderTitle);
            this.Controls.Add(this.lblDefaultReplayFolder);
            this.Controls.Add(this.btnSetDefaultReplayFolder);
            this.Controls.Add(this.lblPlayerInstallationTitle);
            this.Controls.Add(this.lblSimulatorInstallationTitle);
            this.Controls.Add(this.lblPlayerInstallationError);
            this.Controls.Add(this.lblSimulatorInstallationError);
            this.Controls.Add(this.lblPlayerInstallation);
            this.Controls.Add(this.btnSetPlayerInstallation);
            this.Controls.Add(this.lblSimulatorInstallation);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSetSimulatorInstallationLocation);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
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
        private System.Windows.Forms.Label lblSimulatorInstallationTitle;
        private System.Windows.Forms.Label lblPlayerInstallationTitle;
        private System.Windows.Forms.Label lblDefaultReplaysFolderTitle;
        private System.Windows.Forms.Label lblDefaultReplayFolder;
        private System.Windows.Forms.Button btnSetDefaultReplayFolder;
        private System.Windows.Forms.FolderBrowserDialog DefaultReplayFolderBrowser;
        private System.Windows.Forms.Label lblVersion;
    }
}