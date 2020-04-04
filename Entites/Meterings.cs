using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server
{
    public partial class Meterings
    {
        [Key]
        public int Id { get; set; }
        public int SensorId { get; set; }
        [ForeignKey("SensorId")]
        public virtual Sensors Sensor { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int MeteringTypeId { get; set; }
        [ForeignKey("MeteringTypeId")]
        public virtual MeteringTypes MeteringType { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public override string ToString()
        {
            return $"\tId:{Id}\n\tSensorId:{SensorId}\n\tDate:{Date}"
                + $"\n\tValue:{Value}\n\tMeteringTypeId:{MeteringTypeId}";
        }
    }
}
