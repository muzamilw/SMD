using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class GetAudience_Result
    {
        public Nullable<int> MatchingUsers { get; set; }
        public Nullable<int> AllUsers { get; set; }
    }
}
