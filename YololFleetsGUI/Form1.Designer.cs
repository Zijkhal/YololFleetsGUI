
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
            this.rtbConsoleOutput = new System.Windows.Forms.RichTextBox();
            this.lblWinner = new System.Windows.Forms.Label();
            this.btnWatchReplay = new System.Windows.Forms.Button();
            this.saveReplayDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // tbFleet1
            // 
            this.tbFleet1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFleet1.BackColor = System.Drawing.SystemColors.Control;
            this.tbFleet1.ForeColor = System.Drawing.Color.DarkGray;
            this.tbFleet1.Location = new System.Drawing.Point(14, 81);
            this.tbFleet1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbFleet1.Name = "tbFleet1";
            this.tbFleet1.ReadOnly = true;
            this.tbFleet1.Size = new System.Drawing.Size(549, 27);
            this.tbFleet1.TabIndex = 1;
            this.tbFleet1.TabStop = false;
            this.tbFleet1.Text = "Click here to select a fleet";
            this.tbFleet1.Click += new System.EventHandler(this.tbFleet1_Click);
            // 
            // tbFleet2
            // 
            this.tbFleet2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFleet2.BackColor = System.Drawing.SystemColors.Control;
            this.tbFleet2.ForeColor = System.Drawing.Color.DarkGray;
            this.tbFleet2.Location = new System.Drawing.Point(14, 120);
            this.tbFleet2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbFleet2.Name = "tbFleet2";
            this.tbFleet2.ReadOnly = true;
            this.tbFleet2.Size = new System.Drawing.Size(549, 27);
            this.tbFleet2.TabIndex = 3;
            this.tbFleet2.TabStop = false;
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
            this.btnRunBattleSimulation.Location = new System.Drawing.Point(14, 159);
            this.btnRunBattleSimulation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRunBattleSimulation.Name = "btnRunBattleSimulation";
            this.btnRunBattleSimulation.Size = new System.Drawing.Size(86, 31);
            this.btnRunBattleSimulation.TabIndex = 4;
            this.btnRunBattleSimulation.Text = "Simulate!";
            this.btnRunBattleSimulation.UseVisualStyleBackColor = true;
            this.btnRunBattleSimulation.Click += new System.EventHandler(this.btnRunBattleSimulation_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(14, 16);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(86, 31);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // rtbConsoleOutput
            // 
            this.rtbConsoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbConsoleOutput.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbConsoleOutput.Location = new System.Drawing.Point(14, 197);
            this.rtbConsoleOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtbConsoleOutput.Name = "rtbConsoleOutput";
            this.rtbConsoleOutput.ReadOnly = true;
            this.rtbConsoleOutput.ShowSelectionMargin = true;
            this.rtbConsoleOutput.Size = new System.Drawing.Size(549, 559);
            this.rtbConsoleOutput.TabIndex = 99;
            this.rtbConsoleOutput.TabStop = false;
            this.rtbConsoleOutput.Text = "";
            // 
            // lblWinner
            // 
            this.lblWinner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWinner.Location = new System.Drawing.Point(14, 57);
            this.lblWinner.Name = "lblWinner";
            this.lblWinner.Size = new System.Drawing.Size(550, 20);
            this.lblWinner.TabIndex = 8;
            this.lblWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnWatchReplay
            // 
            this.btnWatchReplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWatchReplay.Enabled = false;
            this.btnWatchReplay.Location = new System.Drawing.Point(432, 159);
            this.btnWatchReplay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWatchReplay.Name = "btnWatchReplay";
            this.btnWatchReplay.Size = new System.Drawing.Size(131, 31);
            this.btnWatchReplay.TabIndex = 10;
            this.btnWatchReplay.Text = "Watch Replay";
            this.btnWatchReplay.UseVisualStyleBackColor = true;
            this.btnWatchReplay.Click += new System.EventHandler(this.btnWatchReplay_Click);
            // 
            // saveReplayDialog
            // 
            this.saveReplayDialog.Description = "Select the folder to save the replay files to";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(577, 773);
            this.Controls.Add(this.btnWatchReplay);
            this.Controls.Add(this.lblWinner);
            this.Controls.Add(this.rtbConsoleOutput);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnRunBattleSimulation);
            this.Controls.Add(this.tbFleet2);
            this.Controls.Add(this.tbFleet1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Yolol Fleets GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
        private System.Windows.Forms.RichTextBox rtbConsoleOutput;
        private System.Windows.Forms.Label lblWinner;
        private System.Windows.Forms.Button btnWatchReplay;
        private System.Windows.Forms.FolderBrowserDialog saveReplayDialog;
    }
}

