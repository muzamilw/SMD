

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
using SMD.MIS.Areas.Api.Models;

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

            Mapper.Initialize(cfg => cfg.CreateMap<DomainModel.Phrase, ApiModel.Phrase>());
            var Phrases = _phraseService.GetAllPhrasesByID(PhraseID);
            return Phrases.Select(a => Mapper.Map<DomainModel.Phrase, ApiModel.Phrase>(a)).ToList();
            
        }

        public int Post(PhraseResponseModel Phrases)
        {
            int SectionID = 0;
            //FormCollection
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
               // Mapper.CreateMap<ApiModel.Phrase, DomainModel.Phrase>();
                Mapper.Initialize(cfg => cfg.CreateMap<ApiModel.Phrase, DomainModel.Phrase>());
                foreach (var phrase in Phrases.PhrasesList)
                {
                    if (phrase.IsDeleted)
                    {
                        var nphrase = Mapper.Map<ApiModel.Phrase, DomainModel.Phrase>(phrase);
                        _phraseService.DeletePhrase(phrase.PhraseId);

                        SectionID = phrase.SectionId??0;
                    }
                    else if (phrase.PhraseId > 0)
                    {
                        var nphrase = Mapper.Map<ApiModel.Phrase, DomainModel.Phrase>(phrase);

                        _phraseService.EditPhrase(nphrase);
                        SectionID = phrase.SectionId??0;
                    }
                    else
                    {
                        var nphrase = Mapper.Map<ApiModel.Phrase, DomainModel.Phrase>(phrase);

                        _phraseService.CreatePhrase(nphrase);
                        SectionID = phrase.SectionId??0;
                    
                    }
                }
            }
            return SectionID;
        }



    }
}
