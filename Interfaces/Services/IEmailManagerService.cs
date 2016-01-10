using SMD.Models.IdentityModels;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Email Manager Interface
    /// </summary>
    public interface IEmailManagerService
    {
        /// <summary>
        /// Send Account Verification Email
        /// </summary>
        Task SendAccountVerificationEmail(User oUser, string emailConfirmationLink);

        /// <summary>
        /// On Registration Success Email
        /// </summary>
        Task SendRegisrationSuccessEmail(string aspnetUserId);

        /// <summary>
        /// Send Password Email
        /// </summary>
        Task SendPasswordResetLinkEmail(User oUser, string passwordResetLink);

        /// <summary>
        /// Send Error Log
        /// </summary>
        bool SendErrorLog(string errormsg);

        /// <summary>
        /// Send Invoice
        /// </summary>
        bool SendInovice();


        /// <summary>
        /// Send Email on Question Approval 
        /// </summary>
        Task SendQuestionApprovalEmail(string aspnetUserId);


        /// <summary>
        ///Send Email on Question Rejection 
        /// </summary>
        Task SendQuestionRejectionEmail(string aspnetUserId);


        /// <summary>
        ///Send Email when Collection scheduler run
        /// </summary>
        Task SendCollectionRoutineEmail(string aspnetUserId);
    }
}
