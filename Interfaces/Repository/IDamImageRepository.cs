using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Country Repository Interface 
    /// </summary>
    public interface IDamImageRepository : IBaseRepository<DamImage, long>
    {
        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        List<DamImage> getAllImages(int mode);
     
    }
}
