﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.Common
{
    public class CouponDetails
    {
        public long CouponId { get; set; }
        public string CouponName { get; set; }
        public string CouponTitle { get; set; }


        public string HighlightLine1 { get; set; }
        public string HighlightLine2 { get; set; }
        public string HighlightLine3 { get; set; }
        public string HighlightLine4 { get; set; }
        public string HighlightLine5 { get; set; }


        public string FinePrintLine1 { get; set; }
        public string FinePrintLine2 { get; set; }
        public string FinePrintLine3 { get; set; }
        public string FinePrintLine4 { get; set; }
        public string FinePrintLine5 { get; set; }


        public string LocationLine1 { get; set; }
        public string LocationLine2 { get; set; }
        public string LocationLine3 { get; set; }
        public string LocationLine4 { get; set; }
        public string LocationLine5 { get; set; }


        public string HowToRedeemLine1 { get; set; }
        public string HowToRedeemLine2 { get; set; }
        public string HowToRedeemLine3 { get; set; }
        public string HowToRedeemLine4 { get; set; }
        public string HowToRedeemLine5 { get; set; }

        public string CouponImage1 { get; set; }
        public string CouponImage2 { get; set; }
        public string CouponImage3 { get; set; }
        public string CouponImage4 { get; set; }
        public string CouponSwapValue { get; set; }
        public string CouponActualValue { get; set; }
        public int CouponTakenValue { get; set; }
        public double CouponDiscountedValue { get; set; }


        public int DaysLeft { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string LocationLAT { get; set; }
        public string LocationLON { get; set; }

        public string Phone { get; set; }

        public string AdvertisersLogoPath { get; set; }
        
    }
 
}
