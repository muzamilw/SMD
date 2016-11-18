using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Survey APi Controller 
    /// </summary>
    public class SharedSurveyQuestionController : ApiController
    {
        #region Public
        private readonly ISharedSurveyQuestionService sharedSurveyQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public SharedSurveyQuestionController(ISharedSurveyQuestionService sharedSurveyQuestionService)
        {
            this.sharedSurveyQuestionService = sharedSurveyQuestionService;
        }

        #endregion
        #region Public


       

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public GetSharedSurveyQuestion_Result Get(long SSQID)
        {

            return sharedSurveyQuestionService.GetSharedSurveyQuestion(SSQID);

        }


       


        public List<GetSharedSurveyQuestionsByUserId_Result> Get (string UserId)
        {
            return sharedSurveyQuestionService.GetSharedSurveysByuserID(UserId);
        }


        /// <summary>
        /// create Question
        /// </summary>
        public SharedSurveyQuestionResponse Put(string authenticationToken, SharedSurveyQuestionRequestApiModel surveyQuestion)
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SharedSurveyQuestionRequestApiModel, SharedSurveyQuestion>();

                });


                var SSQID = sharedSurveyQuestionService.CreateAndSend(Mapper.Map<SharedSurveyQuestionRequestApiModel, SharedSurveyQuestion>(surveyQuestion));


                return new SharedSurveyQuestionResponse { GetSharedSurveyQuestion = sharedSurveyQuestionService.GetSharedSurveyQuestion(SSQID), Message = "Success", Status = true };
            }
            catch (Exception e)
            {

                return new SharedSurveyQuestionResponse { Message = e.ToString(), Status = false };
            }
        }


        public BaseApiResponse Post(string authenticationToken, long SurveyQuestionShareId, int UserSelection)
        {
            try
            {
                if (sharedSurveyQuestionService.updateUserSharedSurveyQuestionResponse(SurveyQuestionShareId, UserSelection) == true)
                {
                    return new BaseApiResponse { Message = "Success", Status = true };
                }
                else
                {
                    return new BaseApiResponse { Message = "Failure", Status = false };
                }


            }
            catch (Exception e)
            {

                return new BaseApiResponse { Message = e.ToString(), Status = false };
            }
        }
        


        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public BaseApiResponse Delete(long SSQID)
        {

            try
            {

                if (sharedSurveyQuestionService.DeleteSharedSurveyQuestion(SSQID))
                    return new BaseApiResponse { Message = "success", Status = true };
                else
                    return new BaseApiResponse { Message = "error deleting", Status = false };
            }
            catch (System.Exception e)
            {

                return new BaseApiResponse { Message = e.ToString(), Status = false };
            }
        }

        #endregion
    }
}