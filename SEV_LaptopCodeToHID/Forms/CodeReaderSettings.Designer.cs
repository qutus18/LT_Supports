namespace SEV_LaptopCodeToHID.Forms
{
    partial class CodeReaderSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeReaderSettings));
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.txtLengthMax = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtFilterString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLengthMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listboxCodeList = new System.Windows.Forms.ListBox();
            this.btnRemoveCode = new System.Windows.Forms.Button();
            this.btnEditROI = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddCode = new System.Windows.Forms.Button();
            this.cbCodeType = new System.Windows.Forms.ComboBox();
            this.cbCameraList = new System.Windows.Forms.ComboBox();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.hSmartWD = new HalconDotNet.HSmartWindowControl();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTryOne = new System.Windows.Forms.Button();
            this.pnlSetting.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSetting
            // 
            this.pnlSetting.Controls.Add(this.btnSaveAs);
            this.pnlSetting.Controls.Add(this.btnTryOne);
            this.pnlSetting.Controls.Add(this.btnSave);
            this.pnlSetting.Controls.Add(this.txtLengthMax);
            this.pnlSetting.Controls.Add(this.txtName);
            this.pnlSetting.Controls.Add(this.txtFilterString);
            this.pnlSetting.Controls.Add(this.label2);
            this.pnlSetting.Controls.Add(this.label1);
            this.pnlSetting.Controls.Add(this.label5);
            this.pnlSetting.Controls.Add(this.label4);
            this.pnlSetting.Controls.Add(this.txtLengthMin);
            this.pnlSetting.Controls.Add(this.label3);
            this.pnlSetting.Controls.Add(this.listboxCodeList);
            this.pnlSetting.Controls.Add(this.btnRemoveCode);
            this.pnlSetting.Controls.Add(this.btnEditROI);
            this.pnlSetting.Controls.Add(this.btnAddCode);
            this.pnlSetting.Controls.Add(this.cbCodeType);
            this.pnlSetting.Controls.Add(this.cbCameraList);
            this.pnlSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSetting.Location = new System.Drawing.Point(0, 0);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Size = new System.Drawing.Size(336, 500);
            this.pnlSetting.TabIndex = 0;
            // 
            // txtLengthMax
            // 
            this.txtLengthMax.Location = new System.Drawing.Point(259, 113);
            this.txtLengthMax.Name = "txtLengthMax";
            this.txtLengthMax.Size = new System.Drawing.Size(64, 27);
            this.txtLengthMax.TabIndex = 12;
            this.txtLengthMax.TextChanged += new System.EventHandler(this.CodeReaderValueChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(190, 46);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(133, 27);
            this.txtName.TabIndex = 12;
            this.txtName.TextChanged += new System.EventHandler(this.CodeReaderValueChanged);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // txtFilterString
            // 
            this.txtFilterString.Location = new System.Drawing.Point(190, 146);
            this.txtFilterString.Name = "txtFilterString";
            this.txtFilterString.Size = new System.Drawing.Size(133, 27);
            this.txtFilterString.TabIndex = 12;
            this.txtFilterString.TextChanged += new System.EventHandler(this.CodeReaderValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(114, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(114, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(114, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Filter";
            // 
            // txtLengthMin
            // 
            this.txtLengthMin.Location = new System.Drawing.Point(190, 113);
            this.txtLengthMin.Name = "txtLengthMin";
            this.txtLengthMin.Size = new System.Drawing.Size(63, 27);
            this.txtLengthMin.TabIndex = 12;
            this.txtLengthMin.TextChanged += new System.EventHandler(this.CodeReaderValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(114, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Length";
            // 
            // listboxCodeList
            // 
            this.listboxCodeList.FormattingEnabled = true;
            this.listboxCodeList.ItemHeight = 20;
            this.listboxCodeList.Location = new System.Drawing.Point(12, 46);
            this.listboxCodeList.Name = "listboxCodeList";
            this.listboxCodeList.Size = new System.Drawing.Size(100, 164);
            this.listboxCodeList.TabIndex = 10;
            this.listboxCodeList.SelectedIndexChanged += new System.EventHandler(this.listboxCodeList_SelectedIndexChanged);
            // 
            // btnRemoveCode
            // 
            this.btnRemoveCode.BackColor = System.Drawing.Color.Black;
            this.btnRemoveCode.FlatAppearance.BorderSize = 0;
            this.btnRemoveCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCode.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveCode.ForeColor = System.Drawing.Color.White;
            this.btnRemoveCode.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveCode.Image")));
            this.btnRemoveCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveCode.Location = new System.Drawing.Point(223, 12);
            this.btnRemoveCode.Name = "btnRemoveCode";
            this.btnRemoveCode.Size = new System.Drawing.Size(100, 28);
            this.btnRemoveCode.TabIndex = 9;
            this.btnRemoveCode.Text = " Remove";
            this.btnRemoveCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemoveCode.UseVisualStyleBackColor = false;
            this.btnRemoveCode.Click += new System.EventHandler(this.btnRemoveCode_Click);
            // 
            // btnEditROI
            // 
            this.btnEditROI.BackColor = System.Drawing.Color.PaleVioletRed;
            this.btnEditROI.FlatAppearance.BorderSize = 0;
            this.btnEditROI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditROI.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditROI.ForeColor = System.Drawing.Color.White;
            this.btnEditROI.Image = ((System.Drawing.Image)(resources.GetObject("btnEditROI.Image")));
            this.btnEditROI.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditROI.Location = new System.Drawing.Point(190, 179);
            this.btnEditROI.Name = "btnEditROI";
            this.btnEditROI.Size = new System.Drawing.Size(133, 28);
            this.btnEditROI.TabIndex = 9;
            this.btnEditROI.Text = "  Modify";
            this.btnEditROI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditROI.UseVisualStyleBackColor = false;
            this.btnEditROI.Click += new System.EventHandler(this.btnEditROI_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.BackColor = System.Drawing.Color.LightCoral;
            this.btnSaveAs.FlatAppearance.BorderSize = 0;
            this.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAs.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAs.ForeColor = System.Drawing.Color.White;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveAs.Location = new System.Drawing.Point(11, 237);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(100, 28);
            this.btnSaveAs.TabIndex = 9;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveAs.UseVisualStyleBackColor = false;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(117, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = " Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddCode
            // 
            this.btnAddCode.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnAddCode.FlatAppearance.BorderSize = 0;
            this.btnAddCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCode.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCode.ForeColor = System.Drawing.Color.White;
            this.btnAddCode.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCode.Image")));
            this.btnAddCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCode.Location = new System.Drawing.Point(117, 12);
            this.btnAddCode.Name = "btnAddCode";
            this.btnAddCode.Size = new System.Drawing.Size(100, 28);
            this.btnAddCode.TabIndex = 9;
            this.btnAddCode.Text = "  Add";
            this.btnAddCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddCode.UseVisualStyleBackColor = false;
            this.btnAddCode.Click += new System.EventHandler(this.btnAddCode_Click);
            // 
            // cbCodeType
            // 
            this.cbCodeType.FormattingEnabled = true;
            this.cbCodeType.Items.AddRange(new object[] {
            "Barcode",
            "DMCode",
            "QRCode"});
            this.cbCodeType.Location = new System.Drawing.Point(190, 79);
            this.cbCodeType.Name = "cbCodeType";
            this.cbCodeType.Size = new System.Drawing.Size(133, 28);
            this.cbCodeType.TabIndex = 0;
            this.cbCodeType.SelectedIndexChanged += new System.EventHandler(this.CodeReaderValueChanged);
            // 
            // cbCameraList
            // 
            this.cbCameraList.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCameraList.FormattingEnabled = true;
            this.cbCameraList.Items.AddRange(new object[] {
            "CAM1",
            "CAM2",
            "CAM3",
            "CAM4"});
            this.cbCameraList.Location = new System.Drawing.Point(12, 12);
            this.cbCameraList.Name = "cbCameraList";
            this.cbCameraList.Size = new System.Drawing.Size(99, 28);
            this.cbCameraList.TabIndex = 0;
            this.cbCameraList.SelectedIndexChanged += new System.EventHandler(this.cbCameraList_SelectedIndexChanged);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Controls.Add(this.hSmartWD);
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(336, 0);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(664, 500);
            this.pnlDisplay.TabIndex = 1;
            // 
            // hSmartWD
            // 
            this.hSmartWD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWD.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSmartWD.HDoubleClickToFitContent = true;
            this.hSmartWD.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSmartWD.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWD.HKeepAspectRatio = true;
            this.hSmartWD.HMoveContent = true;
            this.hSmartWD.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWD.Location = new System.Drawing.Point(0, 0);
            this.hSmartWD.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWD.Name = "hSmartWD";
            this.hSmartWD.Size = new System.Drawing.Size(664, 500);
            this.hSmartWD.TabIndex = 0;
            this.hSmartWD.WindowSize = new System.Drawing.Size(664, 500);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(114, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Region";
            // 
            // btnTryOne
            // 
            this.btnTryOne.BackColor = System.Drawing.Color.Turquoise;
            this.btnTryOne.FlatAppearance.BorderSize = 0;
            this.btnTryOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTryOne.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTryOne.ForeColor = System.Drawing.Color.White;
            this.btnTryOne.Image = ((System.Drawing.Image)(resources.GetObject("btnTryOne.Image")));
            this.btnTryOne.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTryOne.Location = new System.Drawing.Point(222, 237);
            this.btnTryOne.Name = "btnTryOne";
            this.btnTryOne.Size = new System.Drawing.Size(100, 28);
            this.btnTryOne.TabIndex = 9;
            this.btnTryOne.Text = "Trigger";
            this.btnTryOne.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTryOne.UseVisualStyleBackColor = false;
            this.btnTryOne.Click += new System.EventHandler(this.btnTryOne_Click);
            // 
            // CodeReaderSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.pnlDisplay);
            this.Controls.Add(this.pnlSetting);
            this.Font = new System.Drawing.Font("Space Mono", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CodeReaderSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeReaderSettings";
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            this.pnlDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSetting;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.ComboBox cbCameraList;
        private HalconDotNet.HSmartWindowControl hSmartWD;
        private System.Windows.Forms.Button btnAddCode;
        private System.Windows.Forms.Button btnRemoveCode;
        private System.Windows.Forms.ListBox listboxCodeList;
        private System.Windows.Forms.Button btnEditROI;
        private System.Windows.Forms.ComboBox cbCodeType;
        private System.Windows.Forms.TextBox txtLengthMax;
        private System.Windows.Forms.TextBox txtFilterString;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLengthMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTryOne;
    }
}