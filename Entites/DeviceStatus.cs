using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server
{
    public partial class DeviceStatus 
    {
        [Key]
        public int Id {get; set;}
        public bool Fan { get; set; }
        public bool HeatingFan { get; set; }
    }
}
