using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class Game
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> AgeRangeStart { get; set; }
        public Nullable<int> AgeRangeEnd { get; set; }
    }
}
