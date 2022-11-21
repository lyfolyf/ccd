namespace Hix_CCD_Module.Controls
{
    partial class CImageFlow
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CImageFlow));
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.cbTasks = new System.Windows.Forms.ComboBox();
            this.gbCamera = new System.Windows.Forms.GroupBox();
            this.txtGain = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.cbTaskInputImage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbId = new System.Windows.Forms.Label();
            this.gbCamera.SuspendLayout();
            this.gbTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCameras
            // 
            this.cbCameras.BackColor = System.Drawing.Color.Bisque;
            this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameras.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(6, 19);
            this.cbCameras.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(226, 24);
            this.cbCameras.TabIndex = 10;
            // 
            // cbTasks
            // 
            this.cbTasks.BackColor = System.Drawing.Color.Bisque;
            this.cbTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTasks.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTasks.FormattingEnabled = true;
            this.cbTasks.Location = new System.Drawing.Point(4, 19);
            this.cbTasks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbTasks.Name = "cbTasks";
            this.cbTasks.Size = new System.Drawing.Size(226, 24);
            this.cbTasks.TabIndex = 13;
            this.cbTasks.SelectedIndexChanged += new System.EventHandler(this.CbTasks_SelectedIndexChanged);
            // 
            // gbCamera
            // 
            this.gbCamera.Controls.Add(this.txtGain);
            this.gbCamera.Controls.Add(this.label2);
            this.gbCamera.Controls.Add(this.txtExp);
            this.gbCamera.Controls.Add(this.label1);
            this.gbCamera.Controls.Add(this.cbCameras);
            this.gbCamera.Location = new System.Drawing.Point(35, 0);
            this.gbCamera.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCamera.Size = new System.Drawing.Size(236, 94);
            this.gbCamera.TabIndex = 14;
            this.gbCamera.TabStop = false;
            this.gbCamera.Text = "Camera";
            // 
            // txtGain
            // 
            this.txtGain.Location = new System.Drawing.Point(165, 55);
            this.txtGain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGain.Name = "txtGain";
            this.txtGain.Size = new System.Drawing.Size(55, 21);
            this.txtGain.TabIndex = 14;
            this.txtGain.Text = "0";
            this.txtGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Gain :";
            // 
            // txtExp
            // 
            this.txtExp.Location = new System.Drawing.Point(52, 55);
            this.txtExp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtExp.Name = "txtExp";
            this.txtExp.Size = new System.Drawing.Size(55, 21);
            this.txtExp.TabIndex = 12;
            this.txtExp.Text = "20";
            this.txtExp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "Exp :";
            // 
            // gbTask
            // 
            this.gbTask.Controls.Add(this.cbTaskInputImage);
            this.gbTask.Controls.Add(this.label3);
            this.gbTask.Controls.Add(this.cbTasks);
            this.gbTask.Location = new System.Drawing.Point(307, 0);
            this.gbTask.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbTask.Name = "gbTask";
            this.gbTask.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbTask.Size = new System.Drawing.Size(234, 94);
            this.gbTask.TabIndex = 15;
            this.gbTask.TabStop = false;
            this.gbTask.Text = "Task";
            // 
            // cbTaskInputImage
            // 
            this.cbTaskInputImage.BackColor = System.Drawing.Color.Bisque;
            this.cbTaskInputImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTaskInputImage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTaskInputImage.FormattingEnabled = true;
            this.cbTaskInputImage.Location = new System.Drawing.Point(86, 58);
            this.cbTaskInputImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbTaskInputImage.Name = "cbTaskInputImage";
            this.cbTaskInputImage.Size = new System.Drawing.Size(144, 24);
            this.cbTaskInputImage.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "InputImage :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(275, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // lbId
            // 
            this.lbId.BackColor = System.Drawing.Color.LemonChiffon;
            this.lbId.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbId.Location = new System.Drawing.Point(2, 34);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(33, 28);
            this.lbId.TabIndex = 16;
            this.lbId.Text = "999";
            this.lbId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CImageFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.gbTask);
            this.Controls.Add(this.gbCamera);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "CImageFlow";
            this.Size = new System.Drawing.Size(545, 100);
            this.gbCamera.ResumeLayout(false);
            this.gbCamera.PerformLayout();
            this.gbTask.ResumeLayout(false);
            this.gbTask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbTasks;
        private System.Windows.Forms.GroupBox gbCamera;
        private System.Windows.Forms.GroupBox gbTask;
        private System.Windows.Forms.TextBox txtGain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTaskInputImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbId;
    }
}
