
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
    
public partial class SurveyQuestion
{

    public SurveyQuestion()
    {

        this.AdCampaignTargetCriterias = new HashSet<AdCampaignTargetCriteria>();

        this.InvoiceDetails = new HashSet<InvoiceDetail>();

        this.SurveyQuestionResponses = new HashSet<SurveyQuestionResponse>();

        this.SurveyQuestionTargetCriterias = new HashSet<SurveyQuestionTargetCriteria>();

        this.SurveyQuestionTargetCriterias1 = new HashSet<SurveyQuestionTargetCriteria>();

        this.SurveyQuestionTargetLocations = new HashSet<SurveyQuestionTargetLocation>();

        this.ProfileQuestionTargetCriterias = new HashSet<ProfileQuestionTargetCriteria>();

        this.CampaignEventHistories = new HashSet<CampaignEventHistory>();

    }


    public long SQID { get; set; }

    public Nullable<int> LanguageID { get; set; }

    public Nullable<int> CountryID { get; set; }

    public Nullable<int> Type { get; set; }

    public string UserID { get; set; }

    public Nullable<int> Status { get; set; }

    public string Question { get; set; }

    public string Description { get; set; }

    public Nullable<int> RepeatPeriod { get; set; }

    public string DisplayQuestion { get; set; }

    public Nullable<System.DateTime> StartDate { get; set; }

    public Nullable<System.DateTime> EndDate { get; set; }

    public Nullable<bool> Approved { get; set; }

    public string ApprovedByUserID { get; set; }

    public Nullable<System.DateTime> ApprovalDate { get; set; }

    public Nullable<System.DateTime> CreationDate { get; set; }

    public Nullable<System.DateTime> ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public string LeftPicturePath { get; set; }

    public string RightPicturePath { get; set; }

    public Nullable<bool> DiscountVoucherApplied { get; set; }

    public string VoucherCode { get; set; }

    public Nullable<long> DiscountVoucherID { get; set; }

    public string RejectionReason { get; set; }

    public Nullable<long> ProjectedReach { get; set; }

    public Nullable<long> ResultClicks { get; set; }

    public Nullable<int> AgeRangeStart { get; set; }

    public Nullable<int> AgeRangeEnd { get; set; }

    public Nullable<int> Gender { get; set; }

    public Nullable<System.DateTime> SubmissionDate { get; set; }

    public Nullable<long> ParentSurveyId { get; set; }

    public Nullable<int> Priority { get; set; }

    public Nullable<int> CompanyId { get; set; }

    public Nullable<int> AnswerNeeded { get; set; }

    public Nullable<double> AmountCharged { get; set; }



    public virtual ICollection<AdCampaignTargetCriteria> AdCampaignTargetCriterias { get; set; }

    public virtual AspNetUser AspNetUser { get; set; }

    public virtual Country Country { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual Language Language { get; set; }

    public virtual ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }

    public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias { get; set; }

    public virtual ICollection<SurveyQuestionTargetCriteria> SurveyQuestionTargetCriterias1 { get; set; }

    public virtual ICollection<SurveyQuestionTargetLocation> SurveyQuestionTargetLocations { get; set; }

    public virtual ICollection<ProfileQuestionTargetCriteria> ProfileQuestionTargetCriterias { get; set; }

    public virtual Company Company { get; set; }

    public virtual ICollection<CampaignEventHistory> CampaignEventHistories { get; set; }

}

}
