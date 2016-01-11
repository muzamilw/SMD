using System.Collections.Generic;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Product Action Request Model 
    /// </summary>
    public class ProductActionRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Item Id
        /// </summary>
        public long? ItemId { get; set; }

        /// <summary>
        /// Type - Ad / Survey / Question
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// ItemType - if Ad (Video, Link, Game)
        /// </summary>
        public int? ItemType { get; set; }

        /// <summary>
        /// Is Skipped
        /// </summary>
        public bool? IsSkipped { get; set; }

        /// <summary>
        /// Ad Clicked / Viewed
        /// </summary>
        public bool? AdClickedViewed { get; set; }

        /// <summary>
        /// Survey User Selection
        /// </summary>
        public int? SqUserSelection { get; set; }

        /// <summary>
        /// Profile Question User Answer
        /// </summary>
        public List<int> PqAnswerIds { get; set; }

        /// <summary>
        /// Ad Reward User Selection
        /// </summary>
        public int? AdRewardUserSelection { get; set; }
    }
}
