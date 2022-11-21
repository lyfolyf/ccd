using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bing.VisionProTool;
using Bing.IVisionTool;
using Bing.Serialization;
using Bing.Tcp;
//using Bing.CogImageStitching;
using Hix_CCD_Module.UI;
using Hix_CCD_Module.Setting;
using Hix_CCD_Module.HixEventArgs;
using System.Threading;
using Hix_CCD_Module.Tool;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using Mark.DigitalIO;
using Mark.CommonFile;
using Cognex.VisionPro;
using Lead.ProdutionInfo;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
using Lead.CPrim.PrimVariableClient;
using Lead.Tool.MongoDB;
using LineProfile2D;
using System.Timers;

namespace Hix_CCD_Module
{
    public partial class FrmMain : Form
    {
       // VariableClient skt;
        #region 系统变量
        FrmLog frmLog = new FrmLog();
        FrmParameters frmParameters = new FrmParameters(default(Parameters));
        FrmLoading frmLoading = new FrmLoading();
        FrmSkin frmSkin = new FrmSkin();
        FrmImageViewing frmImageViewing = new FrmImageViewing();
        FrmResultBin frmResultBin = new FrmResultBin();

        FrmResult frmresult = new FrmResult();

        private List<string> nfOffet = new List<string>();

        FrmResultViewing resultViewing = new FrmResultViewing();
        MongoHelper mongoHelper = new MongoHelper();
        private LineProfile profile = new LineProfile();
        private string[] tableheads = null;
        //拼接的图片
        object stichImage = null;
        object obj = new object();
        //相机取像图片计数
        int indexOfImages = 0;
        bool flag = true;
        bool flag2 = true;
        int productIndex = 0;
        int _iOKCount = 0;
        int _iNGCount = 0;
        public static Dictionary<int, string> FlawTypeDictionary = new Dictionary<int, string>();
        //用于正在任务计算所需要的图片
        public static List<ImageUnit> CurrentImages { get; set; } = null;
        public static List<ImageUnit> CurrentImages2 { get; set; } = null;

        //用于缓存相机图片
        public static List<ImageUnit> TempImages_Camera1 { get; set; } = null;
        public bool IsCache = false;
        //用于在 第一次结果出来前   缓存后续图片
        public static List<ImageUnit> TempImages_Camera_Cache { get; set; } = null;

        private bool IsCamConnect = false;
        //储存 载入点检数据
        public static List<List<double>> ListCheckLoad = new List<List<double>>();

        //储存 测量点检数据
        public static List<List<double>> ListCheckTest = new List<List<double>>();

        public string Version = "2020.06.22.15";
        private string m_IDCode1 = "";
        private string m_IDCode2 = "";
        //用于每组图片置位标志
        private bool newgroup = true;
        //标定后的图片
        public static List<CogImage8Grey> CalibImages { get; set; } = null;
        public static List<CogImage8Grey> CalibImages2 { get; set; } = null;

        //系统参数
        public static Parameters SysParams { get; set; } = new Parameters();
        //任务
        public static Dictionary<string, TaskRunner> DicTasks { get; set; } = new Dictionary<string, TaskRunner>();
        //相机

        //public static Dictionary<string, SVSCam> DicSVSCameras { get; set; } = new Dictionary<string, SVSCam>();

        public static Dictionary<string, HikCamera> DicHikCameras { get; set; } = new Dictionary<string, HikCamera>();


        //标定
        public static Dictionary<string, HixCalibTool> DicCalibTools { get; set; } = new Dictionary<string, HixCalibTool>();
        //雷赛IO板卡
        //public static Leisai_IOC640 LeisaiIO = new Leisai_IOC640(0);

        string dataFilePaht = "";
        //2D与运动控制交互   // offet.ini
        string Path2D = @"D:\2D与运动控制交互\EX.ini";
        string PathConfig = Application.StartupPath + "\\Config.ini";

        string PathOffet = Application.StartupPath + "\\offet.ini";

        private bool IsExchange = true;

        System.Timers.Timer timer = new System.Timers.Timer();
        //ini信号对接
        public class INIhelp1
        {
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);


            public static string GetValue(string Path, string Section, string key)
            {
                StringBuilder s = new StringBuilder(1024);
                GetPrivateProfileString(Section, key, "", s, 1024, Path);
                return s.ToString();
            }


