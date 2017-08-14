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
    public class GetRandomAdCampaignController : ApiController
    {
        #region Attributes
        private readonly IAdvertService _advertService;
        #endregion

        #region Constructor

        public GetRandomAdCampaignController(IAdvertService advertService)
        {
            _advertService = advertService;
        }
        #endregion

        #region Methods

        public List<GetRandomAdCampaign_Result> Get(int type)
        {
            var obj = _advertService.GetRandomAdCampaign(type);
            return obj;
        }
        #endregion
    }
}
