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
    public class UserProfileQuestionsController : ApiController
    {
        #region Public
     
        private readonly ProfileQuestionService profileQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public UserProfileQuestionsController(ProfileQuestionService profileQuestionService)
        {
           
            this.profileQuestionService = profileQuestionService;
        }

        #endregion
        #region Public


        public List<GetUserProfileQuestionsList_Result> Get(string authenticationToken, string UserId)
        {
            return profileQuestionService.GetUserProfileQuestionsList(UserId).ToList();
        }



        /// <summary>
        /// User Payout
        /// </summary>
        [ApiExceptionCustom]
        public BaseApiResponse Post(string authenticationToken, string UserId, int PQID, string Answers, int CompanyId, List<int> ProfileQuestionAnswerIds)
        {
            
            var response = new BaseApiResponse { Message = "Success", Status = true };
            try
            {


                response.Status =  profileQuestionService.SaveUserProfileQuestionResponse(PQID, UserId, CompanyId, ProfileQuestionAnswerIds.ToArray());
                return response;
              
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
