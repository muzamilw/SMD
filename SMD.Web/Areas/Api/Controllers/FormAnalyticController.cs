using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.Models.DomainModels;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class FormAnalyticController : ApiController
    {
        #region
        private readonly ICityService _ICityService;
        private readonly IActiveUser _IActiveUser;
        private readonly IAdvertService _IAdvertService;
        private readonly IAdCampaignResponseService _IAdCampaignResponseService;
        #endregion
        public FormAnalyticController(ICityService _ICityService, IActiveUser _IActiveUser, IAdvertService IAdvertService, IAdCampaignResponseService IAdCampaignResponseService)
        {
            this._IActiveUser = _IActiveUser;
            this._ICityService = _ICityService;
            this._IAdvertService = IAdvertService;
            this._IAdCampaignResponseService = IAdCampaignResponseService;
        }
        // GET: api/FormAnalytic
        public FormAnalyticDDResponseModel Get(long Id)
        {
            FormAnalyticDDResponseModel data = new FormAnalyticDDResponseModel();
            List<string> citiesList = _ICityService.GetTargetCitiesPerCampaign(Id);
            List<CityDD> Citieslst = new List<CityDD>(); 
            CityDD cityitem = new CityDD("All");
            Citieslst.Add(cityitem);
            foreach(string item in citiesList){
                if (item != null)
                {
                    cityitem = new CityDD(item);
                }
                 Citieslst.Add(cityitem);
            }
            data.Cities = Citieslst;
            IEnumerable<String> prfList =_IActiveUser.getProfessions();
            List<Profession> Prof = new List<Profession>();
            Profession prfItem = new Profession("All");
            Prof.Add(prfItem);
            foreach(String item in prfList){
                if (item != null)
                {
                    prfItem = new Profession(item); 
                }
                Prof.Add(prfItem);
            }
            data.Profession = Prof;
            SMD.Models.DomainModels.AdCampaign campaign = _IAdvertService.GetAdCampaignById(Id);
            data.Question = campaign.VerifyQuestion;
            List<QuizChoice> ChoicesList = new List<QuizChoice>();
            QuizChoice chc0 = new QuizChoice("All", 0);
            ChoicesList.Add(chc0);
            if (campaign.Answer1 != null) {
                QuizChoice chc1 = new QuizChoice(campaign.Answer1, 1);
                ChoicesList.Add(chc1);
            }
            if (campaign.Answer2 != null) {
                QuizChoice chc2 = new QuizChoice(campaign.Answer2, 2);
                ChoicesList.Add(chc2);
            }
            
            if (campaign.Answer3 != null) {
                QuizChoice chc3 = new QuizChoice(campaign.Answer3, 3);
                ChoicesList.Add(chc3);
            }
            data.Choices = ChoicesList;

            List<getCampaignByIdFormDataAnalytic_Result> list =  _IAdvertService.getCampaignByIdFormDataAnalytic(Id);
            List<FormDataAnalyticResponse> listformData = new List<FormDataAnalyticResponse>(); 
            foreach (getCampaignByIdFormDataAnalytic_Result item in list) {
                int stat = _IAdCampaignResponseService.getCampaignByIdQQFormAnalytic(Id, 0, 0, 0, "All", "All", item.typ, item.Id!= null ? (int)item.Id : 0);
                FormDataAnalyticResponse dataItem = new FormDataAnalyticResponse(item.Question, item.answer, item.Id != null ? (long)item.Id : 0, item.typ, stat);
                listformData.Add(dataItem);
            }
            data.formData = listformData;
            return data;
        }

        // GET: api/FormAnalytic/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/FormAnalytic
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FormAnalytic/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FormAnalytic/5
        public void Delete(int id)
        {
        }
    }
}
