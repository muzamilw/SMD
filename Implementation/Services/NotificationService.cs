using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
     public class NotificationService : INotificationService
    {

         private readonly INotificationRepository notificationRepository;
       

         public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
            
        }

         public List<vw_Notifications> GetNotificationsByUserId(string UserId, string PhoneNumber)
         {
            


             return notificationRepository.GetAllUnReadNotificationsByUserId(UserId, PhoneNumber).OrderByDescending( g=> g.GeneratedOn).ToList();
         }


         public bool UserHasNotifications(string UserId)
         {
             return notificationRepository.UserHasNotifications(UserId);
         }

    
    }
}
