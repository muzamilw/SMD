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
    }
}
