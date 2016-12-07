using System.Net;
using System.Threading.Tasks;
using System.Web;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Web.Http;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;
using AutoMapper;
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class testController : ApiController
    {
        #region Private

       
        private readonly IEmailManagerService emailService; 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public testController(IEmailManagerService emailService)
        {


            this.emailService = emailService;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Login
        /// </summary>
        [ApiExceptionCustom]
        public string Get(int mode)
        {


            emailService.SendNewDealsEmail(mode);
            return "success";

        }

        #endregion
    }
}
