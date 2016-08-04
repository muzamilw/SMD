
namespace SMD.ExceptionHandling
{
    /// <summary>
    /// MPC Exception Contents
    /// </summary>
    public sealed class SMDExceptionContent
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// MPC Exception Type
        /// </summary>
        public string ExceptionType { get { return SMDExceptionTypes.SMDGeneralException; } }

    }
}
