using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IIndustryRepository : IBaseRepository<Industry, int>
    {
        /// <summary>
        /// Search Invoces for LV
        /// </summary>
        IEnumerable<Industry> SearchIndustries(string input);
    }


}
