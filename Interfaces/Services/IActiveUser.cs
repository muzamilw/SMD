using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.ResponseModels;

namespace SMD.Interfaces.Services
{
    public interface IActiveUser
    {

        ActiveUserResponseModel getActiveUser();
    }
}
