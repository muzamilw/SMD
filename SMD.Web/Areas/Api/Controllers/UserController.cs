using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class UserController : ApiController
    {
        #region Private

        private readonly IActiveUser activeUser;
        private readonly IManageUserService _IManageUserService;

        #endregion

        #region Constructor 

        public UserController(IActiveUser activeUser, IManageUserService _IManageUserService) {
            this.activeUser = activeUser;
            this._IManageUserService = _IManageUserService;
        }

        #endregion

        // GET: api/ActiveUser
        public IEnumerable<GetUserCounts_Result> Get()
        {
            return _IManageUserService.GetUserCounts();
        }

        public IEnumerable<getUserActivitiesOverTime_Result> getUserActivitiesOverTime(int granuality, DateTime DateFrom, DateTime DateTo)
        {
            return _IManageUserService.getUserActivitiesOverTime(DateFrom, DateTo, granuality);
        }

        // GET: api/ActiveUser/5
        public string Get(int id)
        {
            return "value";
        }




        // POST: api/ActiveUser
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ActiveUser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ActiveUser/5
        public void Delete(int id)
        {
        }
    }
}
