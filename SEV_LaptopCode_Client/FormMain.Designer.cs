namespace SEV_LaptopCode_Client
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pnlLogoName = new System.Windows.Forms.Panel();
            this.pnlMainDisplay = new System.Windows.Forms.Panel();
            this.logoRTC = new System.Windows.Forms.PictureBox();
            this.lblProgramLabel = new System.Windows.Forms.Label();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.pnlSplit = new System.Windows.Forms.Panel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuElipse2 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.pnlLogoName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoRTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogoName
            // 
            this.pnlLogoName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(76)))), ((int)(((byte)(161)))));
            this.pnlLogoName.Controls.Add(this.pbExit);
            this.pnlLogoName.Controls.Add(this.logoRTC);
            this.pnlLogoName.Controls.Add(this.lblProgramLabel);
            this.pnlLogoName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogoName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlLogoName.Location = new System.Drawing.Point(0, 0);
            this.pnlLogoName.Name = "pnlLogoName";
            this.pnlLogoName.Size = new System.Drawing.Size(270, 38);
            this.pnlLogoName.TabIndex = 0;
            // 
            // pnlMainDisplay
            // 
            this.pnlMainDisplay.BackColor = System.Drawing.Color.White;
            this.pnlMainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlMainDisplay.Name = "pnlMainDisplay";
            this.pnlMainDisplay.Size = new System.Drawing.Size(270, 494);
            this.pnlMainDisplay.TabIndex = 1;
            // 
            // logoRTC
            // 
            this.logoRTC.BackColor = System.Drawing.Color.White;
            this.logoRTC.Image = ((System.Drawing.Image)(resources.GetObject("logoRTC.Image")));
            this.logoRTC.Location = new System.Drawing.Point(4, 5);
            this.logoRTC.Margin = new System.Windows.Forms.Padding(10);
            this.logoRTC.Name = "logoRTC";
            this.logoRTC.Padding = new System.Windows.Forms.Padding(10);
            this.logoRTC.Size = new System.Drawing.Size(88, 28);
            this.logoRTC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoRTC.TabIndex = 0;
            this.logoRTC.TabStop = false;
            // 
            // lblProgramLabel
            // 
            this.lblProgramLabel.AutoSize = true;
            this.lblProgramLabel.Font = new System.Drawing.Font("Space Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramLabel.ForeColor = System.Drawing.Color.White;
            this.lblProgramLabel.Location = new System.Drawing.Point(95, 7);
            this.lblProgramLabel.Name = "lblProgramLabel";
            this.lblProgramLabel.Size = new System.Drawing.Size(140, 24);
            this.lblProgramLabel.TabIndex = 1;
            this.lblProgramLabel.Text = "Reader Client";
            // 
            // pbExit
            // 
            this.pbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbExit.Image = ((System.Drawing.Image)(resources.GetObject("pbExit.Image")));
            this.pbExit.Location = new System.Drawing.Point(251, 0);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(19, 38);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExit.TabIndex = 2;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 2;
            this.bunifuElipse1.TargetControl = this.logoRTC;
            // 
            // pnlSplit
            // 
            this.pnlSplit.BackColor = System.Drawing.Color.White;
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit.Location = new System.Drawing.Point(0, 38);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Size = new System.Drawing.Size(270, 2);
            this.pnlSplit.TabIndex = 0;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.lblProgramLabel;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 3;
            this.bunifuElipse2.TargetControl = this;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(270, 534);
            this.Controls.Add(this.pnlMainDisplay);
            this.Controls.Add(this.pnlSplit);
            this.Controls.Add(this.pnlLogoName);
            this.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.pnlLogoName.ResumeLayout(false);
            this.pnlLogoName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoRTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogoName;
        private System.Windows.Forms.Panel pnlMainDisplay;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.PictureBox logoRTC;
        private System.Windows.Forms.Label lblProgramLabel;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel pnlSplit;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
    }
}

