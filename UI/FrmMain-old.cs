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
using Lead.Detect.MongodbHelper;
using Lead.CPrim.PrimVariableClient;

namespace Hix_CCD_Module
{
    public partial class FrmMain : Form
    {
        VariableClient skt;
        #region 系统变量
        FrmLog frmLog = new FrmLog();
        FrmParameters frmParameters = new FrmParameters(default(Parameters));
        FrmLoading frmLoading = new FrmLoading();
        FrmSkin frmSkin = new FrmSkin();
        FrmImageViewing frmImageViewing = new FrmImageViewing();
        FrmResultViewing resultViewing = new FrmResultViewing();
        MongoHelper mongoHelper = new MongoHelper();

        //拼接的图片
        object stichImage = null;
        //相机取像图片计数
        int indexOfImages = 0;
        int indexOfImages2 = 0;
        int productIndex = 0;
        public static Dictionary<int, string> FlawTypeDictionary = new Dictionary<int, string>();
        //用于正在任务计算所需要的图片
        public static List<ImageUnit> CurrentImages { get; set; } = null;
        public static List<ImageUnit> CurrentImages2 { get; set; } = null;

        //用于缓存相机图片
        public static List<ImageUnit> TempImages_Camera1 { get; set; } = null;
        public static List<ImageUnit> TempImages_Camera2 { get; set; } = null;

        //用于每组图片置位标志
        private bool newgroup = true;

        //标定后的图片
        public static List<CogImage8Grey> CalibImages { get; set; } = null;
        public static List<CogImage8Grey> CalibImages2 { get; set; } = null;

        //服务器
        public static Server Server { set; get; } = null;
        //系统参数
        public static Parameters SysParams { get; set; } = new Parameters();
        //任务
        public static Dictionary<string, TaskRunner> DicTasks { get; set; } = new Dictionary<string, TaskRunner>();
        //相机
        public static Dictionary<string, CameraRunner> DicCameras { get; set; } = new Dictionary<string, CameraRunner>();
        //海康相机
        public static Dictionary<string, HikCamera> DicHikCameras { get; set; } = new Dictionary<string, HikCamera>();
        //SVS相机
        public static Dictionary<string, SVSCam> DicSVSCameras { get; set; } = new Dictionary<string, SVSCam>();

        //标定
        public static Dictionary<string, HixCalibTool> DicCalibTools { get; set; } = new Dictionary<string, HixCalibTool>();
        //雷赛IO板卡
        public static Leisai_IOC640 LeisaiIO = new Leisai_IOC640(0);
        public static IniFileHelper iniFile = new IniFileHelper(@"E:\Hix50125.ini");
        string dataFilePaht = "";
        //2D与运动控制交互
        string Path2D = @"D:\2D与运动控制交互\EX.ini";
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
            Invoke(new Action(() => frmLog.UpdateLog(log, color)));
        }
        private void InitializeSystem()
        {
            #region Socket初始化
            skt = new VariableClient();
            string re = "";
            skt.ClientName = "_2d";
            skt.Name = "VarClient";
            skt.ServerIpPath = "127.0.0.1";
            skt.IPrimInit();
            skt.IPrimConnect(ref re);
            #endregion
            frmLog.Show(dockPanel, DockState.DockBottom);
            SetProgressValue(0); Logging("系统初始化...", Color.DarkSlateBlue);

            SysParams = Parameters.LoadParametersFromFile(); Logging("加载参数完成...", Color.DarkSlateBlue);
            HikCameraOperator.DeviceListAcq(); Logging("初始化相机列表...", Color.DarkSlateBlue);

            TempImages_Camera1 = new List<ImageUnit>();
            TempImages_Camera2 = new List<ImageUnit>();

            CurrentImages = new List<ImageUnit>(SysParams.PlanedImageNamber);
            CurrentImages2 = new List<ImageUnit>(SysParams.PlanedImageNamber);
            //CurrentImages3 = new List<ImageUnit>(SysParams.PlanedImageNamber);
            //CurrentImages4 = new List<ImageUnit>(SysParams.PlanedImageNamber);

            DicTasks.Clear();
            DicCalibTools.Clear();

            IniFileHelper inifile = new IniFileHelper(Application.StartupPath+ @"\Config.ini");
            string mode = inifile.IniReadValue("System", "Debug");
            if(mode=="true")
            {
                CommonValue.IsDebug = true;
            }

            TaskLoading(UpdateUI);
            // Server = new Server(SysParams.IPAddress, SysParams.ServerPort);
            // Server.TcpConnected += Server_TcpConnected;
            // Server.TcpDisConnected += Server_TcpDisConnected;
            // Server.TcpDateSend += Server_TcpDateSend;
            //Server.TcpDateReceived += Server_TcpDateReceived;
            //Server.Start();
            //if (!CommonValue.IsDebug)
            //{
            //    if (!LeisaiIO.CardInit())
            //    {
            //        MessageBox.Show("雷赛IO初始化失败！");
            //    }

            Thread ioDetectThread = new Thread(IODetect);
            ioDetectThread.IsBackground = true;
            ioDetectThread.Start();
            //}

        }
        /// <summary>
        /// 海康相机图像获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HikCamera_ImageTaked(object sender, HixDataTakedEventArgs e)
        {
            //自动运行状态
            if(CommonValue.IsStart)          
            {
                
              // / skt.Write("v1",true);                     
              //  bool res = (bool)skt.Read("v1");
                bool imgGetOK = e.IsDone;
                if (imgGetOK)
                {
                    Bitmap bitmap = e.Image;
                    if (indexOfImages < 2 * SysParams.PlanedImageNamber)
                    {
                        ImageUnit image = new ImageUnit();
                        image.Image = bitmap;
                        image.CameraID = e.CameraID;
                        if (e.CameraID == 0)
                        {
                            TempImages_Camera1.Add(image);
                            indexOfImages++;
                            Logging($"1#相机get one image ok ! Index  [{indexOfImages}]", Color.Green);
                        }
                        if (e.CameraID == 1)
                        {
                            TempImages_Camera2.Add(image);
                            indexOfImages2++;
                            Logging($"2#相机get one image ok ! Index  [{indexOfImages2}]", Color.Green);
                        }
                        if(CommonValue.IsManual)
                        {
                            string timeSeal = DateTime.Now.ToString(image.CameraID + 1 + "#相机_" + "yyyyMMdd_HH_mm_ss_ff");
                            string filePath = SysParams.OrginImagePath + timeSeal + ".bmp";
                            Invoke(new Action(() => frmImageViewing.SetImage_Manual(image, filePath)));
                        }
                    }
                    if (SysParams.IsShowCameraImage)
                    {
                        Invoke(new Action(() => frmImageViewing.SetImage(bitmap)));
                    }
                }
                else
                    Logging($"get image error !!!", Color.Red);
            }
        }

