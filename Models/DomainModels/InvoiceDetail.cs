

using System;
namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Invoice Detail Domain Model
    /// </summary>
    public class InvoiceDetail
    {
        #region Public

        public long InvoiceDetailId { get; set; }

        public long InvoiceId { get; set; }

        public string ItemName { get; set; }

        public double ItemTax { get; set; }

        public double ItemAmount { get; set; }

        public double ItemGrossAmount { get; set; }

        public string ItemDescription { get; set; }

        public long? CampaignId { get; set; }

        public long? SqId { get; set; }

        public int? ProductId { get; set; }

        public Nullable<int> PQID { get; set; }
        public Nullable<long> CouponID { get; set; }
        #endregion
        #region Navigational

        public virtual AdCampaign AdCampaign { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        #endregion
    }
}
