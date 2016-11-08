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


        /// <summary>
        /// create group
        /// </summary>
        public GetSharedSurveyQuestion_Result Put(string authenticationToken, SharedSurveyQuestionRequestApiModel surveyQuestion)
        {


            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SharedSurveyQuestionRequestApiModel, SharedSurveyQuestion>();
                
            });


            var SSQID = sharedSurveyQuestionService.CreateAndSend(Mapper.Map<SharedSurveyQuestionRequestApiModel, SharedSurveyQuestion>(surveyQuestion));


            return sharedSurveyQuestionService.GetSharedSurveyQuestion(SSQID);
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