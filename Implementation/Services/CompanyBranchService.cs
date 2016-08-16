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
    public class CompanyBranchService : ICompanyBranchService
    {
        private readonly ICompanyBranchRepository _companybranchrepository;
        public CompanyBranchService(ICompanyBranchRepository _companybranchrepository)
        {
            this._companybranchrepository = _companybranchrepository;

        }

        public CompanyBranch GetBranchsByCategoryId(long id)
        {
            return _companybranchrepository.GetBranchsByCategoryId(id);

        }
        public long UpdateBranchAddress(CompanyBranch branch)
        {
            try
            {
                _companybranchrepository.Update(branch);
                _companybranchrepository.SaveChanges();
                return 0; ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public long CreateBranchAddress(CompanyBranch branch)
        {
            CompanyBranch model = new CompanyBranch();
            model.BranchTitle = branch.BranchTitle;
            model.BranchAddressLine1 = branch.BranchAddressLine1;
            model.BranchAddressLine2 = branch.BranchAddressLine2;
            model.BranchCity = branch.BranchCity;
            model.BranchState = branch.BranchState;
            model.BranchZipCode = branch.BranchZipCode;
            model.BranchPhone = branch.BranchPhone;
            model.BranchLocationLat = branch.BranchLocationLat;
            model.BranchLocationLong = branch.BranchLocationLong;
            model.BranchCategoryId = branch.BranchCategoryId;
            model.CompanyId = _companybranchrepository.CompanyId;
            _companybranchrepository.Add(model);
            _companybranchrepository.SaveChanges();
            long id = model.BranchId;
            return id;

        }
        public bool DeleteCompanyBranch(CompanyBranch branch)
        {
            var delBranch = _companybranchrepository.Find(branch.BranchId);
            if (delBranch != null)
                _companybranchrepository.Delete(delBranch);
            _companybranchrepository.SaveChanges();
            return true;
        }

    }
}
