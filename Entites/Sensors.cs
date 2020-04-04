using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server
{
    public partial class Sensors 
    {
        [Key]
        public int Id {get; set;}
        
        [Required]
        public string Name { get; set; }
        public string unit { get; set; }
    }
}
