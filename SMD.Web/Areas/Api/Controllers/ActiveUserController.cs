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
        // GET: api/ActiveUser
        public IEnumerable<Int32> Get()
        {
            return new Int32[] { 1, 2 };
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
