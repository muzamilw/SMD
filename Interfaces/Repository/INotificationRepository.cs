using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface INotificationRepository : IBaseRepository<Notification, long>
    {

        IEnumerable<vw_Notifications> GetAllUnReadNotificationsByUserId(string UserId, string PhoneNumber);


        Notification GetNotificationBySurveyQuestionShareId(long SurveyQuestionShareId);

        bool UserHasNotifications(string UserId);

    }
}
