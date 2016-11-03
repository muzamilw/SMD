using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMD.Implementation.Services
{
    public class SharedSurveyQuestionService : ISharedSurveyQuestionService 
    {

        private readonly ISharedSurveyQuestionRepository sharedSurveyQuestionRepository;
        private readonly ISurveySharingGroupShareRepository surveySharingGroupShareRepository;


        public SharedSurveyQuestionService(ISharedSurveyQuestionRepository sharedSurveyQuestionRepository, ISurveySharingGroupShareRepository surveySharingGroupShareRepository)
        {
            this.sharedSurveyQuestionRepository = sharedSurveyQuestionRepository;
            this.surveySharingGroupShareRepository = surveySharingGroupShareRepository;
        }


        public bool Create(SharedSurveyQuestion survey)
        {

            SaveSurveyImages(survey);
            survey.CreationDate = DateTime.Now;
            sharedSurveyQuestionRepository.Add(survey);
            sharedSurveyQuestionRepository.SaveChanges();

            return true;
        }




        public bool updateUserSharedSurveyQuestionResponse(long SurveyQuestionShareId, int UserSelection)
        {
            var share = surveySharingGroupShareRepository.Find(SurveyQuestionShareId);
            if ( share != null)
            {
                share.Status = 2;
                share.ResponseDateTime = DateTime.Now;
                share.UserSelection = UserSelection;

                surveySharingGroupShareRepository.Update(share);
                surveySharingGroupShareRepository.SaveChanges();


            }

            return true;
        }

        private string[] SaveSurveyImages(SharedSurveyQuestion question)
        {
            string[] savePaths = new string[2];
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/SurveyQuestions/" + question.SSQID);

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            if (!string.IsNullOrEmpty(question.LeftPicturePath) && !question.LeftPicturePath.Contains("guid_LeftPicture") && !question.LeftPicturePath.Contains("http://manage.cash4ads.com/"))
            {
                if (question.LeftPicturePath.Contains("SMD_Content"))
                {
                    string[] paths = question.LeftPicturePath.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    if (directoryPath != null && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    string savePath = directoryPath + "\\guid_LeftPicture.jpg";
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[0] = savePath;
                    question.LeftPicturePath = savePath;
                }
            }
            if (!string.IsNullOrEmpty(question.RightPicturePath) && !question.RightPicturePath.Contains("guid_RightPicture") && !question.RightPicturePath.Contains("http://manage.cash4ads.com/"))
            {
                if (question.RightPicturePath.Contains("SMD_Content"))
                {
                    string[] paths = question.RightPicturePath.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
                    string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
                    if (directoryPath != null && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    string savePath = directoryPath + "\\guid_RightPicture.jpg";
                    File.Copy(url, savePath, true);
                    int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                    savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                    savePaths[1] = savePath;
                    question.RightPicturePath = savePath;
                }
            }
            return savePaths;
        }
    }
}
