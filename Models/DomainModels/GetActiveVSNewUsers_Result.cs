using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
   public partial class GetActiveVSNewUsers_Result
    {
        public string Granual { get; set; }
        public Int64 ActiveStats { get; set; }
        public Int64 NewStats { get; set; }
    }
}
