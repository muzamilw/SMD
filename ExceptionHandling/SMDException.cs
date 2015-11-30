using System;

namespace SMD.ExceptionHandling
{
    /// <summary>
    /// MPC Exception
    /// </summary>
// ReSharper disable InconsistentNaming
    public sealed class SMDException : ApplicationException
// ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// Initializes a new instance of MPC Exception
        /// </summary>
        public SMDException(string message, long organisationId) : base(message)
        {
            OrganisationId = organisationId;
        }

        /// <summary>
        /// Initializes a new instance of MPC Exception
        /// </summary>
        public SMDException(string message, int organisationId, Exception innerException)
            : base(message, innerException)
        {
            OrganisationId = organisationId;
        }

        public long OrganisationId { get; set; }
    }
}
