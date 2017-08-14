using SMD.Models.Common;
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
        /// Type - Ad / Survey / Question / Game
        /// </summary>
        public CampaignType Type { get; set; }

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

        public int? companyId { get; set; }
        public int? UserQuestionResponse { get; set; }


        public string UserLocationLat { get; set; }


        public string UserLocationLong { get; set; }

        public string City { get; set; }


        public string Country { get; set; }

        public CampaignResponseEventType ResponeEventType { get; set; }


        public double? GameAccuracy { get; set; }
        public double? GamePlayTime { get; set; }
        public int? GameScore { get; set; }
        public long? GameId { get; set; }
    }
}
