using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class ProfileQuestionTargetCriteria
    {
        public long ID { get; set; }
        public Nullable<int> PQID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> SQID { get; set; }
        public Nullable<int> PQAnswerID { get; set; }
        public Nullable<long> LinkedSQID { get; set; }
        public Nullable<int> LinkedSQAnswer { get; set; }
        public Nullable<bool> IncludeorExclude { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public Nullable<int> IndustryID { get; set; }
        public Nullable<long> EducationID { get; set; }

    }
}