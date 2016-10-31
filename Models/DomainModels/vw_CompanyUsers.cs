using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    public class vw_CompanyUsers
    {
        public long id { get; set; }
        public string UserId { get; set; }
        public string email { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }

        public DateTime CreatedOn { get; set; }
        public string status { get; set; }

        public int companyid { get; set; }


        public string CompanyName { get; set; }

        public string RoleId { get; set; }

        public string Logo { get; set; }


    }
}
