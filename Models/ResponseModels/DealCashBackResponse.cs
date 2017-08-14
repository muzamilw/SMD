using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
     public class DealCashBackResponse
    {
        public string CounterMessage { get; set; }
        public int CashbackCounter { get; set; }
        public bool IsCounterApplied { get; set; }
        
    }
}
