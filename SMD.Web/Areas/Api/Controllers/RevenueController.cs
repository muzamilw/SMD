using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
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
        #region private
        private readonly IDashboardService IDashboardService;
        #endregion

        public RevenueController(IDashboardService _IDashboardService)
        {
            this.IDashboardService = _IDashboardService;
        }
        // GET: api/Revenue
        public IEnumerable<GetRevenueOverTime_Result> Get(int granuality, DateTime DateFrom, DateTime DateTo)
        {
           
            return IDashboardService.GetRevenueOverTime(466, DateFrom, DateTo, granuality);
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
    //public class ResultDataTest
    //{
    //    public int amountcollected { get; set; }
       
    //    public string granular { get; set; }
        
    //    public ResultDataTest(int amountcollected, string granular)
    //    {
    //       this.amountcollected = amountcollected;
    //       this.granular = granular;
    //    }
    //}
}
