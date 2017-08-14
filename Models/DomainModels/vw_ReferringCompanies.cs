using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class vw_ReferringCompanies
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> vcount { get; set; }
        public Nullable<int> scount { get; set; }
        public Nullable<int> pcount { get; set; }
        public Nullable<int> ReferringCompanyID { get; set; }
    }
}
