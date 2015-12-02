﻿using System.Net;
using System.Web;
using System.Web.Http;
using SMD.Interfaces.Services;
using SMD.MIS.ModelMappers;
using SMD.MIS.Models.RequestResposeModels;
using SMD.Models.RequestModels;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Profile Question Api Controller 
    /// </summary>
    public class ProfileQuestionController : ApiController
    {
        #region Public
        private readonly IProfileQuestionService _profileQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public ProfileQuestionController(IProfileQuestionService profileQuestionService)
        {
            _profileQuestionService = profileQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public ProfileQuestionSearchRequestResponse Get([FromUri] ProfileQuestionSearchRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _profileQuestionService.GetProfileQuestions(request).CraeteFrom();
        }

        #endregion
    }
}