using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Bing.IVisionTool;
using Bing.Serialization;
using Hix_CCD_Module.Tool;

namespace Hix_CCD_Module.Setting
{
    [Serializable]
    public class Parameters
    {
        public Parameters()
        {

        }

        #region System Path

        //[Description("插件加载文件夹")]
        //[Category("系统路径"), DisplayName("插件文件夹")]
        //[Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        //[Browsable(true)]
        //public string AlgorithmPluginsPath { set; get; } = Environment.CurrentDirectory + @"\plugins\";

        //[Description("加载视觉算法插件路径")]
        //[Category("系统路径"), DisplayName("插件路径")]
        //[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        //[Browsable(true)]
        //public string AlgorithmFilePath { set; get; } = Environment.CurrentDirectory + @"\plugins\Vision Pro Robot Guidance.dll";

        //[Description("视觉任务加载路径")]
        //[Category("系统路径"), DisplayName("任务路径")]
        //[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        //public string TaskFilePath { set; get; } = Environment.CurrentDirectory + @"\Task\Vision Pro Nine Hole L.vpp";

        //[Description("相机文件加载路径")]
        //[Category("系统路径"), DisplayName("相机路径")]
        //[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        //public string CameraFilePath { set; get; } = Environment.CurrentDirectory + @"\Fifo.vpp";

