using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class ProfileQuestionGroupAppController : ApiController
    {
        #region Attributes 
        private readonly IProfileQuestionService _profileQuestionService;
        #endregion

        #region Constructor
        public ProfileQuestionGroupAppController(IProfileQuestionService _profileQuestionService)
        {
          this._profileQuestionService = _profileQuestionService;
        
        }
        #endregion

        #region Methods
        public ProfileQuestionGroupApp Get(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.ProfileQuestionGroup, ProfileQuestionGroupApp>());
            var obj = _profileQuestionService.GetPQGroupById(id);
            return Mapper.Map<SMD.Models.DomainModels.ProfileQuestionGroup, ProfileQuestionGroupApp>(obj);
        }

        #endregion
    }
}
