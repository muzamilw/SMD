
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
    
public partial class BranchCategory
{

    public BranchCategory()
    {

        this.CompanyBranches = new HashSet<CompanyBranch>();

    }


    public long BranchCategoryId { get; set; }

    public string BranchCategoryName { get; set; }

    public Nullable<int> CompanyId { get; set; }



    public virtual Company Company { get; set; }

    public virtual ICollection<CompanyBranch> CompanyBranches { get; set; }

}

}
