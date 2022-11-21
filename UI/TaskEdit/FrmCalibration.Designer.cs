namespace Hix_CCD_Module.UI
{
    partial class FrmCalibration
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cImageView1 = new Hix_CCD_Module.Controls.CImageView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCalibTool = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCalibTools = new System.Windows.Forms.ComboBox();
            this.hixCalibToolBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCamera = new System.Windows.Forms.RadioButton();
            this.rbLocal = new System.Windows.Forms.RadioButton();
            this.gbCamera = new System.Windows.Forms.GroupBox();
            this.txtGain = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtExp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.hikCameraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSet = new System.Windows.Forms.Button();
            this.btnGetImage = new System.Windows.Forms.Button();
            this.gbLocal = new System.Windows.Forms.GroupBox();
            this.txtLocalImagePath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hixCalibToolBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hikCameraBindingSource)).BeginInit();
            this.gbLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.29755F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.70245F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1212, 664);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 550);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 110);
            this.panel2.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(392, 110);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cImageView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(404, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(804, 656);
            this.panel1.TabIndex = 0;
            // 
            // cImageView1
            // 
            this.cImageView1.BackColor = System.Drawing.Color.Gray;
            this.cImageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cImageView1.Image = null;
            this.cImageView1.Location = new System.Drawing.Point(0, 0);
            this.cImageView1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cImageView1.Name = "cImageView1";
            this.cImageView1.Size = new System.Drawing.Size(804, 656);
            this.cImageView1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(392, 538);
            this.panel3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCalibTool);
            this.groupBox2.Controls.Add(this.btnCalibrate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbCalibTools);
            this.groupBox2.Location = new System.Drawing.Point(8, 369);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(376, 154);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标定工具";
            // 
            // btnCalibTool
            // 
            this.btnCalibTool.Location = new System.Drawing.Point(220, 98);
            this.btnCalibTool.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCalibTool.Name = "btnCalibTool";
            this.btnCalibTool.Size = new System.Drawing.Size(100, 29);
            this.btnCalibTool.TabIndex = 18;
            this.btnCalibTool.Text = "查看";
            this.btnCalibTool.UseVisualStyleBackColor = true;
            this.btnCalibTool.Click += new System.EventHandler(this.BtnCalibTool_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(48, 98);
            this.btnCalibrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(100, 29);
            this.btnCalibrate.TabIndex = 17;
            this.btnCalibrate.Text = "标定";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.BtnCalibrate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "选择工具：";
            // 
            // cbCalibTools
            // 
            this.cbCalibTools.DataSource = this.hixCalibToolBindingSource;
            this.cbCalibTools.DisplayMember = "Name";
            this.cbCalibTools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalibTools.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCalibTools.FormattingEnabled = true;
            this.cbCalibTools.Location = new System.Drawing.Point(127, 35);
            this.cbCalibTools.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCalibTools.Name = "cbCalibTools";
            this.cbCalibTools.Size = new System.Drawing.Size(237, 33);
            this.cbCalibTools.TabIndex = 15;
            this.cbCalibTools.SelectedIndexChanged += new System.EventHandler(this.CbCalibTools_SelectedIndexChanged);
            // 
            // hixCalibToolBindingSource
            // 
            this.hixCalibToolBindingSource.DataSource = typeof(Hix_CCD_Module.Tool.HixCalibTool);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCamera);
            this.groupBox1.Controls.Add(this.rbLocal);
            this.groupBox1.Controls.Add(this.gbCamera);
            this.groupBox1.Controls.Add(this.gbLocal);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(376, 350);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标定图片来源";
            // 
            // rbCamera
            // 
            this.rbCamera.AutoSize = true;
            this.rbCamera.Location = new System.Drawing.Point(220, 40);
            this.rbCamera.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbCamera.Name = "rbCamera";
            this.rbCamera.Size = new System.Drawing.Size(58, 19);
            this.rbCamera.TabIndex = 3;
            this.rbCamera.Text = "相机";
            this.rbCamera.UseVisualStyleBackColor = true;
            // 
            // rbLocal
            // 
            this.rbLocal.AutoSize = true;
            this.rbLocal.Checked = true;
            this.rbLocal.Location = new System.Drawing.Point(85, 40);
            this.rbLocal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbLocal.Name = "rbLocal";
            this.rbLocal.Size = new System.Drawing.Size(58, 19);
            this.rbLocal.TabIndex = 2;
            this.rbLocal.TabStop = true;
            this.rbLocal.Text = "本地";
            this.rbLocal.UseVisualStyleBackColor = true;
            this.rbLocal.CheckedChanged += new System.EventHandler(this.RbLocal_CheckedChanged);
            // 
            // gbCamera
            // 
            this.gbCamera.Controls.Add(this.txtGain);
            this.gbCamera.Controls.Add(this.label6);
            this.gbCamera.Controls.Add(this.txtExp);
            this.gbCamera.Controls.Add(this.label4);
            this.gbCamera.Controls.Add(this.cbCameras);
            this.gbCamera.Controls.Add(this.btnSet);
            this.gbCamera.Controls.Add(this.btnGetImage);
            this.gbCamera.Enabled = false;
            this.gbCamera.Location = new System.Drawing.Point(8, 176);
            this.gbCamera.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbCamera.Size = new System.Drawing.Size(368, 166);
            this.gbCamera.TabIndex = 1;
            this.gbCamera.TabStop = false;
            this.gbCamera.Text = "相机";
            // 
            // txtGain
            // 
            this.txtGain.Location = new System.Drawing.Point(77, 126);
            this.txtGain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGain.Name = "txtGain";
            this.txtGain.Size = new System.Drawing.Size(137, 25);
            this.txtGain.TabIndex = 19;
            this.txtGain.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 130);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Gain :";
            // 
            // txtExp
            // 
            this.txtExp.Location = new System.Drawing.Point(77, 82);
            this.txtExp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtExp.Name = "txtExp";
            this.txtExp.Size = new System.Drawing.Size(137, 25);
            this.txtExp.TabIndex = 17;
            this.txtExp.Text = "50";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Exp :";
            // 
            // cbCameras
            // 
            this.cbCameras.DataSource = this.hikCameraBindingSource;
            this.cbCameras.DisplayMember = "Name";
            this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameras.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(9, 24);
            this.cbCameras.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(241, 33);
            this.cbCameras.TabIndex = 15;
            this.cbCameras.SelectedIndexChanged += new System.EventHandler(this.CbCameras_SelectedIndexChanged);
            // 
            // hikCameraBindingSource
            // 
            this.hikCameraBindingSource.DataSource = typeof(Hix_CCD_Module.Tool.HikCamera);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Location = new System.Drawing.Point(257, 112);
            this.btnSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(100, 32);
            this.btnSet.TabIndex = 14;
            this.btnSet.Text = "选择";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.BtnSet_Click);
            // 
            // btnGetImage
            // 
            this.btnGetImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetImage.Location = new System.Drawing.Point(257, 24);
            this.btnGetImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetImage.Name = "btnGetImage";
            this.btnGetImage.Size = new System.Drawing.Size(100, 74);
            this.btnGetImage.TabIndex = 13;
            this.btnGetImage.Text = "拍照";
            this.btnGetImage.UseVisualStyleBackColor = true;
            this.btnGetImage.Click += new System.EventHandler(this.BtnGetImage_Click);
            // 
            // gbLocal
            // 
            this.gbLocal.Controls.Add(this.txtLocalImagePath);
            this.gbLocal.Controls.Add(this.btnOpen);
            this.gbLocal.Location = new System.Drawing.Point(8, 85);
            this.gbLocal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbLocal.Name = "gbLocal";
            this.gbLocal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbLocal.Size = new System.Drawing.Size(368, 84);
            this.gbLocal.TabIndex = 0;
            this.gbLocal.TabStop = false;
            this.gbLocal.Text = "本地";
            // 
            // txtLocalImagePath
            // 
            this.txtLocalImagePath.Location = new System.Drawing.Point(9, 18);
            this.txtLocalImagePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLocalImagePath.Multiline = true;
            this.txtLocalImagePath.Name = "txtLocalImagePath";
            this.txtLocalImagePath.Size = new System.Drawing.Size(281, 58);
            this.txtLocalImagePath.TabIndex = 14;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(299, 19);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(59, 54);
            this.btnOpen.TabIndex = 13;
            this.btnOpen.Text = "选择文件";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // FrmCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 664);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmCalibration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标定";
            this.Load += new System.EventHandler(this.FrmCalibration_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hixCalibToolBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbCamera.ResumeLayout(false);
            this.gbCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hikCameraBindingSource)).EndInit();
            this.gbLocal.ResumeLayout(false);
            this.gbLocal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbCamera;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnGetImage;
        private System.Windows.Forms.GroupBox gbLocal;
        private System.Windows.Forms.TextBox txtLocalImagePath;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.RadioButton rbCamera;
        private System.Windows.Forms.RadioButton rbLocal;
        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCalibTool;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCalibTools;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.BindingSource hikCameraBindingSource;
        private System.Windows.Forms.TextBox txtGain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtExp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource hixCalibToolBindingSource;
        private Controls.CImageView cImageView1;
    }
}