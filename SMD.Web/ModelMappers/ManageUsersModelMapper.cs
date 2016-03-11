using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.ModelMappers
{
    public static class ManageUsersModelMapper
    { /// <summary>
        /// Domain To Web Model 
        /// </summary>
        public static ManageUserRolesModel CreateFrom(this Models.IdentityModels.User source)
        {

            return new ManageUserRolesModel
            {
                Name = source.FullName,
                Role = source.Roles.Select(c => c.Name).FirstOrDefault()
            };
        }



            ///// <summary>
            ///// Web To Domain Model 
            ///// </summary>
            //public static Models.IdentityModels.User CreateFrom(this ManageUserRolesModel source)
            //{
            //    return new Models.IdentityModels.User
            //    {
            //        Name = source.FullName,
            //        Role = source.Roles.Select(c => c.Name).FirstOrDefault()
            //    };
            //}
    }
}