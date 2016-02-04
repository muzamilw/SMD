﻿using System.Collections.Generic;
using System.Globalization;
using SMD.Common;
using SMD.ExceptionHandling;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

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

        public ProfileQuestionService(IProfileQuestionRepository profileQuestionRepository, ICountryRepository countryRepository, 
            ILanguageRepository languageRepository, IProfileQuestionGroupRepository profileQuestionGroupRepository, 
            IProfileQuestionAnswerRepository profileQuestionAnswerRepository)
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
        /// Skip Profile Question
        /// </summary>
        public void PerformSkip(int pqId)
        {
            ProfileQuestion profileQuestion = _profileQuestionRepository.Find(pqId);
            if (profileQuestion == null)
            {
                throw new SMDException(string.Format(CultureInfo.InvariantCulture, 
                    LanguageResources.ProfileQuestionService_ProfileQuestionNotFound, pqId));
            }

            // Update Skipped Count
            if (profileQuestion.SkippedCount == null)
            {
                profileQuestion.SkippedCount = 0;
            }

            profileQuestion.SkippedCount += 1;

            // Save Changes
            _profileQuestionRepository.SaveChanges();
        }

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
                dBprofileQuestion.Status = (Int32)ObjectStatus.Archived;     
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
            var serverObj=_profileQuestionRepository.Find(source.PqId);
            #region Edit Question
            // Edit Profile Question 
            if (serverObj != null)
            {
                serverObj.Question = source.Question;
                serverObj.Priority = source.Priority;
                serverObj.HasLinkedQuestions = source.HasLinkedQuestions;

                serverObj.LanguageId = source.LanguageId;
                serverObj.CountryId = source.CountryId;
                serverObj.ProfileGroupId = source.ProfileGroupId;
                serverObj.Type = source.Type;
                serverObj.RefreshTime = source.RefreshTime;
                serverObj.SkippedCount = source.SkippedCount;
                serverObj.ModifiedDate = source.ModifiedDate;
                serverObj.PenalityForNotAnswering = source.PenalityForNotAnswering;
                serverObj.Status = source.Status;
                serverObj.ModifiedDate = DateTime.Now.Add(-(_profileQuestionRepository.UserTimezoneOffSet));
                if (source.ProfileQuestionAnswers != null)
                {
                    if (serverObj.ProfileQuestionAnswers == null)
                    {
                        serverObj.ProfileQuestionAnswers = new List<ProfileQuestionAnswer>();
                    }
                    #region Answer Add/Edit
                    // Add/Edit Answer
                    foreach (var answer in source.ProfileQuestionAnswers)
                    {
                        var serverAns = _profileQuestionAnswerRepository.Find(answer.PqAnswerId);
                        if (serverAns != null)
                        {
                            serverAns.PqId = serverObj.PqId;
                            serverAns.Type = answer.Type;
                            serverAns.AnswerString = answer.AnswerString;
                            serverAns.ImagePath = answer.ImagePath;
                            serverAns.LinkedQuestion1Id = answer.LinkedQuestion1Id;
                            serverAns.LinkedQuestion2Id = answer.LinkedQuestion2Id;
                            serverAns.LinkedQuestion3Id = answer.LinkedQuestion3Id;
                            serverAns.LinkedQuestion4Id = answer.LinkedQuestion4Id;

                            serverAns.LinkedQuestion5Id = answer.LinkedQuestion5Id;
                            serverAns.LinkedQuestion6Id = answer.LinkedQuestion6Id;
                            serverAns.PqAnswerId = answer.PqAnswerId;
                            serverAns.SortOrder = answer.SortOrder;
                            if (serverAns.Type == 2)
                            {
                                serverAns.ImagePath = SaveAnswerImage(serverAns);
                            }
                        }
                        else
                        {
                            serverAns = new ProfileQuestionAnswer
                            {
                                PqId = serverObj.PqId,
                                Type = answer.Type,
                                AnswerString = answer.AnswerString,
                                ImagePath = answer.ImagePath,
                                Status = (int) ObjectStatus.Active,
                                LinkedQuestion1Id = answer.LinkedQuestion1Id,
                                LinkedQuestion2Id = answer.LinkedQuestion2Id,
                                LinkedQuestion3Id = answer.LinkedQuestion3Id,
                                LinkedQuestion4Id = answer.LinkedQuestion4Id,
                                LinkedQuestion5Id = answer.LinkedQuestion5Id,
                                LinkedQuestion6Id = answer.LinkedQuestion6Id,
                                PqAnswerId = answer.PqAnswerId,
                                SortOrder = answer.SortOrder
                            };
                            if (serverAns.Type == 2)
                            {
                                serverAns.ImagePath = SaveAnswerImage(serverAns);
                            }
                            _profileQuestionAnswerRepository.Add(serverAns);
                            serverObj.ProfileQuestionAnswers.Add(serverAns);
                        }
                    }

                    #endregion
                }
              
                    #region Answer Deletion

                if (source.ProfileQuestionAnswers == null)
                {
                    source.ProfileQuestionAnswers= new Collection<ProfileQuestionAnswer>();
                }
                    var serverListOfAns = _profileQuestionAnswerRepository.GetProfileQuestionAnswerByQuestionId(source.PqId);
                    if (serverListOfAns != null)
                    {
                        foreach (var serverAns in serverListOfAns)
                        {
                            if (!source.ProfileQuestionAnswers.Any(ans => ans.PqAnswerId == serverAns.PqAnswerId))
                            {
                                serverAns.Status = (Int32)ObjectStatus.Archived;   
                            }
                        }
                    }
                    #endregion
            }
            #endregion
            #region Add Question
            else
            {
                serverObj= new ProfileQuestion
                {
                    Question = source.Question,
                    Priority = source.Priority,
                    HasLinkedQuestions = source.HasLinkedQuestions,
                    LanguageId = source.LanguageId,
                    CountryId = source.CountryId,
                    ProfileGroupId = source.ProfileGroupId,
                    Type = source.Type,
                    RefreshTime = source.RefreshTime,
                    SkippedCount = source.SkippedCount,
                    ModifiedDate = source.ModifiedDate,
                    PenalityForNotAnswering = source.PenalityForNotAnswering,
                    Status = source.Status
                };
                _profileQuestionRepository.Add(serverObj);
                if (serverObj.ProfileQuestionAnswers == null)
                {
                    serverObj.ProfileQuestionAnswers = new List<ProfileQuestionAnswer>();
                }
                foreach (var answer in source.ProfileQuestionAnswers)
                {
                    var serverAns = new ProfileQuestionAnswer
                        {
                            PqId = serverObj.PqId,
                            Type = answer.Type,
                            AnswerString = answer.AnswerString,
                            LinkedQuestion1Id = answer.LinkedQuestion1Id,
                            LinkedQuestion2Id = answer.LinkedQuestion2Id,
                            LinkedQuestion3Id = answer.LinkedQuestion3Id,
                            LinkedQuestion4Id = answer.LinkedQuestion4Id,
                            LinkedQuestion5Id = answer.LinkedQuestion5Id,
                            LinkedQuestion6Id = answer.LinkedQuestion6Id,
                            PqAnswerId = answer.PqAnswerId,
                            SortOrder = answer.SortOrder,
                            Status = (int) ObjectStatus.Active
                        };
                        if (serverAns.Type == 2)
                        {
                            serverAns.ImagePath = SaveAnswerImage(answer);
                        }
                        _profileQuestionAnswerRepository.Add(serverAns);
                        serverObj.ProfileQuestionAnswers.Add(serverAns);
                }
            }
            #endregion

            _profileQuestionRepository.SaveChanges();
            return _profileQuestionRepository.Find(serverObj.PqId);

        }
        
        /// <summary>
        /// Save Answer Image
        /// </summary>
        public string SaveAnswerImage(ProfileQuestionAnswer source)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["SMD_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath =
                server.MapPath(mpcContentPath + "/ProfileQuestions/" + "PQId-"+source.PqId +
                               "/PQAId-" + source.PqAnswerId);

            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }

            mapPath = ImageHelper.Save(mapPath, string.Empty, string.Empty,
                "AnswerImage_"+DateTime.Now.Second+".png", source.ImagePath, source.ImageUrlBytes);

            return mapPath;
        }

        /// <summary>
        /// Profile Questions For Api
        /// </summary>
        public ProfileQuestionApiSearchResponse GetProfileQuestionsByGroupForApi(GetProfileQuestionApiRequest request)
        {
            //request.UserId = "02de201f-f4ea-420e-a1a8-74e7c1975277";
            var allPqGroups = _profileQuestionGroupRepository.GetAllProfileQuestionGroups();
            var unAnsweredQuestions = new List<ProfileQuestion>();
            double percentageCompleted = 0;

            foreach (var pqGroup in allPqGroups)
            {
                var unAnsweredQuestionsCount = _profileQuestionRepository.GetCountOfUnAnsweredQuestionsByGroupId(pqGroup.ProfileGroupId, request.UserId);
               int totalQuestionsCount= _profileQuestionRepository.GetTotalCountOfGroupQuestion(pqGroup.ProfileGroupId);
                if (unAnsweredQuestionsCount > 0)
                {
                    request.GroupId = pqGroup.ProfileGroupId;
                    unAnsweredQuestions = _profileQuestionRepository.GetUnansweredQuestionsByGroupId(request).ToList();
                    percentageCompleted = ((totalQuestionsCount - unAnsweredQuestionsCount) * 100) / totalQuestionsCount;
                    break;
                }
                // Reseting 
                request.TotalCount = 0;
                request.PageNo = 1;

            }
            return new ProfileQuestionApiSearchResponse
            {
                ProfileQuestions = unAnsweredQuestions,
                PercentageCompleted = percentageCompleted
            };
        }
       
        
        #endregion
    }
}
