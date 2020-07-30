namespace TEST
{
    partial class CameraTriggerAndLiveviewDemo
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
            this.btnGetDevices = new System.Windows.Forms.Button();
            this.cbInterfaceList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDeviceList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnectDevice = new System.Windows.Forms.Button();
            this.btnGrabImage = new System.Windows.Forms.Button();
            this.hSWindow1 = new HalconDotNet.HSmartWindowControl();
            this.btnProcTest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEditROI = new System.Windows.Forms.Button();
            this.btnDoneEditROI = new System.Windows.Forms.Button();
            this.btnFindCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetDevices
            // 
            this.btnGetDevices.Location = new System.Drawing.Point(12, 116);
            this.btnGetDevices.Name = "btnGetDevices";
            this.btnGetDevices.Size = new System.Drawing.Size(187, 23);
            this.btnGetDevices.TabIndex = 0;
            this.btnGetDevices.Text = "Get Available Devices";
            this.btnGetDevices.UseVisualStyleBackColor = true;
            this.btnGetDevices.Click += new System.EventHandler(this.btnGetDevices_Click);
            // 
            // cbInterfaceList
            // 
            this.cbInterfaceList.FormattingEnabled = true;
            this.cbInterfaceList.Location = new System.Drawing.Point(12, 31);
            this.cbInterfaceList.Name = "cbInterfaceList";
            this.cbInterfaceList.Size = new System.Drawing.Size(187, 21);
            this.cbInterfaceList.TabIndex = 1;
            this.cbInterfaceList.SelectedIndexChanged += new System.EventHandler(this.cbInterfaceList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Interface:";
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.FormattingEnabled = true;
            this.cbDeviceList.Location = new System.Drawing.Point(12, 75);
            this.cbDeviceList.Name = "cbDeviceList";
            this.cbDeviceList.Size = new System.Drawing.Size(187, 21);
            this.cbDeviceList.TabIndex = 1;
            this.cbDeviceList.SelectedIndexChanged += new System.EventHandler(this.cbDeviceList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Devices:";
            // 
            // btnConnectDevice
            // 
            this.btnConnectDevice.Location = new System.Drawing.Point(12, 145);
            this.btnConnectDevice.Name = "btnConnectDevice";
            this.btnConnectDevice.Size = new System.Drawing.Size(187, 23);
            this.btnConnectDevice.TabIndex = 0;
            this.btnConnectDevice.Text = "Connect Device";
            this.btnConnectDevice.UseVisualStyleBackColor = true;
            this.btnConnectDevice.Click += new System.EventHandler(this.btnConnectDevice_Click);
            // 
            // btnGrabImage
            // 
            this.btnGrabImage.Location = new System.Drawing.Point(12, 174);
            this.btnGrabImage.Name = "btnGrabImage";
            this.btnGrabImage.Size = new System.Drawing.Size(187, 23);
            this.btnGrabImage.TabIndex = 0;
            this.btnGrabImage.Text = "Test Grab Image";
            this.btnGrabImage.UseVisualStyleBackColor = true;
            this.btnGrabImage.Click += new System.EventHandler(this.btnGrabImage_Click);
            // 
            // hSWindow1
            // 
            this.hSWindow1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSWindow1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSWindow1.HDoubleClickToFitContent = true;
            this.hSWindow1.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSWindow1.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSWindow1.HKeepAspectRatio = true;
            this.hSWindow1.HMoveContent = true;
            this.hSWindow1.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSWindow1.Location = new System.Drawing.Point(236, 15);
            this.hSWindow1.Margin = new System.Windows.Forms.Padding(0);
            this.hSWindow1.Name = "hSWindow1";
            this.hSWindow1.Size = new System.Drawing.Size(932, 623);
            this.hSWindow1.TabIndex = 3;
            this.hSWindow1.WindowSize = new System.Drawing.Size(932, 623);
            // 
            // btnProcTest
            // 
            this.btnProcTest.Location = new System.Drawing.Point(12, 596);
            this.btnProcTest.Name = "btnProcTest";
            this.btnProcTest.Size = new System.Drawing.Size(187, 23);
            this.btnProcTest.TabIndex = 0;
            this.btnProcTest.Text = "Calculate by Proc";
            this.btnProcTest.UseVisualStyleBackColor = true;
            this.btnProcTest.Click += new System.EventHandler(this.btnProcTest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ROI:";
            // 
            // btnEditROI
            // 
            this.btnEditROI.Location = new System.Drawing.Point(12, 266);
            this.btnEditROI.Name = "btnEditROI";
            this.btnEditROI.Size = new System.Drawing.Size(187, 23);
            this.btnEditROI.TabIndex = 0;
            this.btnEditROI.Text = "Edit ROI";
            this.btnEditROI.UseVisualStyleBackColor = true;
            this.btnEditROI.Click += new System.EventHandler(this.btnEditROI_Click);
            // 
            // btnDoneEditROI
            // 
            this.btnDoneEditROI.Location = new System.Drawing.Point(12, 295);
            this.btnDoneEditROI.Name = "btnDoneEditROI";
            this.btnDoneEditROI.Size = new System.Drawing.Size(187, 23);
            this.btnDoneEditROI.TabIndex = 0;
            this.btnDoneEditROI.Text = "Done";
            this.btnDoneEditROI.UseVisualStyleBackColor = true;
            this.btnDoneEditROI.Click += new System.EventHandler(this.btnDoneEditROI_Click);
            // 
            // btnFindCode
            // 
            this.btnFindCode.Location = new System.Drawing.Point(12, 324);
            this.btnFindCode.Name = "btnFindCode";
            this.btnFindCode.Size = new System.Drawing.Size(187, 23);
            this.btnFindCode.TabIndex = 0;
            this.btnFindCode.Text = "Find Code";
            this.btnFindCode.UseVisualStyleBackColor = true;
            this.btnFindCode.Click += new System.EventHandler(this.btnFindCode_Click);
            // 
            // CameraTriggerAndLiveviewDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 647);
            this.Controls.Add(this.hSWindow1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDeviceList);
            this.Controls.Add(this.cbInterfaceList);
            this.Controls.Add(this.btnProcTest);
            this.Controls.Add(this.btnGrabImage);
            this.Controls.Add(this.btnConnectDevice);
            this.Controls.Add(this.btnFindCode);
            this.Controls.Add(this.btnDoneEditROI);
            this.Controls.Add(this.btnEditROI);
            this.Controls.Add(this.btnGetDevices);
            this.Name = "CameraTriggerAndLiveviewDemo";
            this.Text = "CameraTriggerAndLiveviewDemo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetDevices;
        private System.Windows.Forms.ComboBox cbInterfaceList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnectDevice;
        private System.Windows.Forms.Button btnGrabImage;
        private HalconDotNet.HSmartWindowControl hSWindow1;
        private System.Windows.Forms.Button btnProcTest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEditROI;
        private System.Windows.Forms.Button btnDoneEditROI;
        private System.Windows.Forms.Button btnFindCode;
    }
}