using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class CompanyAspNetUsersRepository : BaseRepository<CompaniesAspNetUser>, ICompanyAspNetUsersRepository
    {
           #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CompanyAspNetUsersRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompaniesAspNetUser> DbSet
        {
            get { return db.CompaniesAspNetUsers; }
        }
        #endregion

        
        #region Public


        public CompaniesAspNetUser Find(int id)
        {
            return db.CompaniesAspNetUsers.Where(g => g.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Get List of Coutries 
        /// </summary>
        public IEnumerable<CompaniesAspNetUser> GetUsersByCompanyId(int CompanyId)
        {
            return DbSet.Where( g=> g.CompanyId == CompanyId).ToList();
        }



        public bool RemoveManagedUser(string id)
        {

            var mUser = this.Find(Convert.ToInt32(id));
            if (mUser != null)
            {

                this.Delete(mUser);
                this.SaveChanges();
                return true;
            }
            else
                return false;
        }



        public vw_CompanyUsers CompanyUserExists(string Email)
        {

            return db.vw_CompanyUsers.Where(g => g.email.Contains(Email)).FirstOrDefault();
        }


        public bool VerifyInvitationCode(string InvitationCode)
        {
            var invite = db.CompaniesAspNetUsers.Where(g => g.InvitationCode == InvitationCode).SingleOrDefault();

            if (invite != null)
                return true;
            else
                return false;
        }



        public bool AcceptInvitationCode(string InvitationCode)
        {
            var invite = db.CompaniesAspNetUsers.Where(g => g.InvitationCode == InvitationCode).SingleOrDefault();

            invite.Status = 2;
            invite.InvitationCode = null;

            db.SaveChanges();

            return true;
        }


        public bool AcceptInvitationCode(string InvitationCode, string UserId)
        {
            var invite = db.CompaniesAspNetUsers.Where(g => g.InvitationCode == InvitationCode).SingleOrDefault();

            invite.Status = 2;
            invite.InvitationCode = null;
            invite.UserId = UserId;

            db.SaveChanges();

            return true;
        }

        #endregion
    }
}
