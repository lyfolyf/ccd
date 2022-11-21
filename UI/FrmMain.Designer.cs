namespace Hix_CCD_Module
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.existingTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.existingCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.existingCalibToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hIKVISIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复检ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nG类型定义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayWindoowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CameraEditStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.resultsWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.parametersWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开IO调试窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolChangeJob = new System.Windows.Forms.ToolStripComboBox();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.msMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(764, 25);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.existingTaskToolStripMenuItem,
            this.addTaskToolStripMenuItem,
            this.deleteTaskToolStripMenuItem,
            this.toolStripSeparator3,
            this.existingCameraToolStripMenuItem,
            this.addCameraToolStripMenuItem,
            this.deleteCameraToolStripMenuItem,
            this.toolStripSeparator4,
            this.existingCalibToolToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.editToolStripMenuItem.Text = "编辑";
            // 
            // existingTaskToolStripMenuItem
            // 
            this.existingTaskToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("existingTaskToolStripMenuItem.Image")));
            this.existingTaskToolStripMenuItem.Name = "existingTaskToolStripMenuItem";
            this.existingTaskToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.existingTaskToolStripMenuItem.Text = "编辑任务";
            this.existingTaskToolStripMenuItem.Click += new System.EventHandler(this.ExistingTaskToolStripMenuItem_Click);
            // 
            // addTaskToolStripMenuItem
            // 
            this.addTaskToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addTaskToolStripMenuItem.Image")));
            this.addTaskToolStripMenuItem.Name = "addTaskToolStripMenuItem";
            this.addTaskToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addTaskToolStripMenuItem.Text = "新增任务";
            this.addTaskToolStripMenuItem.Click += new System.EventHandler(this.AddTaskToolStripMenuItem_Click);
            // 
            // deleteTaskToolStripMenuItem
            // 
            this.deleteTaskToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteTaskToolStripMenuItem.Image")));
            this.deleteTaskToolStripMenuItem.Name = "deleteTaskToolStripMenuItem";
            this.deleteTaskToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deleteTaskToolStripMenuItem.Text = "删除任务";
            this.deleteTaskToolStripMenuItem.Click += new System.EventHandler(this.DeleteTaskToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // existingCameraToolStripMenuItem
            // 
            this.existingCameraToolStripMenuItem.Name = "existingCameraToolStripMenuItem";
            this.existingCameraToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.existingCameraToolStripMenuItem.Text = "编辑相机";
            this.existingCameraToolStripMenuItem.Click += new System.EventHandler(this.ExistingCameraToolStripMenuItem_Click);
            // 
            // addCameraToolStripMenuItem
            // 
            this.addCameraToolStripMenuItem.Name = "addCameraToolStripMenuItem";
            this.addCameraToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addCameraToolStripMenuItem.Text = "新增相机";
            this.addCameraToolStripMenuItem.Click += new System.EventHandler(this.AddCameraToolStripMenuItem_Click);
            // 
            // deleteCameraToolStripMenuItem
            // 
            this.deleteCameraToolStripMenuItem.Name = "deleteCameraToolStripMenuItem";
            this.deleteCameraToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deleteCameraToolStripMenuItem.Text = "删除相机";
            this.deleteCameraToolStripMenuItem.Click += new System.EventHandler(this.DeleteCameraToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // existingCalibToolToolStripMenuItem
            // 
            this.existingCalibToolToolStripMenuItem.Name = "existingCalibToolToolStripMenuItem";
            this.existingCalibToolToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.existingCalibToolToolStripMenuItem.Text = "编辑标定工具";
            this.existingCalibToolToolStripMenuItem.Click += new System.EventHandler(this.ExistingCalibToolToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileHorizontalToolStripMenuItem,
            this.hIKVISIONToolStripMenuItem,
            this.imageViewToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.viewToolStripMenuItem.Text = "视图";
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.tileHorizontalToolStripMenuItem.Text = "流";
            this.tileHorizontalToolStripMenuItem.Visible = false;
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // hIKVISIONToolStripMenuItem
            // 
            this.hIKVISIONToolStripMenuItem.Name = "hIKVISIONToolStripMenuItem";
            this.hIKVISIONToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.hIKVISIONToolStripMenuItem.Text = "HIKVISION";
            this.hIKVISIONToolStripMenuItem.Visible = false;
            this.hIKVISIONToolStripMenuItem.Click += new System.EventHandler(this.HIKVISIONToolStripMenuItem_Click);
            // 
            // imageViewToolStripMenuItem
            // 
            this.imageViewToolStripMenuItem.Name = "imageViewToolStripMenuItem";
            this.imageViewToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.imageViewToolStripMenuItem.Text = "ImageView";
            this.imageViewToolStripMenuItem.Click += new System.EventHandler(this.ImageViewToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Checked = true;
            this.settingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skinToolStripMenuItem,
            this.calibrationToolStripMenuItem,
            this.复检ToolStripMenuItem,
            this.nG类型定义ToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.settingToolStripMenuItem.Text = "设置";
            // 
            // skinToolStripMenuItem
            // 
            this.skinToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("skinToolStripMenuItem.Image")));
            this.skinToolStripMenuItem.Name = "skinToolStripMenuItem";
            this.skinToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.skinToolStripMenuItem.Text = "皮肤";
            this.skinToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // calibrationToolStripMenuItem
            // 
            this.calibrationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("calibrationToolStripMenuItem.Image")));
            this.calibrationToolStripMenuItem.Name = "calibrationToolStripMenuItem";
            this.calibrationToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.calibrationToolStripMenuItem.Text = "标定";
            this.calibrationToolStripMenuItem.Click += new System.EventHandler(this.CalibrationToolStripMenuItem_Click);
            // 
            // 复检ToolStripMenuItem
            // 
            this.复检ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("复检ToolStripMenuItem.Image")));
            this.复检ToolStripMenuItem.Name = "复检ToolStripMenuItem";
            this.复检ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.复检ToolStripMenuItem.Text = "复检";
            this.复检ToolStripMenuItem.Click += new System.EventHandler(this.复检ToolStripMenuItem_Click);
            // 
            // nG类型定义ToolStripMenuItem
            // 
            this.nG类型定义ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nG类型定义ToolStripMenuItem.Image")));
            this.nG类型定义ToolStripMenuItem.Name = "nG类型定义ToolStripMenuItem";
            this.nG类型定义ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.nG类型定义ToolStripMenuItem.Text = "NG类型定义";
            this.nG类型定义ToolStripMenuItem.Click += new System.EventHandler(this.nG类型定义ToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayWindoowToolStripMenuItem,
            this.taskEditToolStripMenuItem,
            this.CameraEditStripMenuItem,
            this.toolStripSeparator1,
            this.resultsWindowToolStripMenuItem,
            this.toolStripSeparator2,
            this.parametersWindowToolStripMenuItem,
            this.statusWindowToolStripMenuItem,
            this.logWindowToolStripMenuItem,
            this.打开IO调试窗口ToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.windowsToolStripMenuItem.Text = "窗口";
            // 
            // displayWindoowToolStripMenuItem
            // 
            this.displayWindoowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("displayWindoowToolStripMenuItem.Image")));
            this.displayWindoowToolStripMenuItem.Name = "displayWindoowToolStripMenuItem";
            this.displayWindoowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.displayWindoowToolStripMenuItem.Text = "打开显示窗口";
            this.displayWindoowToolStripMenuItem.Click += new System.EventHandler(this.displayWindoowToolStripMenuItem_Click);
            // 
            // taskEditToolStripMenuItem
            // 
            this.taskEditToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("taskEditToolStripMenuItem.Image")));
            this.taskEditToolStripMenuItem.Name = "taskEditToolStripMenuItem";
            this.taskEditToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.taskEditToolStripMenuItem.Text = "打开任务窗口";
            this.taskEditToolStripMenuItem.Click += new System.EventHandler(this.taskEditToolStripMenuItem_Click);
            // 
            // CameraEditStripMenuItem
            // 
            this.CameraEditStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CameraEditStripMenuItem.Image")));
            this.CameraEditStripMenuItem.Name = "CameraEditStripMenuItem";
            this.CameraEditStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CameraEditStripMenuItem.Text = "打开检测窗口";
            this.CameraEditStripMenuItem.Click += new System.EventHandler(this.CameraEditStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // resultsWindowToolStripMenuItem
            // 
            this.resultsWindowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resultsWindowToolStripMenuItem.Image")));
            this.resultsWindowToolStripMenuItem.Name = "resultsWindowToolStripMenuItem";
            this.resultsWindowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.resultsWindowToolStripMenuItem.Text = "打开结果窗口";
            this.resultsWindowToolStripMenuItem.Click += new System.EventHandler(this.resultsWindowToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // parametersWindowToolStripMenuItem
            // 
            this.parametersWindowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("parametersWindowToolStripMenuItem.Image")));
            this.parametersWindowToolStripMenuItem.Name = "parametersWindowToolStripMenuItem";
            this.parametersWindowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.parametersWindowToolStripMenuItem.Text = "打开参数窗口";
            this.parametersWindowToolStripMenuItem.Click += new System.EventHandler(this.ParametersWindowToolStripMenuItem_Click);
            // 
            // statusWindowToolStripMenuItem
            // 
            this.statusWindowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("statusWindowToolStripMenuItem.Image")));
            this.statusWindowToolStripMenuItem.Name = "statusWindowToolStripMenuItem";
            this.statusWindowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.statusWindowToolStripMenuItem.Text = "打开状态窗口";
            // 
            // logWindowToolStripMenuItem
            // 
            this.logWindowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logWindowToolStripMenuItem.Image")));
            this.logWindowToolStripMenuItem.Name = "logWindowToolStripMenuItem";
            this.logWindowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.logWindowToolStripMenuItem.Text = "打开消息窗口";
            this.logWindowToolStripMenuItem.Click += new System.EventHandler(this.LogWindowToolStripMenuItem_Click);
            // 
            // 打开IO调试窗口ToolStripMenuItem
            // 
            this.打开IO调试窗口ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开IO调试窗口ToolStripMenuItem.Image")));
            this.打开IO调试窗口ToolStripMenuItem.Name = "打开IO调试窗口ToolStripMenuItem";
            this.打开IO调试窗口ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开IO调试窗口ToolStripMenuItem.Text = "打开调试窗口";
            this.打开IO调试窗口ToolStripMenuItem.Click += new System.EventHandler(this.打开调试窗口ToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.aboutToolStripMenuItem.Text = "关于";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // tsMain
            // 
            this.tsMain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tsMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsMain.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRun,
            this.toolStripButton1,
            this.toolStripButton5,
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolChangeJob});
            this.tsMain.Location = new System.Drawing.Point(0, 25);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(764, 37);
            this.tsMain.TabIndex = 2;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbtnRun
            // 
            this.tsbtnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbtnRun.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRun.Image")));
            this.tsbtnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRun.Name = "tsbtnRun";
            this.tsbtnRun.Size = new System.Drawing.Size(75, 34);
            this.tsbtnRun.Text = "运行";
            this.tsbtnRun.Click += new System.EventHandler(this.TsbtnRun_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(75, 34);
            this.toolStripButton1.Text = "点检";
            this.toolStripButton1.ToolTipText = "点检";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(107, 34);
            this.toolStripButton5.Text = "视觉复位";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(14, 34);
            this.toolStripLabel1.Text = "|";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(73, 34);
            this.toolStripLabel2.Text = "任务切换";
            // 
            // toolChangeJob
            // 
            this.toolChangeJob.AutoSize = false;
            this.toolChangeJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolChangeJob.Items.AddRange(new object[] {
            "粉色",
            "银色",
            "黑色"});
            this.toolChangeJob.Name = "toolChangeJob";
            this.toolChangeJob.Size = new System.Drawing.Size(121, 25);
            this.toolChangeJob.SelectedIndexChanged += new System.EventHandler(this.toolChangeJob_SelectedIndexChanged);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Location = new System.Drawing.Point(0, 444);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStripMain.Size = new System.Drawing.Size(764, 22);
            this.statusStripMain.TabIndex = 3;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.BackColor = System.Drawing.Color.Silver;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.Color.LightGray;
            this.dockPanel.Location = new System.Drawing.Point(0, 62);
            this.dockPanel.Margin = new System.Windows.Forms.Padding(2);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(764, 382);
            this.dockPanel.Skin = ((WeifenLuo.WinFormsUI.Docking.DockPanelSkin)(resources.GetObject("dockPanel.Skin")));
            this.dockPanel.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(764, 466);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hix CCD Module";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripMenuItem displayWindoowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logWindowToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripButton tsbtnRun;
        private System.Windows.Forms.ToolStripMenuItem taskEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem existingTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skinToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem addTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem existingCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem CameraEditStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hIKVISIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem existingCalibToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开IO调试窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripMenuItem 复检ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nG类型定义ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripComboBox toolChangeJob;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}

