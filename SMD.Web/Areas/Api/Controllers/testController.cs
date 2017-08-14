using System.Net;
using System.Threading.Tasks;
using System.Web;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Web.Http;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;
using AutoMapper;
using System.Collections.Generic;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class testController : ApiController
    {
        #region Private

       
        private readonly IEmailManagerService emailService; 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public testController(IEmailManagerService emailService)
        {


            this.emailService = emailService;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Login
        /// </summary>
        [ApiExceptionCustom]
        public string Get(int mailid, string email)
        {

            emailService.previewEmail(mailid, email);
            return "success";

        }


        public string Get()
        {
            emailService.SendCampaignPerformanceEmails();
            return "success";

        }

        public string Get(int mode)
        {
            if ( mode > 0 && mode <=7)
                emailService.SendNewDealsEmail(mode);
            else if ( mode == 8)
            {
                emailService.SendDealExpiredNotificationToAdvertiser();
            }
            else if ( mode == 9 )
            {
                emailService.Send3DaysDealExpiredNotificationToAdvertiser();
            }
            else if (mode == 10)
            {
                emailService.SendNotificationToAdvertiserForAdditional20PercentDiscounton3rdLastDay();
            }
            else if (mode == 11)
            {
                emailService.SendNotificationToAdvertiserForAdditional25PercentDiscounton2ndLastDay();
            }
            else if (mode == 12)
            {
                emailService.SendNotificationToAdvertiserForAdditional30PercentDiscountonLastDay();
            }
            else if (mode == 13)
            {
                emailService.SendNotificationToAdvertiserForAdditional10DollarDiscounton3rdLastDay();
            }
            else if (mode == 14)
            {
                emailService.SendNotificationToAdvertiserForAdditional20DollarDiscounton2ndLastDay();
            }
            else if (mode == 15)
            {
                emailService.SendNotificationToAdvertiserForAdditional30DollarDiscountonLastDay();
            }
            else if (mode >= 16 && mode <= 20)
                emailService.SendNewDealsEmail(mode);

            
            return "success";

        }

        #endregion
    }
}
