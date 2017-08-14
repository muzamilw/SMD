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
    public class UserCashoutController : ApiController
    {
        #region Public
     
        private readonly IWebApiUserService _userService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public UserCashoutController( IWebApiUserService _userService)
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
          public BaseApiResponse Post(string authenticationToken, string UserId, string CentzAmount, string Phone, string PayPalId)
        {
            
            var response = new BaseApiResponse { Message = "Success", Status = true };
            try
            {

                var user = _userService.GetUserByUserId(UserId);

                if (string.IsNullOrEmpty(authenticationToken))
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
                }

                var cashoutResult = TransactionManager.PerformUserPayout(UserId, user.CompanyId.Value, Convert.ToDouble(CentzAmount), PayPalId,Phone);

                if (cashoutResult == 1)//success
                    return response;
                    
                else if (cashoutResult == 2 ) //insufficient balance
                {
                    response.Status = false;
                    response.Message = "Insufficient balance to payout";
                    return response;
                }
                else if (cashoutResult == 3) //amount less than min limit
                {
                    response.Status = false;
                    response.Message = "Amount must be higher than minimum cashout limit";
                    return response;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Data Error";
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
