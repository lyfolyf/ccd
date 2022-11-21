using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static Hix_CCD_Module.FrmMain;

namespace Hix_CCD_Module
{
    public partial class FlawTypeDefine : DockContent
    {
        public FlawTypeDefine()
        {
            InitializeComponent();
            FreshData();
        }
        private void FreshData()
        {
            this.dataGridView1.Rows.Clear();
            for (int i=1;i<=FlawTypeDictionary.Count;i++)
            {
                string[] str = new string[2] { i.ToString(), FlawTypeDictionary[i] };
                this.dataGridView1.Rows.Add(str);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FlawTypeDictionary.Clear();
            for (int i=1;i<=this.dataGridView1.Rows.Count;i++)
            {
                string name = this.dataGridView1.Rows[i - 1].Cells[1].Value == null ? "" : this.dataGridView1.Rows[i - 1].Cells[1].Value.ToString();
                FlawTypeDictionary.Add(i, name);
            }
            SaveNGDefine();
        }
    }
}
