using System.Collections.Generic;
using System.Linq;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Net;
using System.Web;
using System.Web.Http;
using System;
using AutoMapper;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using AutoMapper;
using SMD.WebBase.Mvc;
using SMD.Implementation.Services;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CashoutController : ApiController
    {
        #region Public
     
        private readonly IWebApiUserService _userService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CashoutController( IWebApiUserService _userService)
        {
           
            this._userService = _userService;
        }

        #endregion
        #region Public


        public bool Get(string authenticationToken, string UserId)
        {
            return true;
        }



        /// <summary>
        /// User Payout
        /// </summary>
        [ApiExceptionCustom]
          public BaseApiResponse Post(string authenticationToken, string UserId, string PayoutAmount)
        {
            
            var response = new BaseApiResponse { Message = "Success", Status = true };
            try
            {

                var user = _userService.GetUserByUserId(UserId);

                if (string.IsNullOrEmpty(authenticationToken))
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
                }



                if (PayOutScheduler.PerformUserPayout(UserId, user.CompanyId.Value, Convert.ToDouble( PayoutAmount)))
                    return response;
                else
                {
                    response.Status = false;
                    response.Message = "Insufficient balance to payout";
                    return response;
                }


              
            }
            catch (Exception e)
            {

                response.Status = false;
                response.Message = e.ToString();
                return response;
            }
        }


        #endregion
    }
}
