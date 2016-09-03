using System;
using SMD.Models.IdentityModels;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// AdCampaignResponse Domain Model
    /// </summary>
    public class AdCampaignResponse
    {
        public int ResponseId { get; set; }
        public long? CampaignId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string UserId { get; set; }
        public double? EndUserDollarAmount { get; set; }
        public int? SkipCount { get; set; }
        public int? UserSelection { get; set; }
        public int? CompanyId { get; set; }
        public string GameTime { get; set; }
        public int? GameScore { get; set; }
        public int? GameLevel { get; set; }
        public int? GameId { get; set; }
        public int? UserQuestionResponse { get; set; }

        public string UserLocationLat { get; set; }
        public string UserLocationLong { get; set; }
        public string UserLocationCity { get; set; }
        public string UserLocationCountry { get; set; }
        public string UserLocationAddress { get; set; }

        public Nullable<int> ResponseType { get; set; }

        public virtual AdCampaign AdCampaign { get; set; }
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }

        /// <summary>
        /// Makes a copy of Campaign response
        /// </summary>
        public void Clone(AdCampaignResponse target)
        {

            target.CreatedDateTime = CreatedDateTime;
            target.UserId = UserId;
            target.EndUserDollarAmount = EndUserDollarAmount;
            target.SkipCount = SkipCount;
            target.UserSelection = UserSelection;
            target.CompanyId = CompanyId;
            target.GameTime = GameTime;
            target.GameScore = GameScore;
            target.GameLevel = GameLevel;
            target.GameId = GameId;
            target.UserQuestionResponse = UserQuestionResponse;
            target.UserLocationAddress = UserLocationAddress;
            target.UserLocationCity = UserLocationCity;
            target.UserLocationCountry = UserLocationCountry;
            target.UserLocationLat = UserLocationLat;
            target.UserLocationLong = UserLocationLong;

        }

    }
}
