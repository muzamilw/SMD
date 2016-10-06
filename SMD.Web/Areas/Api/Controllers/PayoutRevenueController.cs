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
    public class PayoutRevenueController : ApiController
    {
        #region private
        private readonly ITransactionService ITransactionService;
        #endregion
        // GET api/<controller>
        public PayoutRevenueController(ITransactionService _ITransactionService)
        {
            this.ITransactionService = _ITransactionService;
        }
        public IEnumerable<getPayoutVSRevenueOverTime_Result> Get(int granuality, DateTime DateFrom, DateTime DateTo)
        {

            return ITransactionService.getPayoutVSRevenueOverTime(DateFrom, DateTo, granuality);
        }
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}