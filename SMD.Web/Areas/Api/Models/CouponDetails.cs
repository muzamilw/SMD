﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.MIS.Areas.Api.Models
{
    public class CouponDetails
    {
        public long CouponId { get; set; }
      
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

        public string LocationTitle { get; set; }
        public string LocationLine1 { get; set; }
        public string LocationLine2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZipCode { get; set; }
        public string LocationLAT { get; set; }
        public string LocationLON { get; set; }
        public string LocationPhone { get; set; }


        public string HowToRedeemLine1 { get; set; }
        public string HowToRedeemLine2 { get; set; }
        public string HowToRedeemLine3 { get; set; }
        public string HowToRedeemLine4 { get; set; }
        public string HowToRedeemLine5 { get; set; }

        public string CouponImage1 { get; set; }
        public string CouponImage2 { get; set; }
        public string CouponImage3 { get; set; }
     
        public double Price { get; set; }
        public double Savings { get; set; }
        public double SwapCost { get; set; }
        

        public int DaysLeft { get; set; }

        public string Country { get; set; }

        public bool FlaggedByCurrentUser { get; set; }


        public Nullable<double> distance { get; set; }

    

        public string LogoUrl { get; set; }


        public Nullable<bool> ShowBuyitBtn { get; set; }
        public string BuyitLandingPageUrl { get; set; }
        public string BuyitBtnLabel { get; set; }
        public string AboutUsDescription { get; set; }


        public string CompanyName { get; set; }

        

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }
        


        public virtual ICollection<CouponPriceOption> CouponPriceOptions { get; set; }
        
    }


    //public partial class CouponPriceOption
    //{
    //    public long CouponPriceOptionId { get; set; }
    //    public Nullable<long> CouponId { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<double> Price { get; set; }
    //    public Nullable<double> Savings { get; set; }
    //    public string VoucherCode { get; set; }
    
    //}
 
}
