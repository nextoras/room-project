using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server
{
    public partial class MeteringDTO
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int MeteringTypeId { get; set; }
        public int UserId { get; set; }
    }
}
