﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace server.Identity
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
