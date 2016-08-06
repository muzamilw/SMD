

using SMD.Interfaces.Services;
using ApiModel = SMD.MIS.Areas.Api.Models;
using DomainModel = SMD.Models.DomainModels;
using SMD.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;

namespace SMD.MIS.Areas.Common.Controllers
{
    public class PhaseLibraryController : ApiController
    {

        private readonly ISectionService _sectionService;
        private readonly IPhraseService _phraseService;
        public PhaseLibraryController(ISectionService _sectionService, IPhraseService _phraseService)
        {
            this._phraseService = _phraseService;
            this._sectionService = _sectionService;
        }

        
        public List<ApiModel.Section> Get()
        {
           Mapper.Initialize(cfg => cfg.CreateMap<DomainModel.Section, ApiModel.Section>());
           var GetSections = _sectionService.GetAllSections();
           return GetSections.Select(a => Mapper.Map<DomainModel.Section, ApiModel.Section>(a)).ToList();
        }


        public List<ApiModel.Phrase> Get([FromUri]long PhraseID)
        {
           // return _phraseService.GetAllPhrasesByID(PhraseID);
            return null;
        }

        public int Post(ApiModel.Phrase Phrase)
        {
            //FormCollection
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                if (Phrase.PhraseId > 0)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ApiModel.Phrase, DomainModel.Phrase>());
                    _phraseService.CreatePhrase(Mapper.Map<ApiModel.Phrase, DomainModel.Phrase>(Phrase));
                }
                else
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ApiModel.Phrase, DomainModel.Phrase>());
                    _phraseService.EditPhrase(Mapper.Map<ApiModel.Phrase, DomainModel.Phrase>(Phrase));

                }
            }
            return 1;
        }



    }
}
