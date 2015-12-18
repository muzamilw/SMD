using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class GetAds_Result
    {
        public long CampaignID { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public string VerifyQuestion { get; set; }
        public string LandingPageVideoLink { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public Nullable<int> CorrectAnswer { get; set; }
        public Nullable<double> ClickRate { get; set; }
    }
}