            public static void SetValue(string Path, string Section, string key, string value)
            {
                try
                {
                    WritePrivateProfileString(Section, key, value, Path);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        public FrmMain()
        {
            InitializeComponent();
            frmSkin.SkinChanged += FrmSkin_SkinChanged;
            //QueryMongo();
        }


        private void InitTimer()
        {
            try
            {
                timer.AutoReset = true;
                timer.Interval = 10;
                timer.Enabled = true;
                timer.Elapsed += Timer_Elapsed;
            }
            catch (Exception ex)
            {

            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (IsExchange)
                {

                    string btn = INIhelp1.GetValue(Path2D, "", "开始触发");
                    if (btn == "1")
                    {
                        Logging("开始触发...", Color.DarkSlateBlue);
                        IsExchange = false;
                        INIhelp1.SetValue(Path2D, "", "开始触发反馈", "1");
                        Logging("开始触发反馈...", Color.DarkSlateBlue);
                        if (!IsCache)
                        {
                            TempImages_Camera_Cache.Clear();
                            newgroup = true;
                            indexOfImages = 0;
                        }
                        else
                        {
                            TempImages_Camera1.Clear();
                            newgroup = true;
                            indexOfImages = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


        #region 方法
        private void CheckActiveContent()
        {
            List<string> listDisplayWindows = new List<string>();
            foreach (var item in SysParams.ListDisplayView)
            {
                if (FindDockContent($"{item} - Display") == null)
                {
                    listDisplayWindows.Add(item);
                }
            }
            foreach (string item in listDisplayWindows)
            {
                SysParams.ListDisplayView.Remove(item);
            }

            listDisplayWindows = new List<string>();
            foreach (var item in SysParams.ListResultView)
            {
                if (FindDockContent($"{item} - 计算结果") == null)
                {
                    listDisplayWindows.Add(item);
                }
            }
            foreach (string item in listDisplayWindows)
            {
                SysParams.ListResultView.Remove(item);
            }

            listDisplayWindows = new List<string>();
            foreach (var item in SysParams.ListTaskEditView)
            {
                if (FindDockContent($"{item} - Task") == null)
                {
                    listDisplayWindows.Add(item);
                }
            }
            foreach (string item in listDisplayWindows)
            {
                SysParams.ListTaskEditView.Remove(item);
            }
        }
        private IDockContent FindDockContent(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Contents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }
        private void CloseAllDockContent()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                IDockContent[] dockContents = dockPanel.Contents.ToArray();
                foreach (IDockContent content in dockContents)
                {
                    content.DockHandler.HideOnClose = false;
                    content.DockHandler.Dispose();
                }
            }
        }
        private void SetProgressValue(int value)
        {
            frmLoading.Value = value;
        }
        private void Logging(string log, Color color)
        {
            try
            {
                Invoke(new Action(() => frmLog.UpdateLog(log, color)));
            }
            catch (Exception ex)
            {

   
            }

        }
        private void InitializeSystem()
        {

            frmLog.Show(dockPanel, DockState.DockBottom);
            SetProgressValue(0); Logging("系统初始化...", Color.DarkSlateBlue);

            SysParams = Parameters.LoadParametersFromFile();
            Logging("加载参数完成...", Color.DarkSlateBlue);
        
            Logging("初始化相机列表...", Color.DarkSlateBlue);

            TempImages_Camera1 = new List<ImageUnit>();
            TempImages_Camera_Cache = new List<ImageUnit>();

            CurrentImages = new List<ImageUnit>(SysParams.PlanedImageNamber);
            CurrentImages2 = new List<ImageUnit>(SysParams.PlanedImageNamber);

            DicTasks.Clear();
            DicCalibTools.Clear();

            //string mode  = INIhelp1.GetValue(PathConfig, "System", "Debug");
            CommonValue.CamSerialNum  = INIhelp1.GetValue(PathConfig, "System", "CamSerialNum");

            CommonValue.ExposeTime = Convert.ToDouble(INIhelp1.GetValue(PathConfig, "System", "ExposeTime"));
             CommonValue.Gain = Convert.ToDouble(INIhelp1.GetValue(PathConfig, "System", "Gain"));

            CommonValue.SPCD = Convert.ToUInt32(INIhelp1.GetValue(PathConfig, "System", "SPCD"));
            CommonValue.LineDebouncerTime = Convert.ToUInt32(INIhelp1.GetValue(PathConfig, "System", "LineDebouncerTime"));


            //if (mode=="true")
            //{
            //    CommonValue.IsDebug = true;
            //}
            TaskLoading(UpdateUI);

        }


        /// <summary>
        /// 相机图像获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


       

        private void CameraFly_Action(IDcode idc)
        {
            try
            {
                             
                            List<FAIjudge> ListResultSend1 = new List<FAIjudge>();
                            List<FAIjudge> ListResultSend2 = new List<FAIjudge>();
                                //*************************************************
                            CommonValue.Camera1Ready = true;
        
                            CommonValue.CameraFly1 = true;
                            CurrentImages.Clear();
                            CurrentImages2.Clear();
                            if (!IsCache)
                             {

                                IsCache = true;
                                         //2#载具图像          截取  1 ~5       16~20
                                 CurrentImages2.AddRange(TempImages_Camera1.GetRange(0, (int)(0.5 * SysParams.PlanedImageNamber)));
                                 CurrentImages2.AddRange(TempImages_Camera1.GetRange((int)(1.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));
                                        //1#载具图像         截取  6 ~10  11 ~ 15
                                        CurrentImages.AddRange(TempImages_Camera1.GetRange((int)(0.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));
                                        CurrentImages.AddRange(TempImages_Camera1.GetRange(SysParams.PlanedImageNamber, (int)(0.5 * SysParams.PlanedImageNamber)));
                                        TempImages_Camera1.Clear();
                    
                                 }
                                else
                                {
                                            IsCache = false;
         
                                              // 防止覆盖   从缓存集合取像
                                            CurrentImages2.AddRange(TempImages_Camera_Cache.GetRange(0, (int)(0.5 * SysParams.PlanedImageNamber)));
                                            CurrentImages2.AddRange(TempImages_Camera_Cache.GetRange((int)(1.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));

                            
                                            CurrentImages.AddRange(TempImages_Camera_Cache.GetRange((int)(0.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));
                                            CurrentImages.AddRange(TempImages_Camera_Cache.GetRange(SysParams.PlanedImageNamber, (int)(0.5 * SysParams.PlanedImageNamber)));
                                            TempImages_Camera_Cache.Clear();
                                    }

           
                            indexOfImages = 0;
                    
                      
        

                            #region 图像标定  
                            CalibImages = new List<CogImage8Grey>(SysParams.PlanedImageNamber);
                            CalibImages2 = new List<CogImage8Grey>(SysParams.PlanedImageNamber);
                            try
                            {
                                    for (int i = 0; i < CurrentImages.Count; i++)
                                    {
                                            if (DicCalibTools.Values.Where(item => item.Id / 100 == 1).Where(item => item.Id % 100 == i).ToList().Count > 0)
                                            {
                                                HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 1).Where(item => item.Id % 100 ==  i).ToList()[0];
                                                CogImage8Grey outputImage = null;
                                                Actuator.RunCalib(hixCalibTool, CurrentImages[i].Image, out outputImage);
                                                CalibImages.Add(outputImage);
                                            }
                                            Logging($"1#载具标定图像[{i}]", Color.Magenta);
                                            if (DicCalibTools.Values.Where(item => item.Id / 100 == 2).Where(item => item.Id % 200 == i).ToList().Count > 0)
                                            {
                                                HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 2).Where(item => item.Id % 200 ==             i).ToList()[0];
                                                CogImage8Grey outputImage = null;
                                                Actuator.RunCalib(hixCalibTool, CurrentImages2[i].Image, out outputImage);
                                                CalibImages2.Add(outputImage);
                                            }
                                            Logging($"2#载具标定图像[{i}]", Color.Magenta);
                                    }
                            }
                            catch (Exception ex)
                            {
                              
                            }
                         //标定处理完成后,开始下一次拍摄标志

                            #endregion  end图像标定

                            string strDay = DateTime.Now.ToString("yyyyMMdd");
                            string strTime = DateTime.Now.ToString("HH_mm_ss");
                            string fileDayPath = $@"{SysParams.StitchImagePath}\{strDay}";
                            string fileTimePath = $@"{fileDayPath}\{strTime}";
                            Actuator.CheckDirectory(fileDayPath);
                            string timeStamp = $"{strDay}#{strTime}";

                            #region 图像拼接
                            CogImage8Grey sImage = null;
                            CogImage8Grey sImage2 = null;
                            Logging($"拼接开始", Color.Magenta);
                            bool res1 = false;
                            bool res2 = false;
                            bool falgStitch1 = false;
                            bool falgStitch2 = false;

                            Task.Run(() =>
                            {
                                res1 = Actuator.ImageStitching(CalibImages, SysParams.UnfilledPelValue, out sImage, SysParams.Mode, SysParams.rate);
                                falgStitch1 = true;
                            });

                            Task.Run(() =>
                            {
                                res2 = Actuator.ImageStitching(CalibImages2, SysParams.UnfilledPelValue, out sImage2, SysParams.Mode, SysParams.rate);
                                falgStitch2 = true;
                            });

                           while (true)
                            {
                                if (falgStitch1 & falgStitch2)
                                {
                                    if (res1 && res2)
                                    {
                                        Logging($"拼接完成", Color.Magenta);
                                    }
                                    else
                                    {
                                            if (!res1) Logging($"1#载具图像拼接失败", Color.Magenta);
                                            if (!res2) Logging($"2#载具图像拼接失败", Color.Magenta);
                                    }
                                    break;
                                }
                            }
                        GC.Collect();
                #endregion

                            //拼接图像保存
                            SaveStitchImage(sImage, sImage2, fileTimePath);

                            string frontMSG1 = "";
                            string frontMSG2 = "";
                            string dataMSG1 = "";
                            string dataMSG2 = "";

                            #region 载具一图像处理

                            DicTasks[CommonValue.TaskName].Image = sImage;
                            DicTasks[CommonValue.TaskName].TaskIndex = 1;
                            
                            Actuator.RunTask(DicTasks[CommonValue.TaskName], new string[] { "InputImage" }, new object[] { sImage });
                             this.Invoke(new Action(() => resultViewing.ShowImage(1,sImage)));

                            // this.Invoke(new Action(() => resultViewing.ShowGraphic(1, DicTasks[CommonValue.TaskName].Record)));

                            // 数据本地记录
                            //********************结果获取与处理************************************
                            productIndex = productIndex == Int32.MaxValue ? 0 : productIndex + 1;

                            List<double> list1 = new List<double>();

                            int indexResult = 0;  //表示第几个结果
                            frontMSG1 = $"{timeStamp},1#载具,";
                            string Measure_Result1 = "OK";
                            string data1 = idc.IDCode1+",";
                            if (DicTasks[CommonValue.TaskName].Outputs.Count > 0)
                            {
                                foreach (Terminal t in DicTasks[CommonValue.TaskName].Outputs)
                                {
                                    if (t.ValueType == typeof(List<>))
                                    {
                                        List<double> res = t.Value as List<double>;
                                        if (res != null)
                                        {
                                            for (int i = 0; i < res.Count; i++)
                                            {
                                                data1 += res[i].ToString("f3") + ",";
                                            }
                                        }
                                        continue;
                                    }

                                  if (t.ValueType == typeof(double) || t.ValueType == typeof(int) || t.ValueType == typeof(string) || t.ValueType == typeof(bool))
                                    {
    
                     
                                        if (CommonValue.ListRealValue.Count > indexResult)
                                        {
                                            FAIjudge mFAIjudge = new FAIjudge();
                                            mFAIjudge.名称 = t.Name;
                                            mFAIjudge.是否判定 = true;
                                            mFAIjudge.测量结果 = Math.Round((double)t.Value, 3);
                           
                                            mFAIjudge.上公差 = CommonValue.ListUpValue[indexResult];
                                            mFAIjudge.下公差 = CommonValue.ListDownValue[indexResult];
                                            mFAIjudge.标准值 = CommonValue.ListRealValue[indexResult];
                                            mFAIjudge.补偿值 = 0;
                                            mFAIjudge.补偿系数 = 0;
                                            double max = CommonValue.ListRealValue[indexResult] + CommonValue.ListUpValue[indexResult];
                                            double min = CommonValue.ListRealValue[indexResult] + CommonValue.ListDownValue[indexResult];
                                            if ((double)t.Value <= max && (double)t.Value >= min)
                                            {
                                                mFAIjudge.判定结果 = "OK";
                                            }
                                            else
                                            {
                                                Measure_Result1 = "NG";
                                                mFAIjudge.判定结果 = "NG";
                                            }
                                                if (CommonValue.isCheck)
                                                {
                                                    list1.Add(Math.Round((double)t.Value, 3));
                                                }
                                              ListResultSend1.Add(mFAIjudge);
                                        }
                                        indexResult++;
                                        string valueRes = ((double)t.Value).ToString("f3");
                                        data1 += valueRes + ",";
                                    }

                                }

                                if (!CommonValue.isCheck && CommonValue.isCheckToday)
                                {
                                    if (Measure_Result1 == "NG")
                                    {
                                      _iNGCount++;
                                      this.Invoke(new Action(() => resultViewing.ShowText(1, "NG", CogColorConstants.Red)));
                                   }
                                    else
                                    {
                                        this.Invoke(new Action(() => resultViewing.ShowText(1, "OK", CogColorConstants.DarkGreen)));
                                        _iOKCount++;
                                    }

      
                                  this.Invoke(new MethodInvoker(delegate {

                                        frmresult.AddResultDgv2(ListResultSend1,1,Measure_Result1);
                                    }));
                                    mongoHelper.InsertOneAsync<PartResult>(new PartResult() { ID = idc.IDCode1 , CreateTime = DateTime.Now, Part = PartEnum.S2_L, Measure_Result = Measure_Result1, Is点检 = false, FaiInfos = ListResultSend1 });
                                }

                                if (CommonValue.isCheck)
                                {
                                          ListCheckTest.Add(list1);
                                         mongoHelper.InsertOneAsync<PartResult>(new PartResult() { ID = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"+"点检"), CreateTime = DateTime.Now, Part = PartEnum.S2_L, Measure_Result = "点检", Is点检 = true, FaiInfos = ListResultSend1 });
            
                                }
                        
                            }

                            dataMSG1 = frontMSG1 + Measure_Result1+","+data1;
                            //**********************************************************************

                            #endregion   载具一图像处理


                            #region 载具二图像处理

                            DicTasks[CommonValue.TaskName].Image = sImage2;
                   
                            DicTasks[CommonValue.TaskName].TaskIndex = 2;
                            
                            List<double> list2 = new List<double>();
                            Actuator.RunTask(DicTasks[CommonValue.TaskName], new string[] { "InputImage" }, new object[] { sImage2 });
                            this.Invoke(new Action(() => resultViewing.ShowImage(2, sImage2)));
                            //  this.Invoke(new Action(() => resultViewing.ShowGraphic(2, DicTasks[CommonValue.TaskName].Record)));

                            //********************结果获取与处理************************************


                             productIndex = productIndex == Int32.MaxValue ? 0 : productIndex + 1;
     
                            frontMSG2 = $"{timeStamp},2#载具,";
                            string data2 = idc.IDCode2+",";
                            string Measure_Result2 = "OK";
                            if (DicTasks[CommonValue.TaskName].Outputs.Count > 0)
                            {
                                indexResult = 0;
                                foreach (Terminal t in DicTasks[CommonValue.TaskName].Outputs)
                                {
                                    if (t.ValueType == typeof(List<>))
                                    {
                                        List<double> res = t.Value as List<double>;
                                        if (res != null)
                                        {
                                            for (int i = 0; i < res.Count; i++)
                                            {
                                                data2 += res[i].ToString("f3") + ",";
                                            }
                                        }
                                        continue;
                                    }

                                    if (t.ValueType == typeof(double) ||t.ValueType == typeof(int) || t.ValueType == typeof(string) ||t.ValueType == typeof(bool))
                                    {
                                      if (CommonValue.ListRealValue.Count > indexResult)
                                        {
                                            FAIjudge mFAIjudge = new FAIjudge();
                                            mFAIjudge.名称 = t.Name;
                                            mFAIjudge.是否判定 = true;
                                            mFAIjudge.测量结果 = Math.Round((double)t.Value2,3);
                                            mFAIjudge.上公差 = CommonValue.ListUpValue[indexResult];
                                            mFAIjudge.下公差 = CommonValue.ListDownValue[indexResult];
                                            mFAIjudge.标准值 = CommonValue.ListRealValue[indexResult];
                                            mFAIjudge.补偿值 = 0;
                                            mFAIjudge.补偿系数 = 0;
                                            double max = CommonValue.ListRealValue[indexResult] + CommonValue.ListUpValue[indexResult];
                                            double min = CommonValue.ListRealValue[indexResult] + CommonValue.ListDownValue[indexResult];
                                            if ((double)t.Value2 <= max && (double)t.Value2 >= min)
                                            {
                                                mFAIjudge.判定结果 = "OK";
                                            }
                                            else
                                            {
                                                Measure_Result2 = "NG";
                                                mFAIjudge.判定结果 = "NG";
                                            }
                                            if (CommonValue.isCheck)
                                            {
                                                list2.Add(Math.Round((double)t.Value2, 3));
                                            }
                                            ListResultSend2.Add(mFAIjudge);
                                        }
                                        indexResult++;
                             
                                        string valueRes =((double)t.Value2).ToString("f3") ;
                                        data2 += valueRes + ",";
                                      }
                                  }

                                 if (!CommonValue.isCheck && CommonValue.isCheckToday)
                                 {
                                    if (Measure_Result2 == "NG")
                                    {
                                        _iNGCount++;
                                        this.Invoke(new Action(() => resultViewing.ShowText(2, "NG", CogColorConstants.Red)));
                                    }
                                    else
                                    {
                                        _iOKCount++;
                                        this.Invoke(new Action(() => resultViewing.ShowText(2, "OK", CogColorConstants.DarkGreen)));
                                    }
                                     this.Invoke(new MethodInvoker(delegate {
                                         frmresult.AddResultDgv2(ListResultSend2, 2, Measure_Result2);
                                     }));
                                     mongoHelper.InsertOneAsync<PartResult>(new PartResult() { ID = idc.IDCode2, CreateTime = DateTime.Now, Part = PartEnum.S2_R, Is点检 = false, Measure_Result = Measure_Result2, FaiInfos = ListResultSend2 });
                                 }

                                 if (CommonValue.isCheck)
                                {
                                    mongoHelper.InsertOneAsync<PartResult>(new PartResult() { ID = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "点检"), CreateTime = DateTime.Now, Part = PartEnum.S2_R, Is点检 = true, Measure_Result = "点检", FaiInfos = ListResultSend2 });

                                    ListCheckTest.Add(list2);
                                 }
                            }


                          dataMSG2 = frontMSG2 + Measure_Result2 + "," + data2;
            
                            //**********************************************************************

                            #endregion 载具二图像处理
                            string path = $@"{SysParams.DataSavePath}";
                            dataFilePaht = $@"{path}\{DateTime.Now.ToString("yyyyMMdd")}.csv";
                            if (!File.Exists(dataFilePaht))
                            {
                                File.Create(dataFilePaht).Close();
                            }
                            if (SysParams.IsDataRecordSave)
                            {
                                  if (SysParams.IsSaveData1)
                                  {
                                        if (!CommonValue.isCheck && CommonValue.isCheckToday)
                                        {
                                             CsvHepler.WriteCSV(dataFilePaht, dataMSG1);
                                        }
                                        
                                  }
                                  if (SysParams.IsSaveData2)
                                  {
                                    if (!CommonValue.isCheck && CommonValue.isCheckToday)
                                    {
                                        CsvHepler.WriteCSV(dataFilePaht, dataMSG2);
                                    }
                                  }
                             }

                            if (CommonValue.isCheck)
                            {
                                if (ListCheckTest.Count >= CommonValue.iCheckCount)
                                {
                                    CommonValue.isCheck = false;

                                    Logging($"#####点检数据采集完成，行数:{ListCheckTest.Count} 列数:{ListCheckTest[0].Count} ######", Color.Green);
                                    CheckData();
                                }
                            }
                            SaveOrginImage(fileDayPath, strDay, strTime);
                            CalibImages.Clear();
                            CalibImages2.Clear();
                            CurrentImages.Clear();
                            CurrentImages2.Clear();
                            Logging($"#####任务完成，当天检测总数:{productIndex},OK:{_iOKCount},NG:{_iNGCount},{((_iOKCount*1.0)/ productIndex).ToString("P2")}######", Color.Green);
                            GC.Collect();
                
            }
            catch(Exception ex)
            {
              
            }
        }

        /// <summary>
        /// 点检数据处理
        /// </summary>
        private void CheckData()
        {
            try
            {
                if (ListCheckTest[0].Count < ListCheckLoad[0].Count)
                {
                    Logging($"#####点检载入数据列数({ListCheckLoad[0].Count})与点检采集数据列数({ListCheckTest[0].Count})不匹配，无法比对######", Color.Red);
                    return;
                }
                if (ListCheckTest.Count >= ListCheckLoad.Count)
                {
                    this.Invoke( new MethodInvoker(delegate{
                        FrmCheckResult FrmCheck = new FrmCheckResult(tableheads, ListCheckLoad, ListCheckTest);
                        FrmCheck.ShowDialog();
                        if (CommonValue.isCheckToday)
                        {
                            CommonValue.isCheck = false;
                            ListCheckTest.Clear();
                            INIhelp1.SetValue(PathConfig, "System", "点检", "true");
                            INIhelp1.SetValue(PathConfig, "System", "点检时间", DateTime.Now.ToString("yyyy-MM-dd"));
                            Logging($"#####点检成功!!!######", Color.Green);
                        }
                        else
                        {
                            CommonValue.isCheck = true;
                            ListCheckTest.Clear();
                            Logging($"#####点检失败!!!######", Color.Red);
                            Logging($"#####请调整后再点检,直至点检通过!!!######", Color.Red);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void SaveOrginImage(string fileDayPath,string strDay, string strTime)
        {
            try
            {
                if (SysParams.IsSaveOrginImage)
                {
                    fileDayPath = $@"{SysParams.OrginImagePath}{strDay}";
                    string  fileTimePath = $@"{fileDayPath}\{strTime}";
                    Actuator.CheckDirectory(fileTimePath);

                    Parallel.For(0, CurrentImages.Count,(i,state)=> {

                            if (CurrentImages[i].Image != null)
                            {
                                string filePath = $@"{fileTimePath}\1_{i}.bmp";
                                CurrentImages[i].Image.Save(filePath);
                            }

                            if (CurrentImages2[i].Image != null)
                            {
                                string filePath = $@"{fileTimePath}\2_{i}.bmp";
                                CurrentImages2[i].Image.Save(filePath);
                            }

                    });
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void SaveStitchImage(CogImage8Grey image1, CogImage8Grey image2,string fileTimePath)
        {
            try
            {
                if (SysParams.IsSaveStitchImage)
                {
                    if (image1 != null)
                    {
                           ThreadPool.QueueUserWorkItem(new WaitCallback(delegate {
                            string filePath1 = $@"{fileTimePath}_1.bmp";
                            image1.ToBitmap().Save(filePath1);
                        }));
                    }
                    if (image2 != null)
                    {
                           ThreadPool.QueueUserWorkItem(new WaitCallback(delegate {
                            string filePath2 = $@"{fileTimePath}_2.bmp";
                            image2.ToBitmap().Save(filePath2);
                        }));
                    }
                }
    
            }
            catch (Exception ex)
            {

            }
        }

        private string GetFlawInfo(string info,string flawMsg)
        {
            if (info == "" || info == null)
            {
                info = flawMsg;
                return info;
            }
            string[] strs = info.Split(';');
            int count = 0;
            for (int i=0;i< strs.Length;i++)
            {
                if (strs[i] != flawMsg)
                {
                    count++;
                }
            }
            return count == strs.Length ? $"{info};{flawMsg}" : info;
        }



        private void TaskLoading(Action action)
        {
            Task.Run(() => Invoke(new Action(() =>
            {

                Actuator.CheckDirectory(SysParams.LogPath);
                Actuator.CheckDirectory(SysParams.OrginImagePath);
                Actuator.CheckDirectory(SysParams.ResultImagePath);
                Actuator.CheckDirectory(SysParams.StitchImagePath);

                frmLoading = new FrmLoading();
                frmLoading.Show();
                frmLog.Show(dockPanel, DockState.DockBottom);
                CommonValue.InitOK = false;

                #region 加载相机
                int i = 0;
                int count = 0;
                if (count == 0)
                    count = 1;
                int step = 20 / count;

                #endregion

                #region  相机初始化
                DicHikCameras.Clear();
         
                HikCamera hikCamera = new HikCamera { SN = CommonValue.CamSerialNum, Id = 0 };
                if (hikCamera.InitializeCamera())
                {


                    DicHikCameras.Add("HIK", hikCamera);


                    hikCamera.TriggerMode = TriggerMode.On;

                    hikCamera.Gain = CommonValue.Gain;
                    hikCamera.Exposure = CommonValue.ExposeTime;
                    hikCamera.LineDebouncerTime = CommonValue.LineDebouncerTime ;
                    hikCamera.GevSCPDDelay = CommonValue.SPCD;
           

                    hikCamera.ImageTaked += HikCamera_ImageTaked;
                    IsCamConnect = true;
                    Logging($"相机初始化连接成功！", Color.Green);
                }
                else
                {
                    IsCamConnect = false;
                    Logging($"相机初始化连接失败！", Color.Red);
                }


                #endregion

                #region 根据任务表配置任务
                foreach (var item in SysParams.DicTaskInfos)
                {
                    if (!DicTasks.ContainsKey(item.Key))
                    {
                        TaskRunner taskRunner = new TaskRunner()
                        {
                            Name = item.Key,
                            Description = item.Value.Description,
                            TaskFilePath = item.Value.FilePath
                        };
                        taskRunner.RunCompleted += VisionProTask_RunCompleted;
                        DicTasks.Add(item.Key, taskRunner);
                    }
                }
                //删除已删除的任务
                List<string> unexpectedTaskName = new List<string>();
                foreach (var item in DicTasks.Where(item => !SysParams.DicTaskInfos.ContainsKey(item.Key)))
                {
                    unexpectedTaskName.Add(item.Key);
                }
                foreach (var item in unexpectedTaskName)
                {
                    DicTasks.Remove(item);
                    GC.Collect();
                }
                #endregion

                #region   后台加载任务
                i = 20;


                count = DicTasks.Count;
                if (count == 0)
                    count = 1;
                step = 50 / count;
                foreach (var item in DicTasks)
                {
                    i += step;
                    if (!item.Value.IsLoaded)
                    {

                        if (item.Value.LoadTaskFile())
                        {
                            Logging("加载任务[" + item.Key + "]完成...", Color.Blue);
                            item.Value.IsLoaded = true;
                            CommonValue.TaskName = item.Value.Name;
                        }
                        else
                        {
                            CommonValue.TaskName = "";
                            Logging("加载任务[" + item.Key + "]出错...", Color.Red);
                        }
                    }
                    SetProgressValue(i);
                }


                #endregion


                //string[] _ArrSilver1 = INIhelp1.GetValue(PathOffet, "", "银色一穴").Split(',');
                //string[] _ArrSilver2 = INIhelp1.GetValue(PathOffet, "", "银色二穴").Split(',');
                //for (int k= 0; k < _ArrSilver1.Length; k++)
                //{

                //    DicTasks[CommonValue.TaskName].Outputs[k].AdjustCF = Convert.ToDouble(_ArrSilver1[k]);
                //    DicTasks[CommonValue.TaskName].Outputs[k].AdjustCF2 = Convert.ToDouble(_ArrSilver2[k]);
                //}

                #region 配置标定
                foreach (var item in SysParams.DicCalibInfos)
                {
                    if (!DicCalibTools.ContainsKey(item.Key))
                    {
                        HixCalibTool hixCalibTool = new HixCalibTool()
                        {
                            Name = item.Key,
                            Id = item.Value.Id,
                            CalibFilePath = item.Value.FilePath
                        };
                        DicCalibTools.Add(item.Key, hixCalibTool);
                    }
                }
                //删除已删除的标定
                List<string> unexpectedCalibName = new List<string>();
                foreach (var item in DicCalibTools.Where(item => !SysParams.DicCalibInfos.ContainsKey(item.Key)))
                {
                    unexpectedTaskName.Add(item.Key);
                }
                foreach (var item in unexpectedTaskName)
                {
                    DicCalibTools.Remove(item);
                    GC.Collect();
                }

                #endregion

                #region 加载标定
                i = 70;
                count = DicCalibTools.Count;
                if (count == 0)
                    count = 1;
                step = 30 / count;
                foreach (var item in DicCalibTools)
                {
                    i += step;
                    if (!item.Value.IsLoaded)
                    {
                        if (item.Value.LoadCalibFile())
                        {
                            Logging("加载标定文件[" + item.Key + "]完成...", Color.Blue);
                            item.Value.IsLoaded = true;
                        }
                        else
                        {
                            Logging("加载标定文件[" + item.Key + "]出错...", Color.Red);
                        }
                    }
                    SetProgressValue(i);
                }
                #endregion

                #region 数据本地保存
                string path = $@"{SysParams.DataSavePath}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                dataFilePaht = $@"{path}\{DateTime.Now.ToString("yyyyMMdd")}.csv";
                if (!File.Exists(dataFilePaht))
                {
                    File.Create(dataFilePaht).Close();
                }

                if(SysParams.IsDataRecordSave)
                {
                 

                    if(CsvHepler.IsEmpty(dataFilePaht))
                    {
                        CsvHepler.WriteCSV(dataFilePaht, SysParams.DataHeadContent);
                    }

                }
                #endregion

                #region NG定义
                FlawTypeDictionary.Clear();
                for (int m = 1; m <= 20; m++)
                {
                    FlawTypeDictionary.Add(m, INIhelp.GetValue(Application.StartupPath + "\\FlawDefine.ini", m.ToString()));
                }
                #endregion

                action();
                SetProgressValue(100); Logging("初始化完成...", Color.DarkSlateBlue);
                CommonValue.InitOK = true;
                CommonValue.Camera1Ready = true;

                CommonValue.CameraFly1 = true;

                frmLoading.Close();
                
            })));
        }

        //private void SVSCamera_ImageTaked(object sender, SVSDataTakedEventArgs e)
        //{
        //    //自动运行状态
        //    if (CommonValue.IsStart)
        //    {
        //        bool imgGetOK = e.IsDone;
        //        if (imgGetOK)
        //        {
        //            Bitmap bitmap = e.Image;
        //            ImageUnit image = new ImageUnit();
        //            image.Image = bitmap;
        //            image.CameraID = e.CameraID;
        //            if (e.CameraID == 0)
        //            {

        //                if (newgroup)
        //                {
        //                    indexOfImages = 0;
        //                    newgroup = false;
        //                    GC.Collect();
        //                    return;  //舍弃第一张
        //                }
        //                Logging($"----! Index  [{indexOfImages}]", Color.Green);
        //                if (!IsCache)
        //                {

        //                    TempImages_Camera1.Add(image);
        //                    if (TempImages_Camera1.Count >= 24)
        //                    {
        //                        m_IDCode1 = INIhelp1.GetValue(Path2D, "Config", "ID1");
        //                        m_IDCode2 = INIhelp1.GetValue(Path2D, "Config", "ID2");
        //                        IsExchange = true;
        //                        INIhelp1.SetValue(Path2D, "", "结束触发", "1");
        //                        Logging($"拍摄就绪", Color.Red);
        //                        IDcode m_IDCode = new IDcode() { IDCode1 = m_IDCode1, IDCode2 = m_IDCode2 };
        //                        ThreadPool.QueueUserWorkItem(new WaitCallback((o) => {
        //                            CameraFly_Action(m_IDCode);
        //                        }));


        //                    }
        //                }
        //                else
        //                {
        //                    //需要 缓存 时存入 
        //                    TempImages_Camera_Cache.Add(image);
        //                    if (TempImages_Camera_Cache.Count >= 24)
        //                    {
        //                        m_IDCode1 = INIhelp1.GetValue(Path2D, "Config", "ID1");
        //                        m_IDCode2 = INIhelp1.GetValue(Path2D, "Config", "ID2");
        //                        IsExchange = true;
        //                        INIhelp1.SetValue(Path2D, "", "结束触发", "1");
        //                        Logging($"拍摄就绪", Color.Red);
        //                        IDcode m_IDCode = new IDcode() { IDCode1 = m_IDCode1, IDCode2 = m_IDCode2 };
        //                        ThreadPool.QueueUserWorkItem(new WaitCallback((o) => {
        //                            CameraFly_Action(m_IDCode);
        //                        }));

        //                    }
        //                }
        //                indexOfImages++;
        //                if (indexOfImages == 12)
        //                {
        //                    newgroup = true;   //舍弃第14 张

        //                }
        //            }
        //        }
        //        else
        //        {
        //            Logging($"get image error !!!", Color.Red);
        //        }
        //    }
        //}



        private void HikCamera_ImageTaked(object sender, HixDataTakedEventArgs e)
        {
            //自动运行状态
            if (CommonValue.IsStart)
            {
                bool imgGetOK = e.IsDone;
                if (imgGetOK)
                {
                    Bitmap bitmap = e.Image;
                    ImageUnit image = new ImageUnit();
                    image.Image = bitmap;
                    image.CameraID = e.CameraID;
                    if (e.CameraID == 0)
                    {
                        Logging($"----! Index  [{indexOfImages}]", Color.Green);
                        if (!IsCache)
                        {
                            TempImages_Camera1.Add(image);
                            if (TempImages_Camera1.Count >= 16)
                            {
                                m_IDCode1 = INIhelp1.GetValue(Path2D, "Config", "ID1");
                                m_IDCode2 = INIhelp1.GetValue(Path2D, "Config", "ID2");
                                IsExchange = true;
                                INIhelp1.SetValue(Path2D, "", "结束触发", "1");
                                Logging($"拍摄就绪", Color.Red);
                                IDcode m_IDCode = new IDcode() { IDCode1 = m_IDCode1, IDCode2 = m_IDCode2 };
                                ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
                                {
                                    CameraFly_Action(m_IDCode);
                                }));
                            }
                        }
                        else
                        {
                            //需要 缓存 时存入 
                            TempImages_Camera_Cache.Add(image);
                            if (TempImages_Camera_Cache.Count >= 16)
                            {

                                m_IDCode1 = INIhelp1.GetValue(Path2D, "Config", "ID1");
                                m_IDCode2 = INIhelp1.GetValue(Path2D, "Config", "ID2");
                                IsExchange = true;
                                INIhelp1.SetValue(Path2D, "", "结束触发", "1");
                                Logging($"拍摄就绪", Color.Red);
                                IDcode m_IDCode = new IDcode() { IDCode1 = m_IDCode1, IDCode2 = m_IDCode2 };
                                ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
                                {
                                    CameraFly_Action(m_IDCode);
                                }));

                            }
                        }
                        indexOfImages++;
                    }
                }
                else
                {
                    Logging($"get image error !!!", Color.Red);
                }
            }
        }

        private void UpdateUI()
        {
            dockPanel.Skin = Serialize.BinaryDeserialize<DockPanelSkin>("1.skin");

            CloseAllDockContent();
            frmLog.Show(dockPanel, DockState.DockBottom);  //日志窗口
            frmresult.Show(dockPanel);
            //frmImageViewing.Show(dockPanel);
            resultViewing.Show(dockPanel);

            frmParameters.Param = SysParams;
            frmParameters.ParametersChanged += FrmConfiguration_ParametersChanged;
            frmParameters.Show(dockPanel, DockState.DockRightAutoHide);

            #region 根据信息表更新任务表及界面
            displayWindoowToolStripMenuItem.DropDownItems.Clear();
            taskEditToolStripMenuItem.DropDownItems.Clear();
            resultsWindowToolStripMenuItem.DropDownItems.Clear();
            CameraEditStripMenuItem.DropDownItems.Clear();
            foreach (var item in SysParams.DicTaskInfos)
            {
                ToolStripMenuItem tsmi_Display = new ToolStripMenuItem(item.Key, Properties.Resources.IcoView);
                tsmi_Display.Click += Tsmi_Display_Click;
                displayWindoowToolStripMenuItem.DropDownItems.Add(tsmi_Display);

                ToolStripMenuItem tsmi_TaskEdit = new ToolStripMenuItem(item.Key, Properties.Resources.IcoTask);
                tsmi_TaskEdit.Click += Tsmi_TaskEdit_Click;
                taskEditToolStripMenuItem.DropDownItems.Add(tsmi_TaskEdit);

                ToolStripMenuItem tsmi_TaskResult = new ToolStripMenuItem(item.Key, Properties.Resources.IcoResult);
                tsmi_TaskResult.Click += Tsmi_TaskResult_Click;
                resultsWindowToolStripMenuItem.DropDownItems.Add(tsmi_TaskResult);



            }
            //foreach (var item in SysParams.DicCameraInfos)
            //{
            //    ToolStripMenuItem tsmi_Camera = new ToolStripMenuItem(item.Key, Properties.Resources.Camera);
            //    tsmi_Camera.Click += Tsmi_Camera_Click;
            //    CameraEditStripMenuItem.DropDownItems.Add(tsmi_Camera);
            //}
            #endregion

            #region 打开已有的窗体

            foreach (ToolStripMenuItem item in resultsWindowToolStripMenuItem.DropDownItems)
            {
                if (SysParams.ListResultView.Contains(item.Text))
                    item.PerformClick();
            }
            foreach (ToolStripMenuItem item in taskEditToolStripMenuItem.DropDownItems)
            {
                if (SysParams.ListTaskEditView.Contains(item.Text))
                    item.PerformClick();
            }
            foreach (ToolStripMenuItem item in displayWindoowToolStripMenuItem.DropDownItems)
            {
                if (SysParams.ListDisplayView.Contains(item.Text))
                    item.PerformClick();
            }
            #endregion
    }



        private void ReadRealValueResult()
        {
            try
            {

                string strPath = Application.StartupPath + "\\RealValueResult\\结果判断.csv";
                StreamReader sr = new StreamReader(strPath,Encoding.Default);
                CommonValue.ListTitle = sr.ReadLine().Split(',').ToList();
                for (int i = 0; i < 3; i++)
                {
                    string[] results = sr.ReadLine().Trim().Split(',');
                    switch (i)
                    {
                        case 0:
                            for (int j = 1; j < results.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(results[j]))
                                {
                                    CommonValue.ListRealValue.Add(Convert.ToDouble(results[j]));
                                }

                            }
                            break;

                        case 1:
                            for (int j = 1; j < results.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(results[j]))
                                {
                                    CommonValue.ListUpValue.Add(Convert.ToDouble(results[j]));
                                }

                            }
                            break;

                        case 2:
                            for (int j = 1; j < results.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(results[j]))
                                {
                                    CommonValue.ListDownValue.Add(Convert.ToDouble(results[j]));
                                }

                            }
                            break;

                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

                
            }
        }


     


        private void ReadOffetInI()
        {
            try
            {


            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #region 事件
        private void FrmMain_Load(object sender, EventArgs e)
        {
     
            ReadRealValueResult();
            InitializeSystem();
            InitTimer();
            //bool bRET = false;
            //bRET = profile.LoadDxf(string.Format(@"DXF\N_model.DXF", INIhelp1.GetValue(PathConfig, "System","DXFName")));
            //if (bRET)
            //{
            //    Logging("DXF读取成功...", Color.DarkSlateBlue);
            //}


            CommonValue.isCheckToday = Convert.ToBoolean(INIhelp1.GetValue(PathConfig, "System", "点检"));
            CommonValue.dtCheckTime = INIhelp1.GetValue(PathConfig, "System", "点检时间");



            if (CommonValue.dtCheckTime != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                CommonValue.isCheckToday = false;
                Logging("今日未点检,无法正常测试!!!请先点检通过!!!", Color.Red);
            }
            else
            {
                if (!CommonValue.isCheckToday)
                {
                    Logging("今日未点检,无法正常测试!!!请先点检通过!!!", Color.Red);
                }
                else
                {
                    Logging("今日点检已经通过!!!", Color.Green);
                }
            }

            try
            {
                if (resView==null)
                {
                    resView = DicTasks[CommonValue.TaskName].GetTaskFrmResults();
                    DockContent frmTaskResultsView = resView;
                    frmTaskResultsView.Show(dockPanel, DockState.DockLeft);
                }
                else
                {
                    DockContent frmTaskResultsView = resView;
                    frmTaskResultsView.Show(dockPanel, DockState.DockLeft);
                }
            }
            catch (Exception ex)
            {

           
            }
        }
        FrmResultsView resView = null;
        private void Tsmi_TaskResult_Click(object sender, EventArgs e)
        {
            if (SysParams.IsSaveData4)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                resView = DicTasks[tsmi.Text].GetTaskFrmResults();
                resView.ShowRatio = SysParams.IsShowRatio;
                DockContent frmTaskResultsView = resView;
                frmTaskResultsView.Show(dockPanel, DockState.DockLeft);
                if (!SysParams.ListResultView.Contains(tsmi.Text))
                    SysParams.ListResultView.Add(tsmi.Text);
            }

        }

        private void Tsmi_TaskEdit_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            DockContent frmTaskEdit = DicTasks[tsmi.Text].GetFrmTaskEdit();
            frmTaskEdit.Show(dockPanel);
            if (!SysParams.ListTaskEditView.Contains(tsmi.Text))
                SysParams.ListTaskEditView.Add(tsmi.Text);
        }

        private void Tsmi_Display_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            DockContent frmTaskView = DicTasks[tsmi.Text].GetFrmTaskView();
            frmTaskView.Show(dockPanel);
            if (!SysParams.ListDisplayView.Contains(tsmi.Text))
                SysParams.ListDisplayView.Add(tsmi.Text);
        }

        private void Tsmi_Camera_Click(object sender, EventArgs e)
        {
            //ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ////DockContent frmCameraEdit = DicCameras[tsmi.Text].GetFrmCamreaEdit();
            //frmCameraEdit.Show(dockPanel);
        }
        private void VisionProTask_RunCompleted(object sender, RunningCompletedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                Logging("任务 [" + e.Name + "] 耗时 " + e.RunTime + " ms", Color.Green);
            }));
        }

        private void CameraRunner_ImageTaked(object sender, ImageTakedEventArgs e)
        {
            //自动运行状态
            if (CommonValue.IsStart)
            {
                if (true)
                {
                    Bitmap bitmap = ((ICogImage)e.Image).ToBitmap();
                    if (indexOfImages < 2 * SysParams.PlanedImageNamber)
                    {
                        ImageUnit image = new ImageUnit();
                        image.Image = bitmap;
                        if (CommonValue.IsManual)
                        {
                            string timeSeal = DateTime.Now.ToString(image.CameraID + 1 + "#相机_" + "yyyyMMdd_HH_mm_ss_ff");
                            string filePath = SysParams.OrginImagePath + timeSeal + ".bmp";
                            Invoke(new Action(() => frmImageViewing.SetImage_Manual(image, filePath)));
                        }
                    }
                    Invoke(new Action(() => frmImageViewing.SetImage(bitmap)));
                }
            }
        }
        private void ParametersWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParameters.Show(dockPanel, DockState.DockRight);
        }

        private void TsbtnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(toolChangeJob.Text) )  
            {
                Logging("请先选择任务!!!", Color.Red);
                return;
            }

            if (!IsCamConnect)
            {
                Logging("相机初始化失败!!!", Color.Red);
                return;
            }

            if ( !(CommonValue.isCheck) && (!CommonValue.isCheckToday) )
            {
                Logging("点检未通过,无法测试!!!", Color.Red);
                return;
            }


            CommonValue.IsStart = !CommonValue.IsStart;
            Logging(string.Format("######切换到{0}状态#######", this.tsbtnRun.Text), Color.Red);
            this.tsbtnRun.Image = CommonValue.IsStart?
                global::Hix_CCD_Module.Properties.Resources.IcoStop:
                global::Hix_CCD_Module.Properties.Resources.IcoRun;
            this.tsbtnRun.Text = CommonValue.IsStart ? "停止" : "运行";

            if (CommonValue.IsStart)
            {
                toolChangeJob.Enabled = false;

            }
            else
            {
                toolChangeJob.Enabled = true;
            }
        }

        private void ExistingTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTaskConfiguration frmTaskConfiguration = new FrmTaskConfiguration();
            frmTaskConfiguration.TaskConfigurationChanged += FrmConfiguration_ConfigurationChanged;
            frmTaskConfiguration.ShowDialog();
        }
        /// <summary>
        /// 相机参数修改处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfiguration_ConfigurationChanged(object sender, HixDataChangedEventArgs e)
        {
            CheckActiveContent();
            TaskLoading(UpdateUI);
        }
        private void FrmConfiguration_ParametersChanged(object sender, HixDataChangedEventArgs e)
        {
            try
            {
                resView.ShowRatio = SysParams.IsShowRatio;
            }
            catch (Exception ex)
            {

            
            }

        }

        private void SkinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockPanelSkin skin = dockPanel.Skin;
            frmSkin.Skin = skin;
            frmSkin.Show(dockPanel, DockState.DockRight);

        }

        private void FrmSkin_SkinChanged(object sender, HixDataChangedEventArgs e)
        {
            dockPanel.Skin = (DockPanelSkin)e.NewValue;
            dockPanel.Refresh();
            Serialize.BinarySerialize("1.skin", dockPanel.Skin);
        }

        private void AddTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddNewTask frmAddNewTask = new FrmAddNewTask();
            frmAddNewTask.TaskConfigurationChanged += FrmConfiguration_ConfigurationChanged;
            frmAddNewTask.ShowDialog();
        }

        private void LogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLog.Show(dockPanel, DockState.DockBottom);
        }

        private void DeleteTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDeleteTask frmDeleteTask = new FrmDeleteTask();
            frmDeleteTask.TaskConfigurationChanged += FrmConfiguration_ConfigurationChanged;
            frmDeleteTask.ShowDialog();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (DicHikCameras.Count > 0)
                {
                    if (DicHikCameras["HIK"] != null)
                    {
                        DicHikCameras["HIK"].Close();

                     
                    }
                }

                GC.Collect();


            }
            catch (Exception ex)
            {

                throw;
            }
            CheckActiveContent();
            SysParams.SaveToFile();
        }

  

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmExecutionFlowConfiguration frmExecutionFlowConfiguration = new FrmExecutionFlowConfiguration();
            //frmExecutionFlowConfiguration.Show();
        }

