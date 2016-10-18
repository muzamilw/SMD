//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModelProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.DiscountVouchers = new HashSet<DiscountVoucher>();
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
        }
    
        public int ProductID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public string ProductName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<double> SetupPrice { get; set; }
        public Nullable<double> ClickFeePercentage { get; set; }
        public Nullable<double> AgeClausePrice { get; set; }
        public Nullable<double> AffiliatePercentage { get; set; }
        public string ProductCode { get; set; }
        public Nullable<double> GenderClausePrice { get; set; }
        public Nullable<double> LocationClausePrice { get; set; }
        public Nullable<double> OtherClausePrice { get; set; }
        public Nullable<double> ProfessionClausePrice { get; set; }
        public Nullable<double> EducationClausePrice { get; set; }
        public Nullable<double> BuyItClausePrice { get; set; }
        public Nullable<double> QuizQuestionClausePrice { get; set; }
        public Nullable<double> TenDayDeliveryClausePrice { get; set; }
        public Nullable<double> FiveDayDeliveryClausePrice { get; set; }
        public Nullable<double> ThreeDayDeliveryClausePrice { get; set; }
        public Nullable<double> VoucherClausePrice { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<DiscountVoucher> DiscountVouchers { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
