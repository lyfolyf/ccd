using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hix_CCD_Module.Tool
{
    public static class HikCameraOperator
    {
        static MyCamera.MV_CC_DEVICE_INFO_LIST deviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        private static List<HikGigECamera> listGigECameras = new List<HikGigECamera>();
        private static List<HikUSB3Camera> listUSB3Cameras = new List<HikUSB3Camera>();

        public static MyCamera.MV_CC_DEVICE_INFO_LIST DeviceList { get { return deviceList; } }
        public static List<HikGigECamera> GigECameras { get { return listGigECameras; } }

        public static List<HikUSB3Camera> USB3Cameras { get { return listUSB3Cameras; } }

        public static void DeviceListAcq()
        {
            int nRet;
            // ch:创建设备列表 en:Create Device List
            System.GC.Collect();
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref deviceList);
            if (0 != nRet)
            {
                //ShowErrorMsg("Enumerate devices fail!", 0);
                return;
            }
            listGigECameras.Clear();
            listUSB3Cameras.Clear();
            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < deviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    //创建相机句柄
                    MyCamera myCamera = new MyCamera();

                    nRet = myCamera.MV_CC_CreateDevice_NET(ref device);
                    if (MyCamera.MV_OK != nRet)
                    {

                    }
                    else
                    {
                        listGigECameras.Add(new HikGigECamera(myCamera, gigeInfo));
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                    //创建相机句柄
                    MyCamera myCamera = new MyCamera();

                    nRet = myCamera.MV_CC_CreateDevice_NET(ref device);
                    if (MyCamera.MV_OK != nRet)
                    {

                    }
                    else
                    {
                        listUSB3Cameras.Add(new HikUSB3Camera(myCamera, usbInfo));
                    }

                }
            }

        }

        public static void CloseDevices()
        {
            // ch:关闭设备 | en:Close Device
            int nRet;

            foreach (var item in GigECameras)
            {
                nRet = item.MyCamera.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    System.Windows.Forms.MessageBox.Show($"Destroy Device [ SN={item.SN} ] Fail！\n [ErrorCode={nRet}]");
                    continue;
                }
            }
            foreach (var item in USB3Cameras)
            {
                nRet = item.MyCamera.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    System.Windows.Forms.MessageBox.Show($"Destroy Device [ SN={item.SN} ] Fail！\n [ErrorCode={nRet}]");
                    continue;
                }
            }
        }

    }

    public class HikGigECamera
    {
        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = new MyCamera.MV_GIGE_DEVICE_INFO();
        MyCamera myCamera = new MyCamera();

        public HikGigECamera(MyCamera camera, MyCamera.MV_GIGE_DEVICE_INFO info)
        {
            myCamera = camera;
            gigeInfo = info;
        }
        public string SN
        {
            get
            {
                return GigeInfo.chSerialNumber;
            }
        }

        public MyCamera MyCamera { get { return myCamera; } }

        public MyCamera.MV_GIGE_DEVICE_INFO GigeInfo { get { return gigeInfo; } }
    }

    public class HikUSB3Camera
    {
        MyCamera.MV_USB3_DEVICE_INFO usb3Info = new MyCamera.MV_USB3_DEVICE_INFO();
        MyCamera myCamera = new MyCamera();

        public HikUSB3Camera(MyCamera camera, MyCamera.MV_USB3_DEVICE_INFO info)
        {
            myCamera = camera;
            usb3Info = info;
        }
        public string SN
        {
            get
            {
                return USB3Info.chSerialNumber;
            }
        }

        public MyCamera MyCamera { get { return myCamera; } }

        public MyCamera.MV_USB3_DEVICE_INFO USB3Info { get { return usb3Info; } }
    }
}
