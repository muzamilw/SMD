using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SMD.Models.IdentityModels;
using SMD.Models.DomainModels;
using AutoMapper;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Update Web User Profile Api Controller 
    /// </summary>
    public class CompanyController : ApiController
    {
        #region Private
        private readonly IWebApiUserService webApiUserService;
        private readonly ICompanyService companyService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyController(IWebApiUserService webApiUserService, ICompanyService companyService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
            this.companyService = companyService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get User's Profile 
        /// </summary>
        //public WebApiUser Get()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
        //    }
        //    var response=  webApiUserService.GetLoggedInUser();
        //    return  response.CreateFrom();
        //}

        /// <summary>
        /// Get User's Profile 
        /// </summary>
        public CompanyApiModel Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            



            Mapper.Initialize(cfg => cfg.CreateMap<Company, CompanyApiModel>());
            
            var response = companyService.GetCurrentCompany();
            CompanyApiModel apicompany = Mapper.Map<Company, CompanyApiModel>(response);

            if (!string.IsNullOrEmpty(apicompany.Logo))
                apicompany.Logo = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/" + apicompany.Logo + "?" + DateTime.Now;


           return apicompany;
           
           
          
        }

        /// <summary>
        /// Update User Profile
        /// </summary>
        [ApiExceptionCustom]
        public async Task<BaseApiResponse> Post(CompanyApiModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }


            Mapper.Initialize(cfg => cfg.CreateMap<CompanyApiModel,Company>());
            var company = Mapper.Map<CompanyApiModel,Company>(request);

            

            companyService.UpdateCompany(company, request.LogoImageBytes);

            return new BaseApiResponse {  Status = true, Message = "Success"};
        }

        #endregion
    }
}