
namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Request Model for API request 
    /// </summary>
    public class GetProfileQuestionApiRequest : GetPagedListRequest
    {
        /// <summary>
        /// Request Type | All | Answered | NotAns
        /// </summary>
        public int RequestType { get; set; }

        /// <summary>
        /// Profile Group Id
        /// </summary>
        public double GroupId { get; set; }
        
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
    }
    public class GenerateSmsApiRequest 
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
    }
}
