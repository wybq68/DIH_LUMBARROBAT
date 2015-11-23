using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Research.DynamicDataDisplay;

namespace LumbarRobot.Commands
{
    public class ExportToPngCommand
    {
        #region ExportToPng
        /// <summary>
        /// ExportToPng
        /// </summary>
        /// <param name="path"></param>
        /// <param name="surface"></param>
        public static void ExportToPng(Uri path, Visifire.Charts.Chart surface)
        {
            if (path == null) return;
            //Save current canvas transform 保存当前画布变换
            Transform transform = surface.LayoutTransform;
            //reset current transform (in case it is scaled or rotated) 重设当前画布（如果缩放或旋转）
            surface.LayoutTransform = null;
            //Create a render bitmap and push the surface to it 创建一个渲染位图和表面
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)surface.Width,
                (int)surface.Height,
                96d, 96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);
            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                //Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }
            // Restore previously saved layout 恢复以前保存布局
            surface.LayoutTransform = transform;
        }
        #endregion


        #region ExportToPng
        /// <summary>
        /// ExportChartPlotterPng
        /// </summary>
        /// <param name="path"></param>
        /// <param name="surface"></param>
        public static void ExportChartPlotterPng(Uri path, ChartPlotter surface)
        {
            if (path == null) return;
            //Save current canvas transform 保存当前画布变换
            Transform transform = surface.LayoutTransform;
            //reset current transform (in case it is scaled or rotated) 重设当前画布（如果缩放或旋转）
            surface.LayoutTransform = null;
            //Create a render bitmap and push the surface to it 创建一个渲染位图和表面
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)surface.Width,
                (int)surface.Height,
                96d, 96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);
            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                //Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }
            // Restore previously saved layout 恢复以前保存布局
            surface.LayoutTransform = transform;
        }
        #endregion

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">路劲</param>
        public static void CreateDirectory(string sPath)
        {
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
        }
        #endregion
    }
}
