using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.RequestModels
{
    public class RequestModelForUpdateCompanyStatus
    {
        public int status { get; set; }
        public string userid { get; set; }
        public string comments { get; set; }
        public int companyid { get; set; }
    }
}
