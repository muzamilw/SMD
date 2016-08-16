using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IBranchCategoryRepository : IBaseRepository<BranchCategory, long>
    {
        List<BranchCategory> GetAllBranchCategories();
        

    }
}
