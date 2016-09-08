using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class ActiveUser : IActiveUser
    {
        #region Private

        private readonly IActiveUserRepository activeUserRepository;


        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ActiveUser(IActiveUserRepository activeUserRepository)
        {
            this.activeUserRepository = activeUserRepository;
        }

        #endregion


        public ActiveUserResponseModel getActiveUser()
        {
            return activeUserRepository.getActiveUser();
        }
    }
}
