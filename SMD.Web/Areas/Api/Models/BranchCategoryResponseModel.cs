using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class BranchCategoryResponseModel
    {
       
        public long BranchCategoryId { get; set; }
        public string BranchCategoryName { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public  List<CompanyBranch> CompanyBranches { get; set; }
        public CompanyBranch Branch { get; set; }
        public List<BranchCategory> BranchCategories { get; set; }
    }

        
    
}