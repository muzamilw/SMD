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
    
    public partial class SurveyQuestionTargetLocation
    {
        public long ID { get; set; }
        public Nullable<long> SQID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> Radius { get; set; }
        public Nullable<bool> IncludeorExclude { get; set; }
    
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
