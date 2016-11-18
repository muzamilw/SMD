using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMD.Models.ResponseModels;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Services
{
    public interface IActiveUser
    {

        IEnumerable<GetActiveVSNewUsers_Result> GetActiveVSNewUsers();
        List<String> getProfessions();
    }
}
