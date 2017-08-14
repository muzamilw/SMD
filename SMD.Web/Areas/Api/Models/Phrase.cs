using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public  class Phrase
    {
        public int PhraseId { get; set; }
        public string PhraseName { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> SortOrder { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}