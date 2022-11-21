namespace Hix_CCD_Module.UI
{
    partial class FrmHikCameraEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxOnlineCameras = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxRegisteredCameras = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.propertyGridCamera = new System.Windows.Forms.PropertyGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxOnlineCameras);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统在线的相机";
            // 
            // listBoxOnlineCameras
            // 
            this.listBoxOnlineCameras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxOnlineCameras.Enabled = false;
            this.listBoxOnlineCameras.FormattingEnabled = true;
            this.listBoxOnlineCameras.ItemHeight = 12;
            this.listBoxOnlineCameras.Location = new System.Drawing.Point(3, 17);
            this.listBoxOnlineCameras.Name = "listBoxOnlineCameras";
            this.listBoxOnlineCameras.Size = new System.Drawing.Size(292, 128);
            this.listBoxOnlineCameras.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxRegisteredCameras);
            this.groupBox2.Location = new System.Drawing.Point(6, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 148);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系统注册的相机";
            // 
            // listBoxRegisteredCameras
            // 
            this.listBoxRegisteredCameras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRegisteredCameras.FormattingEnabled = true;
            this.listBoxRegisteredCameras.ItemHeight = 12;
            this.listBoxRegisteredCameras.Location = new System.Drawing.Point(3, 17);
            this.listBoxRegisteredCameras.Name = "listBoxRegisteredCameras";
            this.listBoxRegisteredCameras.Size = new System.Drawing.Size(292, 128);
            this.listBoxRegisteredCameras.TabIndex = 0;
            this.listBoxRegisteredCameras.SelectedIndexChanged += new System.EventHandler(this.ListBoxRegisteredCameras_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(33, 324);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(105, 45);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(172, 324);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(105, 45);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // propertyGridCamera
            // 
            this.propertyGridCamera.CommandsDisabledLinkColor = System.Drawing.Color.Green;
            this.propertyGridCamera.HelpForeColor = System.Drawing.Color.Black;
            this.propertyGridCamera.Location = new System.Drawing.Point(321, 3);
            this.propertyGridCamera.Name = "propertyGridCamera";
            this.propertyGridCamera.Size = new System.Drawing.Size(329, 370);
            this.propertyGridCamera.TabIndex = 4;
            this.propertyGridCamera.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridCamera_PropertyValueChanged);
            // 
            // FrmHikCameraEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 385);
            this.Controls.Add(this.propertyGridCamera);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmHikCameraEdit";
            this.Text = "Camera Edit";
            this.Load += new System.EventHandler(this.FrmHikCameraEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxOnlineCameras;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxRegisteredCameras;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.PropertyGrid propertyGridCamera;
    }
}