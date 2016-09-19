using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class DashboardService : IDashboardService
    {
        #region private
        private readonly ITransactionRepository ITransactionRepository;

        #endregion


        #region Constructor
        public DashboardService(ITransactionRepository _ITransactionRepository)
        {
            this.ITransactionRepository = _ITransactionRepository;

        }

        #endregion


        #region Public
        public IEnumerable<GetAdminDashBoardInsights_Result> GetAdminDashBoardInsights()
        {
            return ITransactionRepository.GetAdminDashBoardInsights();

        }


        #endregion
    }
}
