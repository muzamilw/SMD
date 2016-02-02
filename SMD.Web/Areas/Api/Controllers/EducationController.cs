using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMD.MIS.ModelMappers;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class EducationController : ApiController
    {
            #region Public
        private readonly IEducationService _educationService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public IEnumerable<SMD.MIS.Areas.Api.Models.Education> Get()
        {
            return _educationService.GetEducationsList().Select(lang => lang.CreateFrom());
        }


        #endregion
    }
}
