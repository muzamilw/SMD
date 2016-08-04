
namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Request Model to get Surveys | API
    /// </summary>
    public class GetSurveysApiRequest:GetPagedListRequest
    {

        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
    }
}
