using SMD.Models.Common;

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

        /// <summary>
        /// Survey Question By Column for sorting
        /// </summary>
        public SurveyQuestionByColumn SurveyQuestionOrderBy
        {
            get
            {
                return (SurveyQuestionByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
        // Survey Question ID 
        public long SqId { get; set; }

        public int Status { get; set; }
    }
}