        private void AddCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddNewCamera frmAddNewCamera = new FrmAddNewCamera();
            frmAddNewCamera.CameraConfigurationChanged += FrmConfiguration_ConfigurationChanged;
            frmAddNewCamera.ShowDialog();
        }

        private void DeleteCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDeleteCamera frmDeleteCamera = new FrmDeleteCamera();
            frmDeleteCamera.CameraConfigurationChanged += FrmConfiguration_ConfigurationChanged;
            frmDeleteCamera.ShowDialog();
        }

        private void HIKVISIONToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExistingCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHikCameraEdit frmHikCameraEdit = new FrmHikCameraEdit();
            frmHikCameraEdit.CameraConfigurationChanged += FrmConfiguration_ConfigurationChanged;
            frmHikCameraEdit.ShowDialog();
            SysParams.SaveToFile();
        }

        private void CalibrationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ( SysParams.IsSaveData3)
            {
                FormCa rmCa = new FormCa();
                rmCa.ShowDialog();
            }
            else
            {
                FrmCalibration frmCalibration = new FrmCalibration();
                frmCalibration.ShowDialog();
            }
        }


        private void ExistingCalibToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.FrmCalibToolConfiguration frmCalibToolConfiguration = new FrmCalibToolConfiguration();
            frmCalibToolConfiguration.CalibConfigurationChanged += FrmConfiguration_ConfigurationChanged;
            frmCalibToolConfiguration.ShowDialog();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmAboutBoxcs().ShowDialog();
        }

        private void ImageViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImageViewing.Show(dockPanel);
        }
       
        #endregion

        private void 打开调试窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IOForm form = new IOForm(this);
            form.ShowDialog();
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{
        //    if(!CommonValue.IsManual) Logging("######切换到手动模式#######", Color.Red);
        //    if (CommonValue.IsManual) Logging("######取消手动模式#######", Color.Red);

        //    CommonValue.IsManual = !CommonValue.IsManual;

        //    this.tsBtnManual.BackColor = CommonValue.IsManual ? Color.Gray : SystemColors.ButtonHighlight;

        //}

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                FrmCheck frm = new FrmCheck();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ListCheckLoad.Clear();
                    ListCheckTest.Clear();
                    StreamReader sr = new StreamReader(CommonValue.strCheckPath, Encoding.Default);
                    string[] strarrs = sr.ReadLine().Split(',');
                    tableheads = strarrs;

                    for (int i = 0; i < CommonValue.iCheckCount; i++)
                    {
                        strarrs = sr.ReadLine().Split(',');
                        List<double> list = new List<double>();
                        for (int j = 0; j < strarrs.Length; j++)
                        {
                            list.Add(Convert.ToDouble(strarrs[j]));
                        }

                        ListCheckLoad.Add(list);
                    }

                    Logging($"#####点检数据加载完成，行数:{ListCheckLoad.Count} 列数:{ListCheckLoad[0].Count} ######", Color.Green);
                    sr.Close();
                }

            }
            catch (Exception ex)
            {
                Logging($"#####点检加载数据错误!!! ######", Color.Red);

            }
        }







        private void resultsWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void taskEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ToolStripMenuItem tsmi_TaskEdit = new ToolStripMenuItem(item.Key, Properties.Resources.IcoTask);
            //tsmi_TaskEdit.Click += Tsmi_TaskEdit_Click;
            //taskEditToolStripMenuItem.DropDownItems.Add(tsmi_TaskEdit);
        }

        private void displayWindoowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 复位 所有信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            TempImages_Camera1.Clear();
            indexOfImages = 0;
            TempImages_Camera_Cache.Clear();
            newgroup = true;
            CommonValue.Camera1Ready = true;


            INIhelp1.SetValue(Path2D, "", "开始触发", "0");
             INIhelp1.SetValue(Path2D, "", "结束触发", "0"); 
            INIhelp1.SetValue(Path2D, "", "开始触发反馈", "0");

            //flag = true;
            //indexOfImages = 0;
            //后续注意信号复位问题
            // SetOutput(IOSignalAdress.Camera1Ready, true);

        }

        private void 复检ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDebug form = new FrmDebug();
            form.ShowDialog();
        }


        private void nG类型定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlawTypeDefine form = new FlawTypeDefine();
            form.Show();
        }
        public static void SaveNGDefine()
        {
            for(int i=1;i<= FlawTypeDictionary.Count;i++)
            {
                INIhelp.SetValue(Application.StartupPath + "\\FlawDefine.ini", i.ToString(), FlawTypeDictionary[i]);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

       
            }
            //CheckDataSave();
            //CheckHardDiskFreeSpace();
        }
        private void CheckDataSave()
        {
            DateTime nowDate = DateTime.Now;
            string[] FileList = Directory.GetDirectories(SysParams.OrginImagePath);
            string[] FileList1 = Directory.GetDirectories(SysParams.StitchImagePath);
            string[] FileList2 = Directory.GetDirectories(SysParams.ResultImagePath);
            for (int i = 0; i < FileList.Length; i++)
            {
                if (Directory.Exists(FileList[i]))
                {
                    DateTime nt = Directory.GetCreationTime(FileList[i]);
                    TimeSpan longdays = nowDate - nt;
                    if (longdays.TotalDays >= SysParams.DataSaveDays)

                    {
                        Directory.Delete(FileList[i], true);
                    }
                }
            }
            for (int i = 0; i < FileList1.Length; i++)
            {
                if (Directory.Exists(FileList1[i]))
                {
                    DateTime nt = Directory.GetCreationTime(FileList1[i]);
                    TimeSpan longdays = nowDate - nt;
                    if (longdays.TotalDays >= SysParams.DataSaveDays)

                    {
                        Directory.Delete(FileList1[i], true);
                    }
                }
            }
            for (int i = 0; i < FileList2.Length; i++)
            {
                if (Directory.Exists(FileList2[i]))
                {
                    DateTime nt = Directory.GetCreationTime(FileList2[i]);
                    TimeSpan longdays = nowDate - nt;
                    if (longdays.TotalDays >= SysParams.DataSaveDays)

                    {
                        Directory.Delete(FileList2[i], true);
                    }
                }
            }
        }
        bool flag1 = true;
        private void CheckHardDiskFreeSpace()
        {
            long space = GetHardDiskFreeSpace(SysParams.DataSaveHardDisk);
            if (space < SysParams.FreeHardDisk && flag1)
            {
                flag1 = false;
                MessageBox.Show($"{SysParams.DataSaveHardDisk}盘空间不足{SysParams.FreeHardDisk }GB,请手动清理磁盘空间并缩短数据存放天数！","提示：");
            }
        }
        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                }
            }
            return freeSpace;
        }

        private void 打开分bin窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResultBin.Show(dockPanel);
        }

        private void 打开点检窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCheck frm = new FrmCheck();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ListCheckLoad.Clear();
                    ListCheckTest.Clear();
                    StreamReader sr = new StreamReader(CommonValue.strCheckPath,Encoding.Default);
                    string[] strarrs = sr.ReadLine().Split(',');
                    tableheads = strarrs;

                    for (int i = 0; i < CommonValue.iCheckCount; i++)
                    {
                        strarrs = sr.ReadLine().Split(',');
                        List<double> list = new List<double>();
                        for (int j = 0; j < strarrs.Length; j++)
                        {
                            list.Add(Convert.ToDouble(strarrs[j]));
                        }

                        ListCheckLoad.Add(list);
                    }
        
                    Logging($"#####点检数据加载完成，行数:{ListCheckLoad.Count} 列数:{ListCheckLoad[0].Count} ######", Color.Green);
                    sr.Close();
                }

            }
            catch (Exception ex)
            {
                Logging($"#####点检加载数据错误!!! ######", Color.Red);

            }
        }



     


        private void QueryMongo()
        {
            try
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                List<PartResult> lst = new List<PartResult>();
                mongoHelper.QueryManyByTime<PartResult>(DateTime.Now.ToShortDateString(), str, out lst);
                if (lst.Count > 0)
                {
                    productIndex = lst.Count;
                    foreach (PartResult item in lst)
                    {
                        if (item.Measure_Result == "OK")
                        {
                            _iOKCount++;
                            //OK
                        }
                        else
                        {
                            _iNGCount++;
                            //NG
                        }
                    }
                }
            }
            catch (Exception ex)
            {

               
            }
        }


        private void 导出结果数据格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                //SaveFileDialog file = new SaveFileDialog();
                //string strWtiteScv = "数值类型" + ",";
                //file.DefaultExt = "结果判断.csv";
                //if (file.ShowDialog() == DialogResult.OK)
                //{

                //    if (DicTasks[CommonValue.TaskName].Outputs.Count > 0)
                //    {
                //        foreach (Terminal t in DicTasks[CommonValue.TaskName].Outputs)
                //        {
                //            if (t.ValueType == typeof(double) ||
                //            t.ValueType == typeof(int) ||
                //            t.ValueType == typeof(string) ||
                //            t.ValueType == typeof(bool))
                //            {
                //                strWtiteScv += t.Description + ",";
                //            }
                //        }
                //    }
                //    CsvHepler.WriteCSV(file.FileName, strWtiteScv);
                //}
            }
            catch (Exception ex)
            {

                
            }
        }

        private void CameraEditStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResultViewing resultViewing = new FrmResultViewing();
            resultViewing.Show(dockPanel);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
              this.Invoke(new Action( () => resultViewing.ShowText(1,"NG",CogColorConstants.Red) ) );
        }



        private void toolChangeJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] _ArrWhite1 = INIhelp1.GetValue(PathOffet, "", "粉色一穴").Split(',');
                string[] _ArrWhite2 = INIhelp1.GetValue(PathOffet, "", "粉色二穴").Split(',');

                string[] _ArrSilver1 = INIhelp1.GetValue(PathOffet, "", "银色一穴").Split(',');
                string[] _ArrSilver2 = INIhelp1.GetValue(PathOffet, "", "银色二穴").Split(',');

                string[] _ArrBlack1 = INIhelp1.GetValue(PathOffet, "", "黑色一穴").Split(',');
                string[] _ArrBlack2 = INIhelp1.GetValue(PathOffet, "", "黑色二穴").Split(',');

                if ( (_ArrWhite1.Length == _ArrWhite2.Length)  && (_ArrSilver1.Length == _ArrSilver2.Length) && (_ArrBlack1.Length == _ArrBlack2.Length))
                {

                    if (resView !=null)
                    {
                        resView.Close();
                    }
   
                    switch (toolChangeJob.SelectedIndex)
                    {
                        case 0:
                            for (int i = 0; i < _ArrWhite1.Length; i++)
                            {

                                DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF = Convert.ToDouble(_ArrWhite1[i]);
                                DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF2 = Convert.ToDouble(_ArrWhite2[i]);

                                CogToolBlockTerminal t = DicTasks[CommonValue.TaskName].GuideIns.Outputs[i] as CogToolBlockTerminal;
                                string[] strs = t.Description.Split('#');
                                strs[0] = _ArrWhite1[i];
                                strs[6] = _ArrWhite2[i];
                                string str = "";
                                for (int k = 0; k < strs.Length; k++)
                                {
                                    str += strs[k] + "#";
                                }

                                str = str.Remove(str.Length - 1, 1);
                                t.Description = str;
                                //DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF = Convert.ToDouble(_ArrWhite1[i]);
                                //DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF2 = Convert.ToDouble(_ArrWhite2[i]);
                            }
                            break;

                        case 1:
                            for (int i = 0; i < _ArrSilver1.Length; i++)
                            {

                                DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF = Convert.ToDouble(_ArrSilver1[i]);
                                DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF2 = Convert.ToDouble(_ArrSilver2[i]);

                                CogToolBlockTerminal t = DicTasks[CommonValue.TaskName].GuideIns.Outputs[i] as CogToolBlockTerminal;
                                string[] strs = t.Description.Split('#');
                                strs[0] = _ArrSilver1[i];
                                strs[6] = _ArrSilver2[i];
                                string str = "";
                                for (int k = 0; k < strs.Length; k++)
                                {
                                    str += strs[k] + "#";
                                }

                                str = str.Remove(str.Length - 1, 1);
                                t.Description = str;
      
                            }
                            break;

                        case 2:
                            for (int i = 0; i < _ArrBlack1.Length; i++)
                            {

                                DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF = Convert.ToDouble(_ArrBlack1[i]);
                                DicTasks[CommonValue.TaskName].Outputs[i].AdjustCF2 = Convert.ToDouble(_ArrBlack2[i]);

                                CogToolBlockTerminal t = DicTasks[CommonValue.TaskName].GuideIns.Outputs[i] as CogToolBlockTerminal;
                                string[] strs = t.Description.Split('#');
                                strs[0] = _ArrBlack1[i];
                                strs[6] = _ArrBlack2[i];
                                string str = "";
                                for (int k = 0; k < strs.Length; k++)
                                {
                                    str += strs[k] + "#";
                                }

                                str = str.Remove(str.Length - 1, 1);
                                t.Description = str;

                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("格式错误!");
                }


            }
            catch (Exception ex)
            {

        
            }
        }


    }
    [Serializable]
    public class ImageUnit
    {
        //图像
        public Bitmap Image;
        //产品ID
        public int ProductID;
        //载具ID
        public int CarrierID;
        //相机ID
        public int CameraID;
    }


    public class IDcode
    {
        public string IDCode1;
        //载具ID
        public string IDCode2;
    }

}

#endregion
