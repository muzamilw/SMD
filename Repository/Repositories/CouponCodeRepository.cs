using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class CouponCodeRepository : BaseRepository<CouponCode>, ICouponCodeRepository
    {
         #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public CouponCodeRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CouponCode> DbSet
        {
            get { return db.CouponCodes; }
        }

        public void RemoveAll(List<CouponCode> categories)
        {
            db.CouponCodes.RemoveRange(categories);
            db.SaveChanges();

        }
        public bool IsCodeExist(string Code)
        {
            return db.CouponCodes.Where(c => c.Code == Code).FirstOrDefault() != null ? true : false;
        }
        public List<string> GetUserCoupons(string UserId)
        {
            return db.CouponCodes.Where(c => c.UserId == UserId).Select(c => c.Code).ToList();
        }

        public string UpdateCouponSettings(string VoucherCode, string SecretKey, string UserId)
        {
            User loggedInUser = db.Users.Where(u => u.Id == UserId).SingleOrDefault();
            if (loggedInUser != null)
            {
                Company userCompany = null;
                if (loggedInUser.Company != null)
                {
                    userCompany = loggedInUser.Company;
                }
                else 
                {
                    userCompany = db.Companies.Where(c => c.CompanyId == loggedInUser.CompanyId).SingleOrDefault();
                }

                if (userCompany != null && userCompany.VoucherSecretKey == SecretKey)
                {
                    CouponCode takenCoupon = db.CouponCodes.Where(c => c.Code == VoucherCode && c.IsTaken == true && c.UserId == UserId).FirstOrDefault();
                    if (takenCoupon != null) 
                    {
                        takenCoupon.IsUsed = true;
                        takenCoupon.UsedDateTime = DateTime.Now;
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "Failed! Invalid Voucher Code.";
                    }
                }
                else
                {
                    return "Failed! Invalid Secret key.";
                }
            }
            else 
            {
                return "Failed! User not exists.";
            }
        }
        #endregion

    }
}
