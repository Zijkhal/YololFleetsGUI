
namespace YololFleetsGUI
{
    partial class Form1
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
            this.tbFleet1 = new System.Windows.Forms.TextBox();
            this.tbFleet2 = new System.Windows.Forms.TextBox();
            this.Fleet1Browser = new System.Windows.Forms.FolderBrowserDialog();
            this.Fleet2Browser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnRunBattleSimulation = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnSaveReplay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbFleet1
            // 
            this.tbFleet1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFleet1.BackColor = System.Drawing.SystemColors.Control;
            this.tbFleet1.ForeColor = System.Drawing.Color.DarkGray;
            this.tbFleet1.Location = new System.Drawing.Point(12, 61);
            this.tbFleet1.Name = "tbFleet1";
            this.tbFleet1.ReadOnly = true;
            this.tbFleet1.Size = new System.Drawing.Size(481, 23);
            this.tbFleet1.TabIndex = 1;
            this.tbFleet1.Text = "Click here to select a fleet";
            this.tbFleet1.Click += new System.EventHandler(this.tbFleet1_Click);
            // 
            // tbFleet2
            // 
            this.tbFleet2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFleet2.BackColor = System.Drawing.SystemColors.Control;
            this.tbFleet2.ForeColor = System.Drawing.Color.DarkGray;
            this.tbFleet2.Location = new System.Drawing.Point(12, 90);
            this.tbFleet2.Name = "tbFleet2";
            this.tbFleet2.ReadOnly = true;
            this.tbFleet2.Size = new System.Drawing.Size(481, 23);
            this.tbFleet2.TabIndex = 3;
            this.tbFleet2.Text = "Click here to select a fleet";
            this.tbFleet2.Click += new System.EventHandler(this.tbFleet2_Click);
            // 
            // Fleet1Browser
            // 
            this.Fleet1Browser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.Fleet1Browser.ShowNewFolderButton = false;
            // 
            // Fleet2Browser
            // 
            this.Fleet2Browser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.Fleet2Browser.ShowNewFolderButton = false;
            // 
            // btnRunBattleSimulation
            // 
            this.btnRunBattleSimulation.Enabled = false;
            this.btnRunBattleSimulation.Location = new System.Drawing.Point(12, 119);
            this.btnRunBattleSimulation.Name = "btnRunBattleSimulation";
            this.btnRunBattleSimulation.Size = new System.Drawing.Size(75, 23);
            this.btnRunBattleSimulation.TabIndex = 4;
            this.btnRunBattleSimulation.Text = "Simulate!";
            this.btnRunBattleSimulation.UseVisualStyleBackColor = true;
            this.btnRunBattleSimulation.Click += new System.EventHandler(this.btnRunBattleSimulation_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnSaveReplay
            // 
            this.btnSaveReplay.Enabled = false;
            this.btnSaveReplay.Location = new System.Drawing.Point(93, 119);
            this.btnSaveReplay.Name = "btnSaveReplay";
            this.btnSaveReplay.Size = new System.Drawing.Size(88, 23);
            this.btnSaveReplay.TabIndex = 6;
            this.btnSaveReplay.Text = "Save Replay";
            this.btnSaveReplay.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 450);
            this.Controls.Add(this.btnSaveReplay);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnRunBattleSimulation);
            this.Controls.Add(this.tbFleet2);
            this.Controls.Add(this.tbFleet1);
            this.Name = "Form1";
            this.Text = "Yolol Fleets Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbFleet1;
        private System.Windows.Forms.TextBox tbFleet2;
        private System.Windows.Forms.FolderBrowserDialog Fleet1Browser;
        private System.Windows.Forms.FolderBrowserDialog Fleet2Browser;
        private System.Windows.Forms.Button btnRunBattleSimulation;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnSaveReplay;
    }
}

