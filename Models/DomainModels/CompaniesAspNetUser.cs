using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;

namespace SMD.Models.DomainModels
{
    public class CompaniesAspNetUser
    {

        public long Id { get; set; }
       
        public int CompanyId { get; set; }
        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }


        public string InvitationCode { get; set; }

        public string RoleId { get; set; }



        public virtual User AspNetUser { get; set; }

        public virtual Company Company { get; set; }
        public virtual Role AspNetRole { get; set; }
    }
}
