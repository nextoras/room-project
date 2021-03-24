using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace server.Enteties
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string Password { get; set; }

    }
}
