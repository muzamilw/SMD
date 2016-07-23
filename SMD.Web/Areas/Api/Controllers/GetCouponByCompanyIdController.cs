using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetCouponByCompanyIdController : ApiController
    {
         
        
        #region Private
        private readonly IAdvertService _advertService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCouponByCompanyIdController(IAdvertService advertService)
        {
            
            this._advertService = advertService;
        }

        #endregion

        #region Public

        /// <summary>
        ///invite user
        /// </summary>


        public List<GetCouponsByCompanyId_Result> Get(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return _advertService.GetCouponsByCompanyId(CompanyId);
        }

        #endregion
    }
}
