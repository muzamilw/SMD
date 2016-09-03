using System.Collections.Generic;
using SMD.Models.DomainModels;

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
    }
}
