using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class EmailResponseModel
    {
        public EmailResponseModel()
        {
            this.Emails = new List<SystemEmailModel>();
        }
        /// <summary>
        ///  Rejected Ad Campaigns List
        /// </summary>
        public List<SystemEmailModel> Emails { get; set; }

        /// <summary>
        /// Total Count of Ad Campaigns
        /// </summary>
        public int TotalCount { get; set; }
        
    }
}