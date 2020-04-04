using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace server
{
    public partial class UserDevices
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        
        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public virtual Devices Devices { get; set; }

    }
}
