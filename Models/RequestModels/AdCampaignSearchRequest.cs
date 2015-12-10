using SMD.Models.Common;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Ad Campaign Search Request
    /// </summary>
    public class AdCampaignSearchRequest : GetPagedListRequest
    {
       

        /// <summary>
        ///  Ad Campaign By Column for sorting
        /// </summary>
        public AdCampaignByColumn AdCampaignOrderBy
        {
            get
            {
                return (AdCampaignByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
