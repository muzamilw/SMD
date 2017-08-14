using System.Collections.Generic;
using System.Linq;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Net;
using System.Web;
using System.Web.Http;
using System;
using AutoMapper;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;

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

                Mapper.Initialize(cfg => cfg.CreateMap<SearchCampaigns_Result, Models.SearchCampaigns>());

            

                if (request.CampaignId > 0)
                {
                    return _campaignService.GetCampaignById(request.CampaignId).CreateCampaignFrom();
                }
                else 
                {
                    var result =  _campaignService.SearchCampaigns(request);

                    var response = new CampaignRequestResponseModel();
                    response.CampaignsList = result.Campaign.Select(a => Mapper.Map<SearchCampaigns_Result, Models.SearchCampaigns>(a));
                    response.TotalCount = result.TotalCount;

                    return response;


                }
               
            }
           
        }

        //GetLiveCampaignCountOverTime
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
