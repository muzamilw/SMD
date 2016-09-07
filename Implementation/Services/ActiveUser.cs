using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class ActiveUser : IActiveUser
    {

        public IEnumerable<Int32> getActiveUser() { 
        
        return new Int32[]{1,2};
        }
    }
}
