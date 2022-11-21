namespace Hix_CCD_Module.Controls
{
    partial class CImageView
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
            this.bingPictrueBox1 = new Bing.Controls.BingPictrueBox();
            ((System.ComponentModel.ISupportInitialize)(this.bingPictrueBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bingPictrueBox1
            // 
            this.bingPictrueBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bingPictrueBox1.IsInit = true;
            this.bingPictrueBox1.Location = new System.Drawing.Point(0, 0);
            this.bingPictrueBox1.Name = "bingPictrueBox1";
            this.bingPictrueBox1.Size = new System.Drawing.Size(532, 347);
            this.bingPictrueBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bingPictrueBox1.TabIndex = 0;
            this.bingPictrueBox1.TabStop = false;
            // 
            // CImageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.bingPictrueBox1);
            this.Name = "CImageView";
            this.Size = new System.Drawing.Size(532, 347);
            ((System.ComponentModel.ISupportInitialize)(this.bingPictrueBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bing.Controls.BingPictrueBox bingPictrueBox1;
    }
}
