using SMD.Models.Common;
using System;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Invoice Request Model
    /// </summary>
    public class InvoiceSearchRequest : GetPagedListRequest
    {
        /// <summary>
        /// For Searching , From Date 
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// For Searching , To Date 
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Invoice By Column for sorting
        /// </summary>
        public InvoiceByColumn InvoiceOrderBy
        {
            get
            {
                return (InvoiceByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
