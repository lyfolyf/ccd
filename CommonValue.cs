using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mark.DigitalIO;

namespace Hix_CCD_Module
{
    public class CommonValue
    {
        public static bool IsStart = false;             //启动模式
        public static bool IsManual = false;            //手动模式
        public static bool IsDebug = false;             //调试模式

        public static bool A_Station_Start = false;         //A站飞拍开始
        public static bool A_Station_End = false;           //A站飞拍结束
        public static bool B_Station_Start = false;         //B站飞拍开始
        public static bool B_Station_End = false;           //B站飞拍结束

        public static bool InitOK = false;
        public static bool Camera1Ready = false;        //相机1准备OK

        public static bool ResultType;                  //结果类型
        public static bool ResultType1 = false;         //结果类型1
        public static bool ResultType2 = false;         //结果类型2
        public static bool ResultType3 = false;         //结果类型3
        public static bool ResultType4 = false;         //结果类型4
        public static bool ResultType5 = false;         //结果类型5

        public static string Camera1ProductID;          //1#相机当前拍摄产品序列号
        public static int Camera1CarrierID;             //1#相机当前拍摄对应载具编号1~4

        public static string Camera2ProductID;          //2#相机当前拍摄产品序列号
        public static int Camera2CarrierID;             //3#相机当前拍摄对应载具编号1~4

        public static bool A_Station_Start_Feedback = false;         //A站飞拍开始
        public static bool A_Station_End_Feedback = false;           //A站飞拍结束
        public static bool B_Station_Start_Feedback = false;         //B站飞拍开始
        public static bool B_Station_End_Feedback = false;           //B站飞拍结束

        public static bool CameraFly1 = false;                  //相机1飞拍结果信号




        public static bool isCheck = false;
        public static int iCheckCount = 0;             // 
        public static double dWHValue = 0;             // 
        public static string strCheckPath = "";
        public static bool isCheckToday = false;//当日点检是否通过
        public static string dtCheckTime = DateTime.Now.ToString("yyyy-MM-dd");
        //public static string IDCode1 = "";
        //public static string IDCode2 = "";
        public static string CamSerialNum = "";

        public static uint SPCD = 0;
        public static double ExposeTime = 0;
        public static double Gain = 0;
        public static uint LineDebouncerTime = 0;

        public static List<string> ListTitle = new List<string>();  //导入真值  用于结果判断
        public static List<double> ListRealValue = new List<double>();  //导入真值  用于结果判断
        public static List<double> ListUpValue = new List<double>(); //真值上公差
        public static List<double> ListDownValue = new List<double>(); //真值下公差
        public static string TaskName = "";

    }

    /// <summary>
    /// 交互信号信息地址
    /// </summary>
    public class IOSignalAdress
    {
        //输入信号
        public const int A_Station_Start = 1;       //A站飞拍开始
        public const int A_Station_End = 2;         //A站飞拍结束
        public const int B_Station_Start = 3;       
        public const int B_Station_End = 4;

        //输出信号
        public const int InitOK = 5;            //视觉初始化OK
        public const int Camera1Ready = 6;      //相机1准备OK
        public const int Camera2Ready = 7;      //相机2准备OK

        public const int ResultType1 = 8;       //结果类型1
        public const int ResultType2 = 9;       //结果类型2
        public const int ResultType3 = 10;       //结果类型3
        public const int ResultType4 = 11;       //结果类型4
        public const int ResultType5 = 12;       //结果类型5

        public const int A_Station_Start_Feedback = 1;         //1#相机飞拍开始信号反馈
        public const int A_Station_End_Feedback = 2;          //1#相机飞拍结束信号反馈
        public const int B_Station_Start_Feedback = 3;        //2#相机飞拍开始信号反馈
        public const int B_Station_End_Feedback = 4;          //2#相机飞拍结束信号反馈

        public const int CameraFly1 = 13;                       //相机1飞拍结果信号
        public const int CameraFly2 = 14;                       //相机2飞拍结果信号
    }
    public enum StationTypeConstant
    {
        StationA = 1,
        StationB = 2,
    }
    public enum CarrierConstant
    {
        StationA_2 = 1,                     //工站A——1#载具
        StationA_1 = 2,                     //工站A——2#载具
        StationB_1 = 3,                     //工站B——1#载具
        StationB_2 = 4,                     //工站B——2#载具
    }
    /// <summary>
    /// 产品ID
    /// </summary>
    public class ProductID
    {
        public string Carrier1;       //A工站载具1对应产品编号
        public string Carrier2;       //A工站载具2对应产品编号
        public string Carrier3;       //B工站载具1对应产品编号
        public string Carrier4;       //B工站载具2对应产品编号
        //public int StationA_Carrier1;       //A工站载具1对应产品编号
        //public int StationA_Carrier2;       //A工站载具2对应产品编号
        //public int StationB_Carrier1;       //B工站载具1对应产品编号
        //public int StationB_Carrier2;       //B工站载具2对应产品编号
    }
}
