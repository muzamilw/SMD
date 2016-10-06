using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;
using EntityState = System.Data.Entity.EntityState;

namespace SMD.Repository.Repositories
{
    public class UserStore :
        IQueryableUserStore<User, string>,
        IUserPasswordStore<User, string>,
        IUserLoginStore<User, string>,
        IUserClaimStore<User, string>,
        IUserRoleStore<User, string>,
        IUserSecurityStampStore<User, string>,
        IUserEmailStore<User, string>,
        IUserPhoneNumberStore<User, string>,
        IUserTwoFactorStore<User, string>,
        IUserLockoutStore<User, string>
    {
        private readonly BaseDbContext _db;

        public UserStore(BaseDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _db = db;
            AutoSaveChanges = true;
        }

        /// <summary>
        ///     If true will call SaveChanges after CreateAsync/UpdateAsync/DeleteAsync
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        // IQueryableUserStore<User, int>

        public IQueryable<User> Users
        {
            get { return _db.Users; }
        }

        // IUserStore<User, Key>

        public Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(user.Id))
            {
                user.Id = Guid.NewGuid().ToString();
            }

            _db.Users.Add(user);
            return SaveChanges();
        }

        public Task DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _db.Users.Remove(user);
            return SaveChanges();
        }

        public Task<User> FindByIdAsync(string userId)
        {
            return _db.Users
            .Include(u => u.UserLogins).Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _db.Users
            .Include(u => u.UserLogins).Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _db.Entry(user).State = EntityState.Modified;
            return SaveChanges();
        }

        // IUserPasswordStore<User, Key>

        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        // IUserLoginStore<User, Key>

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLogin = Activator.CreateInstance<UserLogin>();
            userLogin.UserId = user.Id;
            userLogin.LoginProvider = login.LoginProvider;
            userLogin.ProviderKey = login.ProviderKey;
            user.UserLogins.Add(userLogin);
            return Task.FromResult(0);
        }

        public async Task<User> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var provider = login.LoginProvider;
            var key = login.ProviderKey;

            var userLogin = await _db.UserLogins.FirstOrDefaultAsync(l => l.LoginProvider == provider && l.ProviderKey == key);

            if (userLogin == null)
            {
                return default(User);
            }

            return await _db.Users
            .Include(u => u.UserLogins).Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id.Equals(userLogin.UserId));
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.UserLogins == null)
            {
                user.UserLogins = new List<UserLogin>();
            }

            return Task.FromResult<IList<UserLoginInfo>>(user.UserLogins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList());
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            if (user.UserLogins == null)
            {
                user.UserLogins = new List<UserLogin>();
            }

            var provider = login.LoginProvider;
            var key = login.ProviderKey;

            var item = user.UserLogins.SingleOrDefault(l => l.LoginProvider == provider && l.ProviderKey == key);

            if (item != null)
            {
                user.UserLogins.Remove(item);
            }

            return Task.FromResult(0);
        }

        // IUserClaimStore<User, int>

        public Task AddClaimAsync(User user, Claim claim)
        {
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.Claims == null)
            {
                user.Claims = new List<UserClaim>();
            }

            return Task.FromResult<IList<Claim>>(user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            if (user.Claims == null)
            {
                user.Claims = new List<UserClaim>();
            }

            foreach (var item in user.Claims.Where(uc => uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList())
            {
                user.Claims.Remove(item);
            }

            foreach (var item in _db.UserClaims.Where(uc => uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToList())
            {
                _db.UserClaims.Remove(item);
            }

            return Task.FromResult(0);
        }

        // IUserRoleStore<User, int>

        public Task AddToRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value can not be null", "roleName");
            }

            var userRole = _db.Roles.SingleOrDefault(r => r.Name == roleName);

            if (userRole == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Role {0} not found", new object[] { roleName }));
            }

            if (user.Roles == null)
            {
                user.Roles = new List<Role>();
            }

            user.Roles.Add(userRole);
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.Roles == null)
            {
                user.Roles = new List<Role>();
            }

            return Task.FromResult<IList<string>>(user.Roles.Join(_db.Roles, ur => ur.Id, r => r.Id, (ur, r) => r.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value can not be null", "roleName");
            }

            return Task.FromResult(_db.Roles.Any(r => r.Name == roleName && r.Users.Any(u => u.Id.Equals(user.Id))));
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value can not be null or empty", "roleName");
            }

            if (user.Roles == null)
            {
                user.Roles = new List<Role>();
            }

            var userRole = user.Roles.SingleOrDefault(r => r.Name == roleName);

            if (userRole != null)
            {
                user.Roles.Remove(userRole);
            }

            return Task.FromResult(0);
        }

        // IUserSecurityStampStore<User, int>

        public Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        // IUserEmailStore<User, int>

        public Task<User> FindByEmailAsync(string email)
        {
            return _db.Users
                .Include(u => u.UserLogins).Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<string> GetEmailAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(User user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        // IUserPhoneNumberStore<User, int>

        public Task<string> GetPhoneNumberAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        // IUserTwoFactorStore<User, int>

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        // IUserLockoutStore<User, int>

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(
                user.LockoutEndDateUtc.HasValue ?
                    new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) :
                    new DateTimeOffset());
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? null : new DateTime?(lockoutEnd.UtcDateTime);
            return Task.FromResult(0);
        }

        // IDisposable

        public void Dispose()
        {
        }

        private Task SaveChanges()
        {
            try
            {
                return AutoSaveChanges ? _db.SaveChangesAsync() : Task.FromResult(0);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    var entityName = validationResult.Entry.Entity.GetType().Name;
                    errorMessages.AddRange(validationResult.ValidationErrors.Select(error => entityName + "." + error.PropertyName + ": " + error.ErrorMessage));
                }

                throw;
            }
        }



    }
}
