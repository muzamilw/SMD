using SMD.Interfaces.Services;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class ActiveUserController : ApiController
    {
        #region Private

        private readonly IActiveUser activeUser;

        #endregion

        #region Constructor 

        public ActiveUserController(IActiveUser activeUser) {
            this.activeUser = activeUser;
        }

        #endregion

        // GET: api/ActiveUser
        public ActiveUserResponseModel Get()
        {
          ActiveUserResponseModel usr= activeUser.getActiveUser();


            return activeUser.getActiveUser();
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
