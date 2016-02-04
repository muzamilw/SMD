﻿using SMD.Implementation.Services;
using System.Web.Http;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Perform Collection
    /// </summary>
    public class PayoutController : ApiController
    {

        #region Constructor

        #endregion
        
        #region Public

        /// <summary>
        /// Get the collection
        /// </summary>
        [ApiExceptionCustom]
        public void Get()
        {
            PayOutScheduler.PerformCredit();
        }

        #endregion
    }
}