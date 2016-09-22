using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.RequestModels
{
    public class InviteUserRequest
    {
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        public string RoleId { get; set; }

    }



    public class RemoveUserRequest
    {
        public string Id { get; set; }
    }




    public class UpdateManagedUserRequest
    {
        public string Id { get; set; }

        public string RoleId { get; set; }
    }
}
