﻿
using System.Collections.Generic;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Profile Question Service Interface 
    /// </summary>
    public interface IProfileQuestionService
    {
        /// <summary>
        /// Profile Question Search request 
        /// </summary>
        ProfileQuestionSearchRequestResponse GetProfileQuestions(ProfileQuestionSearchRequest request);

        /// <summary>
        /// Delete Profile Question
        /// </summary>
        bool DeleteProfileQuestion(ProfileQuestion profileQuestion);

        /// <summary>
        /// Get Base Data for PQ
        /// </summary>
        ProfileQuestionBaseResponse GetProfileQuestionBaseData();

        /// <summary>
        /// Save Profile Question
        /// </summary>
        ProfileQuestion SaveProfileQuestion(ProfileQuestion source);

        /// <summary>
        /// Profile Questions For Api
        /// </summary>
        ProfileQuestionApiSearchResponse GetProfileQuestionsByGroupForApi(GetProfileQuestionApiRequest request);
    }
}
