using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace Hix_CCD_Module.Tool
{
    public class SVSDataTakedEventArgs : EventArgs
    {
        public bool IsDone { get; set; } = false;
        public string CameraName { get; set; } = string.Empty;
        public Bitmap Image { get; set; }
        public int CameraID { get; set; }
    }

    public class SVSCam
    {
        public delegate void SvsDataTakedEventHandler(object sender, SVSDataTakedEventArgs e);

        public event SvsDataTakedEventHandler SVSImageTaked;
        private GigeApi.StreamCallback GigeCallBack;

        public GigeApi myApi;
        public string SN { get; set; } = "62613";
        public string Description { get; set; }
        public int Id { get; set; }
        public bool BGrabbing { get; private set; } = false;
        //返回值
        private GigeApi.SVSGigeApiReturn errorflag;
        private int hCameraContainer;
        //相机数量
        private int cam_Num;
        //宽度，长度
        private int camWidth = 0, camHeight = 0;
        //相机句柄
        private IntPtr hCamera;
        //相机名称
        public String CamName;
        //steaming channel
        private System.IntPtr hStreamingChannel;
        //相机连接
        private bool cameraConnected = false;
        //相机连接超时
        float timeout = 3.0f;//1500.0f; 

        private bool GigeIsInit;
        private int running = 0;

        private struct imagebufferStruct
        {
            public byte[] imagebytes;

        };
        private imagebufferStruct[] imagebuffer;
        private Bitmap[] bimage;
        private imagebufferStruct[] outputbuffer;
        private ColorPalette imgpal = null;
        private const int bufferCount = 24;
        private int bufferWriteind;
        private int bufferReadind;
        private int PacketSize = 0;
        public SVSCam()
        {
        
            bimage = new Bitmap[bufferCount];
        }




        public void CloseSvsCam()
        {
            try
            {
                GigeApi.SVSGigeApiReturn mSVSGigeApiReturn = myApi.Gige_Camera_closeConnection(hStreamingChannel);
 
            }
            catch (Exception ex)
            {

                
            }
        }



        public bool InitializeCamera()
        {
            try
            {
                myApi = new GigeApi();

 

                if (hCameraContainer != GigeApi.SVGigE_NO_CLIENT)
                {
                    myApi.Gige_CameraContainer_delete(hCameraContainer);
                }
                hCameraContainer = myApi.Gige_CameraContainer_create(GigeApi.SVGigETL_Type.SVGigETL_TypeFilter);
                errorflag = myApi.Gige_CameraContainer_discovery(hCameraContainer);

                if (errorflag == GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    //获取相机数量
                    cam_Num = myApi.Gige_CameraContainer_getNumberOfCameras(hCameraContainer);
                    if (cam_Num > 0)
                    {
                        for (int n = 0; n < cam_Num; n++)
                        {
                            hCamera = myApi.Gige_CameraContainer_getCamera(hCameraContainer, n);
                            if (SN == myApi.Gige_Camera_getSerialNumber(hCamera))
                            {
                                CamName = myApi.Gige_Camera_getModelName(hCamera);
                                //Open connection to camera. A control channel will be established.
                                errorflag = myApi.Gige_Camera_openConnection(hCamera, timeout);
                                if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                                {
                                    Console.WriteLine("Errorflag: " + errorflag.ToString());
                                    cameraConnected = false;
                                    return cameraConnected;
                                }
                                break;
                            }
                        }
                    }
                }
                

                // Adjust camera to maximal possible packet size
                myApi.Gige_Camera_evaluateMaximalPacketSize(hCamera, ref (PacketSize));

                // Adjust desired binning mode (default = OFF, others: 2x2, 3x3, 4x4)
                myApi.Gige_Camera_setBinningMode(hCamera, GigeApi.BINNING_MODE.BINNING_MODE_OFF);

                // Create a StreamCallback
                GigeCallBack = new GigeApi.StreamCallback(this.myStreamCallback);
                // Create a matching streaming channel
                errorflag = myApi.Gige_StreamingChannel_create(ref hStreamingChannel, hCameraContainer, hCamera, 24, GigeCallBack, new IntPtr(0));
                if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    Console.WriteLine("Errorflag: " + errorflag.ToString());
                    cameraConnected = false;
                    return cameraConnected;
                }
                cameraConnected = true;
                errorflag = myApi.Gige_Camera_getSizeX(hCamera, ref (camWidth));

                if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    Console.WriteLine("Errorflag: " + errorflag.ToString());
                    cameraConnected = false;
                    return cameraConnected;
                }
                errorflag = myApi.Gige_Camera_getSizeY(hCamera, ref (camHeight));
                if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    Console.WriteLine("Errorflag: " + errorflag.ToString());
                    cameraConnected = false;
                    return cameraConnected;
                }

                // allocate memory for buffers
                this.initializeBuffer();

                // set camera-mode to grab

                //errorflag = myApi.Gige_Camera_setExposureTime(hCamera, (float)50);
                //if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                //{
                //    Console.WriteLine("Errorflag: " + errorflag.ToString());
                //    cameraConnected = false;
                //    return cameraConnected;
                //}
                //errorflag = myApi.Gige_Camera_setGain(hCamera, (float)18);
                //if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                //{
                //    Console.WriteLine("Errorflag: " + errorflag.ToString());
                //    cameraConnected = false;
                //    return cameraConnected;
                //}
                //errorflag = myApi.Gige_Camera_readEEPROM(hCamera);
                //if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                //{
                //    Console.WriteLine("Errorflag: " + errorflag.ToString());
                //    cameraConnected = false;
                //    return cameraConnected;
                //}

               errorflag = myApi.Gige_Camera_setGigECameraRegister(hCamera, 0xA000, 0x3A08, 42);
                if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    Console.WriteLine("Errorflag: " + errorflag.ToString());
                    cameraConnected = false;
                    return cameraConnected;
                }



                errorflag = myApi.Gige_Camera_setTriggerPolarity(hCamera, GigeApi.TRIGGER_POLARITY.TRIGGER_POLARITY_NEGATIVE);
                if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    Console.WriteLine("Errorflag: " + errorflag.ToString());
                    cameraConnected = false;
                    return cameraConnected;
                }

                errorflag = myApi.Gige_Camera_setAcquisitionControl(hCamera, GigeApi.ACQUISITION_CONTROL.ACQUISITION_CONTROL_START);
                if (errorflag != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    Console.WriteLine("Errorflag: " + errorflag.ToString());
                    cameraConnected = false;
                    return cameraConnected;
                }
                return cameraConnected;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errorflag: " + errorflag.ToString());
                cameraConnected = false;
                return cameraConnected;
            }
        }

        [return: MarshalAs(UnmanagedType.Error)]
        public GigeApi.SVSGigeApiReturn myStreamCallback(int Image, IntPtr Context)
        {
            GigeApi.SVSGigeApiReturn apiReturn;
            int xSize, ySize, size;

            IntPtr imgPtr;
            Bitmap bmp = null;
            //bool imgOk = false;
            imgPtr = myApi.Gige_Image_getDataPointer(Image);
            //GigeApi.SVSGigeApiReturn r = myApi.Gige_StreamingChannel_getBufferData(hStreamingChannel, 2000, 0, ref globaldata);

          //  if ((int)(imgPtr) == 0)   //Win10
           if ((imgPtr) == (IntPtr)0)  //Win7
            {
                return (GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS);
            }

            xSize = myApi.Gige_Image_getSizeX(Image);
            ySize = myApi.Gige_Image_getSizeY(Image);
            size = xSize * ySize;    // = Image-Size = Gesamt-Anz. der Pixel
            int bufferWriteind_current = bufferWriteind;
            lock (bimage[bufferWriteind])
            {
                unsafe
                {
                    System.Runtime.InteropServices.Marshal.Copy(imgPtr, imagebuffer[bufferWriteind].imagebytes, 0, size);
                }
                        
            }
            lock (bimage[bufferReadind])
            {
                // Copy BW8 image
                System.Buffer.BlockCopy(imagebuffer[bufferReadind].imagebytes, 0, outputbuffer[bufferReadind].imagebytes, 0, camWidth * camHeight);
            }
            unsafe
            {
                fixed (byte* MonoPtr = outputbuffer[bufferReadind].imagebytes)
                {
                    bmp = new Bitmap(camWidth, camHeight, camWidth, PixelFormat.Format8bppIndexed, (IntPtr)MonoPtr);

                    imgpal = bmp.Palette;

                    // Build bitmap palette Y8
                    for (uint i = 0; i < 256; i++)
                    {
                        imgpal.Entries[i] = Color.FromArgb(
                        (byte)0xFF,
                        (byte)i,
                        (byte)i,
                        (byte)i);
                    }
                    bmp.Palette = imgpal;
                    imgpal = bmp.Palette;
                }
            }
            MemoryStream stream = new MemoryStream();
            bmp.Save(stream, ImageFormat.Bmp);
            Bitmap bmp2 = new Bitmap(stream);
            SVSDataTakedEventArgs e = new SVSDataTakedEventArgs
            {
                CameraName = CamName,
                CameraID = Id,
                IsDone = true,
                Image = bmp2
            };
            SVSImageTaked?.Invoke(this, e);
            //bufferWriteind = (bufferWriteind + 1) % bufferCount;
            return (GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS);

        }

        private void initializeBuffer()
        {
            try
            {
                int k;

                imagebuffer = new imagebufferStruct[bufferCount];
                outputbuffer = new imagebufferStruct[bufferCount];

                for (k = 0; k < bufferCount; k++)
                {
                    imagebuffer[k].imagebytes = new byte[camHeight * camWidth];
                    outputbuffer[k].imagebytes = new byte[camHeight * camWidth];
                    unsafe
                    {
                        fixed (byte* MonoPtr = outputbuffer[k].imagebytes)
                        {
                            bufferReadind = 0;
                            bufferWriteind = 0;
                            bimage[k] = new Bitmap(camWidth, camHeight, camWidth, PixelFormat.Format8bppIndexed, (IntPtr)MonoPtr);

                            imgpal = bimage[k].Palette;

                            // Build bitmap palette Y8
                            for (uint i = 0; i < 256; i++)
                            {
                                imgpal.Entries[i] = Color.FromArgb(
                                (byte)0xFF,
                                (byte)i,
                                (byte)i,
                                (byte)i);
                            }
                            bimage[k].Palette = imgpal;
                            imgpal = bimage[k].Palette;
                        }
                    }

                } // for
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }   
    }
}
