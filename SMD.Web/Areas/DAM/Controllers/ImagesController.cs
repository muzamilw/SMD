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
            images.Images = damImageService.getAllImages(mode);
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
            //Check if all simple data annotations are valid
            if (ModelState.IsValid)
            {
                //Prepare the needed variables
                Bitmap original = null;
                var name = "newimagefile";
                var errorField = string.Empty;

                
                    errorField = "File";
                    name = Path.GetFileNameWithoutExtension(model.File.FileName);
                    original = Bitmap.FromStream(model.File.InputStream) as Bitmap;
                
                //If we had success so far
                if (original != null)
                {
                    var img = CreateImage(original, model.X, model.Y, model.Width, model.Height);

                    //Demo purposes only - save image in the file system
                    var fn = Server.MapPath("~/Content/img/" + name + ".png");
                    img.Save(fn, System.Drawing.Imaging.ImageFormat.Png);

                    //Redirect to index
                    return RedirectToAction("Index");
                }
                else //Otherwise we add an error and return to the (previous) view with the model data
                    ModelState.AddModelError(errorField, "Your upload did not seem valid. Please try again using only correct images!");
            }

            return View(model);
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