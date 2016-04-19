using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace SMD.Common
{
    /// <summary>
    /// Image Helper
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Saves Image to File System
        /// </summary>
        /// <param name="mapPath">File System Path for Item</param>
        /// <param name="existingImage">Existing File if any</param>
        /// <param name="caption">Unique file caption e.g. ItemId + ItemProductCode + ItemProductName + "_thumbnail_"</param>
        /// <param name="fileName">Name of file being saved</param>
        /// <param name="fileSource">Base64 representation of file being saved</param>
        /// <param name="fileSourceBytes">Byte[] representation of file being saved</param>
        /// <param name="fileDeleted">True if file has been deleted</param>
        /// <returns>Path of File being saved</returns>
        public static string Save(string mapPath, string existingImage, string caption, string fileName,
            string fileSource, byte[] fileSourceBytes, bool fileDeleted = false)
        {
            // return if no file specified
            if (string.IsNullOrEmpty(fileSource) && !fileDeleted)
            {
                return null;
            }
            
            // Look if file already exists then replace it
            if (!string.IsNullOrEmpty(existingImage))
            {
                if (Path.IsPathRooted(existingImage))
                {
                    if (File.Exists(existingImage))
                    {
                        // Remove Existing File
                        File.Delete(existingImage);
                    }
                }
                else
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + existingImage);
                    if (File.Exists(filePath))
                    {
                        // Remove Existing File
                        File.Delete(filePath);
                    }
                }

            }

            // If File has been deleted then set the specified field as empty
            if (fileDeleted)
            {
                return string.Empty;
            }

            // First Time Upload
            string imageurl = mapPath + "\\" + Guid.NewGuid() + ".png";
            File.WriteAllBytes(imageurl, fileSourceBytes);

            int indexOf = imageurl.LastIndexOf("SMD_Content", StringComparison.Ordinal);
            imageurl = imageurl.Substring(indexOf, imageurl.Length - indexOf);
            return imageurl;
        }

        public static void GenerateThumbNail(string sourcefile, string destinationfile, int width)
        {
            System.Drawing.Image image = null;
            int ThumbnailSizeWidth = 200;
            int ThumbnailSizeHeight = 200;
            Bitmap bmp = null;
            try
            {

                using (image = System.Drawing.Image.FromFile(sourcefile))
                {
                    int srcWidth = image.Width;
                    int srcHeight = image.Height;
                    int thumbWidth = width;
                    int thumbHeight;
                    float WidthPer, HeightPer;


                    int NewWidth, NewHeight;

                    if (srcWidth > srcHeight)
                    {
                        NewWidth = ThumbnailSizeWidth;
                        WidthPer = (float)ThumbnailSizeWidth / srcWidth;
                        NewHeight = Convert.ToInt32(srcHeight * WidthPer);
                    }
                    else
                    {
                        NewHeight = ThumbnailSizeHeight;
                        HeightPer = (float)ThumbnailSizeHeight / srcHeight;
                        NewWidth = Convert.ToInt32(srcWidth * HeightPer);
                    }
                    thumbWidth = NewWidth;
                    thumbHeight = NewHeight;
                    bmp = new Bitmap(thumbWidth, thumbHeight);
                    System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    System.Drawing.Rectangle rectDestination =
                           new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                    gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                    
                }
                if (image != null)
                {
                    image.Dispose();
                }
                if (bmp != null)
                {
                    bmp.Save(destinationfile);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }

    }
}
