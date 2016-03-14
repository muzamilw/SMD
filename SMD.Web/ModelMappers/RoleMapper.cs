using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.ModelMappers
{
    public static class RoleMapper
    {

        public static RoleDropDown CreateFromDd(this Models.DomainModels.Role source)
        {
            return new RoleDropDown
            {
                RoleId = source.Id,
                RoleName = source.Name
            };
        }
    }
}