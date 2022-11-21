using Hix_CCD_Module.HixEventArgs;
using Hix_CCD_Module.Setting;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Hix_CCD_Module.Setting;

namespace Hix_CCD_Module.Tool
{
    public class HikCamera
    {
        public event HixDataTakedEventHandler ImageTaked;
        public static MyCamera.cbOutputdelegate ImageCallback;
        public static MyCamera.cbOutputExdelegate ImageCallback_Ex;

        public string Name { get; set; } = string.Empty;
        public HikCameraInfo CameraInfo { get; set; }
        private MyCamera Camera { get; set; } = new MyCamera();
        private TriggerMode triggerMode = TriggerMode.Off;
        private Bitmap image = null;
        private double runTime = 0;
        object obj = new object();
        public double RunTime
        {
            get
            {
                double rt = runTime;
                runTime = 0;
                return rt;
            }
        }
        public int ImageNumber
        {
            get;
            set;
        } = 1;

        public InterfaceType InterfaceType { get; set; } = InterfaceType.GigE;
        public TriggerMode TriggerMode
        {
            get
            {
                return triggerMode;
            }
            set
            {
                triggerMode = value;
                switch (value)
                {
                    case TriggerMode.On:
                        Camera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                        // ch:触发源设为软触发 | en:Set trigger source as Software
                        Camera.MV_CC_SetEnumValue_NET("TriggerSource", 1);
                        //StartExternalTrigger();
                        break;
                    case TriggerMode.Off:
                        // ch:触发源设为外触发
                        Camera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                        break;
                }
            }
        }

        public bool TriggerOnce()
        {
            Camera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
            if (TriggerMode == TriggerMode.Off)
            {
                int nRet;
                //ch: 触发命令 | en:Trigger command
                nRet = Camera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (MyCamera.MV_OK == nRet)
                {
                    return true;
                }
                return false;
            }
            else
            {
                int nRet;
                TriggerMode = TriggerMode.Off;
                //ch: 触发命令 | en:Trigger command
                nRet = Camera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                TriggerMode = TriggerMode.On;
                if (MyCamera.MV_OK == nRet)
                {
                    return true;
                }
                return false;
            }
        }

        [Obsolete]
        public Bitmap GetOneImage()
        {
            try
            {
                TriggerMode = TriggerMode.Off;
                //TriggerOnce();
                return GetImage();
            }
            catch
            {
                return null;
            }
        }

        //取多张图片
        [Obsolete]
        public void StartExternalTrigger()
        {
            Task.Run(() =>
            {
                while (TriggerMode == TriggerMode.On)
                {
                    image = GetImage();
                    HixDataTakedEventArgs e = new HixDataTakedEventArgs
                    {
                        CameraName = Name,
                        IsDone = true,
                        Image = image
                    };
                    ImageTaked?.Invoke(this, e);
                }
            });
        }

