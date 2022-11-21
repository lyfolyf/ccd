//using Bing.CogImageStitching;
using Bing.ImageStitch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Cognex.VisionPro;

namespace Hix_CCD_Module.Tool
{
    public static class Actuator
    {
        /// <summary>
        /// 图片拼接
        /// </summary>
        /// <param name="images">输入图片组</param>
        /// <param name="unfilledPelValue">拼接填充灰度</param>
        /// <param name="stitchedImage">拼接图像</param>
        /// <param name="save">是否保存bmp图片</param>
        /// <param name="path">保存图片路径</param>
        /// <returns></returns>
        //public static bool ImageStitching(List<object> images, int unfilledPelValue, out object stitchedImage, bool save, string path)
        //{
        //    try
        //    {
        //        Stitching stitching = new Stitching(images);
        //        stitchedImage = stitching.GetStitchImage(unfilledPelValue, save, path);
        //        return true;
        //    }
        //    catch
        //    {
        //        stitchedImage = null;
        //        return false;
        //    }
        //}

        /// <summary>
        /// 图片拼接
        /// </summary>
        /// <param name="images">输入图片组</param>
        /// <param name="unfilledPelValue">拼接填充灰度</param>
        /// <param name="stitchedImage">拼接图像</param>
        /// <param name="save">是否保存bmp图片</param>
        /// <param name="path">保存图片路径</param>
        /// <returns></returns>
        //public static bool ImageStitching(List<object> images, int unfilledPelValue, out Bitmap stitchedImage, bool save, string path)
        //{
        //    try
        //    {
        //        Stitching stitching = new Stitching(images);
        //        //todo:待更新算法
        //        stitchedImage = stitching.GetStitchBitmapImage(unfilledPelValue, save, path);
        //        stitching = null;
        //        return true;
        //    }
        //    catch
        //    {
        //        stitchedImage = null;
        //        return false;
        //    }
        //}

        public static bool ImageStitching(List<CogImage8Grey> images, int unfilledPelValue, out CogImage8Grey stitchedImage, int mode, double rate)
        {
            try
            {
                ImageStitchingPro stitch = new ImageStitchingPro(images);
                CogTransform2DLinear trans = null;
                stitchedImage = stitch.GetStitchImage(unfilledPelValue, out trans, mode, rate);
                return true;
            }
            catch
            {
                stitchedImage = null;
                return false;
            }
        }

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="taskRunner">任务</param>
        /// <param name="inPuts">输入终端名称列表</param>
        /// <param name="values">输入终端值列表</param>
        public static void RunTask(TaskRunner taskRunner, string[] inPuts, object[] values)
        {
            if (inPuts.Length >= values.Length)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    taskRunner.SetInsValue(inPuts[i], values[i]);
                }
            }
            else
            {
                for (int i = 0; i < inPuts.Length; i++)
                {
                    taskRunner.SetInsValue(inPuts[i], values[i]);
                }
            }
            taskRunner.Run();
        }

        /// <summary>
        /// 运行标定工具
        /// </summary>
        /// <param name="calibTool">标定工具</param>
        /// <param name="inputImage">输入图像</param>
        /// <param name="outputImage">输出图像</param>
        public static void RunCalib(HixCalibTool calibTool, Bitmap inputImage, out CogImage8Grey outputImage)
        {
            try
            {
                calibTool.InputImage = new CogImage8Grey(inputImage);
                calibTool.Run();
                outputImage = calibTool.OutputImage;
            }
            catch(Exception ex)
            {
                string str = ex.Message;
                outputImage = null;
            }
        }

        /// <summary>
        /// 检查文件夹是否存在，如果不存在则创建文件夹
        /// </summary>
        /// <param name="path">文件夹</param>
        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        //深拷贝
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
    }
}
