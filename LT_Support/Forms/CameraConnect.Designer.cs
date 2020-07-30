namespace LT_Support.Forms
{
    partial class CameraConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraConnect));
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.pbDone = new System.Windows.Forms.PictureBox();
            this.pbRefresh = new System.Windows.Forms.PictureBox();
            this.txtGainValue = new System.Windows.Forms.TextBox();
            this.txtExposureValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.cbInterfaces = new System.Windows.Forms.ComboBox();
            this.btnLive = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.hSmartWD = new HalconDotNet.HSmartWindowControl();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuElipse2 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.pnlSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefresh)).BeginInit();
            this.pnlDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSetting
            // 
            this.pnlSetting.Controls.Add(this.pbDone);
            this.pnlSetting.Controls.Add(this.pbRefresh);
            this.pnlSetting.Controls.Add(this.txtGainValue);
            this.pnlSetting.Controls.Add(this.txtExposureValue);
            this.pnlSetting.Controls.Add(this.label2);
            this.pnlSetting.Controls.Add(this.label1);
            this.pnlSetting.Controls.Add(this.cbDevices);
            this.pnlSetting.Controls.Add(this.cbInterfaces);
            this.pnlSetting.Controls.Add(this.btnLive);
            this.pnlSetting.Controls.Add(this.btnConnect);
            this.pnlSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSetting.Location = new System.Drawing.Point(0, 0);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Size = new System.Drawing.Size(527, 120);
            this.pnlSetting.TabIndex = 0;
            // 
            // pbDone
            // 
            this.pbDone.Image = ((System.Drawing.Image)(resources.GetObject("pbDone.Image")));
            this.pbDone.Location = new System.Drawing.Point(491, 45);
            this.pbDone.Name = "pbDone";
            this.pbDone.Size = new System.Drawing.Size(24, 27);
            this.pbDone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDone.TabIndex = 11;
            this.pbDone.TabStop = false;
            this.pbDone.Click += new System.EventHandler(this.pbDone_Click);
            // 
            // pbRefresh
            // 
            this.pbRefresh.BackColor = System.Drawing.Color.Transparent;
            this.pbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("pbRefresh.Image")));
            this.pbRefresh.Location = new System.Drawing.Point(491, 12);
            this.pbRefresh.Name = "pbRefresh";
            this.pbRefresh.Size = new System.Drawing.Size(24, 27);
            this.pbRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbRefresh.TabIndex = 11;
            this.pbRefresh.TabStop = false;
            this.pbRefresh.Click += new System.EventHandler(this.pbRefresh_Click);
            // 
            // txtGainValue
            // 
            this.txtGainValue.Location = new System.Drawing.Point(158, 79);
            this.txtGainValue.Name = "txtGainValue";
            this.txtGainValue.Size = new System.Drawing.Size(61, 27);
            this.txtGainValue.TabIndex = 10;
            this.txtGainValue.TextChanged += new System.EventHandler(this.CamParamsChanged);
            // 
            // txtExposureValue
            // 
            this.txtExposureValue.Location = new System.Drawing.Point(349, 79);
            this.txtExposureValue.Name = "txtExposureValue";
            this.txtExposureValue.Size = new System.Drawing.Size(136, 27);
            this.txtExposureValue.TabIndex = 10;
            this.txtExposureValue.TextChanged += new System.EventHandler(this.CamParamsChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(238, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "ExposureTime";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(111, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Gain";
            // 
            // cbDevices
            // 
            this.cbDevices.FormattingEnabled = true;
            this.cbDevices.Location = new System.Drawing.Point(158, 45);
            this.cbDevices.Name = "cbDevices";
            this.cbDevices.Size = new System.Drawing.Size(327, 28);
            this.cbDevices.TabIndex = 8;
            // 
            // cbInterfaces
            // 
            this.cbInterfaces.FormattingEnabled = true;
            this.cbInterfaces.Location = new System.Drawing.Point(158, 11);
            this.cbInterfaces.Name = "cbInterfaces";
            this.cbInterfaces.Size = new System.Drawing.Size(327, 28);
            this.cbInterfaces.TabIndex = 8;
            this.cbInterfaces.SelectedIndexChanged += new System.EventHandler(this.cbInterfaceList_SelectedIndexChanged);
            // 
            // btnLive
            // 
            this.btnLive.BackColor = System.Drawing.Color.Gray;
            this.btnLive.FlatAppearance.BorderSize = 0;
            this.btnLive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLive.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLive.ForeColor = System.Drawing.Color.White;
            this.btnLive.Image = ((System.Drawing.Image)(resources.GetObject("btnLive.Image")));
            this.btnLive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLive.Location = new System.Drawing.Point(12, 45);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(140, 28);
            this.btnLive.TabIndex = 7;
            this.btnLive.Text = "  LiveView";
            this.btnLive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLive.UseVisualStyleBackColor = false;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(76)))), ((int)(((byte)(161)))));
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.Location = new System.Drawing.Point(12, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(140, 28);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "  Connect";
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.hSmartWD);
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(0, 120);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(527, 380);
            this.pnlDisplay.TabIndex = 1;
            // 
            // hSmartWD
            // 
            this.hSmartWD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hSmartWD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWD.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.hSmartWD.HDoubleClickToFitContent = true;
            this.hSmartWD.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.Ctrl;
            this.hSmartWD.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWD.HKeepAspectRatio = true;
            this.hSmartWD.HMoveContent = true;
            this.hSmartWD.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWD.Location = new System.Drawing.Point(0, 0);
            this.hSmartWD.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWD.Name = "hSmartWD";
            this.hSmartWD.Size = new System.Drawing.Size(527, 380);
            this.hSmartWD.TabIndex = 0;
            this.hSmartWD.WindowSize = new System.Drawing.Size(527, 380);
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 2;
            this.bunifuElipse1.TargetControl = this.btnConnect;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 2;
            this.bunifuElipse2.TargetControl = this.btnLive;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.pnlSetting;
            this.bunifuDragControl1.Vertical = true;
            // 
            // CameraConnect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(527, 500);
            this.Controls.Add(this.pnlDisplay);
            this.Controls.Add(this.pnlSetting);
            this.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CameraConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CameraConnect";
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefresh)).EndInit();
            this.pnlDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSetting;
        private System.Windows.Forms.Panel pnlDisplay;
        private HalconDotNet.HSmartWindowControl hSmartWD;
        private System.Windows.Forms.Button btnConnect;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.ComboBox cbInterfaces;
        private System.Windows.Forms.TextBox txtGainValue;
        private System.Windows.Forms.TextBox txtExposureValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
        private System.Windows.Forms.PictureBox pbRefresh;
        private System.Windows.Forms.PictureBox pbDone;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
    }
}