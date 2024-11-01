﻿using System;
using System.Web;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.IdentityModels;
using LoginResponse = SMD.Models.ResponseModels.LoginResponse;

namespace SMD.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// WebApi User Mapper
    /// </summary>
    public static class WebApiUserMapper
    {
        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static WebApiUser CreateFrom(this User source)
        {
            return new WebApiUser
                   {
                       FullName = source.FullName,
                       Address1 = source.Address1,
                       CompanyName = source.CompanyName,
                       Email = source.Email,
                       JobTitle = source.Jobtitle,
                       UserTimeZone = source.UserTimeZone,
                       UserId = source.Id,
                       Gender = source.Gender,
                       ImageUrl = !string.IsNullOrEmpty(source.ProfileImage) ? HttpContext.Current.Request.Url.Scheme + "://" + 
                       HttpContext.Current.Request.Url.Host + "/" + source.ProfileImage + "?" + DateTime.Now : string.Empty
                   };
        }

        /// <summary>
        /// Create WebApi User from Domain Model
        /// </summary>
        public static Models.LoginResponse CreateFrom(this LoginResponse source)
        {
            return new Models.LoginResponse
            {
                Status = source.Status,
                Message = source.Message,
                User = source.User != null ? source.User.CreateFrom() : null,
                AuthenticationToken = Guid.NewGuid()
            };
        }
    }
}