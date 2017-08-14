using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.RequestModels
{
    public class BuyItModel
    {
         /// <summary>
        /// Campaign ID
        /// </summary>
        public long CampaignId { get; set; }
             /// <summary>
        /// USerId
        /// </summary>
        public string UserId { get; set; }
    }
}
