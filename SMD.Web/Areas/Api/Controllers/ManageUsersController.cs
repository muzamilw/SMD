﻿using System.Collections.Generic;
using System.Linq;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class ManageUsersController : ApiController
    {
        #region Public
        private readonly IManageUserService _manageUserService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public ManageUsersController(IManageUserService manageUserService)
        {
            this._manageUserService = manageUserService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public IEnumerable<ManageUserRolesModel> Get()
        {
           
            var domainList = _manageUserService.GetManageUsersList();
            return domainList.Select(a => a.CreateFrom()).ToList();
        }
        #endregion
    }
}
