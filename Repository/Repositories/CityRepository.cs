using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
         #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CityRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<City> DbSet
        {
            get { return db.Cities; }
        }
        #endregion
        #region Public
        public City Find(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<City> GetAllCities()
        {
            return DbSet.Select(city => city).ToList();
        }

        /// <summary>
        /// Get List of searched cities 
        /// </summary>
        public IEnumerable<City> GetSearchCities(string searchText)
        {
            return DbSet.Where(city => city.CityName.StartsWith(searchText)).ToList();
        }
        #endregion
    }
}
