using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class DealByCouponIdForAnalyticsResponse
    {
        public IEnumerable<getDealByCouponID_Result> lineCharts { get; set; }
        public IEnumerable<getDealByCouponIdRatioAnalytic_Result> pieCharts { get; set; }
        public List<CampaignResponseLocation> UserLocation { get; set; }
        public List<Profession> Profession { get; set; }
        public String expiryDate { get; set; }
        public int ImpressionStat { get; set; }
        public int ByProfessionImpStat { get; set; }
        public int ClickTrouStat { get; set; }
        public List<getDealImpressionByAgeByCouponId_Result> ByAgeStats { get; set; }
        public List<getDealImpressionByGenderByCouponId_Result> ByGenderStats { get; set; }
        public List<getDealImpressionByProfessionByCouponId_Result> ByProfessionStats  { get; set; }
    }
}