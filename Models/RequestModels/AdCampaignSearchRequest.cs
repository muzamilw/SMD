using SMD.Models.Common;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Ad Campaign Search Request
    /// </summary>
    public class AdCampaignSearchRequest : GetPagedListRequest
    {

        /// <summary>
        ///  text for searching 
        /// </summary>
        public string SearchText { get; set; }

        public long CampaignId { get; set; }
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


        public int status { get; set; }

        public bool? ShowCoupons { get; set; }
    }
}
