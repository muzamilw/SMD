using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class BrachCategoryService : IBrachCategoryService
    {
        /// </summary>
        private readonly IBranchCategoryRepository _branchcategoryrepository;


        /// <summary>
        ///  Constructor
        /// </summary>
        public BrachCategoryService(IBranchCategoryRepository _branchcategoryrepository)
        {
            this._branchcategoryrepository = _branchcategoryrepository;
        }

        public List<BranchCategory> GetAllBranchCategories()
        {

            return _branchcategoryrepository.GetAllBranchCategories();
        }
        public long CreateCategory(BranchCategory category)
        {
            try
            {
                BranchCategory newCategory = new BranchCategory();
                newCategory.BranchCategoryName = category.BranchCategoryName;
                newCategory.CompanyId = _branchcategoryrepository.CompanyId;
                _branchcategoryrepository.Add(newCategory);
                _branchcategoryrepository.SaveChanges();
                long id = newCategory.BranchCategoryId;
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public long UpdateCategory(BranchCategory category)
        {
            try
            {
                category.CompanyId = _branchcategoryrepository.CompanyId;
                _branchcategoryrepository.Update(category);
                _branchcategoryrepository.SaveChanges();
                return 1; ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteCategory(BranchCategory category)
        {

            var delCategory = _branchcategoryrepository.Find(category.BranchCategoryId);
            if (delCategory != null)
                _branchcategoryrepository.Delete(delCategory);
            _branchcategoryrepository.SaveChanges();
            return true;
        }
    }
}
