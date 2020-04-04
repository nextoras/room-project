using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server
{
    public partial class Devices 
    {
        [Key]
        public int Id {get; set;}
        
        [Required]
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
