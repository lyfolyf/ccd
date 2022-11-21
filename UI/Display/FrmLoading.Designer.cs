namespace Hix_CCD_Module.UI
{
    partial class FrmLoading
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
            this.bingProgressBar1 = new Bing.Controls.BingMsgProgressBar();
            this.SuspendLayout();
            // 
            // bingProgressBar1
            // 
            this.bingProgressBar1.BackColor = System.Drawing.SystemColors.Info;
            this.bingProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bingProgressBar1.ForeColor = System.Drawing.Color.PaleGreen;
            this.bingProgressBar1.Location = new System.Drawing.Point(0, 0);
            this.bingProgressBar1.Message = "加载中...";
            this.bingProgressBar1.Name = "bingProgressBar1";
            this.bingProgressBar1.Size = new System.Drawing.Size(627, 45);
            this.bingProgressBar1.TabIndex = 4;
            this.bingProgressBar1.Value = 5;
            // 
            // FrmLoading
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(627, 45);
            this.Controls.Add(this.bingProgressBar1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.OrangeRed;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLoading";
            this.Opacity = 0.8D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLoading";
            this.TopMost = true;
            this.Resize += new System.EventHandler(this.FrmLoading_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Bing.Controls.BingMsgProgressBar bingProgressBar1;
    }
}