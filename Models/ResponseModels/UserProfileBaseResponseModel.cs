﻿using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Models.ResponseModels
{
    public class UserProfileBaseResponseModel
    {
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Industry> Industries { get; set; }
        public IEnumerable<Education> Educations { get; set; }

        public IEnumerable<Role> UserRoles { get; set; }
        public GetApprovalCount_Result GetApprovalCount { get; set; }

    }
}
