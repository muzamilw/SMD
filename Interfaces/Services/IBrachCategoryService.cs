using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface IBrachCategoryService
    {
        List<BranchCategory> GetAllBranchCategories();
        long CreateCategory(BranchCategory categoryName);
        long UpdateCategory(BranchCategory category);
        bool DeleteCategory(BranchCategory category);
    }
}
