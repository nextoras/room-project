using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace server.Enteties
{
    public partial class UserSensors
    {   
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }


        public int SensorId { get; set; }
        [ForeignKey("SensorId")]
        public virtual Sensors Sensors { get; set; }

    }
}
