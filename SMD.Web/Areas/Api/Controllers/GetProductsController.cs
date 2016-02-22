using System;
using System.Threading.Tasks;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Products Controller 
    /// </summary>
    /// 
    public class ProductsController : ApiController
    {
        
        #region Private

        private readonly IWebApiUserService webApiUserService;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constuctor 
        /// </summary>
        public ProductsController(IWebApiUserService webApiUserService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
        }

        #endregion
        
        #region Public

        /// <summary>
        /// Get Ads, Surveys, Questions
        /// </summary>
        [ApiExceptionCustom]
        public ProductViewResponse Get(string authenticationToken,[FromUri] GetProductsRequest request)
        {
            if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return webApiUserService.GetProducts(request).CreateFrom();
        }

        /// <summary>
        /// Response From User on Ads, Surveys, Questions
        /// </summary>
        [ApiExceptionCustom]
        public async Task<BaseApiResponse> Post(string authenticationToken, ProductActionRequest request)
        {
            if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid || string.IsNullOrEmpty(request.UserId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            BaseApiResponse response = await webApiUserService.ExecuteActionOnProductsResponse(request);
            return response;
        }
        
        #endregion
    }
}