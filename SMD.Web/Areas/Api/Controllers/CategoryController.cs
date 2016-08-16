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
    public class CategoryController : ApiController
    {
        private readonly IBrachCategoryService _branchcategoryservice;

        public CategoryController(IBrachCategoryService _branchcategoryservice)
        {
            this._branchcategoryservice = _branchcategoryservice;


        }
        public long Post(BranchCategory category)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ApiModel.BranchCategory, DomainModel.BranchCategory>());
            var mappedBranch = Mapper.Map<ApiModel.BranchCategory, DomainModel.BranchCategory>(category);
            if (category.BranchCategoryId > 0)
            {

                return _branchcategoryservice.UpdateCategory(mappedBranch);

            }
            else
            {

                return _branchcategoryservice.CreateCategory(mappedBranch);

            }

        }

        public Boolean Delete(BranchCategory category)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ApiModel.BranchCategory, DomainModel.BranchCategory>());
            var mappedBranch = Mapper.Map<ApiModel.BranchCategory, DomainModel.BranchCategory>(category);
            _branchcategoryservice.DeleteCategory(mappedBranch);
            return true;
        }
    }
}
