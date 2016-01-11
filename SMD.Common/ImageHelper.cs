using System;
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
            string imageurl = mapPath + "\\" + caption + fileName;
            try
            {
                File.WriteAllBytes(imageurl, fileSourceBytes);
            }
            catch (Exception excep)
            {
                
                throw;
            }

            int indexOf = imageurl.LastIndexOf("SMD_Content", StringComparison.Ordinal);
            imageurl = imageurl.Substring(indexOf, imageurl.Length - indexOf);
            return imageurl;
        }
    }
}
