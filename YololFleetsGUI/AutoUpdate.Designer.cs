
namespace YololFleetsGUI
{
    partial class AutoUpdate
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
            this.lblSimulator = new System.Windows.Forms.Label();
            this.lblSimulatorStatus = new System.Windows.Forms.Label();
            this.btnCancelUpdates = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSimulator
            // 
            this.lblSimulator.AutoSize = true;
            this.lblSimulator.Location = new System.Drawing.Point(12, 9);
            this.lblSimulator.Name = "lblSimulator";
            this.lblSimulator.Size = new System.Drawing.Size(76, 20);
            this.lblSimulator.TabIndex = 0;
            this.lblSimulator.Text = "Simulator:";
            // 
            // lblSimulatorStatus
            // 
            this.lblSimulatorStatus.AutoSize = true;
            this.lblSimulatorStatus.Location = new System.Drawing.Point(94, 9);
            this.lblSimulatorStatus.Name = "lblSimulatorStatus";
            this.lblSimulatorStatus.Size = new System.Drawing.Size(50, 20);
            this.lblSimulatorStatus.TabIndex = 1;
            this.lblSimulatorStatus.Text = "label1";
            // 
            // btnCancelUpdates
            // 
            this.btnCancelUpdates.Location = new System.Drawing.Point(633, 398);
            this.btnCancelUpdates.Name = "btnCancelUpdates";
            this.btnCancelUpdates.Size = new System.Drawing.Size(155, 40);
            this.btnCancelUpdates.TabIndex = 2;
            this.btnCancelUpdates.Text = "Don\'t Update";
            this.btnCancelUpdates.UseVisualStyleBackColor = true;
            // 
            // AutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancelUpdates);
            this.Controls.Add(this.lblSimulatorStatus);
            this.Controls.Add(this.lblSimulator);
            this.Name = "AutoUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoUpdate";
            this.Load += new System.EventHandler(this.AutoUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSimulator;
        private System.Windows.Forms.Label lblSimulatorStatus;
        private System.Windows.Forms.Button btnCancelUpdates;
    }
}