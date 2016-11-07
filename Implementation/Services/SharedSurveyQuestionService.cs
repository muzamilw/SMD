using SMD.Common;
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
        private readonly ISurveySharingGroupRepository surveySharingGroupRepository;
        private readonly INotificationRepository notificationRepository;


        public SharedSurveyQuestionService(ISharedSurveyQuestionRepository sharedSurveyQuestionRepository, ISurveySharingGroupShareRepository surveySharingGroupShareRepository, ISurveySharingGroupRepository surveySharingGroupRepository, INotificationRepository notificationRepository)
        {
            this.sharedSurveyQuestionRepository = sharedSurveyQuestionRepository;
            this.surveySharingGroupShareRepository = surveySharingGroupShareRepository;
            this.surveySharingGroupRepository = surveySharingGroupRepository;
            this.notificationRepository = notificationRepository;

        }


        public bool CreateAndSend(SharedSurveyQuestion survey)
        {

            
            survey.CreationDate = DateTime.Now;
            sharedSurveyQuestionRepository.Add(survey);
            sharedSurveyQuestionRepository.SaveChanges();

            var paths = SaveSurveyImages(survey);
            survey.LeftPicturePath = paths[0];
            survey.RightPicturePath = paths[1];

            sharedSurveyQuestionRepository.Update(survey);
            sharedSurveyQuestionRepository.SaveChanges();

            //adding and creating notification

            var group = surveySharingGroupRepository.Find(survey.SharingGroupId);

            if (group != null && group.SurveySharingGroupMembers != null && group.SurveySharingGroupMembers.Count > 0)
            {

                foreach (var item in group.SurveySharingGroupMembers)
                {
                    var share = new SurveySharingGroupShare { SSQID = survey.SSQID, SharingDate = DateTime.Now , SharingGroupId = survey.SharingGroupId, Status = 1, SharingGroupMemberId = item.SharingGroupMemberId, UserId = item.UserId };
                    surveySharingGroupShareRepository.Add(share);
                    surveySharingGroupShareRepository.SaveChanges();


                    var notification = new Notification { GeneratedOn = DateTime.Now, Type = 1, IsRead = false, PhoneNumber = item.PhoneNumber, SurveyQuestionShareId = share.SurveyQuestionShareId, GeneratedBy = "System", UserID = item.UserId };

                    notificationRepository.Add(notification);

                    
                }

                notificationRepository.SaveChanges();

            }

            return true;
        }


        public SharedSurveyQuestion GetSharedSurveyQuestion(long SSQID)
        {
            return new SharedSurveyQuestion();
        }

        //string mapPath = server.MapPath(smdContentPath + "/Users/" + requestData.CompanyId);


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
            string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Users/" + question.SSQID);

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }


            ImageHelper.SaveBase64(directoryPath + "\\LeftPicture.jpg", question.LeftPictureDataString);
            savePaths[0] = directoryPath + "\\LeftPicture." + question.LeftPictureExtention;

            ImageHelper.SaveBase64(directoryPath + "\\RightPicture.jpg", question.LeftPictureDataString);
            savePaths[1] = directoryPath + "\\RightPicture." + question.RightPictureExtention;

         
            
            //if (!string.IsNullOrEmpty(question.LeftPicturePath) && !question.LeftPicturePath.Contains("LeftPicture") && !question.LeftPicturePath.Contains("http://manage.cash4ads.com/"))
            //{
            //    if (question.LeftPicturePath.Contains("SMD_Content"))
            //    {
            //        string[] paths = question.LeftPicturePath.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
            //        string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
            //        if (directoryPath != null && !Directory.Exists(directoryPath))
            //        {
            //            Directory.CreateDirectory(directoryPath);
            //        }
            //        string savePath = directoryPath + "\\LeftPicture.jpg";
            //        File.Copy(url, savePath, true);
            //        int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
            //        savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
            //        savePaths[0] = savePath;
            //        question.LeftPicturePath = savePath;

                    
            //    }
            //}
            //if (!string.IsNullOrEmpty(question.RightPicturePath) && !question.RightPicturePath.Contains("RightPicture") && !question.RightPicturePath.Contains("http://manage.cash4ads.com/"))
            //{
            //    if (question.RightPicturePath.Contains("SMD_Content"))
            //    {
            //        string[] paths = question.RightPicturePath.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
            //        string url = HttpContext.Current.Server.MapPath("~/SMD_Content/" + paths[paths.Length - 1]);
            //        if (directoryPath != null && !Directory.Exists(directoryPath))
            //        {
            //            Directory.CreateDirectory(directoryPath);
            //        }
            //        string savePath = directoryPath + "\\RightPicture.jpg";
            //        File.Copy(url, savePath, true);
            //        int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
            //        savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
            //        savePaths[1] = savePath;
            //        question.RightPicturePath = savePath;
            //    }
            //}
            return savePaths;
        }


        public bool DeleteSharedSurveyQuestion(long SSQID)
        {

            //delete the images first


            //delete the responses
            return sharedSurveyQuestionRepository.DeleteSharedSurveyQuestion(SSQID);

          
        }
    }
}
