namespace Hix_CCD_Module.UI
{
    partial class FrmAddNewCamera
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.BtnSelectFile = new System.Windows.Forms.Button();
            this.txtNewCameraFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewCameraDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewCameraName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(384, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 47);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(110, 168);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(131, 47);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSelectFile
            // 
            this.BtnSelectFile.Font = new System.Drawing.Font("华文中宋", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSelectFile.Location = new System.Drawing.Point(552, 114);
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.Size = new System.Drawing.Size(36, 23);
            this.BtnSelectFile.TabIndex = 15;
            this.BtnSelectFile.Text = "...";
            this.BtnSelectFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnSelectFile.UseVisualStyleBackColor = true;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // txtNewCameraFilePath
            // 
            this.txtNewCameraFilePath.Location = new System.Drawing.Point(151, 114);
            this.txtNewCameraFilePath.Name = "txtNewCameraFilePath";
            this.txtNewCameraFilePath.Size = new System.Drawing.Size(395, 25);
            this.txtNewCameraFilePath.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "CameraFilePath :";
            // 
            // txtNewCameraDescription
            // 
            this.txtNewCameraDescription.Location = new System.Drawing.Point(151, 70);
            this.txtNewCameraDescription.Name = "txtNewCameraDescription";
            this.txtNewCameraDescription.Size = new System.Drawing.Size(395, 25);
            this.txtNewCameraDescription.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Description:";
            // 
            // txtNewCameraName
            // 
            this.txtNewCameraName.Location = new System.Drawing.Point(151, 26);
            this.txtNewCameraName.Name = "txtNewCameraName";
            this.txtNewCameraName.Size = new System.Drawing.Size(395, 25);
            this.txtNewCameraName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name:";
            // 
            // FrmAddNewCamera
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(615, 241);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.BtnSelectFile);
            this.Controls.Add(this.txtNewCameraFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNewCameraDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewCameraName);
            this.Controls.Add(this.label1);
            this.Name = "FrmAddNewCamera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Camera *";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button BtnSelectFile;
        private System.Windows.Forms.TextBox txtNewCameraFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewCameraDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewCameraName;
        private System.Windows.Forms.Label label1;
    }
}