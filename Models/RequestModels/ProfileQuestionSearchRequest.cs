using SMD.Models.Common;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Profile Question Search Request Model 
    /// </summary>
    public class ProfileQuestionSearchRequest:GetPagedListRequest
    {
        /// <summary>
        /// Question text for searching 
        /// </summary>
        public string ProfileQuestionFilterText { get; set; }

        /// <summary>
        /// Language Filter Text
        /// </summary>
        public int LanguageFilter { get; set; }

        /// <summary>
        /// Question Group Filter text
        /// </summary>

        public int QuestionGroupFilter { get; set; }

        /// <summary>
        /// Country Filter text
        /// </summary>
        public int CountryFilter { get; set; }


        /// <summary>
        /// Profile Question By Column for sorting
        /// </summary>
        public ProfileQuestionByColumn ProfileQuestionOrderBy
        {
            get
            {
                return (ProfileQuestionByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }

        public bool fmode { get; set; }
    }
}
