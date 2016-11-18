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

        /// <summary>
        /// Get List of City Of a Country
        /// </summary>
        public IEnumerable<City> GetAllCitiesOfCountry(long countryId)
        {
            return DbSet.Where(city => city.CountryId == countryId).ToList();
        }
        public IEnumerable<City> GetCities()
        {
            long id = 20;
            return DbSet.Where(city => city.CountryId == id).OrderBy(g=>g.CityName).ToList();
        }
        public int GetCityId(string name)
        {
            int cityId = 0;
            City city = DbSet.Where(g => g.CityName.ToLower() == name.ToLower()).SingleOrDefault();
            if (city != null)
            {
                cityId = city.CityId;
            }
            else
            {
                city = new City();
                city.CityName = name;
                db.Cities.Add(city);
                db.SaveChanges();
                cityId = city.CityId;
            }
            return cityId;
        }
        #endregion
    }
}
