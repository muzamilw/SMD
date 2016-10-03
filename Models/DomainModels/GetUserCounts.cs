using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetUserCounts_Result
    {
        public string title { get; set; }
        public Nullable<int> C7Days { get; set; }
        public Nullable<int> C14Days { get; set; }
        public Nullable<int> C30Days { get; set; }
        public Nullable<int> C60Days { get; set; }
    }
}
