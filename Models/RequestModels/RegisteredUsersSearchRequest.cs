using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.RequestModels
{
    public class RegisteredUsersSearchRequest : GetPagedListRequest
    {
        public string SearchText { get; set; }

        public int status { get; set; }
    }
}
