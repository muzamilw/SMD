using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class ManageUserRolesModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }
    }
}