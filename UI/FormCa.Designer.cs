namespace Hix_CCD_Module.UI
{
    partial class FormCa
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbLocal = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cbCalibTools = new System.Windows.Forms.ComboBox();
            this.txtLocalImagePath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbLocal.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.36053F));
            this.tableLayoutPanel1.Controls.Add(this.gbLocal, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(358, 530);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gbLocal
            // 
            this.gbLocal.Controls.Add(this.groupBox2);
            this.gbLocal.Controls.Add(this.txtLocalImagePath);
            this.gbLocal.Location = new System.Drawing.Point(3, 3);
            this.gbLocal.Name = "gbLocal";
            this.gbLocal.Size = new System.Drawing.Size(350, 524);
            this.gbLocal.TabIndex = 1;
            this.gbLocal.TabStop = false;
            this.gbLocal.Text = "本地";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCalibrate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.cbCalibTools);
            this.groupBox2.Location = new System.Drawing.Point(0, 404);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 123);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标定工具";
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(183, 72);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(91, 45);
            this.btnCalibrate.TabIndex = 17;
            this.btnCalibrate.Text = "标定";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "选择工具：";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(95, 72);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(74, 46);
            this.btnOpen.TabIndex = 13;
            this.btnOpen.Text = "选择文件";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbCalibTools
            // 
            this.cbCalibTools.DisplayMember = "Name";
            this.cbCalibTools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalibTools.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCalibTools.FormattingEnabled = true;
            this.cbCalibTools.Items.AddRange(new object[] {
            "1#",
            "2#"});
            this.cbCalibTools.Location = new System.Drawing.Point(95, 28);
            this.cbCalibTools.Margin = new System.Windows.Forms.Padding(2);
            this.cbCalibTools.Name = "cbCalibTools";
            this.cbCalibTools.Size = new System.Drawing.Size(179, 28);
            this.cbCalibTools.TabIndex = 15;
            // 
            // txtLocalImagePath
            // 
            this.txtLocalImagePath.Location = new System.Drawing.Point(7, 14);
            this.txtLocalImagePath.Multiline = true;
            this.txtLocalImagePath.Name = "txtLocalImagePath";
            this.txtLocalImagePath.Size = new System.Drawing.Size(337, 384);
            this.txtLocalImagePath.TabIndex = 14;
            // 
            // FormCa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 530);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(374, 569);
            this.MinimumSize = new System.Drawing.Size(374, 569);
            this.Name = "FormCa";
            this.Text = "标定";
            this.Load += new System.EventHandler(this.FormCa_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbLocal.ResumeLayout(false);
            this.gbLocal.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbLocal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ComboBox cbCalibTools;
        private System.Windows.Forms.TextBox txtLocalImagePath;
    }
}