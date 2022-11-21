namespace Hix_CCD_Module.UI
{
    partial class FrmAddNewHikCamera
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGain = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.radioBtnGigE = new System.Windows.Forms.RadioButton();
            this.radioBtnUSB3 = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioBtnTriggerModeOff = new System.Windows.Forms.RadioButton();
            this.radioBtnTriggerModeOn = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(106, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(104, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(282, 40);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(104, 21);
            this.txtId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Id :";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(104, 256);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(280, 52);
            this.txtDescription.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description :";
            // 
            // txtExp
            // 
            this.txtExp.Location = new System.Drawing.Point(106, 79);
            this.txtExp.Name = "txtExp";
            this.txtExp.Size = new System.Drawing.Size(104, 21);
            this.txtExp.TabIndex = 7;
            this.txtExp.Text = "50";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Exposure :";
            // 
            // txtGain
            // 
            this.txtGain.Location = new System.Drawing.Point(282, 76);
            this.txtGain.Name = "txtGain";
            this.txtGain.Size = new System.Drawing.Size(104, 21);
            this.txtGain.TabIndex = 11;
            this.txtGain.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(235, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Gain :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "SN :";
            // 
            // cbCameras
            // 
            this.cbCameras.DisplayMember = "Name";
            this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameras.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(106, 115);
            this.cbCameras.Margin = new System.Windows.Forms.Padding(2);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(215, 24);
            this.cbCameras.TabIndex = 12;
            this.cbCameras.SelectedIndexChanged += new System.EventHandler(this.CbCameras_SelectedIndexChanged);
            // 
            // radioBtnGigE
            // 
            this.radioBtnGigE.AutoSize = true;
            this.radioBtnGigE.Checked = true;
            this.radioBtnGigE.Enabled = false;
            this.radioBtnGigE.Location = new System.Drawing.Point(76, 13);
            this.radioBtnGigE.Name = "radioBtnGigE";
            this.radioBtnGigE.Size = new System.Drawing.Size(47, 16);
            this.radioBtnGigE.TabIndex = 13;
            this.radioBtnGigE.TabStop = true;
            this.radioBtnGigE.Text = "GigE";
            this.radioBtnGigE.UseVisualStyleBackColor = true;
            // 
            // radioBtnUSB3
            // 
            this.radioBtnUSB3.AutoSize = true;
            this.radioBtnUSB3.Enabled = false;
            this.radioBtnUSB3.Location = new System.Drawing.Point(199, 13);
            this.radioBtnUSB3.Name = "radioBtnUSB3";
            this.radioBtnUSB3.Size = new System.Drawing.Size(47, 16);
            this.radioBtnUSB3.TabIndex = 14;
            this.radioBtnUSB3.Text = "USB3";
            this.radioBtnUSB3.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(253, 330);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 38);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(47, 330);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(98, 38);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioBtnUSB3);
            this.groupBox1.Controls.Add(this.radioBtnGigE);
            this.groupBox1.Location = new System.Drawing.Point(30, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 42);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioBtnTriggerModeOff);
            this.groupBox2.Controls.Add(this.radioBtnTriggerModeOn);
            this.groupBox2.Location = new System.Drawing.Point(30, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 42);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TriggerMode";
            // 
            // radioBtnTriggerModeOff
            // 
            this.radioBtnTriggerModeOff.AutoSize = true;
            this.radioBtnTriggerModeOff.Checked = true;
            this.radioBtnTriggerModeOff.Location = new System.Drawing.Point(199, 13);
            this.radioBtnTriggerModeOff.Name = "radioBtnTriggerModeOff";
            this.radioBtnTriggerModeOff.Size = new System.Drawing.Size(41, 16);
            this.radioBtnTriggerModeOff.TabIndex = 14;
            this.radioBtnTriggerModeOff.TabStop = true;
            this.radioBtnTriggerModeOff.Text = "Off";
            this.radioBtnTriggerModeOff.UseVisualStyleBackColor = true;
            // 
            // radioBtnTriggerModeOn
            // 
            this.radioBtnTriggerModeOn.AutoSize = true;
            this.radioBtnTriggerModeOn.Location = new System.Drawing.Point(76, 13);
            this.radioBtnTriggerModeOn.Name = "radioBtnTriggerModeOn";
            this.radioBtnTriggerModeOn.Size = new System.Drawing.Size(35, 16);
            this.radioBtnTriggerModeOn.TabIndex = 13;
            this.radioBtnTriggerModeOn.Text = "On";
            this.radioBtnTriggerModeOn.UseVisualStyleBackColor = true;
            // 
            // FrmAddNewHikCamera
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(413, 380);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbCameras);
            this.Controls.Add(this.txtGain);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtExp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "FrmAddNewHikCamera";
            this.Text = "注册新的相机";
            this.Load += new System.EventHandler(this.FrmAddNewHikCamera_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.RadioButton radioBtnGigE;
        private System.Windows.Forms.RadioButton radioBtnUSB3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioBtnTriggerModeOff;
        private System.Windows.Forms.RadioButton radioBtnTriggerModeOn;
    }
}