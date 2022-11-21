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
using System.IO;
using Mark.CommonFile;
using Lead.Tool.MongoDB;

namespace Hix_CCD_Module.UI
{
    public partial class FrmResult : DockContent
    {
        public FrmResult()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }



        public  void AddResultDgv2(List<FAIjudge> list,int loc,string result)
        {
            try
            {
                dgv2.Rows.Insert(0,1);
                dgv2.Rows[0].Cells[0].Value = loc;
                dgv2.Rows[0].Cells[1].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                dgv2.Rows[0].Cells[2].Value = result;
                for (int i = 0; i < list.Count; i++)
                {
                    dgv2.Rows[0].Cells[i+3].Value = list[i].测量结果;
                    if (list[i].判定结果 == "OK")
                    {
                        dgv2.Rows[0].Cells[i+3].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dgv2.Rows[0].Cells[i+3].Style.BackColor = Color.Red;
                    }
                }

                if (dgv2.Rows.Count>50)
                {
                    dgv2.Rows.RemoveAt(dgv2.Rows.Count-1);
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void FrmResult_Load(object sender, EventArgs e)
        {
            try
            {




                int a = CommonValue.ListRealValue.Count >= CommonValue.ListUpValue.Count ? CommonValue.ListRealValue.Count : CommonValue.ListUpValue.Count;
                int b = CommonValue.ListRealValue.Count >= CommonValue.ListDownValue.Count ? CommonValue.ListRealValue.Count : CommonValue.ListDownValue.Count;
                int c = a >= b ? a : b;

                if (c > 0)
                {
                    dataGridView1.Rows.Add(c);


                    if (CommonValue.ListTitle.Count > 0)
                    {
                        CommonValue.ListTitle.RemoveAt(0);
                        dgv2.Columns.Add("11", "穴位");
                        dgv2.Columns.Add("12", "时间");
                        dgv2.Columns.Add("13", "结果");

                        for (int i = 0; i < CommonValue.ListTitle.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(CommonValue.ListTitle[i]))
                            {
                                dataGridView1.Rows[i].Cells[0].Value = CommonValue.ListTitle[i];
                                dgv2.Columns.Add(i.ToString(), CommonValue.ListTitle[i]);
                                dgv2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        
                            }
     
                        }
                    }
                  
                    dgv2.ColumnHeadersVisible = true;
                    dgv2.AutoResizeColumns();
                    if (CommonValue.ListRealValue.Count>0)
                    {
                        for (int i = 0; i < CommonValue.ListRealValue.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = CommonValue.ListRealValue[i];
                        }
                    }

                    if (CommonValue.ListUpValue.Count > 0)
                    {
                        for (int i = 0; i < CommonValue.ListUpValue.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells[2].Value = CommonValue.ListUpValue[i];
                        }
                    }

                    if (CommonValue.ListDownValue.Count > 0)
                    {
                        for (int i = 0; i < CommonValue.ListDownValue.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells[3].Value = CommonValue.ListDownValue[i];
                        }
                    }

                }

            }
            catch (Exception ex)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            catch (Exception ex)
            {


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string strWriteSCV = "";
                string path = Application.StartupPath + "\\RealValueResult";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string dataFilePaht = path + "\\结果判断.csv";

                if (File.Exists(dataFilePaht))
                {
                    File.Delete(dataFilePaht);
                }
     
                strWriteSCV = dataGridView1.Columns[0].HeaderText + ",";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    {
                        strWriteSCV += dataGridView1.Rows[i].Cells[0].Value.ToString() + ",";
                    }
                }
                CsvHepler.WriteCSV(dataFilePaht, strWriteSCV);

                strWriteSCV = "";
                strWriteSCV = dataGridView1.Columns[1].HeaderText + ",";
                CommonValue.ListRealValue.Clear();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value != null)
                    {
                        strWriteSCV += dataGridView1.Rows[i].Cells[1].Value.ToString() + ",";
                        CommonValue.ListRealValue.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value));
                    }
                }
                CsvHepler.WriteCSV(dataFilePaht, strWriteSCV);


                strWriteSCV = "";
                CommonValue.ListUpValue.Clear();
               strWriteSCV = dataGridView1.Columns[2].HeaderText + ",";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[2].Value != null)
                    {
                        strWriteSCV += dataGridView1.Rows[i].Cells[2].Value.ToString() + ",";
                        CommonValue.ListUpValue.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value));
                    }
                }
                CsvHepler.WriteCSV(dataFilePaht, strWriteSCV);


                strWriteSCV = "";
                strWriteSCV = dataGridView1.Columns[3].HeaderText + ",";
                CommonValue.ListDownValue.Clear();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value != null)
                    {
                        strWriteSCV += dataGridView1.Rows[i].Cells[3].Value.ToString() + ",";
                        CommonValue.ListDownValue.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value));
                    }
                }
                CsvHepler.WriteCSV(dataFilePaht, strWriteSCV);

            }
            catch (Exception ex)
            {

              
            }
        }
    }
}
