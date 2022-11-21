namespace Hix_CCD_Module.UI
{
    partial class FrmAddNewTask
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewTaskName = new System.Windows.Forms.TextBox();
            this.txtNewTaskDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewTaskFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnSelectFile = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtNewTaskName
            // 
            this.txtNewTaskName.Location = new System.Drawing.Point(157, 29);
            this.txtNewTaskName.Name = "txtNewTaskName";
            this.txtNewTaskName.Size = new System.Drawing.Size(395, 25);
            this.txtNewTaskName.TabIndex = 1;
            // 
            // txtNewTaskDescription
            // 
            this.txtNewTaskDescription.Location = new System.Drawing.Point(157, 73);
            this.txtNewTaskDescription.Name = "txtNewTaskDescription";
            this.txtNewTaskDescription.Size = new System.Drawing.Size(395, 25);
            this.txtNewTaskDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // txtNewTaskFilePath
            // 
            this.txtNewTaskFilePath.Location = new System.Drawing.Point(157, 117);
            this.txtNewTaskFilePath.Name = "txtNewTaskFilePath";
            this.txtNewTaskFilePath.Size = new System.Drawing.Size(395, 25);
            this.txtNewTaskFilePath.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "TaskFilePath :";
            // 
            // BtnSelectFile
            // 
            this.BtnSelectFile.Font = new System.Drawing.Font("华文中宋", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSelectFile.Location = new System.Drawing.Point(558, 117);
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.Size = new System.Drawing.Size(36, 23);
            this.BtnSelectFile.TabIndex = 6;
            this.BtnSelectFile.Text = "...";
            this.BtnSelectFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnSelectFile.UseVisualStyleBackColor = true;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(116, 171);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(131, 47);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(390, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 47);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // FrmAddNewTask
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(615, 241);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.BtnSelectFile);
            this.Controls.Add(this.txtNewTaskFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNewTaskDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewTaskName);
            this.Controls.Add(this.label1);
            this.Name = "FrmAddNewTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Task *";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewTaskName;
        private System.Windows.Forms.TextBox txtNewTaskDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewTaskFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnSelectFile;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
    }
}