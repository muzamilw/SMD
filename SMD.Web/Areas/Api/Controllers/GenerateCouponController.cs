using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.ResponseModels;
namespace SMD.MIS.Areas.Api.Controllers
{
    public class GenerateCouponController : ApiController
    {
          #region Private
        private readonly IAdvertService _advertService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GenerateCouponController(IAdvertService advertService)
        {
            
            this._advertService = advertService;
        }

        #endregion

        #region Public

        public IEnumerable<CouponCodeModel> Get(long CampaignId, int number)
        {

            return _advertService.GenerateCouponCodes(number, CampaignId).Select(i => i.CreateFrom());
        }

        public BaseApiResponse Post(string VoucherCode, string SecretKey, string UserId)
        {
            BaseApiResponse oResponse = new BaseApiResponse();
            string Message = _advertService.UpdateCouponSettings(VoucherCode, SecretKey, UserId);
            if (Message == "Success")
            {
                oResponse.Status = true;
                oResponse.Message = "Valid Voucher";
            }
            else 
            {
                oResponse.Status = false;
                oResponse.Message = Message;
            }
            return oResponse;
        }
        #endregion
    }
}
