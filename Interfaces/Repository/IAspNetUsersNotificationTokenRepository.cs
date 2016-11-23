using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMD.Interfaces.Repository
{
    public interface IAspNetUsersNotificationTokenRepository : IBaseRepository<AspNetUsersNotificationToken, long>
    {
        List<AspNetUsersNotificationToken> NotificationTokensByUserId(string UserId);
    }
}
