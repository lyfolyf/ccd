namespace Hix_CCD_Module.UI
{
    partial class FrmAddNewCalibTool
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
            this.txtNewCalibFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewCalibId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewCalibName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(288, 134);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 38);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(83, 134);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(98, 38);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSelectFile
            // 
            this.BtnSelectFile.Font = new System.Drawing.Font("华文中宋", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSelectFile.Location = new System.Drawing.Point(414, 91);
            this.BtnSelectFile.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.Size = new System.Drawing.Size(27, 18);
            this.BtnSelectFile.TabIndex = 15;
            this.BtnSelectFile.Text = "...";
            this.BtnSelectFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnSelectFile.UseVisualStyleBackColor = true;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // txtNewCalibFilePath
            // 
            this.txtNewCalibFilePath.Location = new System.Drawing.Point(114, 91);
            this.txtNewCalibFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtNewCalibFilePath.Name = "txtNewCalibFilePath";
            this.txtNewCalibFilePath.Size = new System.Drawing.Size(297, 21);
            this.txtNewCalibFilePath.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "TaskFilePath :";
            // 
            // txtNewCalibId
            // 
            this.txtNewCalibId.Location = new System.Drawing.Point(114, 55);
            this.txtNewCalibId.Margin = new System.Windows.Forms.Padding(2);
            this.txtNewCalibId.Name = "txtNewCalibId";
            this.txtNewCalibId.Size = new System.Drawing.Size(297, 21);
            this.txtNewCalibId.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "Id:";
            // 
            // txtNewCalibName
            // 
            this.txtNewCalibName.Location = new System.Drawing.Point(114, 20);
            this.txtNewCalibName.Margin = new System.Windows.Forms.Padding(2);
            this.txtNewCalibName.Name = "txtNewCalibName";
            this.txtNewCalibName.Size = new System.Drawing.Size(297, 21);
            this.txtNewCalibName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name:";
            // 
            // FrmAddNewCalibTool
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(461, 193);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.BtnSelectFile);
            this.Controls.Add(this.txtNewCalibFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNewCalibId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewCalibName);
            this.Controls.Add(this.label1);
            this.Name = "FrmAddNewCalibTool";
            this.Text = "New CalibTool *";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button BtnSelectFile;
        private System.Windows.Forms.TextBox txtNewCalibFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewCalibId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewCalibName;
        private System.Windows.Forms.Label label1;
    }
}