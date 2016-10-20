using System;
using System.Collections.Generic;
using System.Linq;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Profile Question User Answer Service
    /// </summary>
    public class ProfileQuestionUserAnswerService : IProfileQuestionUserAnswerService
    {
        #region Private

        private readonly IProfileQuestionUserAnswerRepository profileQuestionUserAnswerRepository;

        
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileQuestionUserAnswerService(IProfileQuestionUserAnswerRepository profileQuestionUserAnswerRepository)
        {
            this.profileQuestionUserAnswerRepository = profileQuestionUserAnswerRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Update user's answer for question
        /// </summary>
        public string SaveProfileQuestionUserResponse(UpdateProfileQuestionUserAnswerApiRequest request)
        {

            //not deleting the old responses anymore.

            //#region Deletion of Existing Answers
            //var answerList = profileQuestionUserAnswerRepository.GetProfileQuestionUserAnswerByQuestionId(request);
            //var profileQuestionUserAnswers = answerList as IList<ProfileQuestionUserAnswer> ?? answerList.ToList();
            //if (answerList != null && profileQuestionUserAnswers.Any())
            //{
            //    foreach (var answer in profileQuestionUserAnswers)
            //    {
            //        profileQuestionUserAnswerRepository.Delete(answer);
            //    }
            //}
            //#endregion
            #region Updation

            if (request.ProfileQuestionAnswerIds != null && request.ProfileQuestionAnswerIds.Count > 0 && request.ProfileQuestionAnswerIds[0] != -1)
            {
                foreach (var ansId in request.ProfileQuestionAnswerIds)
                {
                    var newAnswer = new ProfileQuestionUserAnswer
                    {
                        PQID = request.ProfileQuestionId,
                        AnswerDateTime = DateTime.Now,
                        PQAnswerID = ansId,
                        UserID = request.UserId,
                        ResponseType =  (int)request.ResponeEventType,
                        CompanyId = request.companyId
                    };
                    profileQuestionUserAnswerRepository.Add(newAnswer);
                }
            }
            else
            {
                //other events processing if not answerered

                var newAnswer = new ProfileQuestionUserAnswer
                    {
                        PQID = request.ProfileQuestionId,
                        AnswerDateTime = DateTime.Now,
                        PQAnswerID = null,
                        UserID = request.UserId,
                        ResponseType =  (int)request.ResponeEventType,
                        CompanyId = request.companyId
                    };
                    profileQuestionUserAnswerRepository.Add(newAnswer);
            }
            #endregion
            profileQuestionUserAnswerRepository.SaveChanges();



          



            return "Success";
        }
        #endregion
    }
}
