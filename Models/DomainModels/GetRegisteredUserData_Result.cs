using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetRegisteredUserData_Result
    {
        public string Id { get; set; }
        public string fullname { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public string Email { get; set; }
        public Nullable<int> Status { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Nullable<double> AccountBalance { get; set; }
        public Nullable<int> TotalItems { get; set; }
    }
}
