namespace Hix_CCD_Module.UI
{
    partial class FrmResultsView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResultsView));
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultsGridView1 = new Bing.Controls.ResultsGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.terminalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdjustCF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.补偿系数2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.补偿系数3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.补偿系数4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExceptValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TolMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TolMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlawType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.resultsGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.terminalBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Width = 50;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 125;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.Width = 60;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.Width = 125;
            // 
            // valueDataGridViewTextBoxColumn1
            // 
            this.valueDataGridViewTextBoxColumn1.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn1.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.valueDataGridViewTextBoxColumn1.Name = "valueDataGridViewTextBoxColumn1";
            this.valueDataGridViewTextBoxColumn1.Width = 125;
            // 
            // resultsGridView1
            // 
            this.resultsGridView1.AllowUserToAddRows = false;
            this.resultsGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.resultsGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.resultsGridView1.AutoGenerateColumns = false;
            this.resultsGridView1.BackgroundColor = System.Drawing.Color.White;
            this.resultsGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultsGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.resultsGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.resultsGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn2,
            this.nameDataGridViewTextBoxColumn2,
            this.valueDataGridViewTextBoxColumn2,
            this.Value2,
            this.Value3,
            this.Value4,
            this.AdjustCF,
            this.补偿系数2,
            this.补偿系数3,
            this.补偿系数4,
            this.ExceptValue,
            this.TolMax,
            this.TolMin,
            this.FlawType,
            this.Description});
            this.resultsGridView1.DataSource = this.terminalBindingSource;
            this.resultsGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGridView1.EnableHeadersVisualStyles = false;
            this.resultsGridView1.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.resultsGridView1.Location = new System.Drawing.Point(0, 0);
            this.resultsGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resultsGridView1.Name = "resultsGridView1";
            this.resultsGridView1.RowHeadersVisible = false;
            this.resultsGridView1.RowHeadersWidth = 51;
            this.resultsGridView1.RowTemplate.Height = 23;
            this.resultsGridView1.Size = new System.Drawing.Size(803, 136);
            this.resultsGridView1.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn1.HeaderText = "Value";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn2.HeaderText = "值";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // terminalBindingSource
            // 
            this.terminalBindingSource.DataSource = typeof(Bing.IVisionTool.Terminal);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn3.HeaderText = "检测值";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // idDataGridViewTextBoxColumn2
            // 
            this.idDataGridViewTextBoxColumn2.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn2.HeaderText = "序号";
            this.idDataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn2.Name = "idDataGridViewTextBoxColumn2";
            this.idDataGridViewTextBoxColumn2.Width = 45;
            // 
            // nameDataGridViewTextBoxColumn2
            // 
            this.nameDataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn2.HeaderText = "名称";
            this.nameDataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn2.Name = "nameDataGridViewTextBoxColumn2";
            this.nameDataGridViewTextBoxColumn2.Width = 65;
            // 
            // valueDataGridViewTextBoxColumn2
            // 
            this.valueDataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.valueDataGridViewTextBoxColumn2.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn2.HeaderText = "检测值1";
            this.valueDataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.valueDataGridViewTextBoxColumn2.Name = "valueDataGridViewTextBoxColumn2";
            this.valueDataGridViewTextBoxColumn2.Width = 88;
            // 
            // Value2
            // 
            this.Value2.DataPropertyName = "Value2";
            this.Value2.HeaderText = "检测值2";
            this.Value2.Name = "Value2";
            this.Value2.Width = 88;
            // 
            // Value3
            // 
            this.Value3.DataPropertyName = "Value3";
            this.Value3.HeaderText = "检测值3";
            this.Value3.Name = "Value3";
            this.Value3.Width = 88;
            // 
            // Value4
            // 
            this.Value4.DataPropertyName = "Value4";
            this.Value4.HeaderText = "检测值4";
            this.Value4.Name = "Value4";
            this.Value4.Width = 88;
            // 
            // AdjustCF
            // 
            this.AdjustCF.DataPropertyName = "AdjustCF";
            this.AdjustCF.HeaderText = "补偿系数1";
            this.AdjustCF.Name = "AdjustCF";
            this.AdjustCF.Width = 60;
            // 
            // 补偿系数2
            // 
            this.补偿系数2.DataPropertyName = "AdjustCF2";
            this.补偿系数2.HeaderText = "补偿系数2";
            this.补偿系数2.Name = "补偿系数2";
            this.补偿系数2.Width = 60;
            // 
            // 补偿系数3
            // 
            this.补偿系数3.DataPropertyName = "AdjustCF3";
            this.补偿系数3.HeaderText = "补偿系数3";
            this.补偿系数3.Name = "补偿系数3";
            this.补偿系数3.Width = 60;
            // 
            // 补偿系数4
            // 
            this.补偿系数4.DataPropertyName = "AdjustCF4";
            this.补偿系数4.HeaderText = "补偿系数4";
            this.补偿系数4.Name = "补偿系数4";
            this.补偿系数4.Width = 60;
            // 
            // ExceptValue
            // 
            this.ExceptValue.DataPropertyName = "ExceptValue";
            this.ExceptValue.HeaderText = "理论值";
            this.ExceptValue.Name = "ExceptValue";
            this.ExceptValue.Width = 65;
            // 
            // TolMax
            // 
            this.TolMax.DataPropertyName = "TolMax";
            this.TolMax.HeaderText = "公差上限";
            this.TolMax.Name = "TolMax";
            this.TolMax.Width = 60;
            // 
            // TolMin
            // 
            this.TolMin.DataPropertyName = "TolMin";
            this.TolMin.HeaderText = "公差下限";
            this.TolMin.Name = "TolMin";
            this.TolMin.Width = 60;
            // 
            // FlawType
            // 
            this.FlawType.DataPropertyName = "FlawType";
            this.FlawType.HeaderText = "NG类型";
            this.FlawType.Name = "FlawType";
            this.FlawType.Width = 60;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            this.Description.Width = 120;
            // 
            // FrmResultsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 136);
            this.Controls.Add(this.resultsGridView1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmResultsView";
            this.Text = "参数与结果";
            ((System.ComponentModel.ISupportInitialize)(this.resultsGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.terminalBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource terminalBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn1;
        private Bing.Controls.ResultsGridView resultsGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value4;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdjustCF;
        private System.Windows.Forms.DataGridViewTextBoxColumn 补偿系数2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 补偿系数3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 补偿系数4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExceptValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn TolMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn TolMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlawType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}