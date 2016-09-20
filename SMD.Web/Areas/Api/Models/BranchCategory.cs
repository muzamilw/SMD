using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class BranchCategory
    {
        public long BranchCategoryId { get; set; }
        public string BranchCategoryName { get; set; }
        public Nullable<int> CompanyId { get; set; }

        public Company Company { get; set; }
        //public ICollection<CompanyBranch> CompanyBranches { get; set; }
    }
}