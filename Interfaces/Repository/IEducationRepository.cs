using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
  
    public interface IEducationRepository : IBaseRepository<Education, long>
    {
        /// <summary>
        /// Search Invoces for LV
        /// </summary>
        IEnumerable<Education> SearchEducation(string input);

        /// <summary>
        /// Get List of Language 
        /// </summary>
        IEnumerable<Education> GetAllEducations();

        /// <summary>
        /// Get All Active ones
        /// </summary>
        IEnumerable<Education> GetAllAvailable();
    }
}