        /// <summary>
        /// 海康相机图像获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SVSCamera_ImageTaked(object sender, SVSDataTakedEventArgs e)
        {
            //自动运行状态
            if (CommonValue.IsStart)
            {

                // / skt.Write("v1",true);                     
                //  bool res = (bool)skt.Read("v1");
                bool imgGetOK = e.IsDone;
                if (imgGetOK)
                {
                    Bitmap bitmap = e.Image;
                    if (indexOfImages < 2 * SysParams.PlanedImageNamber)
                    {
                        ImageUnit image = new ImageUnit();
                        image.Image = bitmap;
                        image.CameraID = e.CameraID;
                        if (e.CameraID == 0)
                        {
                            if (newgroup)
                            {
                                newgroup = false;
                                return;
                            }
                            TempImages_Camera1.Add(image);
                            indexOfImages++;
                            if (indexOfImages == 12)
                                newgroup = true;
                            Logging($"1#相机get one image ok ! Index  [{indexOfImages}]", Color.Green);
                        }
                        if (e.CameraID == 1)
                        {
                            TempImages_Camera2.Add(image);
                            indexOfImages2++;
                            Logging($"2#相机get one image ok ! Index  [{indexOfImages2}]", Color.Green);
                        }
                        if (CommonValue.IsManual)
                        {
                            string timeSeal = DateTime.Now.ToString(image.CameraID + 1 + "#相机_" + "yyyyMMdd_HH_mm_ss_ff");
                            string filePath = SysParams.OrginImagePath + timeSeal + ".bmp";
                            Invoke(new Action(() => frmImageViewing.SetImage_Manual(image, filePath)));
                        }
                    }
                    if (SysParams.IsShowCameraImage)
                    {
                        Invoke(new Action(() => frmImageViewing.SetImage(bitmap)));
                    }
                }
                else
                    Logging($"get image error !!!", Color.Red);
            }
        }