        //发送数据包大小
        public uint GevSCPSPacketSize
        {
            get
            {
                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                int nRet = Camera.MV_GIGE_GetGevSCPSPacketSize_NET(ref stParam);
                if (MyCamera.MV_OK == nRet)
                {
                    return stParam.nCurValue;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                int nRet = Camera.MV_GIGE_SetGevSCPSPacketSize_NET(value);
                if (nRet != MyCamera.MV_OK)
                {
                    
                }
            }
        }

        /// <summary>
        /// 发送延时
        /// </summary>
        public uint GevSCPDDelay
        {
            get
            {
                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                int nRet = Camera.MV_GIGE_GetGevSCPD_NET(ref stParam);
                if (MyCamera.MV_OK == nRet)
                {
                    return stParam.nCurValue;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                int nRet = Camera.MV_GIGE_SetGevSCPD_NET(value);
                if (nRet != MyCamera.MV_OK)
                {

                }
            }
        }

        /// <summary>
        /// 发送防抖
        /// </summary>
        public uint LineDebouncerTime
        {
            get
            {
                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                int nRet = Camera.MV_CC_GetIntValue_NET("LineDebouncerTime", ref stParam);
                if (MyCamera.MV_OK == nRet)
                {
                    return stParam.nCurValue;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                int nRet = Camera.MV_CC_SetIntValue_NET("LineDebouncerTime",value);
                if (nRet != MyCamera.MV_OK)
                {

                }
            }
        }


        public double Exposure
        {
            get
            {
                MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
                int nRet = Camera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
                if (MyCamera.MV_OK == nRet)
                {
                    return stParam.fCurValue;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Camera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
                int nRet = Camera.MV_CC_SetFloatValue_NET("ExposureTime", (float)value);
                if (nRet != MyCamera.MV_OK)
                {

                }
            }
        }
        public double Gain
        {
            get
            {
                MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
                int nRet = Camera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
                if (MyCamera.MV_OK == nRet)
                {
                    return stParam.fCurValue;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Camera.MV_CC_SetEnumValue_NET("GainAuto", 0);
                int nRet = Camera.MV_CC_SetFloatValue_NET("Gain", (float)value);
                if (nRet != MyCamera.MV_OK)
                {

                }
            }
        }
        public string SN { get; set; } = "00E60221820";
        public string Description { get; set; }
        public int Id { get; set; }
        public bool BGrabbing { get; private set; } = false;
        public Bitmap Image
        {
            get
            {
                Bitmap bitmap = (Bitmap)image.Clone();
                image = null;
                return bitmap;
            }
            private set
            {
                image = value;
            }
        }



        public static T DeepCopyByBin<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        [Obsolete]
        private Bitmap GetImage()
        {
            Bitmap bmp = null;
            while (bmp == null)
            {
                // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
                uint m_nBufSizeForDriver = 3072 * 2048 * 3;
                byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];

                // ch:用于保存图像的缓存 | en:Buffer for saving image
                uint m_nBufSizeForSaveImage = 3072 * 2048 * 3 * 3 + 2048;
                byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];
                int nRet;
                uint nPayloadSize = 0;
                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                nRet = Camera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);//一帧数据的大小
                if (MyCamera.MV_OK != nRet)
                {
                    bmp = null;
                    continue;
                }
                nPayloadSize = stParam.nCurValue;
                if (nPayloadSize > m_nBufSizeForDriver)
                {
                    m_nBufSizeForDriver = nPayloadSize;
                    m_pBufForDriver = new byte[m_nBufSizeForDriver];

                    // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                    // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                    m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                    m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
                }

                IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);

                MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
                DateTime start = DateTime.Now;
                switch (TriggerMode)
                {
                    case TriggerMode.On:
                        nRet = Camera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, int.MaxValue);
                        break;
                    case TriggerMode.Off:
                        TriggerOnce();
                        // ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec
                        nRet = Camera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, 1000);
                        break;
                    default:
                        break;
                }
                DateTime end = DateTime.Now;
                TimeSpan span = end - start;
                runTime = span.TotalMilliseconds;

                if (MyCamera.MV_OK != nRet)
                {
                    bmp = null;
                    continue;
                }

                MyCamera.MvGvspPixelType enDstPixelType;
                if (IsMonoData(stFrameInfo.enPixelType))
                {
                    enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                }
                else if (IsColorData(stFrameInfo.enPixelType))
                {
                    enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                }
                else
                {
                    bmp = null;
                    continue;
                }

                IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
                MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
                MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
                stConverPixelParam.nWidth = stFrameInfo.nWidth;
                stConverPixelParam.nHeight = stFrameInfo.nHeight;
                stConverPixelParam.pSrcData = pData;
                stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
                stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
                stConverPixelParam.enDstPixelType = enDstPixelType;
                stConverPixelParam.pDstBuffer = pImage;
                stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
                nRet = Camera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
                if (MyCamera.MV_OK != nRet)
                {
                    bmp = null;
                    continue;
                }

                if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    //************************Mono8 转 Bitmap*******************************
                    bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

                    ColorPalette cp = bmp.Palette;
                    // init palette
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    // set palette back
                    bmp.Palette = cp;
                }
                else
                {
                    //*********************RGB8 转 Bitmap**************************
                    for (int i = 0; i < stFrameInfo.nHeight; i++)
                    {
                        for (int j = 0; j < stFrameInfo.nWidth; j++)
                        {
                            byte chRed = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3];
                            m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2];
                            m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2] = chRed;
                        }
                    }
                    try
                    {
                        bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pImage);
                        //bmp.Save(path, ImageFormat.Bmp);
                    }
                    catch
                    {
                        bmp = null;
                        continue;
                    }

                }
                //Marshal.Release(pData);
                //Marshal.Release(pImage);
            }
            return bmp;
        }
        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        UInt32 m_nBufSizeForDriver = 3072 * 2048 * 3;
        byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];

        // ch:用于保存图像的缓存 | en:Buffer for saving image
        UInt32 m_nBufSizeForSaveImage = 3072 * 2048 * 3 * 3 + 2048;
        byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];
        private Bitmap GetImage2()
        {
            int nRet;
            Bitmap bmp33 = null;
            UInt32 nPayloadSize = 0;
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = Camera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
            }
            nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

            // ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec
            nRet = Camera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, 1000);
            if (MyCamera.MV_OK != nRet)
            {
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
            stSaveParam.enPixelType = stFrameInfo.enPixelType;
            stSaveParam.pData = pData;
            stSaveParam.nDataLen = stFrameInfo.nFrameLen;
            stSaveParam.nHeight = stFrameInfo.nHeight;
            stSaveParam.nWidth = stFrameInfo.nWidth;
            stSaveParam.pImageBuffer = pImage;
            stSaveParam.nBufferSize = m_nBufSizeForSaveImage;
            stSaveParam.nJpgQuality = 80;
            nRet = Camera.MV_CC_SaveImageEx_NET(ref stSaveParam);
            if (MyCamera.MV_OK != nRet)
            {
            }

            try
            {
                MemoryStream stream = new MemoryStream(m_pBufForSaveImage);
                bmp33 = new Bitmap(stream);
                bmp33.Save("1111111111.bmp");
                stream.Close();
                stream.Dispose();
                //FileStream file = new FileStream("image.jpg", FileMode.Create, FileAccess.Write);
                //file.Write(m_pBufForSaveImage, 0, (int)stSaveParam.nImageLen);
                //file.Close();
            }
            catch(Exception ex)
            {
            }
            return bmp33;
        }
        public HikCamera()
        {
            TriggerMode = TriggerMode.Off;
        }
        ~HikCamera()
        {
            if (BGrabbing)
                Camera.MV_CC_StopGrabbing_NET();
        }

        public bool InitializeCamera()
        {
            try
            {
                HikCameraOperator.DeviceListAcq();
                switch (InterfaceType)
                {
                    case InterfaceType.GigE:
                        Camera = HikCameraOperator.GigECameras.Where(item => item.SN == SN).ToList()[0].MyCamera;
                        break;
                    case InterfaceType.USB3:
                        Camera = HikCameraOperator.USB3Cameras.Where(item => item.SN == SN).ToList()[0].MyCamera;
                        break;
                    default:
                        break;
                }
                //打开相机
                int nRet = Camera.MV_CC_OpenDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    Camera.MV_CC_DestroyDevice_NET();
                    System.Windows.Forms.MessageBox.Show($"Device open fail!, [{nRet}]");
                    return false;
                }
                // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
                if (true)
                {
                    int nPacketSize = Camera.MV_CC_GetOptimalPacketSize_NET();
                    if (nPacketSize > 0)
                    {
                        nRet = Camera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                        if (nRet != MyCamera.MV_OK)
                        {
                            Console.WriteLine("Warning: Set Packet Size failed {0:x8}", nRet);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Warning: Get Packet Size failed {0:x8}", nPacketSize);
                    }
                }
                // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
                Camera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);// ch:工作在连续模式 | en:Acquisition On Continuous Mode

                //注册回调函数
                //ImageCallback = new MyCamera.cbOutputdelegate(ImageCallbackFunc);
                //nRet = Camera.MV_CC_RegisterImageCallBack_NET(ImageCallback, IntPtr.Zero);
                ImageCallback_Ex = new MyCamera.cbOutputExdelegate(ImageCallbackFunc_Ex);
                nRet = Camera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback_Ex, IntPtr.Zero);
                if (MyCamera.MV_OK != nRet)
                {
                    System.Windows.Forms.MessageBox.Show($"Register image callback failed!, [{nRet}]");
                    return false;
                }
                //开始抓图
                nRet = Camera.MV_CC_StartGrabbing_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    Camera.MV_CC_DestroyDevice_NET();
                    System.Windows.Forms.MessageBox.Show($"Trigger Fail!, [{nRet}]");
                    return false;
                }

                BGrabbing = true;
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Close()
        {
            int nRet = Camera.MV_CC_StopGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                System.Windows.Forms.MessageBox.Show($"Stop Device [ SN={SN} ] Fail！\n [ErrorCode={nRet}]");
                return false;
            }
            BGrabbing = false;
            nRet = Camera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                System.Windows.Forms.MessageBox.Show($"Close Device [ SN={SN} ] Fail！\n [ErrorCode={nRet}]");
                return false;
            }
            return true;
        }
        private void ImageCallbackFunc(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser)
        {
            MyCamera.MV_FRAME_OUT_INFO frameInfo = pFrameInfo;

            Task.Run(() =>
            {
                lock (obj)
                {
                    ImageGet(pData, frameInfo);
                }
            });
        }
        private void ImageCallbackFunc_Ex(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            MyCamera.MV_FRAME_OUT_INFO_EX frameInfo = pFrameInfo;

            Task.Run(() =>
            {
                lock (obj)
                {
                    ImageGet2(pData, frameInfo);
                }
            });
        }

        private int SetExposure()
        {
            Camera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            int nRet = Camera.MV_CC_SetFloatValue_NET("ExposureTime", (float)Exposure);
            if (nRet != MyCamera.MV_OK)
            {

            }
            return nRet;
        }

        private int SetGain()
        {
            Camera.MV_CC_SetEnumValue_NET("GainAuto", 0);
            int nRet = Camera.MV_CC_SetFloatValue_NET("Gain", (float)Gain);
            if (nRet != MyCamera.MV_OK)
            {

            }

            return nRet;
        }
        private Bitmap GetImage(IntPtr pData, MyCamera.MV_FRAME_OUT_INFO stFrameInfo)
        {
            Bitmap bmp = null;
            {
                int nRet;
                // ch:用于保存图像的缓存 | en:Buffer for saving image
                uint m_nBufSizeForSaveImage = 3072 * 2048 * 3 * 3 + 2048;
                byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];

                if ((3 * stFrameInfo.nFrameLen + 2048) > m_nBufSizeForSaveImage)
                {
                    m_nBufSizeForSaveImage = 3 * stFrameInfo.nFrameLen + 2048;
                    m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
                }

                IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
                MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();


                //stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
                //stSaveParam.enPixelType = stFrameInfo.enPixelType;
                //stSaveParam.pData = pData;
                //stSaveParam.nDataLen = stFrameInfo.nFrameLen;
                //stSaveParam.nHeight = stFrameInfo.nHeight;
                //stSaveParam.nWidth = stFrameInfo.nWidth;

                //stSaveParam.pImageBuffer = pImage;
                //stSaveParam.nBufferSize = m_nBufSizeForSaveImage;
                //stSaveParam.nJpgQuality = 80;
                //nRet = Camera.MV_CC_SaveImageEx_NET(ref stSaveParam);
                //if (MyCamera.MV_OK == nRet)
                //{
                //    using (MemoryStream stream = new MemoryStream(m_pBufForSaveImage))
                //    {
                //        bmp = new Bitmap(stream);
                //        stream.Flush();
                //        stream.Close();
                //        stream.Dispose();
                //    }
                //    ColorPalette cp = bmp.Palette;
                //    // init palette
                //    //for (int i = 0; i < 256; i++)
                //    //{
                //    //    cp.Entries[i] = Color.FromArgb(i, i, i);
                //    //}
                //    // set palette back
                //    //bmp.Palette = cp;
                //}
                try
                {
                    MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
                    stConverPixelParam.nWidth = stFrameInfo.nWidth;
                    stConverPixelParam.nHeight = stFrameInfo.nHeight;
                    stConverPixelParam.pSrcData = pData;
                    stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
                    stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
                    stConverPixelParam.enDstPixelType = stFrameInfo.enPixelType;
                    stConverPixelParam.pDstBuffer = pImage;
                    stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
                    nRet = Camera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);

                    stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
                    stSaveParam.enPixelType = stFrameInfo.enPixelType;
                    stSaveParam.pData = pData;
                    stSaveParam.nDataLen = stFrameInfo.nFrameLen;
                    stSaveParam.nHeight = stFrameInfo.nHeight;
                    stSaveParam.nWidth = stFrameInfo.nWidth;

                    stSaveParam.pImageBuffer = pImage;
                    stSaveParam.nBufferSize = m_nBufSizeForSaveImage;
                    stSaveParam.nJpgQuality = 80;
                    nRet = Camera.MV_CC_SaveImageEx_NET(ref stSaveParam);
                    if (MyCamera.MV_OK == nRet)
                    {
                        //using (MemoryStream stream = new MemoryStream(m_pBufForSaveImage))
                        //{
                        //    bmp = new Bitmap(stream);
                        //    stream.Flush();
                        //    stream.Close();
                        //    stream.Dispose();
                        //}
                        //ColorPalette cp = bmp.Palette;
                        //// init palette
                        //for (int i = 0; i < 256; i++)
                        //{
                        //    cp.Entries[i] = Color.FromArgb(i, i, i);
                        //}
                        //// set palette back
                        //bmp.Palette = cp;
                    }
                    if (stConverPixelParam.enSrcPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                    {
                        //************************Mono8 转 Bitmap*******************************
                        bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

                        ColorPalette cp = bmp.Palette;
                        // init palette
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        // set palette back
                        bmp.Palette = cp;

                        //bmp.Save("image.bmp", ImageFormat.Bmp);
                    }
                }
                catch(Exception ex)
                {
                    string str = ex.Message;
                }
            }
            GC.Collect();
            return bmp;
        }
        private Bitmap GetImage(IntPtr pData, MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo)
        {
            Bitmap bmp = null;
            {
                int nRet;
                // ch:用于保存图像的缓存 | en:Buffer for saving image
                uint m_nBufSizeForSaveImage = 4896 * 3264 * 3 + 2048;
                byte[] m_pBufForSaveImage = new byte[4896 * 3264 * 3 + 2048];

                if ((3 * stFrameInfo.nFrameLen + 2048) > m_nBufSizeForSaveImage)
                {
                    m_nBufSizeForSaveImage = 3 * stFrameInfo.nFrameLen + 2048;
                    m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
                }

                IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
                MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
     
                try
                {
                    MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
                    stConverPixelParam.nWidth = stFrameInfo.nWidth;
                    stConverPixelParam.nHeight = stFrameInfo.nHeight;
                    stConverPixelParam.pSrcData = pData;
                    stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
                    stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
                    stConverPixelParam.enDstPixelType = stFrameInfo.enPixelType;
                    stConverPixelParam.pDstBuffer = pImage;
                    stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
                    nRet = Camera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);

                    //if (MyCamera.MV_OK == nRet)
                    //{
             
                    //}
                    if (stConverPixelParam.enSrcPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                    {
                        //************************Mono8 转 Bitmap*******************************
                        Bitmap bmp2 = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);
                        MemoryStream stream = new MemoryStream();
                        bmp2.Save(stream, ImageFormat.Bmp);
                        bmp = new Bitmap(stream);
                        ColorPalette cp = bmp.Palette;
                        // init palette
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        // set palette back
                        bmp.Palette = cp;

   
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
            GC.Collect();
            return bmp;
        }
        private bool IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }
        private void ImageGet(IntPtr pData, MyCamera.MV_FRAME_OUT_INFO pFrameInfo)
        {
            Bitmap image2 = GetImage(pData, pFrameInfo);
            //Bitmap image2 = GetImage2();
            bool imgOk = image2 != null;
            HixDataTakedEventArgs e = new HixDataTakedEventArgs
            {
                CameraName = Name,
                CameraID = Id,
                IsDone = imgOk,
                //Image = (Bitmap)image2.Clone()
                Image = image2
            };
            ImageTaked?.Invoke(this, e);
        }
        private void ImageGet2(IntPtr pData, MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo)
        {
            Bitmap image2 = GetImage(pData, pFrameInfo);
            //Bitmap image2 = GetImage2();
            bool imgOk = image2 != null;
            HixDataTakedEventArgs e = new HixDataTakedEventArgs
            {
                CameraName = Name,
                CameraID = Id,
                IsDone = imgOk,
                //Image = (Bitmap)image2.Clone()
                Image = image2
            };
            ImageTaked?.Invoke(this, e);
        }
        /************************************************************************
         *  @fn     IsColorData()
         *  @brief  判断是否是彩色数据
         *  @param  enGvspPixelType         [IN]           像素格式
         *  @return 成功，返回0；错误，返回-1 
         ************************************************************************/
        private bool IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:
                    return true;

                default:
                    return false;
            }
        }
    }
}
