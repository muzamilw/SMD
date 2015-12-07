﻿using System.Threading.Tasks;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// WebApi User Service Interface 
    /// </summary>
    public interface IWebApiUserService
    {
        /// <summary>
        /// External Login 
        /// </summary>
        Task<User> ExternalLogin(ExternalLoginRequest request);

        /// <summary>
        /// Standard Login 
        /// </summary>
        Task<User> StandardLogin(StandardLoginRequest request);
    }
}