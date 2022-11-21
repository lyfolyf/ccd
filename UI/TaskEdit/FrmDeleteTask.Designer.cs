using Hix_CCD_Module.Setting;
namespace Hix_CCD_Module.UI
{
    partial class FrmDeleteTask
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
            this.cbTasks = new System.Windows.Forms.ComboBox();
            this.taskInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.taskInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbTasks
            // 
            this.cbTasks.DataSource = this.taskInfoBindingSource;
            this.cbTasks.DisplayMember = "Name";
            this.cbTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTasks.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTasks.FormattingEnabled = true;
            this.cbTasks.Location = new System.Drawing.Point(12, 49);
            this.cbTasks.Name = "cbTasks";
            this.cbTasks.Size = new System.Drawing.Size(300, 28);
            this.cbTasks.TabIndex = 0;
            // 
            // taskInfoBindingSource
            // 
            this.taskInfoBindingSource.DataSource = typeof(Hix_CCD_Module.Setting.TaskInfo);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(339, 49);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 28);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // FrmDeleteTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 124);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cbTasks);
            this.Name = "FrmDeleteTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Task *";
            this.Load += new System.EventHandler(this.FrmDeleteTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.taskInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTasks;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.BindingSource taskInfoBindingSource;
    }
}