
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
    
public partial class SurveyQuestionResponse
{

    public long SQResponseID { get; set; }

    public Nullable<long> SQID { get; set; }

    public string UserID { get; set; }

    public Nullable<System.DateTime> ResoponseDateTime { get; set; }

    public Nullable<int> UserSelection { get; set; }

    public Nullable<int> SkipCount { get; set; }

    public Nullable<int> CompanyId { get; set; }



    public virtual AspNetUser AspNetUser { get; set; }

    public virtual Company Company { get; set; }

    public virtual SurveyQuestion SurveyQuestion { get; set; }

}

}
