using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Hix_CCD_Module.UI
{
    public partial class FrmResultBin : DockContent
    {


        public FrmResultBin()
        {
            InitializeComponent();
            DataColumn dc = new DataColumn("L1");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("L2");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("L3");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("W1");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("W2");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("W3");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("W4");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("W5");
            DataShow.Columns.Add(dc);
            dc = new DataColumn("W6");
            DataShow.Columns.Add(dc);
        }

        public int index = 1;
        public DataTable DataShow = new DataTable();
        public double St_height = 156.3;
        public double St_width = 71.93;
        public double A_class = 0.07;
        public double B_class = 0.1;
        public double C_class = 0.1;
        private void button11_Click(object sender, EventArgs e)
        {
            index = 1;
            button1.BackColor = Color.Silver;
            button2.BackColor = Color.Silver;
            button3.BackColor = Color.Silver;
            button4.BackColor = Color.Silver;
            button5.BackColor = Color.Silver;
            button6.BackColor = Color.Silver;
            button7.BackColor = Color.Silver;
            button8.BackColor = Color.Silver;
            button9.BackColor = Color.Silver;
            button10.BackColor = Color.Silver;
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
            button10.Text = "";
            DataShow.Clear();
            resultsGridView1.DataSource = DataShow;
        }

        public void AddProductRes(double L1,double L2, double L3, double W1, double W2, double W3, double W4, double W5, double W6)
        {
            if (index > 10)
            {
                index = 1;
                button1.BackColor = Color.Silver;
                button2.BackColor = Color.Silver;
                button3.BackColor = Color.Silver;
                button4.BackColor = Color.Silver;
                button5.BackColor = Color.Silver;
                button6.BackColor = Color.Silver;
                button7.BackColor = Color.Silver;
                button8.BackColor = Color.Silver;
                button9.BackColor = Color.Silver;
                button10.BackColor = Color.Silver;
                button1.Text = "";
                button2.Text = "";
                button3.Text = "";
                button4.Text = "";
                button5.Text = "";
                button6.Text = "";
                button7.Text = "";
                button8.Text = "";
                button9.Text = "";
                button10.Text = "";
                DataShow.Clear();
            }
            DataRow dr = DataShow.NewRow();
            dr["L1"] = L1;
            dr["L2"] = L2;
            dr["L3"] = L3;
            dr["W1"] = W1;
            dr["W2"] = W2;
            dr["W3"] = W3;
            dr["W4"] = W4;
            dr["W5"] = W5;
            dr["W6"] = W6;
            DataShow.Rows.Add(dr);
            resultsGridView1.DataSource = DataShow;
            //显示当前产品分bin结果
            //判断A类
            string Class_prod = string.Empty;
            if ((Math.Abs(L1 - St_height) < 0.07) && (Math.Abs(L2 - St_height) < 0.07) && (Math.Abs(L3 - St_height) < 0.07) &&
                (Math.Abs(W1 - St_width) < 0.07) && (Math.Abs(W2 - St_width) < 0.07) && (Math.Abs(W3 - St_width) < 0.07) &&
                    (Math.Abs(W4 - St_width) < 0.07) && (Math.Abs(W5 - St_width) < 0.07) && (Math.Abs(W6 - St_width) < 0.07))
                Class_prod = "A";


            //判断NG类
            if (Class_prod == string.Empty)
            {
                if ((Math.Abs(L1 - St_height) >= 0.1) || (Math.Abs(L2 - St_height) >= 0.1) || (Math.Abs(L3 - St_height) >= 0.1) ||
                (Math.Abs(W1 - St_width) >= 0.1) || (Math.Abs(W2 - St_width) >= 0.1) || (Math.Abs(W3 - St_width) >= 0.1) ||
                    (Math.Abs(W4 - St_width) >= 0.1) || (Math.Abs(W5 - St_width) >= 0.1) || (Math.Abs(W6 - St_width) >= 0.1))
                    Class_prod = "NG";
            }
            //判断B类
            if (Class_prod == string.Empty)
            {
                if ((Math.Abs(L1 - St_height) < 0.07) && (Math.Abs(L2 - St_height) < 0.07) && (Math.Abs(L3 - St_height) < 0.07) &&
                (Math.Abs(W1 - St_width) < 0.1) && (Math.Abs(W2 - St_width) < 0.1) && (Math.Abs(W3 - St_width) < 0.1) &&
                    (Math.Abs(W4 - St_width) < 0.1) && (Math.Abs(W5 - St_width) < 0.1) && (Math.Abs(W6 - St_width) < 0.1))
                {
                    if ((Math.Abs(W1 - St_width) >= 0.07) || (Math.Abs(W2 - St_width) >= 0.07) || (Math.Abs(W3 - St_width) >= 0.07) ||
                    (Math.Abs(W4 - St_width) >= 0.07) || (Math.Abs(W5 - St_width) >= 0.07) || (Math.Abs(W6 - St_width) >= 0.07))
                        Class_prod = "B";
                }
            }

            //判断C类
            if (Class_prod == string.Empty)
            {
                    Class_prod = "C";
            }
            switch (index)
            {
                case 1:
                    button9.Text = Class_prod;
                    button9.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button9.BackColor = Color.Red;
                    break;
                case 2:
                    button10.Text = Class_prod;
                    button10.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button10.BackColor = Color.Red;
                    break;
                case 3:
                    button5.Text = Class_prod;
                    button5.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button5.BackColor = Color.Red;
                    break;
                case 4:
                    button6.Text = Class_prod;
                    button6.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button6.BackColor = Color.Red;
                    break;
                case 5:
                    button8.Text = Class_prod;
                    button8.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button8.BackColor = Color.Red;
                    break;
                case 6:
                    button7.Text = Class_prod;
                    button7.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button7.BackColor = Color.Red;
                    break;
                case 7:
                    button1.Text = Class_prod;
                    button1.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button1.BackColor = Color.Red;
                    break;
                case 8:
                    button2.Text = Class_prod;
                    button2.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button2.BackColor = Color.Red;
                    break;
                case 9:
                    button3.Text = Class_prod;
                    button3.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button3.BackColor = Color.Red;
                    break;
                case 10:
                    button4.Text = Class_prod;
                    button4.BackColor = Color.Lime;
                    if (Class_prod == "NG")
                        button4.BackColor = Color.Red;
                    break;
                default:
                    break;
            }
            index++;
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }
    }
}
