using System.Web;
using SMD.Interfaces.Services;
using System.Net;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CouponCategoryController : ApiController
    {
        #region Public
        private readonly ICouponCategoryService couponCategoryService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CouponCategoryController(ICouponCategoryService couponCategoryService)
        {
            this.couponCategoryService = couponCategoryService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        [ApiExceptionCustom]
        public Models.CouponCategoryResponse Get(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return couponCategoryService.GetAllCategories().CreateFrom();
        }


        #endregion
    }
}
