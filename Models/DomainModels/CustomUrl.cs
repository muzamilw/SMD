using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class CustomUrl
    {
        public long UrlId { get; set; }
        public string ShortUrl { get; set; }
        public string ActualUrl { get; set; }
        public long? UserId { get; set; }
    }
}
