using System.Web;
using SMD.Interfaces.Services;
using System.Net;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.WebBase.Mvc;
using SMD.Implementation.Services;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class RedeemCouponController : ApiController
    {
        #region Public
        private readonly IEducationService educationService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public RedeemCouponController(IEducationService educationService)
        {
            this.educationService = educationService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        [ApiExceptionCustom]
        public bool Get(string authenticationToken, int companyId, int CouponId,int mode,double amount)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return PayOutScheduler.PerformUserPayout(companyId, CouponId, mode, amount);
        }


        #endregion
    }
}
