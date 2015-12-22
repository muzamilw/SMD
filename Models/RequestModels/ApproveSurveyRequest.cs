namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Approve Survey Request Model 
    /// </summary>
    public class ApproveSurveyRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Survey Question Id
        /// </summary>
        public long SurveyQuestionId { get; set; }

        /// <summary>
        /// Approval Amount
        /// </summary>
        public double Amount { get; set; }
    }
}
