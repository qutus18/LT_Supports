namespace TEST
{
    partial class FindToolByName
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
            this.txtObjectInfo = new System.Windows.Forms.TextBox();
            this.txtObjectName = new System.Windows.Forms.TextBox();
            this.btnFindObject = new System.Windows.Forms.Button();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.SuspendLayout();
            // 
            // txtObjectInfo
            // 
            this.txtObjectInfo.Location = new System.Drawing.Point(12, 38);
            this.txtObjectInfo.Multiline = true;
            this.txtObjectInfo.Name = "txtObjectInfo";
            this.txtObjectInfo.Size = new System.Drawing.Size(354, 236);
            this.txtObjectInfo.TabIndex = 2;
            this.txtObjectInfo.TabStop = false;
            // 
            // txtObjectName
            // 
            this.txtObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjectName.Location = new System.Drawing.Point(12, 6);
            this.txtObjectName.Multiline = true;
            this.txtObjectName.Name = "txtObjectName";
            this.txtObjectName.Size = new System.Drawing.Size(237, 26);
            this.txtObjectName.TabIndex = 1;
            // 
            // btnFindObject
            // 
            this.btnFindObject.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnFindObject.FlatAppearance.BorderSize = 0;
            this.btnFindObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindObject.ForeColor = System.Drawing.Color.White;
            this.btnFindObject.Location = new System.Drawing.Point(256, 6);
            this.btnFindObject.Name = "btnFindObject";
            this.btnFindObject.Size = new System.Drawing.Size(110, 26);
            this.btnFindObject.TabIndex = 0;
            this.btnFindObject.TabStop = false;
            this.btnFindObject.Text = "Find";
            this.btnFindObject.UseVisualStyleBackColor = false;
            this.btnFindObject.Click += new System.EventHandler(this.btnFindObject_Click);
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this.btnFindObject;
            // 
            // FindToolByName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(378, 284);
            this.Controls.Add(this.btnFindObject);
            this.Controls.Add(this.txtObjectName);
            this.Controls.Add(this.txtObjectInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FindToolByName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FindToolByName";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtObjectInfo;
        private System.Windows.Forms.TextBox txtObjectName;
        private System.Windows.Forms.Button btnFindObject;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
    }
}