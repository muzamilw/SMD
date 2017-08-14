using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class DownloadImageController : ApiController
    {
        
        #region Public
        private readonly IDamImageService _damService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public DownloadImageController(IDamImageService damService)
        {
            _damService = damService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get base data for campaigns 
        /// 
        /// </summary>
        public string Get([FromUri] DownloadImageModel request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                return _damService.downloadImage(request.type, request.mode, request.id, request.path);
            }
        }


        #endregion
    }
}