        bool flag = true;
        bool flag2 = true;
        //todo 触发信号检测
        private void IODetect()
        {
            while (true)
            {
                GetAllInputSingal();
                //CommonValue.InitOK
                if (true)
                {
                    if (CommonValue.IsStart)
                    {
                        #region A站飞拍处理
                        //A站飞拍开始
                        if (CommonValue.A_Station_Start && (!CommonValue.A_Station_End))
                        {
                            if (flag)
                            {
                                Logging("收到A站开始飞拍信号！", Color.Green);
                                newgroup = true;
                                indexOfImages = 0;
                                indexOfImages2 = 0;
                                flag = false;
                                flag2 = true;
                                CommonValue.A_Station_Start_Feedback = true;
                                INIhelp1.SetValue(Path2D, "", "开始触发反馈", "1");
                                //SetOutput(IOSignalAdress.A_Station_Start_Feedback, true);
                                //INIhelp.SetValue(Path2D, "Config", "ID1", ID1);
                            }
                        }
                        //A站飞拍结束
                        if ((!CommonValue.A_Station_Start) && (CommonValue.A_Station_End))
                        {
                            if (flag2)
                            {
                                Logging("收到A站飞拍停止信号！", Color.Green);
                                flag = true;
                                flag2 = false;
                                CameraFly_Action(StationTypeConstant.StationA);
                                CommonValue.A_Station_End_Feedback = true;
                                INIhelp1.SetValue(Path2D, "", "结束触发反馈", "1");
                                //todo 0119
                                //SetOutput(IOSignalAdress.A_Station_End_Feedback, true);
                            }
                        }
                        #endregion

                        #region B站飞拍处理
                        //B站飞拍开始
                        if (CommonValue.B_Station_Start && (!CommonValue.B_Station_End))
                        {
                            if (flag)
                            {
                                Logging("收到B站开始飞拍信号！", Color.Green);
                                indexOfImages = 0;
                                indexOfImages2 = 0;
                                flag = false;
                                flag2 = true;
                                CommonValue.B_Station_Start_Feedback = true;
                                SetOutput(IOSignalAdress.B_Station_Start_Feedback, true);
                            }
                        }
                        //B站飞拍结束
                        if ((!CommonValue.B_Station_Start) && (CommonValue.B_Station_End))
                        {
                            if (flag2)
                            {
                                Logging("收到B站飞拍停止信号！", Color.Green);
                                flag = true;
                                flag2 = false;
                                CameraFly_Action(StationTypeConstant.StationB);
                                CommonValue.B_Station_End_Feedback = true;
                                SetOutput(IOSignalAdress.B_Station_End_Feedback, true);
                            }
                        }
                        #endregion
                    }
                }
                Thread.Sleep(10);
            }
        }
        ProductID ProductIDs = null;
        private void CameraFly_Action(StationTypeConstant stationType)
        {
            try
            {
                bool cameraRes1 = indexOfImages == 2 * SysParams.PlanedImageNamber;
                bool cameraRes2 = indexOfImages2 == 2 * SysParams.PlanedImageNamber2;
                //飞拍失败
                if (!(cameraRes1 && cameraRes2))
                {
                    //图片数量错误，发送采图数量错误
                    //后续注意信号复位
                    CommonValue.Camera1Ready = cameraRes1;
                    CommonValue.Camera2Ready = cameraRes2;
                    CommonValue.CameraFly1 = cameraRes1;
                    CommonValue.CameraFly2 = cameraRes2;
                    //todo 信号复位
                    //SetOutput(IOSignalAdress.Camera1Ready, cameraRes1);
                    //SetOutput(IOSignalAdress.Camera2Ready, cameraRes2);
                    //SetOutput(IOSignalAdress.CameraFly1, cameraRes1);
                    //SetOutput(IOSignalAdress.CameraFly2, cameraRes2);

                    if (!cameraRes1)
                        Logging(string.Format("1#相机飞拍图像数量错误,现收集数量为{0}；", indexOfImages), Color.Red);
                    if (!cameraRes2)
                        Logging(string.Format("2#相机飞拍图像数量错误,现收集数量为{0}；", indexOfImages2), Color.Red);
                    #region 生成路径
                    string strDay = DateTime.Now.ToString("yyyyMMdd");
                    string strTime = DateTime.Now.ToString("HH_mm_ss");
                    string fileDayPath = $@"{SysParams.ResultImagePath}\{strDay}";
                    string fileTimePath = $@"{fileDayPath}\{strTime}";
                    Actuator.CheckDirectory(fileDayPath);
                    string timeStamp = $"{strDay}#{strTime}";
                    #endregion

                    #region 保存原图
                    if (SysParams.IsSaveResultImage)
                    {
                        fileDayPath = $@"{SysParams.ResultImagePath}{strDay}";
                        fileTimePath = $@"{fileDayPath}\{strTime}";
                        Actuator.CheckDirectory(fileTimePath);
                        for (int i = 0; i < TempImages_Camera1.Count; i++)
                        {
                            string filePath = stationType == StationTypeConstant.StationA ? $@"{fileTimePath}\{i}.bmp" : $@"{fileTimePath}\{i}.bmp";
                            TempImages_Camera1[i].Image.Save(filePath);
                        }
                    }
                    #endregion
                    TempImages_Camera1.Clear();
                    TempImages_Camera2.Clear();
                    return;
                }
                //A站飞拍成功
                else
                {
                    //载具编号从Ini文件中读取*************************
                    //string iniPath = @"\\Desktop-kkskp45\共享文件夹\S2站与控制端交互\产品实时信息\";
                    //ProdutionParam stationA = ProdutionManager.GetInstance("", iniPath).GetInfo(ProdutionPathEnum.Staion_S2_A_2D);
                    //ProdutionParam stationB = ProdutionManager.GetInstance("", iniPath).GetInfo(ProdutionPathEnum.Staion_S2_B_2D);
                    //ProductIDs = new ProductID();
                    //ProductIDs.Carrier1 = stationA.ProdutionID2;
                    //ProductIDs.Carrier2 = stationA.ProdutionID1;
                    //ProductIDs.Carrier3 = stationB.ProdutionID1;
                    //ProductIDs.Carrier4 = stationB.ProdutionID2;
                    //*************************************************
                    //图片数量正确，发送采图数量正确
                    CommonValue.Camera1Ready = true;
                    CommonValue.Camera2Ready = true;
                    //todo 信号输出更新
                    //SetOutput(IOSignalAdress.Camera1Ready, true);
                    //SetOutput(IOSignalAdress.Camera2Ready, true);
                    #region 执行检测
                    Task.Run(() => this.Invoke(new Action(() =>
                    {
                        string dataSave = "";
                        CommonValue.CameraFly1 = true;
                        CommonValue.CameraFly2 = true;
                    //todo 信号更新
                    //SetOutput(IOSignalAdress.CameraFly1, true);
                    //SetOutput(IOSignalAdress.CameraFly2, true);
                    //2#载具图像
                    CurrentImages2.AddRange(TempImages_Camera1.GetRange(0, (int)(0.5 * SysParams.PlanedImageNamber)));
                        CurrentImages2.AddRange(TempImages_Camera1.GetRange((int)(1.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));
                    //1#载具图像
                    CurrentImages.AddRange(TempImages_Camera1.GetRange((int)(0.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));
                        CurrentImages.AddRange(TempImages_Camera1.GetRange(SysParams.PlanedImageNamber, (int)(0.5 * SysParams.PlanedImageNamber)));
                    //图像标定
                    CalibImages = new List<CogImage8Grey>(SysParams.PlanedImageNamber);
                        CalibImages2 = new List<CogImage8Grey>(SysParams.PlanedImageNamber);
                        try
                        {
                            for (int i = 0; i < CurrentImages.Count; i++)
                            {
                                if (stationType == StationTypeConstant.StationA)
                                {
                                    if (DicCalibTools.Values.Where(item => item.Id / 100 == 1).Where(item => item.Id % 100 == i).ToList().Count > 0)
                                    {
                                        HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 1).Where(item => item.Id % 100 == i).ToList()[0];
                                        CogImage8Grey outputImage = null;
                                        Actuator.RunCalib(hixCalibTool, CurrentImages[i].Image, out outputImage);
                                        CalibImages.Add(outputImage);
                                    }
                                    Logging($"1#载具标定图像[{i}]", Color.Magenta);
                                    if (DicCalibTools.Values.Where(item => item.Id / 100 == 2).Where(item => item.Id % 200 == i).ToList().Count > 0)
                                    {
                                        HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 2).Where(item => item.Id % 200 == i).ToList()[0];
                                        CogImage8Grey outputImage = null;
                                        Actuator.RunCalib(hixCalibTool, CurrentImages2[i].Image, out outputImage);
                                        CalibImages2.Add(outputImage);
                                    }
                                    Logging($"2#载具标定图像[{i}]", Color.Magenta);
                                }
                                if (stationType == StationTypeConstant.StationB)
                                {
                                    if (DicCalibTools.Values.Where(item => item.Id / 100 == 3).Where(item => item.Id % 300 == i).ToList().Count > 0)
                                    {
                                        HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 3).Where(item => item.Id % 300 == i).ToList()[0];
                                        CogImage8Grey outputImage = null;
                                        Actuator.RunCalib(hixCalibTool, CurrentImages2[i].Image, out outputImage);
                                        CalibImages.Add(outputImage);
                                    }
                                    Logging($"3#载具标定图像[{i}]", Color.Magenta);
                                    if (DicCalibTools.Values.Where(item => item.Id / 100 == 4).Where(item => item.Id % 400 == i).ToList().Count > 0)
                                    {
                                        HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 4).Where(item => item.Id % 400 == i).ToList()[0];
                                        CogImage8Grey outputImage = null;
                                        Actuator.RunCalib(hixCalibTool, CurrentImages[i].Image, out outputImage);
                                        CalibImages2.Add(outputImage);
                                    }
                                    Logging($"4#载具标定图像[{i}]", Color.Magenta);
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            string str = ex.Message;
                        }

                    #region 生成路径
                    string strDay = DateTime.Now.ToString("yyyyMMdd");
                        string strTime = DateTime.Now.ToString("HH_mm_ss");
                        string fileDayPath = $@"{SysParams.StitchImagePath}\{strDay}";
                        string fileTimePath = $@"{fileDayPath}\{strTime}";
                        Actuator.CheckDirectory(fileDayPath);
                        string timeStamp = $"{strDay}#{strTime}";
                    #endregion

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
                                    if (stationType == StationTypeConstant.StationA)
                                    {
                                        if (!res1) Logging($"1#载具图像拼接失败", Color.Magenta);
                                        if (!res2) Logging($"2#载具图像拼接失败", Color.Magenta);
                                    }
                                    if (stationType == StationTypeConstant.StationB)
                                    {
                                        if (!res1) Logging($"4#载具图像拼接失败", Color.Magenta);
                                        if (!res2) Logging($"3#载具图像拼接失败", Color.Magenta);
                                    }
                                }
                                break;
                            }
                        }
                        if (SysParams.IsSaveStitchImage)
                        {
                            Task.Run(() =>
                            {
                                if (stationType == StationTypeConstant.StationA)
                                {
                                    string filePath1 = $@"{fileTimePath}_1.bmp";
                                    string filePath2 = $@"{fileTimePath}_2.bmp";
                                    sImage.ToBitmap().Save(filePath1);
                                    sImage2.ToBitmap().Save(filePath2);
                                }
                                if (stationType == StationTypeConstant.StationB)
                                {
                                    string filePath1 = $@"{fileTimePath}_3.bmp";
                                    string filePath2 = $@"{fileTimePath}_4.bmp";
                                    sImage.ToBitmap().Save(filePath2);
                                    sImage2.ToBitmap().Save(filePath1);
                                }

                            });
                        }
                    #endregion

                    string taskName = "HIX50125_0928";
                        string frontMSG1 = "";
                        string frontMSG2 = "";
                        string dataMSG1 = "";
                        string dataMSG2 = "";
                        DicTasks[taskName].Image = sImage;
                        if (stationType == StationTypeConstant.StationA)
                        {
                            DicTasks[taskName].TaskIndex = 1;
                        }
                        else
                        {
                            DicTasks[taskName].TaskIndex = 3;
                        }
                        Actuator.RunTask(DicTasks[taskName], new string[] { "InputImage" }, new object[] { sImage });
                        Invoke(new Action(() => resultViewing.ShowGraphic(1, DicTasks[taskName].Record)));
                    #region 数据本地记录
                    //********************结果获取与处理************************************
                    productIndex = productIndex == Int32.MaxValue ? 0 : productIndex + 1;
                        //ProductInfo product1 = new ProductInfo();
                        //product1.CreateTime = DateTime.Now;
                        //product1.ID = ProductIDs.Carrier1;
                        //product1.Idx = productIndex;
                        //product1.ProjectName = taskName;
                        //product1.Measure2D = new List<MeasureItem>();

                        //if (stationType == StationTypeConstant.StationA)
                        //{
                        //    frontMSG1 = $"{timeStamp},{ProductIDs.Carrier1},1#载具,";
                        //    product1.StationName = "S2";
                        //    product1.LineName = "A";
                        //    product1.PartDir = "Right";
                        //}
                        //else
                        //{
                        //    frontMSG1 = $"{timeStamp},{ProductIDs.Carrier3},3#载具,";
                        //    product1.StationName = "S2";
                        //    product1.LineName = "B";
                        //    product1.PartDir = "Right";
                        //}
                        //frontMSG1 = $"{timeStamp},{ProductIDs.Carrier1},1#载具,";
                        frontMSG1 = $"{timeStamp},1#载具,";
                        string data1 = "";
                        if (DicTasks[taskName].Outputs.Count > 0)
                        {
                            foreach (Terminal t in DicTasks[taskName].Outputs)
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
                                //MeasureItem item = new MeasureItem()
                                //{
                                //    Name = t.Name,
                                //    Value = stationType == StationTypeConstant.StationA ? ((double)t.Value).ToString("f3") : ((double)t.Value3).ToString("f3"),
                                //    ExceptValue = t.ExceptValue.ToString("f3"),
                                //    TolMax = t.TolMax.ToString("f3"),
                                //    TolMin = t.TolMin.ToString("f3"),
                                //    Result = t.Result.ToString(),
                                //    FlawType = t.FlawType == 0 ? "未定义" : FlawTypeDictionary[t.FlawType],
                                //};
                                //if (!t.Result)
                                //{
                                //    product1.NGInfo = GetFlawInfo(product1.NGInfo, item.FlawType);
                                //    product1.OKNG = false;
                                //}
                                //product1.Measure2D.Add(item);
                                if (t.ValueType == typeof(double) ||
                                t.ValueType == typeof(int) ||
                                t.ValueType == typeof(string) ||
                                t.ValueType == typeof(bool))
                                {
                                    string valueRes = stationType == StationTypeConstant.StationA ? ((double)t.Value).ToString("f3") : ((double)t.Value3).ToString("f3");
                                    data1 += valueRes + ",";
                                }

                            }
                        }
                        dataMSG1 = frontMSG1 + data1;
                    //**********************************************************************
                    #endregion

                    DicTasks[taskName].Image = sImage2;
                        if (stationType == StationTypeConstant.StationA)
                        {
                            DicTasks[taskName].TaskIndex = 2;
                        }
                        else
                        {
                            DicTasks[taskName].TaskIndex = 4;
                        }
                        Actuator.RunTask(DicTasks[taskName], new string[] { "InputImage" }, new object[] { sImage2 });
                        Invoke(new Action(() => resultViewing.ShowGraphic(2, DicTasks[taskName].Record)));


                    #region  数据本地记录
                    //********************结果获取与处理************************************
                    productIndex = productIndex == Int32.MaxValue ? 0 : productIndex + 1;
                        //ProductInfo product2 = new ProductInfo();
                        //product2.CreateTime = DateTime.Now;
                        //product2.ID = ProductIDs.Carrier2;
                        //product2.Idx = productIndex;
                        //product2.ProjectName = taskName;
                        //product2.Measure2D = new List<MeasureItem>();

                        //if (stationType == StationTypeConstant.StationA)
                        //{
                        //    frontMSG2 = $"{timeStamp},{ProductIDs.Carrier2},2#载具,";
                        //    product2.StationName = "S2";
                        //    product2.LineName = "A";
                        //    product2.PartDir = "Left";
                        //}
                        //else
                        //{
                        //    frontMSG2 = $"{timeStamp},{ProductIDs.Carrier4},4#载具,";
                        //    product2.StationName = "S2";
                        //    product2.LineName = "B";
                        //    product2.PartDir = "Left";
                        //}
                        //frontMSG2 = $"{timeStamp},{ProductIDs.Carrier2},2#载具,";
                        frontMSG2 = $"{timeStamp},2#载具,";
                        string data2 = "";
                        if (DicTasks[taskName].Outputs.Count > 0)
                        {
                            foreach (Terminal t in DicTasks[taskName].Outputs)
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
                                //MeasureItem item = new MeasureItem()
                                //{
                                //    Name = t.Name,
                                //    Value = stationType == StationTypeConstant.StationA ? ((double)t.Value2).ToString("f3") : ((double)t.Value4).ToString("f3"),
                                //    ExceptValue = t.ExceptValue.ToString("f3"),
                                //    TolMax = t.TolMax.ToString("f3"),
                                //    TolMin = t.TolMin.ToString("f3"),
                                //    Result = t.Result.ToString(),
                                //    FlawType = t.FlawType == 0 ? "未定义" : FlawTypeDictionary[t.FlawType],
                                //};
                                //if (!t.Result)
                                //{
                                //    product2.NGInfo = GetFlawInfo(product2.NGInfo, item.FlawType);
                                //    product2.OKNG = false;
                                //}
                                //product2.Measure2D.Add(item);
                                if (t.ValueType == typeof(double) ||
                                t.ValueType == typeof(int) ||
                                t.ValueType == typeof(string) ||
                                t.ValueType == typeof(bool))
                                {
                                    string valueRes = stationType == StationTypeConstant.StationA ? ((double)t.Value2).ToString("f3") : ((double)t.Value4).ToString("f3");
                                    data2 += valueRes + ",";
                                }
                            }
                        }
                        dataMSG2 = frontMSG2 + data2;
                    //**********************************************************************
                    if (SysParams.IsDataRecordSave)
                        {
                            if (SysParams.IsSaveData1 && stationType == StationTypeConstant.StationA)
                            {
                                CsvHepler.WriteCSV(dataFilePaht, dataMSG1);
                            }
                            if (SysParams.IsSaveData2 && stationType == StationTypeConstant.StationA)
                            {
                                CsvHepler.WriteCSV(dataFilePaht, dataMSG2);
                            }
                            if (SysParams.IsSaveData3 && stationType == StationTypeConstant.StationB)
                            {
                                CsvHepler.WriteCSV(dataFilePaht, dataMSG1);
                            }
                            if (SysParams.IsSaveData4 && stationType == StationTypeConstant.StationB)
                            {
                                CsvHepler.WriteCSV(dataFilePaht, dataMSG2);
                            }
                        }
                        List<ProductInfo> infos = new List<ProductInfo>();
                        //infos.Add(product1);
                        //infos.Add(product2);
                        //mongoHelper.InsertMany<ProductInfo>(infos);
                    #endregion

                    #region 保存原图
                    if (SysParams.IsSaveOrginImage)
                        {
                            //Thread t = new Thread(() => {
                                fileDayPath = $@"{SysParams.OrginImagePath}{strDay}";
                                fileTimePath = $@"{fileDayPath}\{strTime}";
                                Actuator.CheckDirectory(fileTimePath);
                                for (int i = 0; i < CurrentImages.Count; i++)
                                {
                                    string filePath = stationType == StationTypeConstant.StationA ? $@"{fileTimePath}\1_{i}.bmp" : $@"{fileTimePath}\4_{i}.bmp";
                                    CurrentImages[i].Image.Save(filePath);
                                }
                                for (int i = 0; i < CurrentImages2.Count; i++)
                                {
                                    string filePath = stationType == StationTypeConstant.StationA ? $@"{fileTimePath}\2_{i}.bmp" : $@"{fileTimePath}\3_{i}.bmp";
                                    CurrentImages2[i].Image.Save(filePath);
                                }
                                //CurrentImages.Clear();
                                //CurrentImages2.Clear();
                            //});
                            //t.IsBackground = true;
                            //t.Start();
                            
                        }
                    #endregion

                    TempImages_Camera1.Clear();
                        TempImages_Camera2.Clear();
                        CalibImages.Clear();
                        CalibImages2.Clear();
                        CurrentImages.Clear();
                        CurrentImages2.Clear();
                        Logging($"#####任务完成，检测数量{productIndex}######", Color.Green);
                    })));
                    #endregion

                    indexOfImages = 0;
                    indexOfImages2 = 0;
                }
            }
            catch(Exception ex)
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
        private void GetAllInputSingal()
        {
            //手动模式不刷新状态
            if (!CommonValue.IsManual)
            {
                //CommonValue.A_Station_Start = GetInput(IOSignalAdress.A_Station_Start);
                //CommonValue.A_Station_End = GetInput(IOSignalAdress.A_Station_End);
                //CommonValue.B_Station_Start = GetInput(IOSignalAdress.B_Station_Start);
                //CommonValue.B_Station_End = GetInput(IOSignalAdress.B_Station_End);
                CommonValue.A_Station_Start = INIhelp1.GetValue(Path2D, "", "开始触发") =="1"?true:false;
                CommonValue.A_Station_End = INIhelp1.GetValue(Path2D, "", "结束触发") == "1" ? true : false;
            }

            //CommonValue.InitOK = GetOutput(IOSignalAdress.InitOK);
            //CommonValue.Camera1Ready = GetOutput(IOSignalAdress.Camera1Ready);
            //CommonValue.Camera2Ready = GetOutput(IOSignalAdress.Camera2Ready);
            //CommonValue.ResultType1 = GetOutput(IOSignalAdress.ResultType1);
            //CommonValue.ResultType2 = GetOutput(IOSignalAdress.ResultType2);
            //CommonValue.ResultType3 = GetOutput(IOSignalAdress.ResultType3);
            //CommonValue.ResultType4 = GetOutput(IOSignalAdress.ResultType4);
            //CommonValue.ResultType5 = GetOutput(IOSignalAdress.ResultType5);

            if (!CommonValue.A_Station_Start)
            {
                CommonValue.A_Station_Start_Feedback = false;
                INIhelp1.SetValue(Path2D,"", "开始触发反馈", "0");
                //SetOutput(IOSignalAdress.A_Station_Start_Feedback, false);
            }
            if (!CommonValue.A_Station_End)
            {
                CommonValue.A_Station_End_Feedback = false;
                INIhelp1.SetValue(Path2D, "", "结束触发反馈", "0");
                //SetOutput(IOSignalAdress.A_Station_End_Feedback, false);
            }
            //if (!CommonValue.B_Station_Start)
            //{
            //    CommonValue.B_Station_Start_Feedback = false;
            //    SetOutput(IOSignalAdress.B_Station_Start_Feedback, false);
            //}
            //if (!CommonValue.B_Station_End)
            //{
            //    CommonValue.B_Station_End_Feedback = false;
            //    SetOutput(IOSignalAdress.B_Station_End_Feedback, false);
            //}
        }
        private void TaskLoading(Action action)
        {
            Task.Run(() => Invoke(new Action(() =>
            {
                //Hide();
                Actuator.CheckDirectory(SysParams.LogPath);
                Actuator.CheckDirectory(SysParams.OrginImagePath);
                Actuator.CheckDirectory(SysParams.ResultImagePath);
                Actuator.CheckDirectory(SysParams.StitchImagePath);

                frmLoading = new FrmLoading();
                frmLoading.Show();
                frmLog.Show(dockPanel, DockState.DockBottom);
                CommonValue.InitOK = false;
                #region 配置相机
                foreach (var item in SysParams.DicCameraInfos)
                {
                    if (!DicCameras.ContainsKey(item.Key))
                    {
                        CameraRunner cameraRunner = new CameraRunner
                        {
                            Name = item.Key,
                            CameraFilePath = item.Value.FilePath,
                            Description = item.Value.Description
                        };
                        cameraRunner.ImageTaked += CameraRunner_ImageTaked;
                        DicCameras.Add(item.Key, cameraRunner);
                    }
                }
                //删除已删除的相机
                List<string> unexpectedCameraName = new List<string>();
                foreach (var item in DicCameras.Where(item => !SysParams.DicCameraInfos.ContainsKey(item.Key)))
                {
                    unexpectedCameraName.Add(item.Key);
                }
                foreach (var item in unexpectedCameraName)
                {
                    DicCameras.Remove(item);
                    GC.Collect();
                }
                #endregion

                #region 加载相机
                int i = 0;
                int count = DicCameras.Count;
                if (count == 0)
                    count = 1;
                int step = 20 / count;
                foreach (var item in DicCameras)
                {
                    i += step;
                    if (!item.Value.IsLoaded)
                    {
                        if (item.Value.LoadCameraFile())
                        {
                            Logging($"加载相机[{item.Key}]完成...", Color.Blue);
                            item.Value.IsLoaded = true;
                            item.Value.StartAcq();
                        }
                        else
                        {
                            Logging($"加载相机[{item.Key}]出错...", Color.Red);
                        }
                    }
                    SetProgressValue(i);
                }
                #endregion

                #region hik 相机初始化
                DicCameras.Clear();
                if (DicHikCameras.Count > 0)
                {
                    foreach (var item in DicHikCameras.Values)
                    {
                        if (item.BGrabbing)
                            item.Close();
                    }
                }
                DicHikCameras.Clear();
                DicSVSCameras.Clear();

                //svs相机初始化
                foreach (var item in SysParams.DicHikCameraInfos.Values)
                {
                    SVSCam svsCamera = new SVSCam
                    {
                        SN = item.SN,
                        Id = item.Id
                    };
                    if (svsCamera.InitializeCamera())
                    {
                        DicSVSCameras.Add(item.Name, svsCamera);
                        //svsCamera.TriggerMode = item.TriggerMode;
                        //hikCsvsCameraamera.Exposure = item.Exposure;
                        //hikCamera.Gain = item.Gain;
                        svsCamera.SVSImageTaked += SVSCamera_ImageTaked;
                        Logging($"相机[{svsCamera.CamName}]初始化连接成功！", Color.Green);
                    }
                    else
                    {
                        Logging($"相机[{svsCamera.CamName}]初始化连接失败！", Color.Red);
                    }
                }

                //海康相机初始化
                //foreach (var item in SysParams.DicHikCameraInfos.Values)
                //{
                //    HikCamera hikCamera = new HikCamera
                //    {
                //        InterfaceType = item.InterfaceType,
                //        SN = item.SN,
                //        Name = item.Name,
                //        Id = item.Id
                //    };
                //    if (hikCamera.InitializeCamera())
                //    {
                //        DicHikCameras.Add(item.Name, hikCamera);
                //        hikCamera.TriggerMode = item.TriggerMode;
                //        hikCamera.Exposure = item.Exposure;
                //        hikCamera.Gain = item.Gain;
                //        hikCamera.ImageTaked += HikCamera_ImageTaked;
                //        Logging($"相机[{hikCamera.Name}]初始化连接成功！", Color.Green);
                //    }
                //    else
                //    {
                //        Logging($"相机[{hikCamera.Name}]初始化连接失败！", Color.Red);
                //    }
                //}
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

                #region 加载任务
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
                        }
                        else
                        {
                            Logging("加载任务[" + item.Key + "]出错...", Color.Red);
                        }
                    }
                    SetProgressValue(i);
                }
                #endregion

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

                #region Mogo数据库
                mongoHelper.MongodbHelperSet(SysParams.DataBaseAddress, SysParams.DataBaseName);
                #endregion

                action();
                SetProgressValue(100); Logging("初始化完成...", Color.DarkSlateBlue);
                CommonValue.InitOK = true;
                CommonValue.Camera1Ready = true;
                CommonValue.Camera2Ready = true;
                CommonValue.CameraFly1 = true;
                CommonValue.CameraFly2 = true;
                if (!CommonValue.IsDebug)
                {
                    SetOutput(IOSignalAdress.InitOK, true);
                    SetOutput(IOSignalAdress.Camera1Ready, true);
                    SetOutput(IOSignalAdress.Camera2Ready, true);
                    SetOutput(IOSignalAdress.CameraFly1, true);
                    SetOutput(IOSignalAdress.CameraFly2, true);
                }
                frmLoading.Close();
                //Show();
            })));
        }
        private void UpdateUI()
        {
            dockPanel.Skin = Serialize.BinaryDeserialize<DockPanelSkin>("1.skin");

            CloseAllDockContent();
            frmLog.Show(dockPanel, DockState.DockBottom);
            frmImageViewing.Show(dockPanel);
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
            foreach (var item in SysParams.DicCameraInfos)
            {
                ToolStripMenuItem tsmi_Camera = new ToolStripMenuItem(item.Key, Properties.Resources.Camera);
                tsmi_Camera.Click += Tsmi_Camera_Click;
                CameraEditStripMenuItem.DropDownItems.Add(tsmi_Camera);
            }
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
        private bool GetInput(ushort index)
        {
            return LeisaiIO.ReadInIO(index);
        }
        private bool GetOutput(ushort index)
        {
            return LeisaiIO.ReadOutIO(index);
        }
        public void SetOutput(ushort index, bool value)
        {
            LeisaiIO.WriteIO(index, value);
        }

        #endregion

        #region 事件
        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitializeSystem();
        }

        

        //server 接收事件
        private void Server_TcpDateReceived(object sender, TcpDateEventArgs e)
        {
            Logging($"DateRecived [{e.Client.Client.RemoteEndPoint}] :{e.Message}", Color.DarkBlue);
        }

        private void Server_TcpDateSend(object sender, TcpDateEventArgs e)
        {
            Logging($"DateSend [{e.Client.Client.RemoteEndPoint}] :{e.Message}", Color.Tomato);
        }

        private void Server_TcpDisConnected(object sender, TcpDateEventArgs e)
        {
            Logging($"{e.Message}", Color.Red);
        }

        private void Server_TcpConnected(object sender, TcpDateEventArgs e)
        {
            Logging($"{e.Message}", Color.DarkOliveGreen);
        }
        FrmResultsView resView = null;
        private void Tsmi_TaskResult_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            resView = DicTasks[tsmi.Text].GetTaskFrmResults();
            resView.ShowRatio = SysParams.IsShowRatio;
            DockContent frmTaskResultsView = resView;
            frmTaskResultsView.Show(dockPanel, DockState.DockLeft);
            if (!SysParams.ListResultView.Contains(tsmi.Text))
                SysParams.ListResultView.Add(tsmi.Text);
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
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            DockContent frmCameraEdit = DicCameras[tsmi.Text].GetFrmCamreaEdit();
            frmCameraEdit.Show(dockPanel);
        }
        private void VisionProTask_RunCompleted(object sender, RunningCompletedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                //Server.Send(e.Outputs[0].Value.ToString());
                //DicTasks[e.Name].GetTaskFrmResults().Subject = e.Outputs;
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
                        //image.CameraID = e.CameraID;
                        //if (e.CameraID == 0)
                        //{
                        //    TempImages_Camera1.Add(image);
                        //    indexOfImages++;
                        //    Logging($"1#相机get one image ok ! Index  [{indexOfImages}]", Color.Green);
                        //}
                        //if (e.CameraID == 1)
                        //{
                        //    TempImages_Camera2.Add(image);
                        //    indexOfImages2++;
                        //    Logging($"2#相机get one image ok ! Index  [{indexOfImages2}]", Color.Green);
                        //}
                        if (CommonValue.IsManual)
                        {
                            string timeSeal = DateTime.Now.ToString(image.CameraID + 1 + "#相机_" + "yyyyMMdd_HH_mm_ss_ff");
                            string filePath = SysParams.OrginImagePath + timeSeal + ".bmp";
                            Invoke(new Action(() => frmImageViewing.SetImage_Manual(image, filePath)));
                        }
                    }
                    //if (SysParams.IsShowCameraImage)
                    Invoke(new Action(() => frmImageViewing.SetImage(bitmap)));
                }
                else
                    Logging($"get image error !!!", Color.Red);
            }
        }
        private void ParametersWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParameters.Show(dockPanel, DockState.DockRight);
        }

        private void TsbtnRun_Click(object sender, EventArgs e)
        {
            CommonValue.IsStart = !CommonValue.IsStart;
            Logging(string.Format("######切换到{0}状态#######", this.tsbtnRun.Text), Color.Red);
            this.tsbtnRun.Image = CommonValue.IsStart?
                global::Hix_CCD_Module.Properties.Resources.IcoStop:
                global::Hix_CCD_Module.Properties.Resources.IcoRun;
            this.tsbtnRun.Text = CommonValue.IsStart ? "停止" : "运行";
            //Thread t = new Thread(() => { AcceptSignal(); });
            //t.IsBackground = true;
            //t.Start();
         
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
            resView.ShowRatio = SysParams.IsShowRatio;
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
            foreach (var item in DicHikCameras.Values)
            {
                item.Close();
            }
            HikCameraOperator.CloseDevices();

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
            //HikCameraOperator.CloseDevices();
            //FrmHikVisionCameraDemo frmHikVisionCameraDemo = new FrmHikVisionCameraDemo();
            //frmHikVisionCameraDemo.Show();
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
            FrmCalibration frmCalibration = new FrmCalibration();
            frmCalibration.ShowDialog();
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(!CommonValue.IsManual) Logging("######切换到手动模式#######", Color.Red);
            if (CommonValue.IsManual) Logging("######取消手动模式#######", Color.Red);

            CommonValue.IsManual = !CommonValue.IsManual;

            this.tsBtnManual.BackColor = CommonValue.IsManual ? Color.Gray : SystemColors.ButtonHighlight;

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            CommonValue.A_Station_Start = true;
            CommonValue.A_Station_End = false;
            //skt.Write("A_Station_Start", true);
            //skt.Write("A_Station_End", false);
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CommonValue.A_Station_Start = false;
            CommonValue.A_Station_End = true;
            //skt.Write("A_Station_Start", false);
            //skt.Write("A_Station_End", true);

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CommonValue.B_Station_Start = true;
            CommonValue.B_Station_End = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CommonValue.B_Station_Start = false;
            CommonValue.B_Station_End = true;
        }

        private void resultsWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void taskEditToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void displayWindoowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            TempImages_Camera1.Clear();
            TempImages_Camera2.Clear();
            indexOfImages = 0;
            indexOfImages2 = 0;
            CommonValue.Camera1Ready = true;
            CommonValue.Camera2Ready = true;
            //后续注意信号复位问题
            SetOutput(IOSignalAdress.Camera1Ready, true);
            SetOutput(IOSignalAdress.Camera2Ready, true);
        }

        private void 复检ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDebug form = new FrmDebug();
            form.ShowDialog();
        }


        #region *****************backup
        //private void IODetect()
        //{
        //    while (true)
        //    {
        //        GetAllInputSingal();
        //        if (CommonValue.InitOK)
        //        {
        //            if (CommonValue.IsStart)
        //            {

        //                #region A站飞拍处理
        //                //A站飞拍开始
        //                if (CommonValue.A_Station_Start && (!CommonValue.A_Station_End))
        //                {
        //                    //TODO：ini中获取载具编号及产品编号

        //                    //************************************
        //                    if (flag)
        //                    {
        //                        indexOfImages = 0;
        //                        indexOfImages2 = 0;
        //                        SetOutput(IOSignalAdress.A_Station_Start_Feedback, true);
        //                        flag = false;
        //                        flag2 = true;
        //                    }
        //                }
        //                //A站飞拍结束
        //                if ((!CommonValue.A_Station_Start) && (CommonValue.A_Station_End))
        //                {
        //                    if (flag2)
        //                    {
        //                        flag = true;
        //                        flag2 = false;
        //                        bool cameraRes1 = indexOfImages == 2 * SysParams.PlanedImageNamber;
        //                        bool cameraRes2 = indexOfImages2 == 2 * SysParams.PlanedImageNamber2;
        //                        //飞拍失败
        //                        if (!(cameraRes1 && cameraRes2))
        //                        {
        //                            //图片数量错误，发送采图数量错误
        //                            //后续注意信号复位
        //                            CommonValue.Camera1Ready = cameraRes1;
        //                            CommonValue.Camera2Ready = cameraRes2;
        //                            //TODO:后续注意信号复位问题
        //                            SetOutput(IOSignalAdress.Camera1Ready, cameraRes1);
        //                            SetOutput(IOSignalAdress.Camera2Ready, cameraRes2);
        //                            TempImages_Camera1.Clear();
        //                            TempImages_Camera2.Clear();
        //                            if (!cameraRes1)
        //                                Logging(string.Format("1#相机飞拍图像数量错误,现收集数量为{0}；", indexOfImages), Color.Red);
        //                            if (!cameraRes2)
        //                                Logging(string.Format("2#相机飞拍图像数量错误,现收集数量为{0}；", indexOfImages2), Color.Red);
        //                            SetOutput(IOSignalAdress.A_Station_End_Feedback, true);
        //                        }
        //                        //A站飞拍成功
        //                        else
        //                        {
        //                            //载具编号从Ini文件中读取*************************
        //                            int carrierId = 2;
        //                            int productId = 889;
        //                            int currentWorkstation = 1;
        //                            //*************************************************
        //                            //图片数量正确，发送采图数量正确
        //                            CommonValue.Camera1Ready = true;
        //                            CommonValue.Camera2Ready = true;
        //                            SetOutput(IOSignalAdress.Camera1Ready, true);
        //                            SetOutput(IOSignalAdress.Camera2Ready, true);
        //                            //do action ...
        //                            #region action example
        //                            Task.Run(() => this.Invoke(new Action(() =>
        //                            {
        //                                string dataSave = "";
        //                                List<ImageUnit> temp1 = Actuator.DeepCopyByBin(TempImages_Camera1);
        //                                List<ImageUnit> temp2 = Actuator.DeepCopyByBin(TempImages_Camera2);
        //                                TempImages_Camera1.Clear();
        //                                TempImages_Camera2.Clear();
        //                                //TempImages.Clear();
        //                                //TempImages2.Clear();
        //                                //TempImages3.Clear();
        //                                //TempImages4.Clear();
        //                                //2#载具图像
        //                                CurrentImages2.AddRange(temp1.GetRange(0, (int)(0.5 * SysParams.PlanedImageNamber)));
        //                                CurrentImages2.AddRange(temp1.GetRange(SysParams.PlanedImageNamber, (int)(0.5 * SysParams.PlanedImageNamber)));

        //                                //1#载具图像
        //                                CurrentImages.AddRange(temp1.GetRange((int)(0.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));
        //                                CurrentImages.AddRange(temp1.GetRange((int)(1.5 * SysParams.PlanedImageNamber), (int)(0.5 * SysParams.PlanedImageNamber)));
        //                                //图像标定
        //                                CalibImages = new List<CogImage8Grey>(SysParams.PlanedImageNamber);
        //                                CalibImages2 = new List<CogImage8Grey>(SysParams.PlanedImageNamber);
        //                                for (int i = 0; i < CurrentImages.Count; i++)
        //                                {
        //                                    if (DicCalibTools.Values.Where(item => item.Id / 100 == 1).Where(item => item.Id % 100 == i).ToList().Count > 0)
        //                                    {
        //                                        HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 1).Where(item => item.Id % 100 == i).ToList()[0];
        //                                        CogImage8Grey outputImage = null;
        //                                        Actuator.RunCalib(hixCalibTool, CurrentImages[i].Image, out outputImage);
        //                                        CalibImages.Add(outputImage);
        //                                    }
        //                                    Logging($"1#载具标定图像[{i}]", Color.Magenta);
        //                                }
        //                                for (int i = 0; i < CurrentImages2.Count; i++)
        //                                {

        //                                    if (DicCalibTools.Values.Where(item => item.Id / 100 == 2).Where(item => item.Id % 200 == i).ToList().Count > 0)
        //                                    {
        //                                        HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == 2).Where(item => item.Id % 200 == i).ToList()[0];
        //                                        CogImage8Grey outputImage = null;
        //                                        Actuator.RunCalib(hixCalibTool, CurrentImages2[i].Image, out outputImage);
        //                                        CalibImages2.Add(outputImage);
        //                                    }
        //                                    Logging($"2#载具标定图像[{i}]", Color.Magenta);
        //                                }

        //                                #region 生成路径
        //                                string strDay = DateTime.Now.ToString("yyyyMMdd");
        //                                string strTime = DateTime.Now.ToString("HH_mm_ss");
        //                                string fileDayPath = $@"{SysParams.StitchImagePath}\{strDay}";
        //                                string fileTimePath = $@"{fileDayPath}\{strTime}";
        //                                Actuator.CheckDirectory(fileDayPath);
        //                                string timeStamp = $"{strDay}#{strTime}";
        //                                #endregion

        //                                #region 图像拼接
        //                                CogImage8Grey sImage = null;
        //                                CogImage8Grey sImage2 = null;
        //                                Logging($"拼接", Color.Magenta);
        //                                bool res1 = Actuator.ImageStitching(CalibImages, SysParams.UnfilledPelValue, out sImage);
        //                                bool res2 = Actuator.ImageStitching(CalibImages2, SysParams.UnfilledPelValue, out sImage2);
        //                                if (res1 && res2)
        //                                {
        //                                    Logging($"拼接完成", Color.Magenta);
        //                                }
        //                                else
        //                                {
        //                                    if (!res1) Logging($"1#载具图像拼接失败", Color.Magenta);
        //                                    if (!res2) Logging($"2#载具图像拼接失败", Color.Magenta);
        //                                }
        //                                if (SysParams.IsSaveStitchImage)
        //                                {
        //                                    Task.Run(() =>
        //                                    {
        //                                        string filePath1 = $@"{fileTimePath}_1.bmp";
        //                                        string filePath2 = $@"{fileTimePath}_2.bmp";
        //                                        sImage.ToBitmap().Save(filePath1);
        //                                        sImage2.ToBitmap().Save(filePath2);
        //                                    });
        //                                }
        //                                //if (SysParams.IsShowStitchImage)
        //                                //    Invoke(new Action(() => resultViewing.ShowImage(sImage, sImage2)));
        //                                #endregion

        //                                DicTasks["HIX50125_0928"].Image = sImage;
        //                                Actuator.RunTask(DicTasks["HIX50125_0928"], new string[] { "InputImage" }, new object[] { sImage });
        //                                Invoke(new Action(() => resultViewing.ShowGraphic(1, DicTasks["HIX50125_0928"].Record)));
        //                                //********************结果获取与处理************************************
        //                                string frontMSG = $"{timeStamp},{productId},{carrierId},";

        //                                //**********************************************************************

        //                                DicTasks["HIX50125_0928"].Image = sImage2;
        //                                Actuator.RunTask(DicTasks["HIX50125_0928"], new string[] { "InputImage" }, new object[] { sImage2 });
        //                                Invoke(new Action(() => resultViewing.ShowGraphic(2, DicTasks["HIX50125_0928"].Record)));
        //                                //********************结果获取与处理************************************
        //                                // resultViewing.ShowImage(sImage, sImage2);

        //                                //**********************************************************************



        //                                #region 保存原图
        //                                if (SysParams.IsSaveOrginImage)
        //                                {
        //                                    fileDayPath = $@"{SysParams.OrginImagePath}\{strDay}";
        //                                    fileTimePath = $@"{fileDayPath}\{strTime}";
        //                                    Actuator.CheckDirectory(fileTimePath);
        //                                    for (int i = 0; i < CurrentImages.Count; i++)
        //                                    {
        //                                        string filePath = $@"{fileTimePath}_1\{i}.bmp";
        //                                        CurrentImages[i].Image.Save(filePath);
        //                                    }
        //                                    for (int i = 0; i < CurrentImages2.Count; i++)
        //                                    {
        //                                        string filePath = $@"{fileTimePath}_2\{i}.bmp";
        //                                        CurrentImages2[i].Image.Save(filePath);
        //                                    }
        //                                }
        //                                #endregion

        //                                CalibImages.Clear();
        //                                CalibImages2.Clear();
        //                                CurrentImages.Clear();
        //                                CurrentImages2.Clear();
        //                            })));
        //                            #endregion

        //                            indexOfImages = 0;
        //                            indexOfImages2 = 0;
        //                            SetOutput(IOSignalAdress.A_Station_End_Feedback, true);
        //                        }
        //                    }
        //                }
        //                #endregion

        //                #region B站飞拍处理
        //                //B站飞拍开始
        //                if (CommonValue.B_Station_Start && (!CommonValue.B_Station_End))
        //                {
        //                    //TODO：ini中获取载具编号及产品编号

        //                    //************************************
        //                    indexOfImages = 0;
        //                    indexOfImages2 = 0;
        //                    SetOutput(IOSignalAdress.B_Station_Start_Feedback, true);
        //                }
        //                //B站飞拍结束
        //                if ((!CommonValue.A_Station_Start) && (CommonValue.A_Station_End))
        //                {
        //                    //飞拍结束处理逻辑同A站 
        //                    SetOutput(IOSignalAdress.B_Station_End_Feedback, true);
        //                }
        //                #endregion

        //            }
        //            if (CommonValue.IsManual)
        //            {
        //            }
        //        }
        //        Thread.Sleep(10);
        //    }
        //}

        #endregion

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
            CheckDataSave();
            CheckHardDiskFreeSpace();
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
        
        //public void AcceptSignal()
        //{
        //    while(true)
        //    {
        //      CommonValue.A_Station_Start =  (bool)skt.Read("A_Station_Start");
        //        CommonValue.A_Station_End = (bool)skt.Read("A_Station_End");
        //        if(CommonValue.A_Station_Start == true && CommonValue.A_Station_End == false)
        //        {
                    
        //        }
        //        else if(CommonValue.A_Station_Start == true && CommonValue.A_Station_End == true)
        //            Thread.Sleep(10);
        //    }
        //}
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

}
