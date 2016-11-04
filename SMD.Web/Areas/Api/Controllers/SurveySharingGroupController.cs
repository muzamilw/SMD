using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Survey APi Controller 
    /// </summary>
    public class SurveySharingGroupController : ApiController
    {
        #region Public
        private readonly ISurveySharingGroupService surveySharingGroupService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public SurveySharingGroupController(ISurveySharingGroupService surveySharingGroupService)
        {
            this.surveySharingGroupService = surveySharingGroupService;
        }

        #endregion
        #region Public


        public List<SurveySharingGroupApiModel> Get(string UserId)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SurveySharingGroup, SurveySharingGroupApiModel>();
                cfg.CreateMap<SurveySharingGroupMember, SurveySharingGroupMemberApiModel>();
            });


            return Mapper.Map<List<SurveySharingGroup>, List<SurveySharingGroupApiModel>>(surveySharingGroupService.GetUserGroups(UserId));
        }


        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public SurveySharingGroupApiModel Get(long SharingGroupId)
        {


            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SurveySharingGroup, SurveySharingGroupApiModel>();
                cfg.CreateMap<SurveySharingGroupMember, SurveySharingGroupMemberApiModel>();
            });

           
            return Mapper.Map<SurveySharingGroup, SurveySharingGroupApiModel>(surveySharingGroupService.GetGroupDetails(SharingGroupId));
           
        }


        /// <summary>
        /// Update group
        /// </summary>
        public SurveySharingGroupApiModel Post([FromUri] SurveySharingGroupApiModel group)
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SurveySharingGroupApiModel, SurveySharingGroup>();
                cfg.CreateMap<SurveySharingGroupMemberApiModel, SurveySharingGroupMember>();
            });


            var result =  surveySharingGroupService.Update(Mapper.Map<SurveySharingGroupApiModel, SurveySharingGroup>(group));

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SurveySharingGroup, SurveySharingGroupApiModel>();
                cfg.CreateMap<SurveySharingGroupMember, SurveySharingGroupMemberApiModel>();
            });


            return Mapper.Map<SurveySharingGroup, SurveySharingGroupApiModel>(result);
        }


        /// <summary>
        /// create group
        /// </summary>
        public SurveySharingGroupApiModel Put([FromUri] SurveySharingGroupApiModel group)
        {

            if (string.IsNullOrEmpty(group.GroupName))
                throw new Exception("Group name cannot be empty");

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SurveySharingGroupApiModel, SurveySharingGroup>();
                cfg.CreateMap<SurveySharingGroupMemberApiModel, SurveySharingGroupMember>();
            });


            var result = surveySharingGroupService.Create(Mapper.Map<SurveySharingGroupApiModel, SurveySharingGroup>(group));

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SurveySharingGroup, SurveySharingGroupApiModel>();
                cfg.CreateMap<SurveySharingGroupMember, SurveySharingGroupMemberApiModel>();
            });


            return Mapper.Map<SurveySharingGroup, SurveySharingGroupApiModel>(result);
        }


        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public BaseApiResponse Delete(long SharingGroupId)
        {

            try
            {


                if (surveySharingGroupService.DeleteGroup(SharingGroupId))
                    return new BaseApiResponse { Message = "success", Status = true };
                else
                    return new BaseApiResponse { Message = "error deleting", Status = false };
            }
            catch (System.Exception e)
            {

                return new BaseApiResponse { Message = e.ToString(), Status = false };
            }
        }

        #endregion
    }
}