using SMD.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.RequestModels
{
    public class SurveySearchRequest : GetPagedListRequest
    {
        /// <summary>
        ///  text for searching 
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Language Filter Text
        /// </summary>
        public int LanguageFilter { get; set; }


        /// <summary>
        /// Country Filter text
        /// </summary>
        public int CountryFilter { get; set; }

        public bool FirstLoad { get; set; }
        ///// <summary>
        ///// Profile Question By Column for sorting
        ///// </summary>
        //public ProfileQuestionByColumn ProfileQuestionOrderBy
        //{
        //    get
        //    {
        //        return (ProfileQuestionByColumn)SortBy;
        //    }
        //    set
        //    {
        //        SortBy = (short)value;
        //    }
        //}
    }
}
