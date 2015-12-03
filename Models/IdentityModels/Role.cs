using System.Collections.Generic;
using SMD.Models.IdentityModels;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Role Domain Model
    /// </summary>
    public partial class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