        [Description("日志保存路径")]
        [Category("系统路径"), DisplayName("日志路径")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string LogPath { get; set; } = Environment.CurrentDirectory + @"\SystemLog\";

        [Description("原始图片保存路径")]
        [Category("系统路径"), DisplayName("原始图片路径")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string OrginImagePath { get; set; } = Environment.CurrentDirectory + @"\OrginImage\";

        [Description("结果图片保存路径")]
        [Category("系统路径"), DisplayName("结果图片路径")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string ResultImagePath { get; set; } = Environment.CurrentDirectory + @"\ResultImage\";

        [Description("拼接图片保存路径")]
        [Category("系统路径"), DisplayName("拼接图片路径")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string StitchImagePath { get; set; } = Environment.CurrentDirectory + @"\StitchImage\";

        [Description("缓存图片保存路径")]
        [Category("系统路径"), DisplayName("缓存图片路径")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        [Browsable(false)]
        public string TempImagePath { get; set; } = Environment.CurrentDirectory + @"\TempImage\";
        #endregion

        #region Server
        [Description("服务器ip")]
        [Category("通讯"), DisplayName("IP地址")]
        public string ServerIP { get; set; }

        [Browsable(false)]   //默认 ：false，控制m_Count 参数能否在控件上显示
        [Category("通讯")]
        public IPAddress IPAddress
        {
            get
            {
                if (ServerIP == null || ServerIP == string.Empty)
                {
                    return IPAddress.Any;
                }
                else
                {
                    return IPAddress.Parse(ServerIP);
                }
            }
        }

        [Description("服务器端口号")]
        [Category("通讯"), DisplayName("端口")]
        public int ServerPort { get; set; } = 56668;
        [Category("数据库"), DisplayName("地址")]
        public string DataBaseAddress { get; set; } = "mongodb://localhost:27017";
        [Category("数据库"), DisplayName("表单名称")]
        public string DataBaseName { get; set; } = "Lead";

        #endregion

        #region Orders
        [Description("接收到该指令，系统开始接受采集图片")]
        [Category("指令"), DisplayName("采集开始")]
        public string StartOrder { get; set; } = "start";

        [Description("接收到该指令，系统停止接受采集图片")]
        [Category("指令"), DisplayName("采集结束")]
        public string EndOrder { get; set; } = "end";

        [Description("采集图片数量与规划数量一致，返回该条指令代码")]
        [Category("指令"), DisplayName("采图数量正确")]
        public string ImageNumCorrentOrder { get; set; } = "ok";

        [Description("采集图片数量与规划数量不同，返回该条指令代码")]
        [Category("指令"), DisplayName("采图数量出错")]
        public string ImageNumErrorOrder { get; set; } = "error";

        [Browsable(false)]
        public Dictionary<string, OrderExecutionInfo> DicOrderExecutionInfos { get; set; }
            = new Dictionary<string, OrderExecutionInfo>();
        //[Category("指令")]
        //public string RunTaskOrder { get; set; } = "G";

        //[Category("指令")]
        //public string SaveOrginImageOrder { get; set; } = "SO";

        //[Category("指令")]
        //public string SaveResultImageOrder { get; set; } = "SR";

        #endregion

        #region 图片
        //[Category("图片设置")]
        //public int CutAreaStartX { get; set; } = 0;
        //[Category("图片设置")]
        //public int CutAreaStartY { get; set; } = 0;
        //[Category("图片设置")]
        //public int CutAreaHeight { get; set; } = 0;
        //[Category("图片设置")]
        //public int CutAreaWidth { get; set; } = 0;
        [Category("图片保存"), DisplayName("保存拼接图")]
        public bool IsSaveStitchImage { get; set; } = false;

        [Category("图片保存"), DisplayName("保存原图")]
        public bool IsSaveOrginImage { get; set; } = false;
        [Category("图片保存"), DisplayName("保存结果图")]
        public bool IsSaveResultImage { get; set; } = false;
        [Category("图片显示"), DisplayName("开启/关闭相机取像显示")]
        public bool IsShowCameraImage { get; set; } = false;
        [Category("图片显示"), DisplayName("开启/关闭拼接图像显示")]
        public bool IsShowStitchImage { get; set; } = false;
        
        #endregion

        #region 规划
        [Category("规划"), DisplayName("1#相机采图数量")]
        public int PlanedImageNamber { get; set; } = 8;

        //[Category("规划"), DisplayName("2#相机采图数量")]
        //public int PlanedImageNamber2 { get; set; } = 0;

        [Category("规划"), DisplayName("图片拼接填充灰度")]
        public int UnfilledPelValue { get; set; } = 128;
        [Category("规划"), DisplayName("是否显示补偿系数")]
        public bool IsShowRatio { get; set; } = true;
        [Category("调试"), DisplayName("参数1")]
        public int Mode { get; set; } = 0;
        [Category("调试"), DisplayName("参数2")]
        public double rate { get; set; } = 200;
        #endregion

        #region 数据保存
        [Category("数据保存"), DisplayName("存放路径")]
        public string DataSavePath { get; set; } = "DataRecord";
        [Category("数据保存"),DisplayName("数据表眉头")]
        public string DataHeadContent { get; set; } = "长度,宽度";
        [Category("数据保存"),DisplayName("是否保存数据")]
        public bool IsDataRecordSave { get; set; } = true;
        [Category("数据保存"), DisplayName("开启/关闭1#产品数据保存")]
        public bool IsSaveData1 { get; set; } = false;
        [Category("数据保存"), DisplayName("开启/关闭2#产品数据保存")]
        public bool IsSaveData2 { get; set; } = false;

        [Category("调试"), DisplayName("标定模式")]
        public bool IsSaveData3 { get; set; } = false;

        [Category("数据保存"), DisplayName("参数显示")]
        public bool IsSaveData4 { get; set; } = false;

        [Category("数据保存"), DisplayName("数据存放盘符")]
        public string DataSaveHardDisk { get; set; } = "D";
        [Category("数据保存"), DisplayName("数据存放天数")]
        public int DataSaveDays { get; set; } = 3;
        [Category("数据保存"), DisplayName("硬盘剩余空间提示(GB)")]
        public int FreeHardDisk { get; set; } = 20;

        #endregion

        #region 任务
        [Browsable(false)]
        public Dictionary<string, TaskInfo> DicTaskInfos { get; set; } = new Dictionary<string, TaskInfo>();//name,info
        [Browsable(false)]
        public Dictionary<string, CameraInfo> DicCameraInfos { get; set; } = new Dictionary<string, CameraInfo>();//name,info
        [Browsable(false)]
        public Dictionary<string, HikCameraInfo> DicHikCameraInfos { get; set; } = new Dictionary<string, HikCameraInfo>();//name,info
        [Browsable(false)]
        public Dictionary<string, CalibInfo> DicCalibInfos { get; set; } = new Dictionary<string, CalibInfo>();//name,info
        #endregion

        #region UI
        [Browsable(false)]
        public List<string> ListDisplayView { get; set; } = new List<string>();
        [Browsable(false)]
        public List<string> ListResultView { get; set; } = new List<string>();
        [Browsable(false)]
        public List<string> ListTaskEditView { get; set; } = new List<string>();
        #endregion

        #region

        #endregion
        public static Parameters LoadParametersFromFile()
        {
            if (!File.Exists(Environment.CurrentDirectory + @"\Settings.par"))
            {
                SaveParametersToFile(new Parameters());
            }
            return Serialize.BinaryDeserialize<Parameters>(Environment.CurrentDirectory + @"\Settings.par");
        }
        public static void SaveParametersToFile(Parameters parameters)
        {
            Serialize.BinarySerialize(Environment.CurrentDirectory + @"\Settings.par", parameters);
        }
        public void SaveToFile()
        {
            Serialize.BinarySerialize(Environment.CurrentDirectory + @"\Settings.par", this);
        }

    }

    [Serializable]
    public class TaskInfo
    {
        public string Name { get; set; }
        [Editor(typeof(PropertyGridRichText), typeof(UITypeEditor))]
        public string Description { get; set; }
        public string FilePath { get; set; } = $"{Environment.CurrentDirectory}\\Task\\{new Random().Next(0, 10000)}.vpp";
        public TaskInfo() { }
        public TaskInfo(string name, string description, string filepath)
        {
            Name = name;
            Description = description;
            FilePath = filepath;
        }

    }

    [Serializable]
    public class CalibInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Editor(typeof(PropertyGridRichText), typeof(UITypeEditor))]
        public string Description { get; set; }
        public string FilePath { get; set; } = $"{Environment.CurrentDirectory}\\Calib\\{new Random().Next(0, 10000)}.vpp";
        public CalibInfo() { }
        public CalibInfo(string name, int id, string filepath)
        {
            Name = name;
            Id = id;
            FilePath = filepath;
        }
    }

    [Obsolete]
    [Serializable]
    public class CameraInfo
    {
        public string Name { get; set; }
        [Editor(typeof(PropertyGridRichText), typeof(UITypeEditor))]
        public string Description { get; set; }
        public string FilePath { get; set; } = $"{Environment.CurrentDirectory}\\Camera\\{new Random().Next(0, 10000)}.vpp";
        public CameraInfo() { }
        public CameraInfo(string name, string description, string filepath)
        {
            Name = name;
            Description = description;
            FilePath = filepath;
        }

    }

    [Serializable]
    public class HikCameraInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Editor(typeof(PropertyGridRichText), typeof(UITypeEditor))]
        public string Description { get; set; }
        [ReadOnly(true)]
        public string SN { get; set; } = "00234736204";
        public double Exposure { get; set; } = 500;
        public double Gain { get; set; } = 0;
        [ReadOnly(true)]
        public InterfaceType InterfaceType { get; set; } = InterfaceType.USB3;
        public TriggerMode TriggerMode { get; set; } = TriggerMode.On;
        public HikCameraInfo() { }
        public HikCameraInfo(string name, int id, string sn)
        {
            Name = name;
            Id = id;
            SN = sn;
        }
    }

    [Serializable]
    public class OrderExecutionInfo
    {
        public List<string> Cameras { get; set; } = new List<string>();

        public List<string> Tasks { get; set; } = new List<string>();

        public List<ImageFlow> ImageFlows { get; set; } = new List<ImageFlow>();

        public List<DataFlow> DataFlows { get; set; } = new List<DataFlow>();
    }

    [Serializable]
    public class ImageFlow
    {
        public int ID { get; set; }
        public double Exprosure { get; set; }
        public double Gain { get; set; }
        public int CameraId { get; set; }
        public string CameraName { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string InputImageName { get; set; }
    }
    [Serializable]
    public class DataFlow
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string InputName { get; set; }
        public object InputValue { get; set; }
    }

    [Serializable]
    public enum InterfaceType
    {
        GigE,
        USB3
    }
    [Serializable]
    public enum TriggerMode
    {
        On,
        Off
    }
}