using Microsoft.AspNet.Identity.Owin;
using SMD.Common;
using SMD.ExceptionHandling;
using SMD.Implementation.Identity;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace SMD.Implementation.Services
{
    public class SurveySharingGroupService : ISurveySharingGroupService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISurveySharingGroupRepository surveySharingGroupRepository;
        private readonly ISurveySharingGroupMemberRepository surveySharingGroupMemberRepository;
        private readonly IAspnetUsersRepository aspnetUserRepository;
      
       
        #endregion
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public SurveySharingGroupService(ISurveySharingGroupRepository surveySharingGroupRepository, ISurveySharingGroupMemberRepository surveySharingGroupMemberRepository, IAspnetUsersRepository aspnetUserRepository)
        {
            this.surveySharingGroupRepository = surveySharingGroupRepository;
            this.surveySharingGroupMemberRepository = surveySharingGroupMemberRepository;
            this.aspnetUserRepository = aspnetUserRepository;
        }

        #endregion
        #region public


        public List<SurveySharingGroup> GetUserGroups(string UserId)
        {
            return surveySharingGroupRepository.GetUserGroups(UserId).ToList();
        }
        public SurveySharingGroup GetGroupDetails(long SharingGroupId)
        {
            return surveySharingGroupRepository.Find(SharingGroupId);
        }

        public SurveySharingGroup Create(SurveySharingGroup group)
        {

            group.CreationDate = DateTime.Now;
            



            foreach (var item in group.SurveySharingGroupMembers)
            {

                var user = aspnetUserRepository.GetUserbyPhoneNo(item.PhoneNumber);
                if ( user != null)
                {
                    item.UserId = user.Id;
                    item.MemberStatus = 1;
                }
                else
                {
                    item.MemberStatus = 0;
                    //call the sms api

                    //user not found send SMS to join

                }

                //item.SharingGroupId = group.SharingGroupId;

                //surveySharingGroupMemberRepository.Update(item);

                
            }

            surveySharingGroupRepository.Add(group);
            surveySharingGroupRepository.SaveChanges();

            //surveySharingGroupMemberRepository.SaveChanges();



            return group;
        }

        public SurveySharingGroup Update(SurveySharingGroup group, ICollection<SurveySharingGroupMember> addedMembers, ICollection<SurveySharingGroupMember> deletedMembers)
        {

            //new user add logic.
            foreach (var item in addedMembers)
            {
                var user = aspnetUserRepository.GetUserbyPhoneNo(item.PhoneNumber);
                if (user != null)
                {
                    item.UserId = user.Id;
                    item.MemberStatus = 1;
                }
                else
                {
                    item.MemberStatus = 0;
                    //call the sms api

                    //user not found send SMS to join

                }
                group.SurveySharingGroupMembers.Add(item);
            }



            //delete user logic
            foreach (var item in deletedMembers)
            {

                var member = group.SurveySharingGroupMembers.Where(g => g.SharingGroupMemberId == item.SharingGroupMemberId).SingleOrDefault();
                member.MemberStatus = 3;

            }
           
                      
            surveySharingGroupRepository.Update(group);
            surveySharingGroupRepository.SaveChanges();


            return group;
        }

        public bool DeleteGroup(long SharingGroupId)
        {
            return surveySharingGroupRepository.DeleteUserGroup(SharingGroupId);
        }

        #endregion
    }

}
