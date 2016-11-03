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

        public SurveySharingGroup Create(SurveySharingGroup group)
        {

            group.CreationDate = DateTime.Now;
            


            surveySharingGroupRepository.Add(group);
            surveySharingGroupRepository.SaveChanges();


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

                surveySharingGroupMemberRepository.Add(item);

                
            }
            surveySharingGroupMemberRepository.SaveChanges();



            return group;
        }



        #endregion
    }

}
