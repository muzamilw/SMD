using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface IDashboardService
    {
        IEnumerable<GetAdminDashBoardInsights_Result> GetAdminDashBoardInsights();
    }
}
