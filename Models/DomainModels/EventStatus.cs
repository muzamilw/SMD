using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class EventStatus
    {
        public EventStatus()
        {
            this.CampaignEventHistories = new HashSet<CampaignEventHistory>();
        }

        public int EventStatusId { get; set; }
        public string EventName { get; set; }

        public virtual ICollection<CampaignEventHistory> CampaignEventHistories { get; set; }
    }
}
