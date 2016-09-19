using SMD.Interfaces.Services;
using SMD.MIS.Areas.DAM.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Net;

namespace SMD.MIS.Areas.DAM.Controllers
{
    public class ImagesController : Controller
    {
          #region Private

        private readonly IDamImageService damImageService;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ImagesController(IDamImageService damImageService)
        {
            if (damImageService == null)
            {
                throw new ArgumentNullException("surveyQuestionService");
            }

            this.damImageService = damImageService;
        }

        #endregion
        // GET: DAM/Images
        public ActionResult Index(int mode)
        {
            ViewBag.Status = 1;
            if(mode == 0 )
            {
                ViewBag.Status = 0;
                return View();
            }
            var images = new ImagesModel();
            int companyId = 0;
            images.Images = damImageService.getAllImages(mode,out companyId);
            foreach (var img in images.Images)
            {
                img.ImageFileName = "/SMD_Content/DamImages/" + companyId + "/" + mode + "/" + img.ImageFileName;
            }
            images.FreeImages = damImageService.getAllFreeImages();
            foreach (var img in images.FreeImages)
            {
                img.ImageFileName = "/SMD_Content/DamImages/FreeImages/" + img.ImageFileName;
            }
            images.mode = mode;
            images.CompanyId = companyId;
            return View(images);
        }
        public ActionResult AdminIndex()
        {
            ViewBag.Status = 1;
            var images = new ImagesModel();
            int companyId = 0;
            images.Images = damImageService.getAllFreeImages();
            foreach (var img in images.Images)
            {
                img.ImageFileName = "/SMD_Content/DamImages/FreeImages/"  + img.ImageFileName;
            }
            images.CompanyId = companyId;
            return View(images);
        }
        public ActionResult Grid()
        {
            
            return View();
        }

        //
        // POST: /Home/UploadImage

        [HttpPost]
        public ActionResult UploadImage(ImagesModel model)
        {
           
                //Prepare the needed variables
               // Bitmap original = null;
                var name = "newimagefile";
                var errorField = string.Empty;

                
                    errorField = "File";
                    name = string.Format("text-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                   // original = Bitmap.FromStream(model.File.InputStream) as Bitmap;
                    if (!Directory.Exists(Server.MapPath("~/SMD_Content/DamImages/" + model.CompanyId + "/" + model.mode + "/")))
                        Directory.CreateDirectory(Server.MapPath("~/SMD_Content/DamImages/" + model.CompanyId + "/" + model.mode + "/"));
                    var fn = Server.MapPath("~/SMD_Content/DamImages/" + model.CompanyId + "/" + model.mode + "/" + name + ".png");
                   // original.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
                    if (!String.IsNullOrEmpty(model.bytes))
                    {
                        string base64 = model.bytes.Substring(model.bytes.IndexOf(',') + 1);
                        base64 = base64.Trim('\0');
                        byte[] data = Convert.FromBase64String(base64);
                        System.IO.File.WriteAllBytes(fn, data);
                    }
                    DamImage damImg = new DamImage();
                    damImg.CompanyId = model.CompanyId;
                    damImg.ImageCategory = model.mode;
                    damImg.ImageFileName = name + ".png";
                    damImg.ImageTitle = name;
                    damImageService.addImage(damImg);
                    return RedirectToAction("Index", "Images", new { mode = model.mode ,iId = damImg.ImageId});

          
        }
        [HttpPost]
        public bool UploadImageAdmin(ImagesModel model)
        {

            //Prepare the needed variables
            // Bitmap original = null;
            var name = "newimagefile";
            var errorField = string.Empty;


            errorField = "File";
            name = string.Format("text-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            // original = Bitmap.FromStream(model.File.InputStream) as Bitmap;
            if (!Directory.Exists(Server.MapPath("~/SMD_Content/DamImages/FreeImages/")))
                Directory.CreateDirectory(Server.MapPath("~/SMD_Content/DamImages/FreeImages/"));
           // var fn = Server.MapPath("~/SMD_Content/DamImages/FreeImages/");
           
               var httpPostedFile = HttpContext.Request.Files["UploadedImage"];

               if (httpPostedFile != null)
               {
                   var fileSavePath = Path.Combine(Server.MapPath("~/SMD_Content/DamImages/FreeImages/"), httpPostedFile.FileName);
                   DamImage damImg = new DamImage();
                   damImg.CompanyId = null;
                   damImg.ImageCategory = model.mode;
                   damImg.ImageFileName = httpPostedFile.FileName;
                   damImg.ImageTitle = httpPostedFile.FileName;
                   damImageService.addImage(damImg);
                   httpPostedFile.SaveAs(fileSavePath);
               }
           
            return true;


        }
        public bool UpdateImage(ImageCropModel model)
        {
            string fileName = damImageService.updateImage(model.imageId, model.fileName);
            if (!String.IsNullOrEmpty(model.bytes))
            {
                string base64 = model.bytes.Substring(model.bytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                if (!Directory.Exists(Server.MapPath("~/SMD_Content/DamImages/" + model.CompanyId + "/" + model.mode + "/")))
                    Directory.CreateDirectory(Server.MapPath("~/SMD_Content/DamImages/" + model.CompanyId + "/" + model.mode + "/"));
                var fn = Server.MapPath("~/SMD_Content/DamImages/" + model.CompanyId + "/" + model.mode + "/" + fileName);
                System.IO.File.WriteAllBytes(fn, data);
            }
            return true;
        }
        public bool DeleteImage(ImageCropModel model)
        {
            damImageService.deleteImage(model.imageId);
            return true;
        }
        /// <summary>
        /// Gets an image from the specified URL.
        /// </summary>
        /// <param name="url">The URL containing an image.</param>
        /// <returns>The image as a bitmap.</returns>
        Bitmap GetImageFromUrl(string url)
        {
            var buffer = 1024;
            Bitmap image = null;

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return image;

            using (var ms = new MemoryStream())
            {
                var req = WebRequest.Create(url);

                using (var resp = req.GetResponse())
                {
                    using (var stream = resp.GetResponseStream())
                    {
                        var bytes = new byte[buffer];
                        var n = 0;

                        while ((n = stream.Read(bytes, 0, buffer)) != 0)
                            ms.Write(bytes, 0, n);
                    }
                }

                image = Bitmap.FromStream(ms) as Bitmap;
            }

            return image;
        }

        /// <summary>
        /// Gets the filename that is placed under a certain URL.
        /// </summary>
        /// <param name="url">The URL which should be investigated for a file name.</param>
        /// <returns>The file name.</returns>
        string GetUrlFileName(string url)
        {
            var parts = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var last = parts[parts.Length - 1];
            return Path.GetFileNameWithoutExtension(last);
        }

        /// <summary>
        /// Creates a small image out of a larger image.
        /// </summary>
        /// <param name="original">The original image which should be cropped (will remain untouched).</param>
        /// <param name="x">The value where to start on the x axis.</param>
        /// <param name="y">The value where to start on the y axis.</param>
        /// <param name="width">The width of the final image.</param>
        /// <param name="height">The height of the final image.</param>
        /// <returns>The cropped image.</returns>
        Bitmap CreateImage(Bitmap original, int x, int y, int width, int height)
        {
            var img = new Bitmap(width, height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            return img;
        }
    }
}