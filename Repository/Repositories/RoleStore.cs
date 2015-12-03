using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using Microsoft.AspNet.Identity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Role Store
    /// </summary>
    public class RoleStore : IQueryableRoleStore<Role, string>
    {
        private readonly BaseDbContext _db;

        public RoleStore(BaseDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _db = db;
        }

        // IQueryableRoleStore<Role, TKey>

        public IQueryable<Role> Roles
        {
            get { return _db.Roles; }
        }

        // IRoleStore<Role, TKey>

        public virtual Task CreateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (string.IsNullOrEmpty(role.Id))
            {
                role.Id = Guid.NewGuid().ToString();
            }

            _db.Roles.Add(role);
            return _db.SaveChangesAsync();
        }

        public Task DeleteAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _db.Roles.Remove(role);
            return _db.SaveChangesAsync();
        }

        public Task<Role> FindByIdAsync(string roleId)
        {
            return _db.Roles.FindAsync(new object[] { roleId });
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return _db.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _db.Entry(role).State = EntityState.Modified;
            return _db.SaveChangesAsync();
        }

        // IDisposable

        public void Dispose()
        {
        }
    }

}
