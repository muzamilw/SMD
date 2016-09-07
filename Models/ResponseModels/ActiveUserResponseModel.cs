using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
    public class ActiveUserResponseModel
    {
        public Int32 Last1DayActiveUser { get; set; }
        public Int32 Last7DayActiveUser { get; set; }
        public Int32 Last14DayActiveUser { get; set; }
        public Int32 Last30DayActiveUser { get; set; }
        public Int32 Last3MonthsActiveUser { get; set; }

    }
}
