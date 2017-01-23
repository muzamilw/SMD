using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CouponAnalyticsController : ApiController
    {
        #region private
        private readonly ICouponService _ICouponService;
        private readonly IActiveUser _IActiveUser;
        #endregion

        #region Constructor
        public CouponAnalyticsController(ICouponService ICouponService, IActiveUser IActiveUser)
        {
            this._ICouponService = ICouponService ;
            this._IActiveUser = IActiveUser;
        }
         #endregion
        
        #region public
        public DealByCouponIdForAnalyticsResponse getDealByCouponIDAnalytics(int CouponID, int dateRange, int Granularity, int type, int Gender, int age, int Stype, string profession)
        {

            DealByCouponIdForAnalyticsResponse data = new DealByCouponIdForAnalyticsResponse();
            
            if (type == 1)
            {
                data.lineCharts = _ICouponService.getDealByCouponIDAnalytics(CouponID, dateRange, Granularity);
                data.pieCharts = _ICouponService.getDealByCouponIdRatioAnalytic(CouponID, dateRange);
                data.ImpressionStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 1);
                data.ClickTrouStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 2);
                data.UserLocation = _ICouponService.getDealUserLocationByCId(CouponID);
                data.ByAgeStats = _ICouponService.getDealImpressionByAgeByCouponId(CouponID);
                data.ByProfessionStats = _ICouponService.getDealImpressionByProfessionByCouponId(CouponID);
                data.ByGenderStats = _ICouponService.getDealImpressionByGenderByCouponId(CouponID);

                IEnumerable<String> prfList = _IActiveUser.getProfessions();
                List<Profession> Prof = new List<Profession>();
                Profession prfItem = new Profession("All");
                Prof.Add(prfItem);
                foreach (String item in prfList)
                {
                    if (item != null)
                    {
                        prfItem = new Profession(item);
                    }
                    Prof.Add(prfItem);
                }
                data.Profession = Prof;


            }
            else
            {
                if (Stype == 1) {
                     data.ImpressionStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 1);
                }
                else if (Stype == 2) 
                {
                    data.ClickTrouStat = _ICouponService.getDealStatByCouponIdFormAnalytic(CouponID, Gender, age, 2);
                }
                else if (Stype == 3)
                {
                    // data.ByProfessionImpStat
                }
                
            }

         //   data.expiryDate = _ICouponService.getExpiryDate(CouponID).ToString("D");
            return data;
        }
        #endregion
    }
}

 //public int getDealStatByCouponIdFormAnalytic(long dealId, int Gender, int age, int type)
 //       {
 //           return couponRepository.getDealStatByCouponIdFormAnalytic(dealId, Gender, age, type); 
 //       }