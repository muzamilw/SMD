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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            var result =  surveySharingGroupRepository.Find(SharingGroupId);

            //filtering the members with status 3 as they are marked as deleted.
            //foreach (var item in result.SurveySharingGroupMembers)
            //{
            //    if (item.MemberStatus == 3)
            //        result.SurveySharingGroupMembers.Remove(item);
            //}


            for (int i = result.SurveySharingGroupMembers.Count - 1; i >= 0; i--)
            {
                if (result.SurveySharingGroupMembers.ToArray()[i].MemberStatus == 3)
                    result.SurveySharingGroupMembers.Remove(result.SurveySharingGroupMembers.ToArray()[i]);
            }


            //result.SurveySharingGroupMembers = result.SurveySharingGroupMembers.Where(i => i.MemberStatus != 3);

            

            return result;
        }

        public SurveySharingGroup Create(SurveySharingGroup group)
        {

            group.CreationDate = DateTime.Now;
            



            foreach (var item in group.SurveySharingGroupMembers)
            {
                item.PhoneNumber = Regex.Replace(item.PhoneNumber, @"\s+", "");
                item.PhoneNumber = item.PhoneNumber.Replace("-", "");

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


            var dbGroup = surveySharingGroupRepository.Find(group.SharingGroupId);
            if (dbGroup != null)
            {


                dbGroup.GroupName = group.GroupName;

                var dbExistingMembers = surveySharingGroupMemberRepository.GetAllGroupMembers(group.SharingGroupId);
                if (addedMembers.Count > 0)
                {


                    //new user add logic.
                    foreach (var item in addedMembers)
                    {

                        //if already exists then do not add again.
                        if (dbExistingMembers.FindAll(g => g.PhoneNumber == item.PhoneNumber).Count == 0)
                        {

                            item.SharingGroupId = dbGroup.SharingGroupId;
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

                            item.PhoneNumber = Regex.Replace(item.PhoneNumber, @"\s+", "");

                            if (dbGroup.SurveySharingGroupMembers == null)
                            {
                                dbGroup.SurveySharingGroupMembers = new Collection<SurveySharingGroupMember>();
                            }

                            dbGroup.SurveySharingGroupMembers.Add(item);
                        }
                    }
                }


                //delete user logic
                foreach (var item in deletedMembers)
                {

                    var member = dbGroup.SurveySharingGroupMembers.Where(g => g.SharingGroupMemberId == item.SharingGroupMemberId).SingleOrDefault();
                    member.MemberStatus = 3;

                }


                surveySharingGroupRepository.Update(dbGroup);
                surveySharingGroupRepository.SaveChanges();

            }
            return group;
        }

        public bool DeleteGroup(long SharingGroupId)
        {
            return surveySharingGroupRepository.DeleteUserGroup(SharingGroupId);
        }

        #endregion
    }

}
