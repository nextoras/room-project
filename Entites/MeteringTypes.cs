using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server
{
    public partial class MeteringTypes
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
