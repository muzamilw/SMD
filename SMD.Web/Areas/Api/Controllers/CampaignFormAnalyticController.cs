using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CampaignFormAnalyticController : ApiController
    {
        // GET api/<controller>
        public FormAnalyticResponseModel Get()
        {
            FormAnalyticResponseModel data = new FormAnalyticResponseModel();

            return data;
        }

        // GET api/<controller>/5
    }
}