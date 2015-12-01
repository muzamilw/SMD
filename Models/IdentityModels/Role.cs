using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Role Domain Model
    /// </summary>
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
