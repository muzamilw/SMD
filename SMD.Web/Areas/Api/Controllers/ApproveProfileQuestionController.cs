using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class ApproveProfileQuestionController : ApiController
    {

        #region Attributes
        private readonly IProfileQuestionService _profileQuestionService;
        #endregion

        #region Constructor
        public ApproveProfileQuestionController(IProfileQuestionService _profileQuestionService)
        {
          this._profileQuestionService = _profileQuestionService;
        
        }
        #endregion

        #region Method

        public PQResponseModelForApproval Get([FromUri]GetPagedListRequest request)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.ProfileQuestion, ApproveProfileQuestion>());
            var obj = _profileQuestionService.GetProfileQuestionForAproval(request);
            var retobj = new PQResponseModelForApproval();
            foreach (var item in obj.Coupons)
            {
                retobj.ProfileQuestion.Add(Mapper.Map<SMD.Models.DomainModels.ProfileQuestion, ApproveProfileQuestion>(item));
            }
            retobj.TotalCount = obj.TotalCount;
            return retobj;
        }
        #endregion

    }
}
