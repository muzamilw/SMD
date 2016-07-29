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
    
    public partial class SurveyQuestionTargetCriteria
    {
        public long ID { get; set; }
        public Nullable<long> SQID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> PQID { get; set; }
        public Nullable<int> PQAnswerID { get; set; }
        public Nullable<long> LinkedSQID { get; set; }
        public Nullable<int> LinkedSQAnswer { get; set; }
        public Nullable<bool> IncludeorExclude { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public Nullable<int> IndustryID { get; set; }
        public Nullable<long> EducationID { get; set; }
    
        public virtual Education Education { get; set; }
        public virtual Industry Industry { get; set; }
        public virtual Language Language { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual ProfileQuestionAnswer ProfileQuestionAnswer { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public virtual SurveyQuestion SurveyQuestion1 { get; set; }
    }
}
