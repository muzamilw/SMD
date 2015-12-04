using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.RequestModels
{
    public class AdvertGridRequest
    {
        public long CampaignId { get; set; }
        public int Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double MaxBudget { get; set; }
        public double ClickRate { get; set; }
        public string DisplayTitle { get; set; }
        public long ResultClicks { get; set; }
        public double? AmountSpent { get; set; }
    }
}
