using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mark.CommonFile;
using System.IO;

namespace Hix_CCD_Module.UI
{
    public partial class FrmCheckResult : Form
    {

        private  List<List<double>> ListCheckLoad = new List<List<double>>();

        //储存 测量点检数据
        private  List<List<double>> ListCheckTest = new List<List<double>>();
        string[] tableheads = null;

        private int[] iArrRows = new int[20];

        public FrmCheckResult(string[] tableheads, List<List<double>> ListCheckLoad, List<List<double>> ListCheckTest)
        {
            InitializeComponent();
            this.ListCheckLoad = ListCheckLoad;
            this.ListCheckTest = ListCheckTest;
            this.tableheads = tableheads;
        }

        private void FrmCheckResult_Load(object sender, EventArgs e)
        {
            try
            {

                string strWriteSCV = "";
                bool check = true;

                for (int i = 0; i < 20; i++)
                {
                    iArrRows[i] = i * 3;
                }
                
                string[] _ArrTitle = new string[3] { "真值","测值","差值"};

                dgv.Columns.Add("datavalue", "数值");
                strWriteSCV = "数值类型"+ ",";
                for (int i = 0; i < tableheads.Length; i++)
                {
                    strWriteSCV += tableheads[i] + ","; 
                    dgv.Columns.Add(tableheads[i], tableheads[i]);
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                for (int i = 0; i < ListCheckLoad.Count; i++)
                {
                    dgv.Rows.Add(3);
                    for (int j = 0; j < ListCheckLoad[i].Count; j ++)
                    {
                        dgv.Rows[iArrRows[i]].Cells[j+1].Value = ListCheckLoad[i][j];
                    }
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {

                    switch (i%3)
                    {
                        case 0:
                            dgv.Rows[i].Cells[0].Value = "真值";
                            break;

                        case 1:
                            dgv.Rows[i].Cells[0].Value = "测值";
                            break;

                        case 2:
                            dgv.Rows[i].Cells[0].Value = "差值";
                            break;

                        default:
                            break;
                    }

            
                }
                


                for (int i = 0; i < ListCheckLoad.Count; i++)
                {
                    for (int j = 0; j < ListCheckLoad[i].Count; j++)
                    {
                        dgv.Rows[iArrRows[i]+1].Cells[j+1].Value = ListCheckTest[i][j];

                        double dd = Math.Abs(ListCheckTest[i][j] - ListCheckLoad[i][j]);
                        dgv.Rows[iArrRows[i] + 2].Cells[j+1].Value = dd.ToString("F3");

                            if (dd >= CommonValue.dWHValue)
                            {
                                check = false;
                                dgv.Rows[iArrRows[i] + 2].Cells[j+1].Style.BackColor = Color.Red;
                            }
                            else
                            {
                                dgv.Rows[iArrRows[i] + 2].Cells[j+1].Style.BackColor = Color.Green;
                            }
                        

                    }
                }

                CommonValue.isCheckToday = check;

                string path = @"D:\Mark\DataSave\CheckData";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string dataFilePaht = $@"{path}\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.csv";
                CsvHepler.WriteCSV(dataFilePaht, strWriteSCV);
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    strWriteSCV = "";
                    for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    {
                        strWriteSCV += dgv.Rows[i].Cells[j].Value.ToString() + ",";
                    }
                    CsvHepler.WriteCSV(dataFilePaht, strWriteSCV);
                }
 
            }
            catch (Exception ex)
            {

                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommonValue.isCheckToday = true;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonValue.isCheckToday = false;
            this.DialogResult = DialogResult.OK;
        }
    }
}
