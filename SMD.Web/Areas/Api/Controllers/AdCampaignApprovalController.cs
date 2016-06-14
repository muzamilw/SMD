using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class AdCampaignApprovalController : ApiController
    {
           #region Public
        private readonly IAdvertService _advertService;
        private readonly IEmailManagerService _emailManagerService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public AdCampaignApprovalController(IAdvertService advertService, IEmailManagerService emailManagerService)
        {
            _advertService = advertService;
            _emailManagerService = emailManagerService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Update Ad Campaign 
        /// </summary>
        public string Post(AdCampaign campaign)
        {
            if (campaign == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _advertService.UpdateAdApprovalCampaign(campaign.CreateFrom());
           
        }
        #endregion
    }
}
