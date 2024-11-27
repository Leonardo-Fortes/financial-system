﻿using Microsoft.AspNetCore.Identity;

namespace Dima.api.Models
{
    public class User : IdentityUser<long>
    {
        public List<IdentityRole<long>>? Roles { get; set; }
    }
}