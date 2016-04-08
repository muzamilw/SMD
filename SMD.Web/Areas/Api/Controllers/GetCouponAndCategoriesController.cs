using SMD.Interfaces.Services;
using SMD.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetCouponAndCategoriesController : ApiController
    {
        #region Private
        private readonly IAdvertService _advertService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCouponAndCategoriesController(IAdvertService advertService)
        {
            
            this._advertService = advertService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>


        public List<Coupons> Get()
        {
            
            return _advertService.GetAllCoupons();            
        }

        #endregion
    }
}
