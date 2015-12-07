using System;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Profile Question Service 
    /// </summary>
    public sealed class ProfileQuestionService : IProfileQuestionService
    {
        #region Private
        private readonly IProfileQuestionRepository _profileQuestionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IProfileQuestionGroupRepository _profileQuestionGroupRepository;
        private readonly IProfileQuestionAnswerRepository _profileQuestionAnswerRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>

        public ProfileQuestionService(IProfileQuestionRepository profileQuestionRepository, ICountryRepository countryRepository, ILanguageRepository languageRepository, IProfileQuestionGroupRepository profileQuestionGroupRepository, IProfileQuestionAnswerRepository profileQuestionAnswerRepository)
        {
            _profileQuestionRepository = profileQuestionRepository;
            _countryRepository = countryRepository;
            _languageRepository = languageRepository;
            _profileQuestionGroupRepository = profileQuestionGroupRepository;
            _profileQuestionAnswerRepository = profileQuestionAnswerRepository;
        }

        #endregion
        #region Public
        /// <summary>
        /// Profile Question Search request 
        /// </summary>
        public ProfileQuestionSearchRequestResponse GetProfileQuestions(ProfileQuestionSearchRequest request)
        {
            int rowCount;
            return new ProfileQuestionSearchRequestResponse
            {
                ProfileQuestions = _profileQuestionRepository.SearchProfileQuestions(request, out rowCount),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Delete Profile Question
        /// </summary>
        public bool DeleteProfileQuestion(ProfileQuestion profileQuestion)
        {
            var dBprofileQuestion=_profileQuestionRepository.Find(profileQuestion.PqId);
            if (dBprofileQuestion != null)
            {
                dBprofileQuestion.Status = 0;     // 0 -> archived  || 1 -> active
                _profileQuestionRepository.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get Base Data for PQ
        /// </summary>
        public ProfileQuestionBaseResponse GetProfileQuestionBaseData()
        {
            return new ProfileQuestionBaseResponse
            {
                Countries = _countryRepository.GetAllCountries(),
                Languages = _languageRepository.GetAllLanguages(),
                ProfileQuestionGroups = _profileQuestionGroupRepository.GetAllProfileQuestionGroups(),
                ProfileQuestions = _profileQuestionRepository.GetAllProfileQuestions()
            };
        }

        /// <summary>
        /// Save Profile Question
        /// </summary>
        public ProfileQuestion SaveProfileQuestion(ProfileQuestion source)
        {
            //var serverObj=_profileQuestionRepository.Find(source.PqId);
            //if (serverObj != null)
            //{
            //    serverObj.Question = source.Question;
            //    serverObj.Priority = source.Priority;
            //    serverObj.HasLinkedQuestions = source.HasLinkedQuestions;

            //    serverObj.LanguageId = source.LanguageId;
            //    serverObj.CountryId = source.CountryId;
            //    serverObj.ProfileGroupId = source.ProfileGroupId;
            //    serverObj.Type = source.Type;
            //    serverObj.RefreshTime = source.RefreshTime;
            //    serverObj.SkippedCount = source.SkippedCount;
            //    serverObj.ModifiedDate = source.ModifiedDate;
            //    serverObj.PenalityForNotAnswering = source.PenalityForNotAnswering;
            //    serverObj.Status = source.Status;
            //    if (source.ProfileQuestionAnswers != null)
            //    {
            //        #region Answer Add/Edit
            //        foreach (var answer in source.ProfileQuestionAnswers)
            //        {
            //            var serverAns = _profileQuestionAnswerRepository.Find(answer.PqAnswerId);
            //            if (serverAns != null)
            //            {
            //                serverAns.PqId = serverObj.PqId;
            //                serverAns.Type = answer.Type;
            //                serverAns.AnswerString = answer.AnswerString;
            //                serverAns.ImagePath = answer.ImagePath;

            //                serverAns.LinkedQuestion1Id = answer.LinkedQuestion1Id;
            //                serverAns.LinkedQuestion2Id = answer.LinkedQuestion2Id;
            //                serverAns.LinkedQuestion3Id = answer.LinkedQuestion3Id;
            //                serverAns.LinkedQuestion4Id = answer.LinkedQuestion4Id;

            //                serverAns.LinkedQuestion5Id = answer.LinkedQuestion5Id;
            //                serverAns.LinkedQuestion6Id = answer.LinkedQuestion6Id;
            //                serverAns.PqAnswerId = answer.PqAnswerId;
            //                serverAns.SortOrder = answer.SortOrder;
            //            }
            //            else
            //            {
            //                serverAns = new ProfileQuestionAnswer
            //                {
            //                    PqId = serverObj.PqId,
            //                    Type = answer.Type,
            //                    AnswerString = answer.AnswerString,
            //                    ImagePath = answer.ImagePath,
            //                    LinkedQuestion1Id = answer.LinkedQuestion1Id,
            //                    LinkedQuestion2Id = answer.LinkedQuestion2Id,
            //                    LinkedQuestion3Id = answer.LinkedQuestion3Id,
            //                    LinkedQuestion4Id = answer.LinkedQuestion4Id,
            //                    LinkedQuestion5Id = answer.LinkedQuestion5Id,
            //                    LinkedQuestion6Id = answer.LinkedQuestion6Id,
            //                    PqAnswerId = answer.PqAnswerId,
            //                    SortOrder = answer.SortOrder
            //                };
            //                _profileQuestionAnswerRepository.Add(serverAns);
            //            }
            //        }
            //        #endregion
            //        #region Answer Deletion

            //        var serverListOfAns=_profileQuestionAnswerRepository.GetProfileQuestionAnswerByQuestionId(source.PqId);
            //        if (serverListOfAns != null)
            //        {
            //            foreach (var serverAns in serverListOfAns)
            //            {
            //                if (!source.ProfileQuestionAnswers.Any(ans => ans.PqAnswerId == serverAns.PqAnswerId))
            //                {
            //                    _profileQuestionAnswerRepository.Delete(serverAns);
            //                }
            //            }
            //        }
            //        #endregion

            //    }
                
            //}
            return new ProfileQuestion();
        }
        #endregion
    }
}
