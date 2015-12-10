using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMD.Implementation.Services
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
          #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISurveyQuestionRepository surveyQuestionRepository;
        private readonly ICountryRepository countryRepository;
        private readonly ILanguageRepository languageRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public SurveyQuestionService(ISurveyQuestionRepository _surveyQuestionRepository, ICountryRepository _countryRepository, ILanguageRepository _languageRepository)
        {
            this.surveyQuestionRepository = _surveyQuestionRepository;
            this.languageRepository = _languageRepository;
            this.countryRepository = _countryRepository;
        }

        #endregion
        #region public

        public SurveyQuestionResponseModel GetSurveyQuestions(SurveySearchRequest request)
        {
            int rowCount;
            return new SurveyQuestionResponseModel
            {
                SurveyQuestions = surveyQuestionRepository.SearchSurveyQuestions(request, out rowCount),
                Countries =  new List<Country>(),
                Languages = new List<Language>(),
                TotalCount = rowCount
            };
        }
        public SurveyQuestionResponseModel GetSurveyQuestions()
        {
            int rowCount;
            return new SurveyQuestionResponseModel
            {
                SurveyQuestions = surveyQuestionRepository.SearchSurveyQuestions(null, out rowCount),
                Countries = countryRepository.GetAllCountries(),
                Languages = languageRepository.GetAllLanguages(),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Get Survey Questions that are need aprroval
        /// </summary>
        public SurveyQuestionResposneModelForAproval GetRejectedSurveyQuestionsForAproval(SurveySearchRequest request)
        {
            int rowCount;
            return new SurveyQuestionResposneModelForAproval
            {
                SurveyQuestions = surveyQuestionRepository.SearchRejectedProfileQuestions(request, out rowCount),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Edit Survey Question 
        /// </summary>
        public SurveyQuestion EditSurveyQuestion(SurveyQuestion source)
        {
            var dbServey=surveyQuestionRepository.Find(source.SqId);
            if (dbServey != null)
            {
               // Approved 
                if (source.Approved == true)
                {
                    dbServey.Approved = source.Approved;
                    dbServey.ApprovalDate = source.ApprovalDate;
                    dbServey.ApprovedByUserId = surveyQuestionRepository.LoggedInUserIdentity;
                    dbServey.Status = (Int32)AdCampaignStatus.Live;
                } // Rejected 
                else
                {
                    dbServey.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbServey.Approved = false;
                    dbServey.RejectionReason = source.RejectionReason;
                }
                dbServey.ModifiedDate = DateTime.Now;
                dbServey.ModifiedBy = surveyQuestionRepository.LoggedInUserIdentity;
            }
            surveyQuestionRepository.SaveChanges();
            return surveyQuestionRepository.Find(source.SqId);
        }
        #endregion
    }

}
