using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class ProfileQuestionTargetLocation
    {
        public long ID { get; set; }
        public Nullable<int> PQID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> Radius { get; set; }
        public Nullable<bool> IncludeorExclude { get; set; }
    }
}