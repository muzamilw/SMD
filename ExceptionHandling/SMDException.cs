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
        public SMDException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of MPC Exception
        /// </summary>
        public SMDException(string message, Exception innerException)
            : base(message, innerException)
        {
           
        }

    }
}
