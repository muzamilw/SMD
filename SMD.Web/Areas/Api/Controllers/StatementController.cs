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
    public class StatementController : ApiController
    {
        #region Private

        private readonly IWebApiUserService webApiUserService;
        private readonly ITransactionService transactionService; 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StatementController(ITransactionService transactionService)
        {


            this.transactionService = transactionService;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Login
        /// </summary>
        [ApiExceptionCustom]
        public async Task<StatementInquiryResponse> Get(string authenticationToken, [FromUri] UserTransactionInquiryRequest request)
        {
            if (request == null || !ModelState.IsValid || string.IsNullOrEmpty(authenticationToken))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            //LoginResponse response = await webApiUserService.GetById(request.UserId);
            //return response.CreateFromForStatementBalance();


            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.GetTransactions_Result, StatementTrasaction>());

            try
            {
                var trans = transactionService.GetUserVirtualTransactions(request.CompanyId);

                return new StatementInquiryResponse
                {
                    Status = true,
                    Message = "Success",
                    Balance = trans[0].CurrentBalance,
                    Transactions = Mapper.Map<List<SMD.Models.DomainModels.GetTransactions_Result>, List<StatementTrasaction>>(trans)
                };
            }
            catch (Exception e)
            {

                return new StatementInquiryResponse
                {
                    Status = false ,
                    Message = "Error :" + e.ToString(),
                    Balance = 0,
                    Transactions = null
                };
            }



          


        }

        #endregion
    }
}
