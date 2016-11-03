using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface INotificationRepository : IBaseRepository<Notification, long>
    {

        IEnumerable<Notification> GetAllUnReadNotificationsByUserId(string UserId);

    }
}
