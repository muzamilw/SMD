using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class RevenueController : ApiController
    {
        // GET: api/Revenue
        public IEnumerable<ResultDataTest> Get(int granuality, DateTime DateFrom, DateTime DateTo)
        {
            List<ResultDataTest> list = new List<ResultDataTest>();

            ResultDataTest obj1 = new ResultDataTest(30,"2016-08-28");
            ResultDataTest obj2 = new ResultDataTest(7,"2016-09-08");
            ResultDataTest obj3 = new ResultDataTest(3, "2016-09-15");
            list.Add(obj1);
            list.Add(obj2);
            list.Add(obj3);

            return list;
        }

        // GET: api/Revenue/5
        //public string Get()
        //{
        //    return "value";
        //}

        // POST: api/Revenue
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Revenue/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Revenue/5
        public void Delete(int id)
        {
        }
    }
    public class ResultDataTest
    {
        public int amountcollected { get; set; }
       
        public string granular { get; set; }
        
        public ResultDataTest(int amountcollected, string granular)
        {
           this.amountcollected = amountcollected;
           this.granular = granular;
        }
    }
}
