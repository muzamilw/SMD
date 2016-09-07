using AutoMapper;
using SMD.Interfaces.Repository;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetProductController : ApiController
    {
        #region Attributes
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public GetProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Methods
        public Product Get()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.Product, Product>());
            var pro = _productRepository.GetProductByCountryId("PQID");
            return Mapper.Map<SMD.Models.DomainModels.Product, Product>(pro);
        }
        #endregion
    }
}
