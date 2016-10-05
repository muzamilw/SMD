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
        private readonly ITransactionService ITransactionService;
        #endregion

        public RevenueController(ITransactionService _ITransactionService)
        {
            this.ITransactionService = _ITransactionService;
        }
        // GET: api/Revenue
        public IEnumerable<getPayoutVSRevenueOverTime_Result> Get(int granuality, DateTime DateFrom, DateTime DateTo)
        {

            return ITransactionService.getPayoutVSRevenueOverTime(DateFrom, DateTo, granuality);
        }
        public  IEnumerable<GetRevenueByCampaignOverTime_Result> Get(int compnyId, int CampaignType, int Granularity, DateTime DateFrom, DateTime DateTo)
        {

            return ITransactionService.GetRevenueByCampaignOverTime(compnyId, CampaignType, DateFrom, DateTo, Granularity);
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
