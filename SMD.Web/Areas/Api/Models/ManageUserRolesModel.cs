using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class ManageUserRolesModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string email { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }

        public DateTime CreatedOn { get; set; }
        public string status { get; set; }

        public int CompanyId { get; set; }

        public string RoleId { get; set; }
    }
}