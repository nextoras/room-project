using System;
using System.Collections.Generic;

namespace server
{
    public partial class SensorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AvereageValueDTO> AvereageValues { get; set;}
    }

    public class AvereageValueDTO
    {
        public double Value { get; set; }
    }
    public class SensorValues
    {
        public double Value { get; set; }
    }
}
