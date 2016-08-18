using SMD.Interfaces.Services;
using SMD.MIS.Areas.DAM.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}