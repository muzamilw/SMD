using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface INotificationService
    {

        List<vw_Notifications> GetNotificationsByUserId(string UserId, string PhoneNumber);

        bool UserHasNotifications(string UserId);
    }
}
