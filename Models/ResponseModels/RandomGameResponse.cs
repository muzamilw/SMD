using System.Collections.Generic;
using SMD.Models.DomainModels;
using System;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Industry Response 
    /// </summary>
    public class RandomGameResponse : BaseApiResponse
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public string GameUrl { get; set; }

        public string GameSmallImage { get; set; }
        public string GameLargeImage { get; set; }

        public string GameInstructions { get; set; }

        public Nullable<double> PlayTime { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<double> Accuracy { get; set; }
    }
}
