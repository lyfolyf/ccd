using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mark.DigitalIO;

namespace Hix_CCD_Module.UI
{
    public partial class IOForm : Form
    {
        FrmMain from = null;
        public IOForm(FrmMain main)
        {
            InitializeComponent();
            from = main;
            timer1.Start();
            //IniFileHelper inifile = new IniFileHelper("F:\\Hix50125.ini");
            //string id1 = inifile.IniReadValue("ProductID", "Camera1ProductID");
            //string id2 = inifile.IniReadValue("ProductID", "Camera2ProductID");
            //this.textBox1.Text = id1;
            //this.textBox2.Text = id2;
            Color lColor = Color.Green;
            this.label8.ForeColor = lColor;
            this.label9.ForeColor = lColor;
            this.label10.ForeColor = lColor;
            this.label11.ForeColor = lColor;
            this.label12.ForeColor = lColor;
            this.label13.ForeColor = lColor;
            this.label14.ForeColor = lColor;
            this.label20.ForeColor = lColor;
            this.label21.ForeColor = lColor;
            this.label22.ForeColor = lColor;
            this.label23.ForeColor = lColor;
            this.label24.ForeColor = lColor;
            this.label25.ForeColor = lColor;
            this.label26.ForeColor = lColor;
            this.label27.ForeColor = lColor;
            this.label28.ForeColor = lColor;


            this.label8.Text = string.Format("Input:{0}#", IOSignalAdress.A_Station_Start);
            this.label9.Text = string.Format("Input:{0}#", IOSignalAdress.A_Station_End);
            this.label10.Text = string.Format("Input:{0}#", IOSignalAdress.B_Station_Start);
            this.label11.Text = string.Format("Input:{0}#", IOSignalAdress.B_Station_End);
            this.label12.Text = string.Format("Output:{0}#", IOSignalAdress.InitOK);
            this.label13.Text = string.Format("Output:{0}#", IOSignalAdress.Camera1Ready);
            this.label14.Text = string.Format("Output:{0}#", IOSignalAdress.Camera2Ready);
            this.label20.Text = string.Format("Output:{0}#", IOSignalAdress.ResultType1);
            this.label21.Text = string.Format("Output:{0}#", IOSignalAdress.ResultType2);
            this.label22.Text = string.Format("Output:{0}#", IOSignalAdress.ResultType3);
            this.label23.Text = string.Format("Output:{0}#", IOSignalAdress.ResultType4);
            this.label24.Text = string.Format("Output:{0}#", IOSignalAdress.ResultType5);

            //反馈信号地址
            this.label28.Text = string.Format("Output:{0}#", IOSignalAdress.A_Station_Start_Feedback);
            this.label27.Text = string.Format("Output:{0}#", IOSignalAdress.A_Station_End_Feedback);
            this.label26.Text = string.Format("Output:{0}#", IOSignalAdress.B_Station_Start_Feedback);
            this.label25.Text = string.Format("Output:{0}#", IOSignalAdress.B_Station_End_Feedback);

        }
        Color HightColor = Color.Red;
        Color LowColor = Color.Green;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox1.BackColor = CommonValue.A_Station_Start ? HightColor : LowColor;
            this.textBox2.BackColor = CommonValue.A_Station_End ? HightColor : LowColor;
            this.textBox3.BackColor = CommonValue.B_Station_Start ? HightColor : LowColor;
            this.textBox4.BackColor = CommonValue.B_Station_End ? HightColor : LowColor;


            this.button1.BackColor = CommonValue.InitOK ? HightColor : LowColor;
            this.button2.BackColor = CommonValue.Camera1Ready ? HightColor : LowColor;
            //this.button3.BackColor = CommonValue.Camera2Ready ? HightColor : LowColor;
            this.button4.BackColor = CommonValue.ResultType1 ? HightColor : LowColor;
            this.button5.BackColor = CommonValue.ResultType2 ? HightColor : LowColor;
            this.button6.BackColor = CommonValue.ResultType3 ? HightColor : LowColor;
            this.button7.BackColor = CommonValue.ResultType4 ? HightColor : LowColor;
            this.button8.BackColor = CommonValue.ResultType5 ? HightColor : LowColor;

            //反馈信号颜色
            this.button12.BackColor = CommonValue.A_Station_Start_Feedback ? HightColor : LowColor;
            this.button11.BackColor = CommonValue.A_Station_End_Feedback ? HightColor : LowColor;
            this.button10.BackColor = CommonValue.B_Station_Start_Feedback ? HightColor : LowColor;
            this.button9.BackColor = CommonValue.B_Station_End_Feedback ? HightColor : LowColor;

        }















       









      
    }
}
