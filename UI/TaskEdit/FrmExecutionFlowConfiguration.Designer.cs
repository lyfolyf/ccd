namespace Hix_CCD_Module.UI
{
    partial class FrmExecutionFlowConfiguration
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
            this.cListImageFlows1 = new Hix_CCD_Module.Controls.CListImageFlows();
            this.cOrderTreeView1 = new Hix_CCD_Module.Controls.COrderTreeView();
            this.SuspendLayout();
            // 
            // cListImageFlows1
            // 
            this.cListImageFlows1.AutoScroll = true;
            this.cListImageFlows1.BackColor = System.Drawing.Color.LightGray;
            this.cListImageFlows1.Location = new System.Drawing.Point(509, 12);
            this.cListImageFlows1.Name = "cListImageFlows1";
            this.cListImageFlows1.Size = new System.Drawing.Size(573, 463);
            this.cListImageFlows1.TabIndex = 0;
            // 
            // cOrderTreeView1
            // 
            this.cOrderTreeView1.Location = new System.Drawing.Point(12, 12);
            this.cOrderTreeView1.Name = "cOrderTreeView1";
            this.cOrderTreeView1.Size = new System.Drawing.Size(478, 463);
            this.cOrderTreeView1.TabIndex = 1;
            // 
            // FrmExecutionFlowConfiguration
            // 
            this.ClientSize = new System.Drawing.Size(1111, 487);
            this.Controls.Add(this.cOrderTreeView1);
            this.Controls.Add(this.cListImageFlows1);
            this.Name = "FrmExecutionFlowConfiguration";
            this.Load += new System.EventHandler(this.FrmExecutionFlowConfiguration_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CListImageFlows cListImageFlows1;
        private Controls.COrderTreeView cOrderTreeView1;
    }
}