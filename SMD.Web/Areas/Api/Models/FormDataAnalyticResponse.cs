using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class FormDataAnalyticResponse
    {
        public string Question { get; set; }
        public string answer { get; set; }
        public Nullable<long> Id { get; set; }
        public int typ { get; set; }
        public int Stats { get; set; }

        public FormDataAnalyticResponse() { 
        
        }
        public FormDataAnalyticResponse(string _Question, string _answer, long _Id, int _type, int _stats)
        {
            Question = _Question;
            answer = _answer;
            Id = _Id;
            typ = _type;
            Stats = _stats;

        }
    }
}