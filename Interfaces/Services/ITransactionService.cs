
namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Transaction Service INterface
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Makes Transaction on Approval 
        /// </summary>
        void SurveyApprovalTransaction(long sQid);
    }
}
