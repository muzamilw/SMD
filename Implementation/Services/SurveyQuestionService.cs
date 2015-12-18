using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


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
        private readonly IEmailManagerService emailManagerService;

        private string[] SaveSurveyImages(SurveyQuestion question)
        {
            string[] savePaths = new string[2];
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/SurveyQuestions/" + question.SqId);
            
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (question.LeftPictureBytes != null)
            {
                string base64 = question.LeftPictureBytes.Substring(question.LeftPictureBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string savePath = directoryPath + "\\LeftPicture.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[0] = savePath;
            }
            if (question.RightPictureBytes != null)
            {
                string base64 = question.RightPictureBytes.Substring(question.RightPictureBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\RightPicture.jpg";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                savePaths[1] = savePath;
            }
            return savePaths;
        }
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public SurveyQuestionService(ISurveyQuestionRepository _surveyQuestionRepository, ICountryRepository _countryRepository, ILanguageRepository _languageRepository, IEmailManagerService emailManagerService)
        {
            this.surveyQuestionRepository = _surveyQuestionRepository;
            this.languageRepository = _languageRepository;
            this.emailManagerService = emailManagerService;
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
        /// Get Survey Questions that need aprroval | baqer
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
        /// Edit Survey Question | baqer
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
                    emailManagerService.SendQuestionApprovalEmail(dbServey.UserId);

                } // Rejected 
                else
                {
                    dbServey.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbServey.Approved = false;
                    dbServey.RejectionReason = source.RejectionReason;
                    emailManagerService.SendQuestionRejectionEmail(dbServey.UserId);
                }
                dbServey.ModifiedDate = DateTime.Now;
                dbServey.ModifiedBy = surveyQuestionRepository.LoggedInUserIdentity;
            }
            surveyQuestionRepository.SaveChanges();
            return surveyQuestionRepository.Find(source.SqId);
        }

        public bool Create(SurveyQuestion survey)
        {
            try
            {
                survey.UserId = surveyQuestionRepository.LoggedInUserIdentity;
                survey.Type = (int)SurveyQuestionType.Advertiser;
                surveyQuestionRepository.Add(survey);
                surveyQuestionRepository.SaveChanges();
                string[] paths = SaveSurveyImages(survey);
               // return surveyQuestionRepository.updateSurveyImages(paths, survey.SqId);
                survey.LeftPicturePath = paths[0];
                survey.RightPicturePath = paths[1];
                surveyQuestionRepository.SaveChanges();
                return true;
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public SurveyQuestionEditResponseModel GetSurveyQuestion(long SqId)
        {
            SurveyQuestion Servey = surveyQuestionRepository.Get(SqId);
            Servey.SurveyQuestionResponses = null;
            foreach (var criteria in Servey.SurveyQuestionTargetCriterias)
            {
                criteria.SurveyQuestion = null;
            }
            foreach (var loc in Servey.SurveyQuestionTargetLocations)
            {
                loc.SurveyQuestion = null;
            }
            return new SurveyQuestionEditResponseModel
            {
                SurveyQuestionObj = Servey
           
            };
        }
        #endregion
    }

}
