using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class SystemEmailController : ApiController
    {
        #region Attribues
        private readonly ICompanyService _companyService;

        #endregion

        #region Constructor
        public SystemEmailController(ICompanyService companyService)
        {
            _companyService = companyService;
        
        }

        #endregion

        #region  Method

        public EmailResponseModel Get([FromUri]GetPagedListRequest request)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.SystemMail, SystemEmailModel>());
            var obj = _companyService.GetEmails(request);
            var retobj = new EmailResponseModel();
            foreach (var item in obj.Emails)
            {
                retobj.Emails.Add(Mapper.Map<SMD.Models.DomainModels.SystemMail, SystemEmailModel>(item));
            }
            retobj.TotalCount = obj.TotalCount;
            return retobj;
        }
        public Boolean Post(SystemEmailModel obj)
        {


            Mapper.Initialize(cfg => cfg.CreateMap<SystemEmailModel, SystemMail>());
            var mappedObj = Mapper.Map<SystemEmailModel, SystemMail>(obj);
            if (obj.MailId > 0)
            {

                return _companyService.UpdateSystemEmail(mappedObj);

            }
            else
            {

                return false;

            }

        }

        #endregion
    }
}
