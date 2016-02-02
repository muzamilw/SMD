using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.MIS.Areas.Api.Models;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class IndustryController : ApiController
    {
          #region Public
        private readonly IIndustryService _industryService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public IEnumerable<SMD.MIS.Areas.Api.Models.Industry> Get()
        {
            return _industryService.GetIndustryList().Select(lang => lang.CreateFrom());
        }


        #endregion
    }
}
