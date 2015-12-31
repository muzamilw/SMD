using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
  
    public interface IEducationRepository : IBaseRepository<Education, int>
    {
        /// <summary>
        /// Search Invoces for LV
        /// </summary>
        IEnumerable<Education> SearchEducation(string input);
    }
}
