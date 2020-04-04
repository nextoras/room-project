using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server
{
    public partial class Users
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }

    }
}
