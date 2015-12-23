using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.Common;
using SMD.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CampaignController : ApiController
    {
        #region Public
        private readonly IAdvertService _campaignService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CampaignController(IAdvertService campaignService)
        {
            _campaignService = campaignService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get base data for campaigns 
        /// 
        /// </summary>
        public CampaignRequestResponseModel Get([FromUri] AdCampaignSearchRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                if (request.CampaignId > 0)
                {
                    return _campaignService.GetCampaignById(request.CampaignId).CreateCampaignFrom();
                }
                else 
                {
                    return _campaignService.GetCampaigns(request).CreateCampaignFrom();
                }
               
            }
           
        }


        public void Post(SMD.Models.DomainModels.AdCampaign campaignModel)
        {
            //campaignModel.Status = (int)AdCampaignStatus.Draft;
            if (campaignModel.CampaignId > 0)
            {
                _campaignService.UpdateCampaign(campaignModel);

            }
            else 
            {
                _campaignService.CreateCampaign(campaignModel);

            }
           
        }
        #endregion
    }
}
