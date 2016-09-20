using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiModel = SMD.MIS.Areas.Api.Models;
using DomainModel = SMD.Models.DomainModels;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class BranchCategoryController : ApiController
    {
        private readonly IBrachCategoryService _branchcategoryservice;
        private readonly ICompanyBranchService _companyBranchservice;

        public BranchCategoryController(IBrachCategoryService _branchcategoryservice, ICompanyBranchService _companyBranchservice)
        {
            this._branchcategoryservice = _branchcategoryservice;
            this._companyBranchservice = _companyBranchservice;

        }

        public List<ApiModel.BranchCategoryResponseModel> Get()
        {
            List<ApiModel.BranchCategoryResponseModel> lst = new List<ApiModel.BranchCategoryResponseModel>();
            var GetBrachCategories = _branchcategoryservice.GetAllBranchCategories();
            Mapper.Initialize(cfg => cfg.CreateMap<DomainModel.BranchCategory, ApiModel.BranchCategory>());

            foreach (var Category in GetBrachCategories)
            {
                var mapSection = Mapper.Map<DomainModel.BranchCategory, ApiModel.BranchCategory>(Category);
                lst.Add(new ApiModel.BranchCategoryResponseModel
                {
                    BranchCategoryId = mapSection.BranchCategoryId,
                    BranchCategoryName = mapSection.BranchCategoryName,
                    CompanyId = mapSection.CompanyId,
                    CompanyBranches = new List<ApiModel.CompanyBranch>()
                });
                Mapper.Initialize(cfg => cfg.CreateMap<DomainModel.BranchCategory, ApiModel.BranchCategory>());

            }

            Mapper.Initialize(cfg => cfg.CreateMap<DomainModel.CompanyBranch, ApiModel.CompanyBranch>());
            foreach (var comp in GetBrachCategories)
            {
                var compBranches = GetBrachCategories.Where(a => a.BranchCategoryId == comp.BranchCategoryId).SelectMany(s => s.CompanyBranches).ToList();
                var mapped = compBranches.Select(a => GetMapped(a)).ToList();
                var curListItem = lst.FirstOrDefault(br => br.BranchCategoryId == comp.BranchCategoryId);
                if (curListItem != null)
                {
                    curListItem.CompanyBranches = mapped;
                }

            }

            return lst;

        }
        private ApiModel.CompanyBranch GetMapped(DomainModel.CompanyBranch source)
        {
            return Mapper.Map<DomainModel.CompanyBranch, ApiModel.CompanyBranch>(source);
        }
        public ApiModel.CompanyBranch Get([FromUri]long branchId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DomainModel.CompanyBranch, ApiModel.CompanyBranch>());
            var obj = _companyBranchservice.GetBranchsByCategoryId(branchId);
            return Mapper.Map<DomainModel.CompanyBranch, ApiModel.CompanyBranch>(obj);
        }


        public long Post(CompanyBranch branch)
        {


            Mapper.Initialize(cfg => cfg.CreateMap<ApiModel.CompanyBranch, DomainModel.CompanyBranch>());
            var mappedBranch = Mapper.Map<ApiModel.CompanyBranch, DomainModel.CompanyBranch>(branch);
            if (branch.BranchId > 0)
            {

                return _companyBranchservice.UpdateBranchAddress(mappedBranch);

            }
            else
            {

                return _companyBranchservice.CreateBranchAddress(mappedBranch);

            }

        }
        public Boolean Delete(CompanyBranch branch)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ApiModel.CompanyBranch, DomainModel.CompanyBranch>());
            var mappedBranch = Mapper.Map<ApiModel.CompanyBranch, DomainModel.CompanyBranch>(branch);
            _companyBranchservice.DeleteCompanyBranch(mappedBranch);
            return true;
        }

    }
}
